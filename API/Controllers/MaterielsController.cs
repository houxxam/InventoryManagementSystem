using InvWebApp.Data;
using InvWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MaterielsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public MaterielsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Materiels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Materiel>>> GetMateriels()
        {
            if (_context.Materiels == null)
            {
                return NotFound();
            }
            return await _context.Materiels.ToListAsync();
        }

        // GET: api/Materiels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Materiel>> GetMateriel(int id)
        {
            if (_context.Materiels == null)
            {
                return NotFound();
            }
            var materiel = await _context.Materiels.FindAsync(id);

            if (materiel == null)
            {
                return NotFound();
            }

            return materiel;
        }

        // PUT: api/Materiels/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMateriel(int id, Materiel materiel)
        {
            if (id != materiel.Id)
            {
                return BadRequest();
            }

            _context.Entry(materiel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MaterielExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Materiels
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Materiel>> PostMateriel(string materielName, string serialNumber, string categorie, string service, string group)
        {
            // HIT-RAD-REC
            var cat = _context.Categories.FirstOrDefault(c => c.CategorieName.ToLower() == categorie.ToLower());
            var ser = _context.Services.FirstOrDefault(s => s.ServiceName.ToLower() == service.ToLower());
            var grp = _context.serviceGroups.FirstOrDefault(g => g.GroupName.ToLower() == group.ToLower());





            if (cat is null)
            {
                _context.Categories.Add(new() { CategorieName = categorie, UserId = 1 });
                _context.SaveChanges();
                cat = _context.Categories.OrderByDescending(c => c.Id).First();
            }
            if (ser is null)
            {
                _context.Services.Add(new() { ServiceName = service, UserId = 1 });
                _context.SaveChanges();
                ser = _context.Services.OrderByDescending(s => s.Id).First();
            }
            if (grp is null)
            {
                _context.serviceGroups.Add(new() { GroupName = group, ServiceId = ser.Id });
                _context.SaveChanges();
                grp = _context.serviceGroups.OrderByDescending(s => s.Id).First();
            }



            var materiel = new Materiel();
            materiel.MaterielName = materielName;
            materiel.CreatedDate = DateTime.Now;
            materiel.SerialNumber = serialNumber;
            materiel.CategorieId = cat.Id;
            materiel.ServiceId = ser.Id;
            materiel.ServiceGroupId = grp.Id;
            materiel.UserId = 1;
            _context.Materiels.Add(materiel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMateriel", new { id = materiel.Id }, materiel);
        }

        // DELETE: api/Materiels/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMateriel(int id)
        {
            if (_context.Materiels == null)
            {
                return NotFound();
            }
            var materiel = await _context.Materiels.FindAsync(id);
            if (materiel == null)
            {
                return NotFound();
            }

            _context.Materiels.Remove(materiel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MaterielExists(int id)
        {
            return (_context.Materiels?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
