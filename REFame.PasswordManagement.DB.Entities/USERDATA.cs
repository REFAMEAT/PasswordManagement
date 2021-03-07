using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using REFame.PasswordManagement.DB.Entities.Base;

namespace REFame.PasswordManagement.DB.Entities
{
    [Table("USERDATA")]
    public class USERDATA : GenerateTable
    {
        [Key]
        [Required]
        [Column("USID", TypeName = "nvarchar(50)")]
        public string USID { get; set; }

        [Required]
        [Column("USUSERNAME", TypeName = "nvarchar(MAX)")]
        public string USUSERNAME { get; set; }

        [Required]
        [Column("USPASSWORD", TypeName = "nvarchar(MAX)")]
        public string USPASSWORD { get; set; }

        [Column("USSALT", TypeName = "nvarchar(MAX)")]
        public string USSALT { get; set; }
    }
}