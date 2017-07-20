using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prasla_Ali_HW2
{
    class ProductionWorker : Employee
    {
        public Decimal PayRate { get; set; }
        public Decimal HoursWorked { get; set; }
        public string Shift { get; set; }
        public static Shifts ValidateShift(String strInput)
        {
            //try to convert from string to enum
            try
            {
                return (Shifts)Enum.Parse(typeof(Shifts), strInput.ToUpper());
            }
            catch
            {
                return Shifts.Error;
            }
        }
        public decimal CalcTotalPay () {
            return (PayRate * HoursWorked);
        }

    }
}
