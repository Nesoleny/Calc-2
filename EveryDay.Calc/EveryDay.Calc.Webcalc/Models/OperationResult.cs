using EveryDay.Calc.Calculation.Interfaces;
using System;
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
            FavoriteOperations = new List<string>();
        }

        [DisplayName("Операция")]
        [Required(ErrorMessage = "Выбери операцию, бро")]
        public string Operation { get; set; }

        public IEnumerable<SelectListItem> OperationList { get; set; }

        public IEnumerable<string> FavoriteOperations { get; set; }

        [DisplayName("Входные данные")]
        [Required(ErrorMessage = "Ввведи данные")]
        public double[] Inputs { get; set; }

        public bool IsEasy { get; set; }

        public DateTime ExecutionDate { get; set; }

        [ReadOnly(true)]
        public double? Result { get; set; }

        [ReadOnly(false)]
        public int ExecutionTime { get; set; }
    }
}