using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Finance
{
    public class PV
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="present_value"></param>
        /// <param name="rate"></param>
        /// <param name="period"></param>
        /// <returns></returns>
        public static double FutureValue(double present_value, double rate, double period)
        {
            return present_value * Math.Exp(rate * period);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="arr"></param>
        /// <param name="rate"></param>
        /// <param name="period"></param>
        /// <returns></returns>
        public static double
        CashFlowPVDiscreteAnnual(List<double> arr,
                    double rate,
                    double period)
        {

            int len = arr.Count;
            double PV = 0.0;
            for (int i = 0; i < len; ++i)
            {
                PV += arr[i] / Math.Pow((1+rate/100), i);
            }

            return PV;

        }


       public static double
       CashFlow_IRR_Annual(List<double> arr,
                   double rate,
                   double period)
        {

           const double ACCURACY = 1.0e-5;
           const int MAX_ITERATIONS = 50;
           const double ERROR = -1e30;
           double x1 = 0.0;
           double x2 = 20.0;
           // create an initial bracket, 
           // with a root somewhere between bot,top
           double f1 = CashFlowPVDiscreteAnnual(arr,x1,period);
           double f2 = CashFlowPVDiscreteAnnual(arr,x2,period);

           for (int j = 0; j < MAX_ITERATIONS; ++j)
           {
               if ( (f1*f2) < 0.0) { 
                   break; 
               }
               if (Math.Abs(f1) < Math.Abs(f2))
               {
                  f1 = CashFlowPVDiscreteAnnual(arr,x1+= 1.06*(x1-x2),period );
               }
               else {
                  f2 = CashFlowPVDiscreteAnnual(arr,x2+=1.06*(x2-x1),period);
               }
           }

           if (f2*f1>0.0) 
           { 
               return ERROR; 
           }
           double f = CashFlowPVDiscreteAnnual(arr,x1,period);
           double rtb;
           double dx=0;
           if (f<0.0) 
           {
              rtb = x1;
              dx=x2-x1;
           }
           else {
              rtb = x2;
              dx = x1-x2;
           }

           for (int i=0;i<MAX_ITERATIONS;i++)
           {
                  dx *= 0.5;
                  double x_mid = rtb+dx;
                  double f_mid = CashFlowPVDiscreteAnnual(arr,x_mid,period);
                  if (f_mid<=0.0) { rtb = x_mid; }
                  if ( (Math.Abs(f_mid)<ACCURACY) || (Math.Abs(dx)<ACCURACY) ) 
                      return x_mid;
            }
            return ERROR; // error.

        }



    }

    public class EntryPoint {

       public static void Main(String [] args ) {
            List<double> returns = new List<double>();
            returns.Add(-100);
            returns.Add( 60 );
            returns.Add( 60 );
            double dcfactor =  PV.CashFlow_IRR_Annual(returns,0,2);
            Console.WriteLine(dcfactor);
            List<double> returns2 = new List<double>();
            returns2.Add(-123400);
            returns2.Add( 36200 );
            returns2.Add( 54800 );
            returns2.Add( 48100 );
            double dcfactor2 =  PV.CashFlow_IRR_Annual(returns2,0,3);
            Console.WriteLine(dcfactor2);
       }

    }
}
