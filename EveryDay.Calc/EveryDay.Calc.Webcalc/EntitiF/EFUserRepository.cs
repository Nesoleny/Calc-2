using EveryDay.Calc.Webcalc.Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace EveryDay.Calc.Webcalc.EntitiF
{
    public class EFUserRepository : IUserRepository
    {
        public User GetByName(string login)
        {
            using (var db = new CalcContext())
            {
                var user = db.Users.FirstOrDefault(u => u.Login == login);

                db.Entry(user).Collection(u => u.OperResults).Load();

                return user;

            }
        }

        public bool Check(string login, string password)
        {
            using (var dbContext = new CalcContext())
            {
                return dbContext.Users.Count(u => u.Login == login && u.Password == password) == 1;
            }
        }

        public void Create(User obj)
        {
            throw new NotImplementedException();
        }

        public void Delete(long Id)
        {
            using (CalcContext db = new CalcContext())
            {
                User delUser = db.Users.FirstOrDefault(c => c.Id == Id);
                if (delUser != null)
                {
                    delUser.Status = UserStatus.Dead;
                }
                db.SaveChanges();
            }
        }

        public IEnumerable<User> GetAll()
        {
            using (var dbContext = new CalcContext())
            {
                return dbContext.Users.Where(u => u.Status == UserStatus.Active);
            }
        }

        public IEnumerable<User> Find(Expression<Func<User, bool>> filter)
        {
            using (var dbContext = new CalcContext())
            {
                return dbContext.Users
                    .Where(u => u.Status == UserStatus.Active)
                    .Where(filter)
                    .ToList();
            }
        }

        public User Read(long Id)
        {
            using (var db = new CalcContext())
            {
                return db.Users.FirstOrDefault(u => u.Id == Id);
            }
        }

        public void Update(User obj)
        {
            using (var db = new CalcContext())
            {
                db.Entry(obj).State = EntityState.Modified;
                db.SaveChanges();
            }
        }
    }
}