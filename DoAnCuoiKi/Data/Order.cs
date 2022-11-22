using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DoAnCuoiKi.Data
{
    [Table("Order")]
    public class Order
    {
        [Key]
        public int? orderId { get; set; }
        
        public string ? customerName { get; set; }
        public string ? customerAddress { get; set; }
        public string ? customerEmail { get; set; }
        public string ? customerPhone { get; set; }
        public string ? otherInformation { get; set; }
        public double totalPrice { get; set; }
        public DateTime dateCreate { get; set; }
        public DateTime? dateReceive { get; set; }
        public int status { get; set; }
        public int userId { get; set; }

        [ForeignKey("userId")]
        public User? user { get; set; }


    }
}
