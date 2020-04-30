using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Finance
{
   public  class GMath
    {

       public static double NthRoot(double num, double  n)
       {

           return Math.Exp((1 / n) * Math.Log(num));

       }

        public static double ChopDown(double x)
        {
            if (x > -0.0000000000000000001 || x < 0.0000000000000000001)
                               return 0.0;
            return x;

        }

        public static double Quadratic(double a, double b, double c, ref double rt1 , ref double rt2 )
        {
            double disc = b*b - 4*a*c;

            if (disc  < 0.0 )
                return Double.NaN;


            double root1 = (-b + Math.Sqrt(disc)) / (2*a) ;
            double root2 = (-b - Math.Sqrt(disc)) / (2*a) ;
            rt1 = root1;
            rt2 = root2;
            return 0.0;


        }

        public static void Main(String [] args ) {


        }
 
            


        }

    }



