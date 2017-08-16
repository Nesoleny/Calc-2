using EveryDay.Calc.Webcalc.Repository;
using System.Data.Entity;

namespace EveryDay.Calc.Webcalc.EntitiF
{
    public class CalcContext: DbContext
    {
        public CalcContext() : base(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\education\Calc-2\EveryDay.Calc\EveryDay.Calc.Webcalc\App_Data\calc.mdf;Integrated Security=True")
        {
        }

        public DbSet<Operation> Operations { get; set; }

        public DbSet<OperResult> OperationResults { get; set; }

        public DbSet<User> Users { get; set; }
    }
}