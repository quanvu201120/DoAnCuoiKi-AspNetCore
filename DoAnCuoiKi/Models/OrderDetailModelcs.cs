using DoAnCuoiKi.Data;

namespace DoAnCuoiKi.Models
{
    public class OrderDetailModelcs
    {
        public Order order { get; set; }
        public List<OrderDetails> orderDetails { get; set; }
    }
}
