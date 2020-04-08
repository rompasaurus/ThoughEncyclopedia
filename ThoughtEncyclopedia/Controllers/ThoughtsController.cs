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
    public class ThoughtsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger _log;

        public ThoughtsController(ApplicationDbContext context, UserManager<IdentityUser> um, ILogger log)
        {
            _context = context;
            _userManager = um;
            _log = log;
        }

        // GET: Thoughts
        public async Task<IActionResult> Index()
        {
            return View(await _context.Thoughts.ToListAsync());
        }

        // GET: Thoughts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var thought = await _context.Thoughts
                .FirstOrDefaultAsync(m => m.ThoughtId == id);
            if (thought == null)
            {
                return NotFound();
            }

            return View(thought);
        }
        //Maps the categories available to the user insied a SelectListItem allows Dropdown to be created
        private List<SelectListItem> GetCategoryOptions(string userID)
        {
            List<SelectListItem> TopicItems =
                (from topic in _context.Topics.Include(u => u.User)
                 where topic.User.Id == userID
                 select new SelectListItem
                 {
                     Text = topic.Title.ToString(),
                     Value = topic.TopicID.ToString(),
                 }
                 ).ToList();
            return TopicItems;
        }

        //Populates the Topics established by the user into the thoughtview model and presents it to the create page allowing for a dropdown to be created
        public async Task<IActionResult> CreateAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            string userId = user.Id;
            ThoughtView tv = new ThoughtView();
            tv.Topics = GetCategoryOptions(userId);
            return View(tv);
        }

        // POST: Thoughts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ThoughtId,ContentText,TopicId")] ThoughtView tv)
        {
            if (ModelState.IsValid)
            {
                Thought thought = new Thought 
                { 
                    ContentText = tv.ContentText, 
                    Topic = _context.Topics.Find(tv.TopicId),
                    User= await _userManager.GetUserAsync(User)
                };
                _log.LogInformation("\nSaving Thought data as... \n User: {0} \n Text: {1} \n Topic: {2} \n Category: {3}", thought.User.UserName, thought.ContentText, thought.Topic);
                _context.Add(thought);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tv);
        }

        // GET: Thoughts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var thought = await _context.Thoughts.FindAsync(id);
            if (thought == null)
            {
                return NotFound();
            }
            return View(thought);
        }

        // POST: Thoughts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ThoughtId,ContentText,DateCreated,DateModified,ViewCount,UpvoteCount,DownvoteCount,LikeCount")] Thought thought)
        {
            if (id != thought.ThoughtId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(thought);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ThoughtExists(thought.ThoughtId))
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
            return View(thought);
        }

        // GET: Thoughts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var thought = await _context.Thoughts
                .FirstOrDefaultAsync(m => m.ThoughtId == id);
            if (thought == null)
            {
                return NotFound();
            }

            return View(thought);
        }

        // POST: Thoughts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var thought = await _context.Thoughts.FindAsync(id);
            _context.Thoughts.Remove(thought);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ThoughtExists(int id)
        {
            return _context.Thoughts.Any(e => e.ThoughtId == id);
        }
    }
}
