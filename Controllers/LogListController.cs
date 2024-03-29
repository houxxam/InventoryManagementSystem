﻿using InvWebApp.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InvWebApp.Controllers
{

    [Authorize]
    public class LogListController : Controller
    {
        private readonly AppDbContext _context;

        public LogListController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            return _context.LogLists.Include(u => u.Id) != null ?
                         View(await _context.LogLists.Include(u => u.User).OrderByDescending(l => l.LogDate).ToListAsync()) :
                         Problem("Entity set 'AppDbContext.Categories'  is null.");
        }
    }
}
