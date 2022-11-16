using DoAnCuoiKi.Data;

namespace DoAnCuoiKi.Models
{
    public class ProductDataModel
    {
        public List<Brand>? brands { get; set; }
        public List<Product>? products  { get; set; }
        public string? categoryName { get; set; }
        public string? categoryId { get; set; }
        public string? brandId { get; set; }
        public string? price { get; set; }

    }
}
