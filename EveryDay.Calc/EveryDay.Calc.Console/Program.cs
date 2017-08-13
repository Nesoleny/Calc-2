using EveryDay.Calc.Calculation;
using EveryDay.Calc.Calculation.Interfaces;
using EveryDay.Calc.Calculation.Models;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;
using SConsole = System.Console;
using System.IO;

namespace EveryDay.Calc.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var operations = LoadOperations();

            var calc = new Calculator(operations);

            

            if(args.Length > 1)
            {
                var oper = args[0].ToLower();
                var numbers = args.Skip(1).Select(Str2Int).ToArray();
                var result = calc.Calc(oper, numbers);
                if (result != null)
                {
                    SConsole.WriteLine("Результат: {0}", result);
                }
                else
                {
                    SConsole.WriteLine("Операция не найдена или произошла ошибка. Введите данные заново");
                }
                SConsole.ReadKey();
                return;
            }
            
            PrintOperations(operations);
            
            while (true)
            {
                SConsole.WriteLine("Введите выбранную операцию:");
                var oper = SConsole.ReadLine().ToLower();
                if (oper == "exit")
                    break;

                SConsole.WriteLine("Введите данные, разделитель - пробел:");
                var strParams = SConsole.ReadLine().Split(' ');
                var numbers = strParams.Select(Str2Int).ToArray();
                var result = calc.Calc(oper, numbers);
                if (result != null)
                {
                    SConsole.WriteLine("Результат: {0}", result);
                }
                else
                {
                    SConsole.WriteLine("Операция не найдена или произошла ошибка. Введите данные заново");
                }
                SConsole.WriteLine("-------------------");
            }
        }

        private static void PrintOperations(IEnumerable<IOperation> opers)
        {
            SConsole.WriteLine("Доступные операции:");
            foreach (var oper in opers)
            {
                var baseOper = oper as Operation;
                if (baseOper != null)
                {
                    SConsole.WriteLine(baseOper.GetDescription());
                }
                else
                {
                    SConsole.WriteLine(oper.Name);
                }
            }
            SConsole.WriteLine("\"exit\" - выход из программы");
        }

        private static IEnumerable<IOperation> LoadOperations()
        {
            var opers = new List<IOperation>();

            var typeOperation = typeof(IOperation);

            // найти все dll, которые находятся рядом с нашим exe
            var dlls = Directory.GetFiles(Environment.CurrentDirectory, "*.dll");

            // перебираем
            foreach (var dll in dlls)
            {
                // загружаем сборку из файла
                var assembly = Assembly.LoadFrom(dll);
                // получаем типы/классы/интерфейсы из сброрки
                var types = assembly.GetTypes();

                // перебираем типы
                foreach (var type in types)
                {
                    if (type.IsAbstract)
                        continue;

                    var interfaces = type.GetInterfaces();
                    // если тип реализует наш интерфейс 
                    if (interfaces.Contains(typeOperation))
                    {
                        // пытаемся создать экземпляр
                        var instance = Activator.CreateInstance(type) as IOperation;
                        if (instance != null)
                        {
                            // добавляем в список операций
                            opers.Add(instance);
                        }
                    }
                }
            }

            return opers;
        }

        private static double Str2Int(string str)
        {
            double result;

            if (!double.TryParse(str, out result))
            {
                SConsole.WriteLine("Не удалось преобразовать \"{0}\" в число", str);
            }
            return result;
        }
    }
}
