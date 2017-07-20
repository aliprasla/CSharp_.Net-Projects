using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prasla_Ali_HW0
{
    class Program
    {
        static void Main(string[] args)
        {
            //This is the main method
            Console.WriteLine("Hello World");
            //Code to keep console open
            Test test = new Test();
            Console.WriteLine(Convert.ToString(test.val));
            test.change(10.13m);
            Console.WriteLine(Convert.ToString(test.val));
            Console.WriteLine("Press any key");
            Console.ReadLine();            
        }
    }//End of Class

    /*
    Comment Chunk. Same as Python
    
    */
    public class Test {
       public Decimal val = 100.32m;
        public void change(Decimal whatchange) {
            Test.val = whatchange;
        }
    }
}
