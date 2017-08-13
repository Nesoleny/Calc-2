using System.Linq;

namespace EveryDay.Calc.Calculation.Models
{
    public class SumOperation : Operation
    {
        public override string Name
        {
            get { return "Sum"; }
        }

        public override string Description
        {
            get { return "Складывает числа"; }
        }

        public override double? GetResult()
        {
            return Input.Sum();
        }
    }
}
