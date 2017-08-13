using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EveryDay.Calc.Calculation.Interfaces
{
    public interface IExtendedOperation : IOperation
    {
        string Description { get; }

        string Error { get; set; }
    }
}
