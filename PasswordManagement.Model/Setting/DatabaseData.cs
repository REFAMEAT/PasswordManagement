﻿namespace REFame.PasswordManagement.Model.Setting
{
    public class DatabaseData
    {
        public string DatabaseName { get; set; }
        public string ServerName { get; set; }
        public string Password { get; set; }
        public string Username { get; set; }
        public bool IntegratedSecurity { get; set; }
        public bool UseDatabase { get; set; }
    }
}