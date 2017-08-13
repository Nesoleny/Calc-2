using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EveryDay.Calc.Calculation.Interfaces
{
    /// <summary>
    /// Операция
    /// </summary>
    public interface IOperation
    {
        /// <summary>
        /// Наименование
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Входные параметры
        /// </summary>
        double[] Input { get; set; }

        /// <summary>
        /// Получить результат
        /// </summary>
        /// <returns>Результат операции</returns>
        double? GetResult();
    }
}
