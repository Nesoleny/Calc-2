using EveryDay.Calc.Calculation.Interfaces;
using EveryDay.Calc.Webcalc.Repository;
using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace EveryDay.Calc.Webcalc.Models
{
    /// <summary>
    /// Результат операции
    /// </summary>
    public class OperationResult
    {
        public OperationResult()
        {
            OperationList = new List<SelectListItem>();
            FavoriteOperations = new List<KeyValuePair<long, string>>();
        }

        public OperationResult(IEnumerable<Operation> operations, IEnumerable<long> tops)
        {
            SetViewModel(operations, tops);
        }

        public void SetViewModel(IEnumerable<Operation> operations, IEnumerable<long> tops)
        {
            OperationList = operations.Select(o => new SelectListItem() { Text = o.Name, Value = o.Id.ToString(), Selected = false });
            FavoriteOperations = operations.Where(o => tops.Contains(o.Id)).Select(o => new KeyValuePair<long, string>(o.Id, o.Name));
        }

        [DisplayName("Операция")]
        [Required(ErrorMessage = "Выбери операцию, бро")]
        public long Operation { get; set; }

        public IEnumerable<SelectListItem> OperationList { get; set; }

        public IEnumerable<KeyValuePair<long, string>> FavoriteOperations { get; set; }

        [DisplayName("Входные данные")]
        [Required(ErrorMessage = "Ввведи данные")]
        public double[] Inputs { get; set; }

        public bool IsEasy { get; set; }

        public DateTime ExecutionDate { get; set; }

        public double? Result { get; set; }

        [ReadOnly(false)]
        public long ExecutionTime { get; set; }
    }
}