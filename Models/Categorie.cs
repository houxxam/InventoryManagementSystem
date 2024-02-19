using System.ComponentModel.DataAnnotations.Schema;

namespace InvWebApp.Models
{
    public class Categorie
    {
        public int Id { get; set; }
        public string CategorieName { get; set; }
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public User? User { get; set; }
    }
}