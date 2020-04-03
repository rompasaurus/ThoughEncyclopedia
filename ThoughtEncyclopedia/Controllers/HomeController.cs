using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ThoughtEncyclopedia.Data;
using ThoughtEncyclopedia.Models;

namespace ThoughtEncyclopedia.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult About()
        {
            return View();
        }

        [Authorize]
        public IActionResult Ruminate()
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
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
