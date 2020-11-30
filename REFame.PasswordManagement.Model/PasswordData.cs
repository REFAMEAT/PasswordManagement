using System;

namespace REFame.PasswordManagement.Model
{
    /// <summary>
    ///     The password-storaging model
    /// </summary>
    [Serializable]
    public class PasswordData
    {
        public string Identifier { get; set; }
        public string Description { get; set; }
        public string Password { get; set; }
        public string Comments { get; set; }
    }
}