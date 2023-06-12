namespace projectmvc.Models
{
    public class OrderStatus
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual List<Orders> Orders { get; set; }
    }
}
