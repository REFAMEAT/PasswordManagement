using REFame.PasswordManagement.DB.Contracts;
using REFame.PasswordManagement.DB.Entities;
using REFame.PasswordManagement.DB.Internals;

namespace REFame.PasswordManagement.DB
{
    public class PwmDbContext : DbContextBase, IPwmDbContext
    {
        private readonly Context context;

        public PwmDbContext(Context context) : base(context)
        {
            this.context = context;
            PASSWORDDATA = new DataSet<PASSWORDDATA>(context.PASSWORDDATA);
            USERDATA = new DataSet<USERDATA>(context.USERDATA);
            USERTHEME = new DataSet<USERTHEME>(context.USERTHEME);
        }

        public IDataSet<PASSWORDDATA> PASSWORDDATA { get; set; }

        public IDataSet<USERDATA> USERDATA { get; set; }

        public IDataSet<USERTHEME> USERTHEME { get; set; }


        public void Dispose()
        {
            context.Dispose();
        }
    }
}