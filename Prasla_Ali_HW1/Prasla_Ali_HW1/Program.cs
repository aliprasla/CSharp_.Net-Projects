using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prasla_Ali_HW1
{
    class Program
    {
        //Author: Ali Prasla
        //Date: January 31, 2016
        //Assignment: Homework 1
        //Description: Calculates ticket prices

        static void Main(string[] args)
        {
            //Prompt user with data inputs, premium and GA tickets
            UserInput current = new UserInput();
            current.ShowPrice();
            //pauses console
            Console.ReadLine();
        }
    }
    public class UserInput
    {
        //initalize variable
        public String num_pre;
        public String num_gen;
        public int num_p = 0;
        public int num_g = 0;
        //define constructor
        public UserInput()
        {
                //user inputs
                Console.WriteLine("Enter the number of premium tickets you wish to purchase");
                num_pre = Console.ReadLine();
                Console.WriteLine("Enter the number of general admission tickets you wish to purchase");
                num_gen = Console.ReadLine();
                this.Validate(num_pre,num_gen);
                Console.WriteLine();
                //error checks for no tickets
                if (num_p + num_g == 0)
                {
                    Console.WriteLine("You must purchase some tickets");
                    Console.WriteLine();
                    Console.WriteLine("Please re-enter the ticket amounts");
                }
            }
        
        //validate method error checks for negatives and non whole numbers
        public void Validate(string val1,string val2)
        {
            int temp1 = 0;
            int temp2 = 0;
            while (int.TryParse(val1, out temp1) == false || int.TryParse(val2, out temp2) == false || temp1 < 0 || temp2 < 0 || (temp1 == 0 && temp2 == 0))
            {
                Console.WriteLine();
                if (int.TryParse(val1, out temp1) == false || int.TryParse(val2, out temp2) == false)
                {
                    Console.WriteLine("You can only use positive whole numbers.");
                } else if (temp1 == 0 && temp2 == 0)
                {
                    Console.WriteLine("You must have at least one total ticket. ");
                }
                else
                {
                    Console.WriteLine("You must enter only positive numbers.");
                }
                    
                Console.WriteLine("Enter the number of premium tickets you wish to purchase");
                val1 = Console.ReadLine();
                Console.WriteLine("Enter the number of general admission tickets you wish to purchase");
                val2 = Console.ReadLine();
                
            }
            this.num_p = Convert.ToInt32(val1);
            this.num_g = Convert.ToInt32(val2);

        }
        //performs final calclulations and prints output
        public void ShowPrice()
        {
            Decimal totalTick = num_p + num_g;
            Decimal premiumprice = 75;
            Decimal generalprice = 50;
            Double taxrate = .0875;
            Decimal sub = premiumprice * num_p + generalprice * num_g;
            Decimal prem_sub = premiumprice * num_p;
            Decimal gen_sub = generalprice *  num_g;
            Decimal tax = Convert.ToDecimal(Convert.ToDouble(sub) * taxrate);
            Console.WriteLine(string.Concat("Total Tickets: ", totalTick.ToString()));
            Console.WriteLine(string.Concat("Premium Subtotal : ", prem_sub.ToString("C2")));
            Console.WriteLine(string.Concat("General Admission Subtotal : ", gen_sub.ToString("C2") ));
            Console.WriteLine(string.Concat("Subtotal : ", sub.ToString("C2")));
            Console.WriteLine(string.Concat("Sales Tax : ", tax.ToString("C2")));
            Console.WriteLine(string.Concat("Grand Total : ", (sub + tax).ToString("C2")));
            Console.WriteLine(string.Concat("Premium percentage : ", Math.Round(Convert.ToDouble(num_p/totalTick*100),0).ToString(),"%"));
        }
         
       }

    }


