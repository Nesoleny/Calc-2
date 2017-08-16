using System;
using System.Linq;
using System.Data.SqlClient;
using System.Globalization;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace EveryDay.Calc.Webcalc.Repository
{
    public class OperationResultRepository : BaseRepository<OperResult>, IOperationResultRepository
    {
        public OperationResultRepository() 
            : base("OperationResult", 
                  new[] { "OperationId", "Inputs", "ExecutionDate", "ExecutionTime", "Result", "Error", "UserId" }
                  )
        {
        }

        public bool Check(long operationId, string inputs, out double? result, out string error)
        {
            result = null;
            error = string.Empty;

            var all = GetAll();

            var res = all.FirstOrDefault(o => o.OperationId == operationId && o.Inputs == inputs);

            if (res == null)
                return false;

            result = res.Result;
            error = res.Error;

            return true;
        }

        public IEnumerable<long> GetTopOperations(long userId, int limit = 3)
        {
            var result = new List<long>();

            string queryString = @"
select top({1}) OR1.OperationId as Id, count(OR1.Id) as Count
from OperationResult as OR1
where OR1.UserId = {0}
group by OR1.OperationId
having count(OR1.Id) > 1
order by Count desc";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(string.Format(queryString, userId, limit), connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                // Call Read before accessing data.
                while (reader.Read())
                {
                    result.Add(reader.GetInt64(0));
                }

                // Call Close when done reading.
                reader.Close();
            }
            return result;
        }

        public override OperResult ReadSingleRow(SqlDataReader record)
        {
            var obj = new OperResult()
            {
                Id = record.GetInt64(0),
                OperationId = record.GetInt64(1),
                Inputs = record.GetString(2),
                ExecutionDate = record.GetDateTime(3),
                ExecutionTime = record.GetInt64(4),
                Result = record.GetDouble(5),
                Error = record.IsDBNull(6) ? "" : record.GetString(6),
                UserId = record.GetInt64(7)
            };
            return obj;
        }

        public override string WriteSingleRow(OperResult obj)
        {
            return string.Format("{0}, '{1}', {2}, {3}, {4}, '{5}', {6}", 
                obj.OperationId, 
                obj.Inputs, 
                "GETDATE()", 
                obj.ExecutionTime, 
                obj.Result.HasValue ? obj.Result.Value.ToString(CultureInfo.InvariantCulture) : "", 
                obj.Error, 
                obj.UserId);
        }
    }

    [Table("OperationResult")]
    public class OperResult
    {
        public long Id { get; set; }

        public long OperationId { get; set; }

        public string Inputs { get; set; }

        public DateTime ExecutionDate { get; set; }

        public long ExecutionTime { get; set; }

        public double? Result { get; set; }

        public string Error { get; set; }

        public long UserId { get; set; }

        public virtual User User { get; set; }

        public virtual Operation Operation { get; set; }
    }
}