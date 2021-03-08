using System;
using REFame.PasswordManagement.AppCore.Contracts;

namespace REFame.PasswordManagement.AppCore
{
    public class PWCore
    {
        public static void Create()
        {
            if (CurrentCore == null)
            {
                CurrentCore = new Core(); 
            }
            else
            {
                throw new ApplicationException("App core already exist");
            }
        }

        public static ICore CurrentCore { get; set; }
    }
}