using System;

namespace PasswordManagement.Backend.Binary
{
    /// <summary>
    /// The password-storaging model 
    /// </summary>
    [Serializable]
    public class PasswordData
    {
        internal string Identifier { get; set; }
        internal string Description { get; set; }
        internal string Password { get; set; }
        internal string Comments { get; set; }
    }
}