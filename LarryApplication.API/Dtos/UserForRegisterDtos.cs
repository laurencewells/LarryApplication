using System.ComponentModel.DataAnnotations;

namespace LarryApplication.API.Dtos
{
    public class UserForRegisterDtos
    {
        [Required]
        public string Username { get; set; }    
        
        [Required]
        [StringLength(8,MinimumLength = 4,ErrorMessage = "You must do something")]
        public string Password { get; set; }
        
    }
}