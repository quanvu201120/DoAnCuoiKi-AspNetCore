using DoAnCuoiKi.Data;

namespace DoAnCuoiKi.Areas.Admin.Models
{
    public class ProductEditDataModel
    {
        public Product product { get; set; }
        public List<Brand> brands { get; set; }
        public List<Category> categories { get; set; }
    }
}
