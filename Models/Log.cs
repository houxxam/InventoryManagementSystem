using System.ComponentModel.DataAnnotations.Schema;

namespace InvWebApp.Models
{
    public class Log
    {
        public int Id { get; set; }
        public string LogMessage { get; set; }
        public string LogType { get; set; }
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public User? User { get; set; }
    }
}
