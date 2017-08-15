using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveryDay.Calc.Webcalc.Repository
{
    public interface IUserRepository : IRepository<User>
    {
        bool Check(string login, string password);
    }
}
