using EveryDay.Calc.Calculation.Interfaces;
using System;
using System.Linq;

namespace EveryDay.Calc.Calculation.Models
{
    public class SqrtOperation : IOperation
    {
        public double[] Input { get; set; }

        public string Name
        {
            get { return "Sqrt"; }
        }

        public double? GetResult()
        {
            return Math.Sqrt(Input[0]);
        }
    }
}
