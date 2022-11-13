using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DoAnCuoiKi.Data
{
    [Table("Brand")]
    public class Brand
    {
        [Key]
        public int brandId { get; set; }

        public string name { get; set; }
    }
}
