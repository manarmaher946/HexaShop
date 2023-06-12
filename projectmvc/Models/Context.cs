using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace projectmvc.Models
{
    public class Context:IdentityDbContext<ApplicationUser>
    {
        public Context() : base()
        {

        }
        //using during injection
        public Context(DbContextOptions option) : base(option)
        {

        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Orders>? Orders { get; set; }
        public DbSet<OrderStatus> OrderStatuses { get; set; }
        public DbSet<OrderItems> OrderItems { get; set; }
        public DbSet<Cart> Cart{ get; set; }
        public DbSet<Color> Color { get; set; }
        public DbSet<Size> Size { get; set; }
        public DbSet<ProductSizeColor> ProductSizeColor { get; set; }
        public DbSet<SubCategory> SubCategory { get; set; }
        public DbSet<Review> Review { get; set; }
        public DbSet<Shipper> Shippers{ get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=ENGISRAA\\SQL19;Initial Catalog=HEXASHOP;Integrated Security=True;Encrypt=False");
            base.OnConfiguring(optionsBuilder);
        }
    }
}
