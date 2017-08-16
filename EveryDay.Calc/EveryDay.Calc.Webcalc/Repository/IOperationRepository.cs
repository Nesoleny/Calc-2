using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace EveryDay.Calc.Webcalc.Repository
{
    public interface IOperationRepository : IRepository<Operation>
    {
        IEnumerable<Operation> Find(Expression<Func<Operation, bool>> filter);
    }
}
