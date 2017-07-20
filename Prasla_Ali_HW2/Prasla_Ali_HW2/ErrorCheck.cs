using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prasla_Ali_HW2
{
    static class ErrorCheck
    {
        //Error Check Keeps prompting users after an invalid input is inputed
        public static string EmpType(string input) {
            if (Validate.EmpType(input))
            {
                return input;
            }
            else {
                do
                {
                    Console.WriteLine("Invalid Employee Type. Please enter a valid Employee Type. Production Worker or Shift Supervisor");
                    input = Console.ReadLine();
                } while (!Validate.EmpType(input));
                return input;
            }

        }
        public static string EmpNo(string input){
            if (Validate.EmpNum(input))
            {
                return input;
            }
            else {
                do {
                    Console.WriteLine("Error. Please enter a valid integer Employee Number");
                    input = Console.ReadLine();
                } while (!Validate.EmpNum(input));
                return input;
            }

        }
        public static string PayRate(string input) {
            if (Validate.PayRate(input))
            {
                return input;
            }
            else {
                do
                {
                    Console.WriteLine("Error. Please enter a valid Decimal Pay Rate");
                    input = Console.ReadLine();
                } while (!Validate.PayRate(input));
                return input;
            }
        }
        public static string HoursWorked(string input) {
            if (Validate.HoursWorked(input))
            {
                return input;
            }
            else {
                do
                {
                    Console.WriteLine("Error. Please enter a valid Decimal Hours Worked");
                    input = Console.ReadLine();
                } while (!Validate.HoursWorked(input));
                return input;
            }
        }
        public static string Salary(string input) {
            if (Validate.Salary(input))
            {
                return input;
            }
            else {
                do
                {
                    Console.WriteLine("Error. Please enter a valid Decimal Annual Salary. ");
                    input = Console.ReadLine();
                } while (!Validate.Salary(input));
                return input;
            }
        }
        public static string Bonus(string input) {
            if (Validate.Bonus(input))
            {
                return input;
            }
            else {
                do
                {
                    Console.WriteLine("Error. Please enter a valid Decimal Bonus Salary. ");
                    input = Console.ReadLine();
                } while (!Validate.Bonus(input));
                return input;
            }
        }
    }
}
