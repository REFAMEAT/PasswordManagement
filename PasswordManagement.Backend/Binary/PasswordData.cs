using System;

namespace PasswordManagement.Backend.BinarySerializer
{
    [Serializable]
    public class PasswordData
    {
        public string Description { get; set; }
        public string Password { get; set; }
        public string Comments { get; set; }
    }
}