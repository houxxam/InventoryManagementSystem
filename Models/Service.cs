using System.ComponentModel.DataAnnotations.Schema;

namespace InvWebApp.Models
{
    public class Service
    {
        public int Id { get; set; }
        public string ServiceName { get; set; }
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public User? User { get; set; }
    }
}