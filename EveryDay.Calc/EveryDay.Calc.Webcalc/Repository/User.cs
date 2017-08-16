using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EveryDay.Calc.Webcalc.Repository
{
    [Table("User")]
    public class User
    {
        public User()
        {
            OperResults = new List<OperResult>();
        }

        public long Id { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        public string FIO { get; set; }

        public UserStatus Status { get; set; }

        public virtual ICollection<OperResult> OperResults { get; set; }

    } 

    public enum UserStatus
    {
        Blocked = 0,
        Active = 1,
        Dead = 2
    }
}