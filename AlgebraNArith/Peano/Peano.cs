using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Curry
{
    class Program
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            ///
            // A Lamda Expression which takes two doubles as parameter
            //
            Func<int, int , int> adder = (a, b) => a + b;
           
            ///
            /// Successor function....
            ///
            Func<int, int> succ = (c) => adder(c, 1);
            //
            // Predecessor function....
            //
            Func<int, int> pred = (c) => adder(c, -1);
            ///
            ///
            /// Mul function defined in term of addition....
            /// 
            Func<int , int, int > mul = null;
            mul = (a, b) => b == 0 ? 0 : adder(mul(a, b - 1), a);
          
            Console.WriteLine(mul(6,2));                    // displays 8
           

            //
            // Invoke the arity reduced function... 
            //
            Console.WriteLine(succ(10));
            Console.WriteLine(pred(21));
            Console.WriteLine(mul(17, 17));
            Console.Read();



        }
    }
}
