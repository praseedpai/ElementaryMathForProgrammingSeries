using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BarCamp2009
{
    public class SubSet
    {
        private string _str;
        private int _length;
        private  ulong _count ;
        private ulong _max;

        public SubSet( string str)
        {
            _str = str;
            _length = _str.Length;
            _count = 0;
            _max =(ulong) Math.Pow(2, _length);
        }

       public string Next
       {
           get {

               
               if (_count == _max)
               {
                   return null;
               }
               uint rs = 0;
               string retstr = "";
               while (rs < _length)
               {
                   if ((_count & (1u << (int)rs)) >  0)
                   {
                       retstr = retstr + _str[(int)rs];
                   }
                   rs++;
               }
               _count++;
               return retstr;

           }
       }
 static void Main(string[] args)
        {
           // SubSet sb = new SubSet("Thequickbrownfoxjumpsoverthelazydog");
            SubSet sb = new SubSet("ABC"); 
            string s = null;
            while ((s = sb.Next) != null)
            {
                Console.WriteLine(s);
                

                

            }

            ulong rs =(ulong)Math.Pow(2, 35) ;
            Console.WriteLine(rs / (60 * 60 * 24*1000));



            Console.Read();
        }

    }
}
