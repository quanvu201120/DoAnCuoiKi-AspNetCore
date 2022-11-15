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
                gender = "Nam",
                role = "Admin",
                phone = "0338786222",
                address = "VN"
            };

            var listCate = new List<Category>
            {
                new Category{categoryId=1,name="Chuột - bàn phím"},
                new Category{categoryId=2,name="Thiết bị âm thanh"},
                new Category{categoryId=3,name="Linh kiện PC - Laptop"},
                new Category{categoryId=4,name="SSD"},
                new Category{categoryId=5,name="HDD"},
                new Category{categoryId=6,name="Ram máy tính"},
                new Category{categoryId=7,name="Ổ cứng di động"},
                new Category{categoryId=8,name="Ổ cứng SSD di động"},
                new Category{categoryId=9,name="Thẻ nhớ"},
                new Category{categoryId=10,name="USB"}
            };


            modelBuilder.Entity<User>().HasData(user);
            modelBuilder.Entity<Category>().HasData(listCate);

            base.OnModelCreating(modelBuilder);
        }

    }
}
