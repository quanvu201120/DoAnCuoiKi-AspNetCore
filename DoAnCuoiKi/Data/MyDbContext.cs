using Microsoft.EntityFrameworkCore;

namespace DoAnCuoiKi.Data
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {
        }

        public DbSet<Brand>? brands { get; set; }
        public DbSet<Cart>? carts { get; set; }
        public DbSet<Category>? categories { get; set; }
        public DbSet<Order>? orders { get; set; }
        public DbSet<OrderDetails>? OrderDetails { get; set; }
        public DbSet<Product>? products { get; set; }
        public DbSet<User>? users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var user = new User
            {
                userId = 1,
                email = "admin@gmail.com",
                password = "123123",
                name = "admin",
                gender = 1,
                role = "Admin",
                phone = "0338786222",
                address = "VN"
            };

            modelBuilder.Entity<User>().HasData(user);
            base.OnModelCreating(modelBuilder);
        }

    }
}
