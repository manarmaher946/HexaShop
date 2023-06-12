using Microsoft.AspNetCore.Identity;

namespace projectmvc.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Address { get; set; }

        //public virtual List<Orders> Orders { get; set; }



    }
}
