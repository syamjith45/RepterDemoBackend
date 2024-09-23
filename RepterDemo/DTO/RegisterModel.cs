using System.ComponentModel.DataAnnotations;

namespace RepterDemo.DTO
{
    public class RegisterModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Role { get; set; } = "User"; // Default role
    }

}
