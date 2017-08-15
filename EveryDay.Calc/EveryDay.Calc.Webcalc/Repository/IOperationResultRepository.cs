using System.Collections.Generic;

namespace EveryDay.Calc.Webcalc.Repository
{
    public interface IOperationResultRepository : IRepository<OperResult>
    {
        bool Check(long operationId, string inputs, out double? result, out string error);

        IEnumerable<long> GetTopOperations(long userId, int limit = 3);
    }
}
