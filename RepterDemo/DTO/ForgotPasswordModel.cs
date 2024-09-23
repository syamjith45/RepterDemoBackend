using System.ComponentModel.DataAnnotations;

namespace RepterDemo.DTO
{
    public class ForgotPasswordModel
    {
        [Required]
        public string Email { get; set; }
    }
}
