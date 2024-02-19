using System.ComponentModel.DataAnnotations.Schema;

namespace InvWebApp.Models
{
    public class ServiceGroup
    {
        public int Id { get; set; }
        public string GroupName { get; set; }

        public int ServiceId { get; set; }

        [ForeignKey("ServiceId")]
        public Service? Service { get; set; }
    }
}
