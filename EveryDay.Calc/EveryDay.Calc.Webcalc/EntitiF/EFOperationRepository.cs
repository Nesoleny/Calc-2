using EveryDay.Calc.Webcalc.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Linq.Expressions;

namespace EveryDay.Calc.Webcalc.EntitiF
{
    public class EFOperationRepository : IOperationRepository
    {
        public void Create(Operation obj)
        {
            throw new NotImplementedException();
        }

        public void Delete(long Id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Operation> Find(Expression<Func<Operation, bool>> filter)
        {
            using (var dbContext = new CalcContext())
            {
                return dbContext.Operations
                    .Where(filter)
                    .ToList();
            }
        }

        public IEnumerable<Operation> GetAll()
        {
            using (var dbContext = new CalcContext())
            {
                return dbContext.Operations.ToList();
            }
        }

        public Operation Read(long Id)
        {
            using (var db = new CalcContext())
            {
                var operation = db.Operations.FirstOrDefault(u => u.Id == Id);

                db.Entry(operation).Collection(u => u.OperResults).Load();

                return operation;
            }
        }

        public void Update(Operation obj)
        {
            throw new NotImplementedException();
        }
    }
}