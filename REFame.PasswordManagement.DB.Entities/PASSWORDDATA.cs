using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using REFame.PasswordManagement.DB.Entities.Base;

namespace REFame.PasswordManagement.DB.Entities
{
    [Table("PASSWORDDATA")]
    public class PASSWORDDATA : GenerateTable
    {
        [Key]
        public string PWID { get; set; }

        [Required]
        public USERDATA USER { get; set; }

        public string PWDATA { get; set; }

        public string PWCOMMENT { get; set; }

        public string PWDESCRIPTION { get; set; }

        public string USERUSID { get; set; }
    }
}