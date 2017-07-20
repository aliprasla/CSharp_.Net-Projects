using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prasla_Ali_HW2
{
    class ShiftSupervisor : Employee
    {
        public decimal Salary { get; set; }
        public decimal ProductionBonus { get; set; }
        public decimal CalcAnnualPay {
            get { return Salary + ProductionBonus; }
        } 
    }
}
