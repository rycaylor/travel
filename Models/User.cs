using System.ComponentModel.DataAnnotations;
 
namespace travel.Models
{
    public class User 
    {
        public int Id {get; set;}

        [Required (ErrorMessage = "First Name field is required.")]
        [MinLength(3)]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Name can only contain letters")]
        public string FirstName { get; set; }

        [Required (ErrorMessage = "Last Name field is required.")]
        [MinLength(3)]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Name can only contain letters")]
        public string LastName { get; set; }
 
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required (ErrorMessage = "User Name field is required.")]
        [MinLength(3)]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Username can only contain letters")]
        public string UserName { get; set; }
 
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required (ErrorMessage = "Password Confirmation field is required.")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password and confirmation must match.")]
        public string PasswordConfirmation { get; set; }
    }

    public class UserTest
    {
        public int Id { get; set; }
        
        [Required (ErrorMessage = "Invalid User Name/Password.")]
        [MinLength(3)]
        public string UserName { get; set; }
        [Required (ErrorMessage = "Invalid User Name/Password.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}