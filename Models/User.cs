using System.ComponentModel.DataAnnotations;

namespace LoginRegistration.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        
        public String firstname { get; set; }
        [Required]
        public string lastname { get; set; }
        [Required]
        public string email { get; set; }
        [Required]
        public string password { get; set; }
        [Required]
        public string rpassword { get; set; }
        [Required]
        public string gender { get; set; }
        
    }
}