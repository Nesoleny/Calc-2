using System;
using System.Linq;
using System.Threading;

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
            Thread.Sleep(new Random().Next(0, 100) * 100);
            return Input.Sum();
        }
    }
}
