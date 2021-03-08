namespace REFame.PasswordManagement.Login.Contracts
{
    public class Credentials
    {
        public string Salt { get; set; }
        public string Hash { get; set; }
    }
}