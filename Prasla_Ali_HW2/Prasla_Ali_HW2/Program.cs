using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prasla_Ali_HW2
{
    public enum Shifts { DAY, NIGHT, Error };
    class Program
    {
        static void Main(string[] args)
        {
            //Decide which Employee type
            Console.WriteLine("Please enter an employee type. Production Worker or Shift Supervisor");
            string which_emp = ErrorCheck.EmpType(Console.ReadLine());

            Console.WriteLine("Please enter the Employee's Name:");
            string temp_name = Console.ReadLine();

            Console.WriteLine("Please enter " + temp_name + "'s Employee Number");
            int temp_num = Convert.ToInt32(ErrorCheck.EmpNo(Console.ReadLine()));
            //Call Process Methods
            if (which_emp == "Production Worker") {
                ProcessProdWorker(temp_name,temp_num);
            }else if (which_emp == "Shift Supervisor")
            {
                Program.ProcessShiftSupervisor(temp_name,temp_num);

            }
            //Pause
            Console.ReadLine();

        }
        public static void ProcessProdWorker(string temp_name, int temp_num)
        {
            ProductionWorker current = new ProductionWorker();
            //Data Input with Error Checking Static Class
            current.Name = temp_name;
            current.Number = temp_num;
            Console.WriteLine("Please enter " + current.Name +"'s Pay Rate");
            current.PayRate = Convert.ToDecimal(ErrorCheck.PayRate(Console.ReadLine()));
            //Enter adjusted Enum code
            Shifts EmployeeShift;
            do
            {
                Console.WriteLine("Please enter " + current.Name + "'s shift, Day or Night");
                EmployeeShift = ProductionWorker.ValidateShift(Console.ReadLine());
            } while (EmployeeShift == Shifts.Error);

            Console.WriteLine("Please enter " + current.Name + "'s Hours Worked");
            current.HoursWorked = Convert.ToDecimal(ErrorCheck.HoursWorked(Console.ReadLine()));
            
            
            //Print Output
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Employee Name: " + current.Name);
            Console.WriteLine(current.Name +"'s Number: " + current.Number.ToString());
            Console.WriteLine(current.Name + "'s Shift: " + EmployeeShift.ToString());
            Console.WriteLine(current.Name+  "'s Pay Rate: " + current.PayRate.ToString("c"));
            Console.WriteLine(current.Name + "'s Hours Worked: " + current.HoursWorked.ToString());
            Console.WriteLine(current.Name + "'s Total Pay: " + current.CalcTotalPay().ToString("c"));
        }
        public static void ProcessShiftSupervisor(string temp_name,  int temp_num ) {

            //Input data
            ShiftSupervisor current = new ShiftSupervisor();
            current.Name = temp_name;
            current.Number = temp_num;
            Console.WriteLine("Please enter " + current.Name + "'s Annual Salary ");
            current.Salary = Convert.ToDecimal(ErrorCheck.Salary(Console.ReadLine()));
            Console.WriteLine("Please enter  " + current.Name + "'s Yearly Bonus");
            current.ProductionBonus = Convert.ToDecimal(ErrorCheck.Bonus(Console.ReadLine()));


            //Print an Data
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Employee Name: " + current.Name);
            Console.WriteLine(current.Name + "'s Number: " + current.Number.ToString());
            Console.WriteLine(current.Name + "'s Annual Salary : " + current.Salary.ToString("c"));
            Console.WriteLine(current.Name + "'s Production Bonus: " + current.ProductionBonus.ToString("c"));
            Console.WriteLine(current.Name + "'s Annual Pay : " + current.CalcAnnualPay.ToString("c"));

            Console.WriteLine(current.Name + "'s Number: " + current.Number.ToString());
        }
             
    }
}
