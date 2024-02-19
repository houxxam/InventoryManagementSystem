using InvWebApp.Data;
using InvWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InvWebApp
{
    [Route("api/[controller]")]
    [ApiController]
    public class MaterielsApi : ControllerBase
    {
        private readonly AppDbContext _context;

        public MaterielsApi(AppDbContext context)
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





        // POST: api/Materiels
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Materiel>> PostMateriel(string materielName, string serialNumber, string categorie, string service, string group)
        {
            // /api/MaterielsApi?materielName1=&serialNumber=&categorie=&service=&group=
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




    }
}
