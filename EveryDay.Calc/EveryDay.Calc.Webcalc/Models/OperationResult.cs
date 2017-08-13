using EveryDay.Calc.Calculation.Interfaces;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace EveryDay.Calc.Webcalc.Models
{
    /// <summary>
    /// Результат операции
    /// </summary>
    public class OperationResult
    {
        [DisplayName("Операция")]
        [Required(ErrorMessage = "Выбери операцию, бро")]
        public string Operation { get; set; }

        [DisplayName("Входные данные")]
        [Required(ErrorMessage = "Введи данные")]
        public double[] Inputs { get; set; }

        public bool IsEasy { get; set; }

        public DateTime ExecutionDate { get; set; }

        [ReadOnly(true)]
        public double? Result { get; set; }

        [ReadOnly(false)]
        public int ExecutionTime { get; set; }
    }
}