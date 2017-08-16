using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EveryDay.Calc.Webcalc.Repository
{
    public interface IUserRepository : IRepository<User>
    {
        bool Check(string login, string password);

        User GetByName(string login);

        /// <summary>
        /// Найти пользователей по условию
        /// </summary>
        /// <param name="filter">Условие</param>
        /// <returns>Список пользователей</returns>
        IEnumerable<User> Find(Expression<Func<User, bool>> filter);
    }
}
