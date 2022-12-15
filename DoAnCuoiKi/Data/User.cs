using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DoAnCuoiKi.Data
{

    [Table("User")]
    public class User
    {
        [Key]
        public int ? userId { get; set; }
        public string ? name { get; set; }

        public string ? phone { get; set; }

        public string ? email { get; set; }

        public string? gender { get; set; }

        public string ? password { get; set; }

        public string ? address { get; set; }

        public string? role { get; set; }

        public Boolean isDelete { get; set; }


    }
}
