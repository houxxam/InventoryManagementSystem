using InvWebApp.Data;
using InvWebApp.Extentions;
using InvWebApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace InvWebApp.Controllers
{
    [Authorize]
    public class MaterielsController : Controller
    {
        private readonly AppDbContext _context;

        public MaterielsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Materiels
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Materiels.OrderByDescending(m => m.Id).Include(m => m.Categorie).Include(m => m.Service);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Materiels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Materiels == null)
            {
                return NotFound();
            }

            var materiel = await _context.Materiels
                .Include(m => m.Categorie)
                .Include(m => m.Service)
                .Include(m => m.serviceGroup)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (materiel == null)
            {
                return NotFound();
            }

            return View(materiel);
        }

        // GET: Materiels/Create
        public IActionResult Create()
        {
            ViewData["CategorieId"] = new SelectList(_context.Categories, "Id", "CategorieName");
            ViewData["ServiceId"] = new SelectList(_context.Services, "Id", "ServiceName");
            return View();
        }

        // POST: Materiels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("Id,MaterielName,CreatedDate,SerialNumber,InventoryNumber,MaterielOwner,CategorieId,ServiceId,ServiceGroupId,UserId")]
            Materiel materiel)
        {

            // check categorie if is exists allready before insert
            var existingMateriel = _context.Materiels.FirstOrDefault(u => u.SerialNumber.ToLower() == materiel.SerialNumber.ToLower());



            if (existingMateriel != null)
            {
                ModelState.AddModelError("SerialNumber", "SerialNumber already exists.");
                //return View(materiel);
            }

            if (ModelState.IsValid)
            {
                try
                {
                    materiel.UserId = User.getUserId(_context);
                    _context.Add(materiel);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    ViewBag.existData = $"{materiel.SerialNumber} allready Exist";
                }

            }
            if (materiel.ServiceId != null)
            {
                ViewBag.Groups = await _context.serviceGroups.Where(s => s.ServiceId == materiel.ServiceId).ToListAsync();
            }


            ViewData["CategorieId"] =
                new SelectList(_context.Categories, "Id", "CategorieName", materiel.CategorieId);
            ViewData["ServiceId"] = new SelectList(_context.Services, "Id", "ServiceName", materiel.ServiceId);
            return View(materiel);


        }
        [AllowAnonymous]
        [HttpPost]
        [Route("/api/create")]
        public async Task<IActionResult> CreateMateriel(
            [Bind("Id,MaterielName,CreatedDate,SerialNumber,InventoryNumber,CategorieId,ServiceId")]
            Materiel materiel)
        {
            _context.Add(materiel);
            await _context.SaveChangesAsync();
            return Ok(materiel);

        }

        // GET: Materiels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Materiels == null)
            {
                return NotFound();
            }

            var materiel = await _context.Materiels.FindAsync(id);
            if (materiel == null)
            {
                return NotFound();
            }

            ViewData["CategorieId"] = new SelectList(_context.Categories, "Id", "CategorieName", materiel.CategorieId);
            ViewData["ServiceId"] = new SelectList(_context.Services, "Id", "ServiceName", materiel.ServiceId);
            return View(materiel);
        }

        // POST: Materiels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,
            [Bind("Id,MaterielName,CreatedDate,SerialNumber,InventoryNumber,MaterielOwner,CategorieId,ServiceId,UserId")]
            Materiel materiel)
        {
            if (id != materiel.Id)
            {
                return NotFound();
            }

            // check categorie if is exists allready before update
            var existingMateriel = _context.Materiels.FirstOrDefault(u => u.SerialNumber.ToLower() == materiel.SerialNumber.ToLower());

            if (existingMateriel != null)
            {
                ModelState.AddModelError("SerialNumber", "SerialNumber already exists.");

            }

            if (ModelState.IsValid)
            {
                try
                {
                    materiel.UserId = User.getUserId(_context);
                    _context.Update(materiel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {

                    if (!MaterielExists(materiel.Id))
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

            ViewData["CategorieId"] = new SelectList(_context.Categories, "Id", "CategorieName", materiel.CategorieId);
            ViewData["ServiceId"] = new SelectList(_context.Services, "Id", "ServiceName", materiel.ServiceId);
            return View(materiel);
        }

        // GET: Materiels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Materiels == null)
            {
                return NotFound();
            }

            var materiel = await _context.Materiels
                .Include(m => m.Categorie)
                .Include(m => m.Service)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (materiel == null)
            {
                return NotFound();
            }

            return View(materiel);
        }

        // POST: Materiels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Materiels == null)
            {
                return Problem("Entity set 'AppDbContext.Materiels'  is null.");
            }

            var materiel = await _context.Materiels.FindAsync(id);
            if (materiel != null)
            {
                _context.Materiels.Remove(materiel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MaterielExists(int id)
        {
            return (_context.Materiels?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        [HttpGet]
        public IActionResult GetGroups(int serviceId)
        {
            // Retrieve groups for the selected service
            var groups = _context.serviceGroups.Where(s => s.ServiceId == serviceId).Select(g => new { Id = g.Id, GroupName = g.GroupName }).ToList();
            return Json(groups);
        }
    }
}