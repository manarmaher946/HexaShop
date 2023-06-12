using Microsoft.Build.ObjectModelRemoting;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace projectmvc.Models
{
    public class Shipper
    {
        [Key]
        [ForeignKey("shipper")]
        public string Id { get; set; }
        public bool IsValid{ get; set; }

     
        public virtual ApplicationUser shipper { get; set; }
        public virtual List<Orders> Orders { get; set; }
    }
}
