using InvWebApp.Data;
using InvWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace InvWebApp.Controllers
{
    public class ServiceGroupsController : Controller
    {
        private readonly AppDbContext _context;

        public ServiceGroupsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: ServiceGroups
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.serviceGroups.Include(s => s.Service);
            return View(await appDbContext.ToListAsync());
        }

        // GET: ServiceGroups/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.serviceGroups == null)
            {
                return NotFound();
            }

            var serviceGroup = await _context.serviceGroups
                .Include(s => s.Service)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (serviceGroup == null)
            {
                return NotFound();
            }

            return View(serviceGroup);
        }

        // GET: ServiceGroups/Create
        public IActionResult Create()
        {
            ViewData["ServiceName"] = new SelectList(_context.Services, "Id", "ServiceName");
            return View();
        }

        // POST: ServiceGroups/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,GroupName,ServiceId")] ServiceGroup serviceGroup)
        {
            if (ModelState.IsValid)
            {
                serviceGroup.GroupName = serviceGroup.GroupName.ToUpper();
                _context.Add(serviceGroup);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ServiceId"] = new SelectList(_context.Services, "Id", "Id", serviceGroup.ServiceId);
            return View(serviceGroup);
        }

        // GET: ServiceGroups/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.serviceGroups == null)
            {
                return NotFound();
            }

            var serviceGroup = await _context.serviceGroups.FindAsync(id);
            if (serviceGroup == null)
            {
                return NotFound();
            }
            ViewData["ServiceId"] = new SelectList(_context.Services, "Id", "ServiceName", serviceGroup.ServiceId);
            return View(serviceGroup);
        }

        // POST: ServiceGroups/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,GroupName,ServiceId")] ServiceGroup serviceGroup)
        {
            if (id != serviceGroup.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {

                    serviceGroup.GroupName = serviceGroup.GroupName.ToUpper();
                    _context.Update(serviceGroup);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ServiceGroupExists(serviceGroup.Id))
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
            ViewData["ServiceId"] = new SelectList(_context.Services, "Id", "Id", serviceGroup.ServiceId);
            return View(serviceGroup);
        }

        // GET: ServiceGroups/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.serviceGroups == null)
            {
                return NotFound();
            }

            var serviceGroup = await _context.serviceGroups
                .Include(s => s.Service)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (serviceGroup == null)
            {
                return NotFound();
            }

            return View(serviceGroup);
        }

        // POST: ServiceGroups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.serviceGroups == null)
            {
                return Problem("Entity set 'AppDbContext.serviceGroups'  is null.");
            }
            var serviceGroup = await _context.serviceGroups.FindAsync(id);
            if (serviceGroup != null)
            {
                _context.serviceGroups.Remove(serviceGroup);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ServiceGroupExists(int id)
        {
            return (_context.serviceGroups?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
