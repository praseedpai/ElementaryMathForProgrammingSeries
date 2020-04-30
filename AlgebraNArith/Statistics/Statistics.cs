using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Finance
{
   public class Statistics
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="arr"></param>
        /// <returns></returns>
        public static double Mean(List<double> arr)
        {
            double sum = 0.0;
            foreach(double a in arr )
            {
                sum += a;
            }

            return sum / arr.Count;

        }
       /// <summary>
       /// 
       /// </summary>
       /// <param name="arr"></param>
       /// <returns></returns>
        public static double GMean(List<double> arr)
        {
            double sum = 0.0;
            foreach (double a in arr)
            {
                sum += Math.Log(a);
            }

            return Math.Exp(sum / arr.Count);

        }
       /// <summary>
       /// 
       /// </summary>
       /// <param name="arr"></param>
       /// <returns></returns>
        public static double HMean(List<double> arr)
        {
            double sum = 0.0;
            foreach (double a in arr)
            {
                sum += 1/a;
            }

            return arr.Count/sum;

        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="arr"></param>
        /// <returns></returns>

        public static double STD(List<double> arr)
        {
            double av = Mean(arr);

            double sum = 0.0;
            foreach (double a in arr)
            {
                sum += (a - av )*(a-av);
            }
            return Math.Sqrt(sum / arr.Count);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ds1"></param>
        /// <param name="ds2"></param>
        /// <returns></returns>
        public static double Covariance(List<double> ds1, List<double> ds2)
        {
            if (ds1.Count != ds2.Count)
                return Double.NaN;

            double first_mean  = Mean(ds1);
            double second_mean = Mean(ds2);

            int len = ds1.Count;
            double sum = 0.0;
            for (int i = 0; i < len; i++)
            {
                double dev_first  = ds1[i] - first_mean;
                double dev_second = ds2[i] - second_mean;
                sum += dev_first * dev_second;
            }
            return sum / len;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ds1"></param>
        /// <param name="ds2"></param>
        /// <returns></returns>
        public static double Correlation(List<double> ds1, List<double> ds2)
        {
            if (ds1.Count != ds2.Count)
                return Double.NaN;

            double covariance = Covariance(ds1, ds2);
            double stdx = STD(ds1);
            double stdy = STD(ds2);
            return (covariance / (stdx * stdy));
                       
         }
       /// <summary>
       /// 
       /// </summary>
       /// <param name="scrip"></param>
       /// <param name="bench"></param>
       /// <returns></returns>

        public static double BetaUsingCovariance(List<double> scrip,
                                                  List<double> bench)
        {
            if (scrip.Count != bench.Count)
                return Double.NaN;

            double cov = Covariance(scrip, bench);
            double var = Math.Exp(2*Math.Log(STD(bench)));

            return cov / var;

          
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ds1"></param>
        /// <param name="ds2"></param>
        /// <returns></returns>
 
        public static double ComputeStockParams(
            List<double> ds1,
            List<double> ds2,
            ref double correlation,
            ref double variance_first,
            ref double std_first,
            ref double variance_second,
            ref double std_second,
            ref double alpha,
            ref double beta)
        {

            if (ds1.Count != ds2.Count)
                return Double.NaN;

            double sigma_ysquared = 0;
            double sigma_xsquared = 0;
            double sigma_xy = 0;
            double sigma_x = 0;
            double sigma_y = 0;
            int i = 0;
            int nelem = ds1.Count; 
            for (i = 0; i < nelem; ++i)
            {
                sigma_ysquared += ds2[i] * ds2[i];
                sigma_xsquared += ds1[i] * ds1[i];
                sigma_xy += ds1[i] * ds2[i];
                sigma_x += ds1[i];
                sigma_y += ds2[i];
            }

            double numerator = nelem * sigma_xy - sigma_x * sigma_y;
            double xvar = Math.Sqrt(nelem * sigma_xsquared - sigma_x * sigma_x);
            double yvar = Math.Sqrt(nelem * sigma_ysquared - sigma_y * sigma_y);

            correlation = numerator / (xvar * yvar);
            variance_first = (nelem * sigma_xsquared - sigma_x * sigma_x)/(nelem*nelem);
            std_first = Math.Sqrt(variance_first);
            variance_second = (nelem * sigma_ysquared - sigma_y * sigma_y) / (nelem * nelem);
            std_second = Math.Sqrt(variance_second);

            beta = correlation * std_first * std_second / variance_second;
            alpha = (sigma_x / nelem) - beta * (sigma_y / nelem);
            return 0.0;

            
        }

        /// <summary>
        ///   
        /// </summary>
        /// <param name="lc"></param>
        /// <param name="mean"></param>
        /// <param name="std"></param>
        /// <param name="skew"></param>
        /// <param name="kurt"></param>
        /// <returns></returns>
        public static double Moments(List<double> lc,
            ref double mean,
            ref double std,
            ref double tvar,
            ref double skew,
            ref double kurt)
        {
            double sum = 0.0;
            int len = lc.Count;
            foreach (double t in lc)
            {
                sum += t;
            }

            mean = sum / len;

            double[] V = new double[4];
            int i = 0;
            int k = 0;
            for (i = 0; i < 4; ++i)
            {
              V[i] = 0.0;
              for (k = 0; k < len; ++k)
              {

                  double dev = lc[k] - mean;
                  V[i] += Math.Pow(dev, i+1);
              }
              V[i] = V[i] / len;
            }

            std =Math.Sqrt(V[1]);
            tvar = V[1];
            skew = V[2] /  Math.Pow(V[1]*V[1]*V[1],0.5);
            kurt = V[3] / Math.Pow(V[1], 2);

            return 0.0;

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Y"></param>
        /// <param name="X"></param>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static double LinearRegression(List<double> Y,
            List<double> X,
            ref double a,
            ref double b)
        {
            double sigma_x=0.0;
            double sigma_y=0.0;
            double sigma_xsquared=0.0;
            double sigma_xy=0.0;
            if (Y.Count != X.Count)
                return Double.NaN;

            int len = X.Count; 
            for (int i = 0; i < len; ++i)
            {
                sigma_x += X[i];
                sigma_y += Y[i];
                sigma_xsquared += X[i] * X[i];
                sigma_xy += X[i] * Y[i];
            }
            b = (len*sigma_xy - sigma_x*sigma_y)/((len*sigma_xsquared) - sigma_x*sigma_x );
            a = (sigma_y / len) - (b * sigma_x / len);

            return 0.0;
        }
        /// <summary>
        ///    
        /// </summary>
        /// <param name="Y"></param>
        /// <param name="X"></param>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static double ExponentialFit(List<double> Y,
            List<double> X, ref double a, ref double b)
        {

            double sigma_x = 0.0;
            double sigma_y = 0.0;
            double sigma_xsquared = 0.0;
            double sigma_xy = 0.0;
            if (Y.Count != X.Count) 
                return Double.NaN;

            int len = X.Count;
            for (int i = 0; i < len; ++i)
            {
                sigma_x += Math.Log(X[i]);
                sigma_y += Math.Log(Y[i]);
                sigma_xsquared += Math.Log(X[i]) * Math.Log(X[i]);
                sigma_xy += Math.Log(X[i])*Math.Log(Y[i]) ;
            }
            b = (len * sigma_xy - sigma_x * sigma_y) / ((len * sigma_xsquared) - sigma_x * sigma_x);
            double R = (sigma_y - b * sigma_x) / len;
            a = Math.Exp(R);
            return 0.0;

        }





    }
}
