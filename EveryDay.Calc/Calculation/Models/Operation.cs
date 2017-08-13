using EveryDay.Calc.Calculation.Interfaces;
using System.Text;

namespace EveryDay.Calc.Calculation.Models
{
    public abstract class Operation : IExtendedOperation
    {
        public double[] Input { get; set; }

        public virtual string Error { get; set; }

        public virtual string Name { get { return ""; } }

        public virtual string Description { get { return ""; } }

        public virtual double? GetResult()
        {
            return null;
        }

        public string GetDescription()
        {
            var sb = new StringBuilder();
            sb.Append(Name);
            if (!string.IsNullOrWhiteSpace(Description))
            {
                sb.AppendFormat(" / {0}", Description);
            }
            return sb.ToString();
        }
    }
}
