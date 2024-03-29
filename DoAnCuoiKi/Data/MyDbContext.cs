﻿using Microsoft.EntityFrameworkCore;

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
                address = "VN",
                isDelete = false
            };

            var listCate = new List<Category>
            {
                new Category{categoryId=1,name="Chuột - bàn phím",  isDelete = false},
                new Category{categoryId=2,name="Thiết bị âm thanh",  isDelete = false},
                new Category{categoryId=3,name="Linh kiện PC - Laptop",  isDelete = false},
                new Category{categoryId=4,name="SSD",  isDelete = false},
                new Category{categoryId=5,name="HDD",  isDelete = false},
                new Category{categoryId=6,name="Ram máy tính",  isDelete = false},
                new Category{categoryId=7,name="Ổ cứng di động",  isDelete = false},
                new Category{categoryId=8,name="Ổ cứng SSD di động",  isDelete = false},
                new Category{categoryId=9,name="Thẻ nhớ",  isDelete = false},
                new Category{categoryId=10,name="USB",  isDelete = false}
            };

            var listBrand = new List<Brand>
            {
                new Brand{ brandId=1, name = "ASUS",  isDelete = false},
                new Brand{ brandId=2, name = "GIGABYTE",  isDelete = false},
                new Brand{ brandId=3, name = "MSI",  isDelete = false},
                new Brand{ brandId=4, name = "Asrock",  isDelete = false},
                new Brand{ brandId=5, name = "Intel",  isDelete = false},
                new Brand{ brandId=6, name = "Samsung",  isDelete = false},
                new Brand{ brandId=7, name = "Apacer",  isDelete = false},
                new Brand{ brandId=8, name = "Kingston",  isDelete = false},
                new Brand{ brandId=9, name = "Kingmax",  isDelete = false},
                new Brand{ brandId=10, name = "Sony",  isDelete = false},
                new Brand{ brandId=11, name = "JBL",  isDelete = false},
                new Brand{ brandId=12, name = "Sennheiser",  isDelete = false},
                new Brand{ brandId=13, name = "Corsair",  isDelete = false},
                new Brand{ brandId=14, name = "Logitech",  isDelete = false},
                new Brand{ brandId=15, name = "Apple",  isDelete = false},
            };


            modelBuilder.Entity<User>().HasData(user);
            modelBuilder.Entity<Category>().HasData(listCate);
            modelBuilder.Entity<Brand>().HasData(listBrand);

            base.OnModelCreating(modelBuilder);
        }

    }
}
