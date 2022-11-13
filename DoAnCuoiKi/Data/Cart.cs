using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DoAnCuoiKi.Data
{
    [Table("Cart")]
    public class Cart
    {
        [Key]
        public int cartId { get; set; }

        public string ? name { get; set; }
        
        public int amount { get; set; }

        public double price { get; set; }

        public int userId { get; set; }

        public int productId { get; set; }

        [ForeignKey("userId")]
        public User ? user { get; set; }

        [ForeignKey("productId")]
        public Product? Product { get; set; }
    }
}
