using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PasswordManagement.Database.Model
{
    [Table("USERDATA")]
    public class USERDATA
    {
        [Key]
        [Column("USID", TypeName = "nvarchar(50)")]
        public string USID { get; set; }

        [Column("USUSERNAME", TypeName = "nvarchar(50)")]
        public string USUSERNAME { get; set; }

        [Column("USPASSWORD", TypeName = "nvarchar(MAX)")]
        public string USPASSWORD { get; set; }

        public string USSALT { get; set; }
    }
}