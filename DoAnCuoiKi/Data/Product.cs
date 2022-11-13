using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DoAnCuoiKi.Data
{
    [Table("Product")]
    public class Product
    {
        [Key]
        public int ? productId { get; set; }

        public string ? name { get; set; }

        public string ? description { get; set; }

        public string ? image { get; set; }

        public int amount { get; set; }

        public double price { get; set; }

        public int brandId { get; set; }
        public int categoryId { get; set; }


        [ForeignKey("brandId")]
        public Brand? brand { get; set; }

        [ForeignKey("categoryId")]
        public Category? category { get; set; }
    }
}
