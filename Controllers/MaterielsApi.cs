using InvWebApp.Data;
using InvWebApp.Models;
using InvWebApp.Models.Dtos;
using Microsoft.AspNetCore.Authorization;
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

        //[AllowAnonymous]
      
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Materiel>>> GetMateriels()
        //{
        //    if (_context.Materiels == null)
        //    {
        //        return NotFound();
        //    }
        //    return await _context.Materiels.ToListAsync();
        //}





        // POST: api/Materiels
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<Materiel>> PostMateriel([FromBody] CreateMaterielDto dto)
        {
            var mat = _context.Materiels.FirstOrDefault(s=>s.SerialNumber == dto.SerialNumber);

           
                // /api/MaterielsApi?materielName1=&serialNumber=&categorie=&service=&group=
                var cat = _context.Categories.FirstOrDefault(c => c.CategorieName.ToLower() == dto.Categorie.ToLower());
                var ser = _context.Services.FirstOrDefault(s => s.ServiceName.ToLower() == dto.Service.ToLower());
                var grp = _context.serviceGroups.FirstOrDefault(g => g.GroupName.ToLower() == dto.Group.ToLower());

            if (mat is null)
            {

                if (cat is null)
                {
                    _context.Categories.Add(new() { CategorieName = dto.Categorie, UserId = 1 });
                    _context.SaveChanges();
                    cat = _context.Categories.OrderByDescending(c => c.Id).First();
                }
                if (ser is null)
                {
                    _context.Services.Add(new() { ServiceName = dto.Service, UserId = 1 });
                    _context.SaveChanges();
                    ser = _context.Services.OrderByDescending(s => s.Id).First();
                }
                if (grp is null)
                {
                    _context.serviceGroups.Add(new() { GroupName = dto.Group, ServiceId = ser.Id });
                    _context.SaveChanges();
                    grp = _context.serviceGroups.OrderByDescending(s => s.Id).First();
                }



                var materiel = new Materiel();
                materiel.MaterielName = dto.MaterielName;
                materiel.CreatedDate = DateTime.Now;
                materiel.SerialNumber = dto.SerialNumber;
                materiel.CategorieId = cat.Id;
                materiel.ServiceId = ser.Id;
                materiel.ServiceGroupId = grp.Id;
                materiel.UserId = 1;
                _context.Materiels.Add(materiel);
                await _context.SaveChangesAsync();

                return Ok();
            }
            else {
                 return BadRequest("SerialNumber already exist"); 
            }
        
        }


        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<Materiel>> GetMateriel(string materielName, string serialNumber, string categorie, string service, string group)
        {
            var mat = _context.Materiels.FirstOrDefault(s => s.SerialNumber == serialNumber);

            var cat = _context.Categories.FirstOrDefault(c => c.CategorieName.ToLower() == categorie.ToLower());
            var ser = _context.Services.FirstOrDefault(s => s.ServiceName.ToLower() == service.ToLower());
            var grp = _context.serviceGroups.FirstOrDefault(g => g.GroupName.ToLower() == group.ToLower());

            if (mat is null)
            {
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

                return Ok();
            }
            else
            {
                return BadRequest("SerialNumber already exist");
            }
        }




    }
}
