using System.ComponentModel.DataAnnotations;

namespace projectmvc.ViewModel
{
    public class RegistrationViewModel
    {
        [Required]
        public string UserName { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        //[DataType(DataType.EmailAddress)]
        //public string Email { get; set; }

        public string Address { get; set; }
    }
}
