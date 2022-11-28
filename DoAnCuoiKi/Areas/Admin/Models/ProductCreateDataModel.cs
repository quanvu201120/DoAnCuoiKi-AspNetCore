using DoAnCuoiKi.Data;

namespace DoAnCuoiKi.Areas.Admin.Models
{
    public class ProductCreateDataModel
    {
        public List<Brand> brand { get; set; }
        public List<Category> category { get; set; }
    }
}
