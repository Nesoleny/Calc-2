using EveryDay.Calc.Calculation.Interfaces;
using System;
using System.Linq;
using System.Collections.Generic;

namespace EveryDay.Calc.Calculation
{
    public class Calculator
    {
        public Calculator(IEnumerable<IOperation> operations)
        {
            this.operations = operations;
        }

        public Calculator()
        {
            this.operations = new List<IOperation>();
        }

        /// <summary>
        /// Список доступных операций
        /// </summary>
        private IEnumerable<IOperation> operations { get; set; }

        /// <summary>
        /// Выполнить операцию
        /// </summary>
        /// <param name="operationName">Наименование операции</param>
        /// <param name="input">Входные данные</param>
        /// <returns></returns>
        public double? Calc(string operationName, double[] input)
        {
            // найти операцию по имени
            var operation = operations.FirstOrDefault(o => o.Name.Equals(operationName, StringComparison.CurrentCultureIgnoreCase));

            // если операцию не нашли, возвращаем null
            if (operation == null)
                return null;
            
            // вводим данные в операцию
            operation.Input = input;
            // рассчитываем
            var result = Calc(operation);
            // возвращаем результат
            return result;
        }

        public double? Calc(IOperation operation)
        {
            return operation.GetResult();
        }

        #region Устаревшее

        [Obsolete("Используйте Calc(\"sum\", new []{x, y})")]
        public double Sum(double x, double y)
        {
            return Calc("sum", new double[] { x, y }) ?? double.NaN;
        }

        public double Div(double x, double y)
        {
            return y == 0 ? 0 : x / y;
        }

        public double Sqrt(double x)
        {
            return Math.Sqrt(x);
        }

        #endregion
    }
}
