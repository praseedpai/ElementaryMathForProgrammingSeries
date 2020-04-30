using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Permutation
{
    public class CPermutation
    {
        private int[] rs = null;
        private String _str = "";
        private int _Len;
        private bool first_perm = false;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        public CPermutation(string str)
        {
            _str = str;
            _Len = str.Length;
            rs = new int[_Len + 1];
            rs[0] = -1;
            for (int j = 1; j <= _Len ; ++j)
                rs[j] = j;
            first_perm = false;
        }

        /// <summary>
        /// 
        /// </summary>
        public string NextPerm
        {
            get
            {
                if (first_perm == false)
                {
                    first_perm = true;
                    return _str;
                }
                int k = _Len - 1;

                while (k > 0 && rs[k] > rs[k + 1])
                    k--;

                int left = k + 1;
                int right = _Len;

                while (left < right)
                {
                    int tmp = rs[left];
                    rs[left] = rs[right];
                    rs[right] = tmp;
                    left++;
                    right--;
                }

                if (k == 0)
                    return null;

                int i = k + 1;

                while (rs[i] < rs[k])
                    i++;
                int tmps = rs[i];
                rs[i] = rs[k];
                rs[k] = tmps;

                //---------- Now that we have generated
                //-----------next permutation form the string..

                string ret_str = "";

                for (int tk = 1; tk < _Len+1 ; ++tk)
                {
                    ret_str = ret_str + _str[rs[tk] - 1];
                }

                return ret_str;

            }

        }
        static void Main(string[] args)
        {
            CPermutation cp = new CPermutation("ABCDEFGHIJKLMNOPQRSTUVWXYZ");
           // CPermutation cp = new CPermutation("ABCDEF");
            string str="";

            long nd = 1;
            while ((str = cp.NextPerm) != null)
                Console.WriteLine(str + " " + nd++);

            Console.Read();

        }

    }
}
