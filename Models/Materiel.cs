using System.ComponentModel.DataAnnotations.Schema;

namespace InvWebApp.Models
{
    public class Materiel
    {
        public int Id { get; set; }
        public string MaterielName { get; set; }
        public DateTime CreatedDate => DateTime.Now;
        public string SerialNumber { get; set; }
        public string InventoryNumber { get; set; }
        public int CategorieId { get; set; }
        public int ServiceId { get; set; }
        [ForeignKey("CategorieId")]
        public Categorie? Categorie { get; set; }
        [ForeignKey("ServiceId")] public Service? Service { get; set; }
    }
}