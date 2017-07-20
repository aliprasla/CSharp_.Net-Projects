using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prasla_Ali_HW2
{
    static class Validate
    {
        //Validate Class Simply returns if an input is acceptable or not

        public static bool EmpType(string type) {
            if (type == "Production Worker" || type == "Shift Supervisor")
            {
                return true;
            }
            else {
                return false;
            }
        }
        public static bool EmpNum(string Num) {
            try
            {
                Convert.ToInt32(Num);
                return true;
            }
            catch {
                return false;
            }
        }
        public static bool PayRate(string Pay) {
            try
            {
                Convert.ToDecimal(Pay);
                return true;
            }
            catch {

                return false;
            }
        }
        public static bool HoursWorked (string strInput) {
            try
            {
                Convert.ToDecimal(strInput);
                return true;
            }
            catch {
                return false;
            }
         }
        public static bool Salary(string strInput) {
            try
            {
                Convert.ToDecimal(strInput);
                return true;
            }
            catch {
                return false;


            }
        }
        public static bool Bonus(string strInput) {
            try
            {
                Convert.ToDecimal(strInput);
                return true;
            }
            catch {
                return false;
            }

        }
    }
}
