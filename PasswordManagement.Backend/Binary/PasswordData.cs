using System;

namespace PasswordManagement.Backend.Binary
{
    /// <summary>
    /// The password-storaging model 
    /// </summary>
    [Serializable]
    public class PasswordData
    {
        public string Description { get; set; }
        public string Password { get; set; }
        public string Comments { get; set; }
    }
}