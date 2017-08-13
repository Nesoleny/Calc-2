using EveryDay.Calc.Calculation.Interfaces;
using System;

namespace SuperPuperCalcLibrary
{
    public class SuperOperaton : IOperation
    {
        public double[] Input
        {
            get; set;
        }

        public string Name
        {
            get { return "Super"; }
        }

        public double? GetResult()
        {
            return Double.MaxValue;
        }
    }
}
