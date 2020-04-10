using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ThoughtEncyclopedia.Data;
using ThoughtEncyclopedia.Models;

namespace ThoughtEncyclopedia.Controllers
{
    public class TopicsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        //Ilogger cannot be of generic type when passed as DI needs a ClassnName as the type
        private readonly ILogger<TopicsController> _log;

        public TopicsController(ILogger<HomeController> logger, ApplicationDbContext context, UserManager<IdentityUser> um, ILogger<TopicsController> log)
        {
            _context = context;
            _userManager = um;
            _log = log;
        }

        // GET: Topics
        public async Task<IActionResult> Index()
        {
            return View(await _context.Topics.ToListAsync());
        }

        // GET: Topics/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var topic = await _context.Topics
                .FirstOrDefaultAsync(m => m.TopicID == id);
            if (topic == null)
            {
                return NotFound();
            }

            return View(topic);
        }

        // GET: Topics/Create
        public IActionResult Create()
        {
            var tpc = new TopicView();
            tpc.Categories = GetCategoryOptions();
            if (tpc.Categories == null)
            {
                return NotFound("No Categories found");
            }
            return View(tpc);
        }

        // POST: Topics/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,Description,CategoryId")] TopicView topicView)
        {
            if (ModelState.IsValid)
            {
                //Businees logic needs its own classes
                Topic topic = new Topic
                {
                    Title = topicView.Title,
                    Description = topicView.Description,
                    User = await _userManager.GetUserAsync(User),
                    Category = _context.Categories.Find(topicView.CategoryId)
                };
                _log.LogInformation("\nSaving topic data as... \n User: {0} \n Description: {1} \n Title: {2} \n Category: {3}", topic.User.UserName, topic.Description,topic.Title, topic.Category);
                _context.Add(topic);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
          
            return View(topicView);
        }
        //Maps the categories available to the user insied a SelectListItem allows Dropdown to be created
        private List<SelectListItem> GetCategoryOptions()
        {
            try
            {
            List<SelectListItem> CategoryItems =
                (from category in _context.Categories
                 select new SelectListItem
                 {
                     Text = category.CategoryName.ToString(),
                     Value = category.CategoryId.ToString(),
                 }
                 ).ToList();
            return CategoryItems;
            }catch(Exception ex)
            {
                _log.LogError("Exception occured pulling categories from db: " + ex.InnerException);
            }
            return null;
        }
        // GET: Topics/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var topic = await _context.Topics.FindAsync(id);
            if (topic == null)
            {
                return NotFound();
            }
            return View(topic);
        }

        // POST: Topics/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TopicID,ParentID,DateCreated,DateModified,Title,UpvoteCount,DownvoteCount,LikeCount,KnowlegeTypeID,Description,ViewCount")] Topic topic)
        {
            if (id != topic.TopicID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(topic);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TopicExists(topic.TopicID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(topic);
        }

        // GET: Topics/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var topic = await _context.Topics
                .FirstOrDefaultAsync(m => m.TopicID == id);
            if (topic == null)
            {
                return NotFound();
            }

            return View(topic);
        }

        // POST: Topics/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var topic = await _context.Topics.FindAsync(id);
            _context.Topics.Remove(topic);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TopicExists(int id)
        {
            return _context.Topics.Any(e => e.TopicID == id);
        }
    }
}
