namespace REFame.PasswordManagement.Login.Contracts
{
    public class User
    {
        public string Identifier { get; set; }

        public string UserName { get; set; }

        public string Title { get; set; }

        public string FullName { get; set; }

        public string EMail { get; set; }

        public Credentials Credentials { get; set; }
    }
}