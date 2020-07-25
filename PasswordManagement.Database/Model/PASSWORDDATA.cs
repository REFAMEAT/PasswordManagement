using System.ComponentModel.DataAnnotations;

namespace PasswordManagement.Database.Model
{
    public class PASSWORDDATA : GenerateTable
    {
        [Required]
        [StringLength(40)]
        public string PWUSID { get; set; }

        [Required]
        public USERDATA USER { get; set; }

        public string PWDATA { get; set; }

        public string PWCOMMENT { get; set; }

        public string PWDESCRIPTION { get; set; }
    }
}