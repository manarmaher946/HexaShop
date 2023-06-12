using System.ComponentModel.DataAnnotations;

namespace projectmvc.ViewModel
{
   public class LoginViewModel
    {

        public string Username { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool rememberMe { get; set; }

    }
}
