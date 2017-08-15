using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EveryDay.Calc.Webcalc.Repository;

namespace EveryDay.Calc.Webcalc.EntitiF
{
    public class EFUserRepository : IUserRepository
    {
        public bool Check(string login, string password)
        {
            User result;
            using (var dbContext = new CalcContext())
            {
                result = dbContext.Users.FirstOrDefault(u => u.Login == login && u.Password == password);
            }

            return result != null;
        }

        public void Create(User obj)
        {
            using (var dbContext = new CalcContext())
            {
                dbContext.Users.Add(obj);
            }
        }

        public void Delete(long Id)
        {
            using (var dbContext = new CalcContext())
            {
                dbContext.Users.Remove(dbContext.Users.Find(Id));
            }
        }

        public IEnumerable<User> GetAll()
        {
            using (var dbContext = new CalcContext())
            {
                return dbContext.Users.ToList();
            }
        }

        public User Read(long Id)
        {
            using (var dbContext = new CalcContext())
            {
                return dbContext.Users.Find(Id);
            }
        }

        public void Update(User obj)
        {
            using (var dbContext = new CalcContext())
            {
                //dbContext.Users. не работает
            }
        }

    }
}