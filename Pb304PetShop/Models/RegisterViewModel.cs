using System.ComponentModel.DataAnnotations;

namespace Pb304PetShop.Models
{
    public class RegisterViewModel
    {
        public required string Username { get; set; }
        public required string Fullname  { get; set; }
        [DataType(DataType.EmailAddress)]
        public required string Email { get; set; }
        [DataType(DataType.Password)]
        public required string Password { get; set; }
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password and confirmation password do not match.")]
        public required string ConfirmPassword { get; set; }

    }
}
