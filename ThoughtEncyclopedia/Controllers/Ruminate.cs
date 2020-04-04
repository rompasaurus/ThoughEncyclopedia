using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ThoughtEncyclopedia.Data;
using ThoughtEncyclopedia.Models;

namespace ThoughtEncyclopedia.Controllers
{
    [Authorize]
    public class Ruminate : Controller
    {
        private readonly ApplicationDbContext _context;

        public Ruminate(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Ruminate
        public async Task<IActionResult> Index()
        {
            
                string currentUserId = User.Identity.GetUserId();
                Console.WriteLine(currentUserId);
                ViewData["Title"] = "Ruminations";
                var Thoughts = from thought in _context.Thoughts.Include(u => u.User).Include(t => t.Topic)
                               where thought.User.Id == currentUserId
                               select thought;
                var Topics = from topic in _context.Topics.Include(u => u.User)
                             where topic.User.Id == currentUserId
                             select topic;
                ViewData["Thoughts"] = Thoughts;
                ViewData["Topics"] = Topics;
                var currentUser = _context.Users.FirstOrDefault(x => x.Id == currentUserId);

                return View();
            
            //return View(await _context.Thoughts.ToListAsync());
        }

        // GET: Ruminate/Details/5
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

        // GET: Ruminate/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Ruminate/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ThoughtId,ContentText,DateCreated,DateModified,ViewCount,UpvoteCount,DownvoteCount,LikeCount")] Thought thought)
        {
            if (ModelState.IsValid)
            {
                _context.Add(thought);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(thought);
        }

        // GET: Ruminate/Edit/5
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

        // POST: Ruminate/Edit/5
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

        // GET: Ruminate/Delete/5
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

        // POST: Ruminate/Delete/5
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
