using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DoAnCuoiKi.Data
{
    [Table("Category")]
    public class Category
    {
        [Key]
        public int? categoryId { get; set; }
        public string ? name { get; set; }
        public Boolean? isDelete { get; set; }

    }
}
