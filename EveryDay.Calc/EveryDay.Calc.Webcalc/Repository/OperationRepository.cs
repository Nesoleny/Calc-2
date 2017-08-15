using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlClient;

namespace EveryDay.Calc.Webcalc.Repository
{
    public class OperationRepository : BaseRepository<Operation>, IOperationRepository
    {
        public OperationRepository() 
            : base("Operation", new[] { "Name", "Description", "IsDeleted" })
        {
        }

        public override Operation ReadSingleRow(SqlDataReader record)
        {
            var obj = new Operation()
            {
                Id = record.GetInt64(0),
                Name = record.GetString(1),
                //Description = record.GetString(2)
            };
            return obj;
        }

        public override string WriteSingleRow(Operation obj)
        {
            return string.Format("'{0}', '{1}', {2}", obj.Name, obj.Description, obj.IsDeleted ? 1 : 0);
        }
    }

    [Table("Operation")]
    public class Operation
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool IsDeleted { get; set; }
    }
}