﻿using InvWebApp.Data;
using InvWebApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InvWebApp.Controllers
{
	[Authorize]
	public class ServicesController : Controller
	{
		private readonly AppDbContext _context;

		public ServicesController(AppDbContext context)
		{
			_context = context;
		}

		// GET: Services
		public async Task<IActionResult> Index()
		{
			return _context.Services != null ?
						View(await _context.Services.ToListAsync()) :
						Problem("Entity set 'AppDbContext.Services'  is null.");
		}

		// GET: Services/Details/5
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null || _context.Services == null)
			{
				return NotFound();
			}

			var service = await _context.Services
				.FirstOrDefaultAsync(m => m.Id == id);
			if (service == null)
			{
				return NotFound();
			}

			return View(service);
		}

		// GET: Services/Create
		public IActionResult Create()
		{
			return View();
		}

		// POST: Services/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("Id,ServiceName")] Service service)
		{
			if (ModelState.IsValid)
			{
				_context.Add(service);
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
			return View(service);
		}

		// GET: Services/Edit/5
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null || _context.Services == null)
			{
				return NotFound();
			}

			var service = await _context.Services.FindAsync(id);
			if (service == null)
			{
				return NotFound();
			}
			return View(service);
		}

		// POST: Services/Edit/5
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, [Bind("Id,ServiceName")] Service service)
		{
			if (id != service.Id)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				try
				{
					_context.Update(service);
					await _context.SaveChangesAsync();
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!ServiceExists(service.Id))
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
			return View(service);
		}

		// GET: Services/Delete/5
		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null || _context.Services == null)
			{
				return NotFound();
			}

			var service = await _context.Services
				.FirstOrDefaultAsync(m => m.Id == id);
			if (service == null)
			{
				return NotFound();
			}

			return View(service);
		}

		// POST: Services/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			if (_context.Services == null)
			{
				return Problem("Entity set 'AppDbContext.Services'  is null.");
			}
			var service = await _context.Services.FindAsync(id);
			if (service != null)
			{
				_context.Services.Remove(service);
			}

			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}

		private bool ServiceExists(int id)
		{
			return (_context.Services?.Any(e => e.Id == id)).GetValueOrDefault();
		}
	}
}
