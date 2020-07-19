using System;
using System.Reflection;
using System.Threading.Tasks;
using PasswordManagement.Database;

namespace PasswordManagement.DatabaseBuilder.Console
{
    class Program
    {
        static async Task Main(string[] args)
        {
           await new Database().Build<GenerateTable>(true,Assembly.GetAssembly(typeof(GenerateTable)));
        }
    }
}
