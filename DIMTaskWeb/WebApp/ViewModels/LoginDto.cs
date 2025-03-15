using System.ComponentModel.DataAnnotations;

namespace WebApp.ViewModels
{
    public class LoginDto
    {
        public string PhoneNumber { get; set; }
        [Required, DataType(DataType.Password)]
        public string Password { get; set; }
    
    }
}
