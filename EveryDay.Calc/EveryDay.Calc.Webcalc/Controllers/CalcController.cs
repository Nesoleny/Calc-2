using EveryDay.Calc.AppCalc;
using EveryDay.Calc.Calculation;
using EveryDay.Calc.Calculation.Interfaces;
using EveryDay.Calc.Webcalc.Models;
using EveryDay.Calc.Webcalc.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EveryDay.Calc.Webcalc.Controllers
{
    public class CalcController : Controller
    {
        private IRepository<Operation> OpRepository { get; set; }

        private Calculator Calculator { get; set; }

        private IEnumerable<Operation> Operations { get; set; }

        public CalcController()
        {
            //var extensionPath = Server.MapPath("~/App_Data/Extensions");
            // Operations = Helper.LoadOperations();

            OpRepository = new OperationRepository();

            Operations = OpRepository.GetAll();
        }

        [HttpGet]
        public ActionResult Index(string operation)
        {
            var model = new OperationResult();
            model.Operation = operation;
            model.Inputs = new double[] { 12, 9 };

            var favOpers = Operations.Where(o => o.Name.Length % 2 == 0).Take(1);

            model.OperationList = Operations.Select(o => new SelectListItem() { Text = o.Name, Value = o.Name, Selected = false });
            model.FavoriteOperations = favOpers.Select(o => o.Name);

            return View(model);
        }

        [HttpPost]
        public ActionResult Index(OperationResult model, string inputs, string favOperation)
        {
            if (Calculator == null)
            {
                Calculator = new Calculator(Helper.LoadOperations());
            }

            var operation = !string.IsNullOrWhiteSpace(favOperation) ? favOperation : model.Operation;

            var favOpers = Operations.Where(o => o.Name.Length % 2 == 0).Take(1);

            model.OperationList = Operations.Select(o => new SelectListItem() { Text = o.Name, Value = o.Name, Selected = o.Name == model.Operation });
            model.FavoriteOperations = favOpers.Select(o => o.Name);

            model.ExecutionDate = DateTime.Now;
            model.Inputs = inputs.Split(' ').Select(Helper.Str2Double).ToArray();

            model.Result = Calculator.Calc(operation, model.Inputs);

            return View(model);
        }

    }
}