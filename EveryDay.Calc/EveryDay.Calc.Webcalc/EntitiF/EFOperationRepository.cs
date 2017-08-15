using EveryDay.Calc.Webcalc.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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

        public IEnumerable<Operation> GetAll()
        {
            using (var dbContext = new CalcContext())
            {
                return dbContext.Operations.ToList();
            }
        }

        public Operation Read(long Id)
        {
            throw new NotImplementedException();
        }

        public void Update(Operation obj)
        {
            throw new NotImplementedException();
        }
    }
}