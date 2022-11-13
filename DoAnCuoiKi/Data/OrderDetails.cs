using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DoAnCuoiKi.Data
{
    [Table("OrderDetails")]
    public class OrderDetails
    {
        [Key]
        public int orderDetailsId { get; set; }

        public string ? productName { get; set; }
        public string ? productPrice { get; set; }
        public string ? image { get; set; }
        public int productAmout { get; set; }

        public double totalPrice { get; set; }


        public int orderId { get; set; }

        [ForeignKey("userId")]
        public Order ? order { get; set; }
    }
}
