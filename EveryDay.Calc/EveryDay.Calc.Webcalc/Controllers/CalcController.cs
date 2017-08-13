using EveryDay.Calc.AppCalc;
using EveryDay.Calc.Calculation;
using EveryDay.Calc.Webcalc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EveryDay.Calc.Calculation.Interfaces;
using System.Reflection;

namespace EveryDay.Calc.Webcalc.Controllers
{
    public class CalcController : Controller
    {

        private Calculator Calculator { get; set; }

        public CalcController()
        {
        }

        [HttpGet]
        public ActionResult Index(string operation)
        {
            var model = new OperationResult();
            model.Operation = operation;
            ViewBag.operations = LoadOperations().ToArray();
            model.Inputs = new double[] { 12, 6, 9 };
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(OperationResult model, string inputs)
        {
            if (Calculator == null)
            {
                var extensionPath = Server.MapPath("~/App_Data/Extensions");
                var operations = Helper.LoadOperations();
                Calculator = new Calculator(operations);
            }

            ViewBag.operations = LoadOperations().ToArray();

            model.ExecutionDate = DateTime.Now;
            model.Inputs = inputs.Split(' ').Select(Helper.Str2Double).ToArray();

            model.Result = Calculator.Calc(model.Operation, model.Inputs);

            return View(model);
        }

        public static IEnumerable<string> LoadOperations()
        {
            var opers = new List<string>();

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
                        opers.Add(instance.Name);
                    }
                }
            }


            return opers;
        }

    }
}