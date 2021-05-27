using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using REFame.PasswordManagement.DB.Entities.Base;

// ReSharper disable InconsistentNaming

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
        [Column("USUSERNAME", TypeName = "nvarchar(255)")]
        public string USUSERNAME { get; set; }

        [Required]
        [Column("USNAME", TypeName = "nvarchar(255)")]
        public string USNAME { get; set; }

        [Required]
        [Column("USPASSWORD", TypeName = "nvarchar(255)")]
        public string USPASSWORD { get; set; }

        [Column("USSALT", TypeName = "nvarchar(255)")]
        public string USSALT { get; set; }

        [Column("USMAIL", TypeName = "nvarchar(255)")]
        public string USMAIL { get; set; }

        [Column("USTITLE", TypeName = "nvarchar(255)")]
        public string USTITLE { get; set; }
    }
}