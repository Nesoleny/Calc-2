using EveryDay.Calc.Calculation.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace EveryDay.Calc.AppCalc
{
    public static class Helper
    {
        public static double Str2Double(string str)
        {
            double result;

            double.TryParse(str, out result);

            return result;
        }

        public static IEnumerable<IOperation> LoadOperations()
        {
            var opers = new List<IOperation>();

            var typeOperation = typeof(IOperation);

            // загружаем сборку из файла
            var assembly = Assembly.GetAssembly(typeOperation);
            // получаем типы/классы/интерфейсы из сброрки
            var types = assembly.GetTypes();

            // перебираем типы
            foreach (var type in types)
            {
                if (type.IsAbstract || type.IsInterface)
                    continue;

                var interfaces = type.GetInterfaces().Select(i => i.FullName);
                // если тип реализует наш интерфейс 
                if (interfaces.Contains(typeOperation.FullName))
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


            return opers;
        }

        public static IEnumerable<IOperation> LoadOperations(string path = "")
        {
            var opers = new List<IOperation>();

            var typeOperation = typeof(IOperation);

            // найти все dll, которые находятся рядом с нашим exe
            var dlls = Directory.GetFiles(!string.IsNullOrWhiteSpace(path) ? path : Environment.CurrentDirectory, "*.dll");

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
                    if (type.IsAbstract || type.IsInterface)
                        continue;

                    var interfaces = type.GetInterfaces().Select(i => i.FullName);
                    // если тип реализует наш интерфейс 
                    if (interfaces.Contains(typeOperation.FullName))
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

    }
}
