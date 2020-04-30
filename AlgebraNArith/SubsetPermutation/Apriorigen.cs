using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apriori
{
 public class Gaussian
    {
        private bool _available;
        private double _nextGauss;
        private Random _rng;

        public Gaussian()
        {
            _rng = new Random();
        }

        public double RandomGauss()
        {
            if (_available)
            {
                _available = false;
                return _nextGauss;
            }

            double u1 = _rng.NextDouble();
            double u2 = _rng.NextDouble();
            double temp1 = Math.Sqrt(-2.0 * Math.Log(u1));
            double temp2 = 2.0 * Math.PI * u2;

            _nextGauss = temp1 * Math.Sin(temp2);
            _available = true;
            return temp1 * Math.Cos(temp2);
        }

        public double RandomGauss(double mu, double sigma)
        {
            return mu + sigma * RandomGauss();
        }

        public double RandomGauss(double sigma)
        {
            return sigma * RandomGauss();
        }
    }

      public class SubSet
    {
        private List<string> items_list = new List<string>();
        private List<List<string>> trans_list = new List<List<string>>();
        private int _length;
        private ulong _count;
        private ulong _max;

        public SubSet(List<string> itemlist)
        {
            items_list = itemlist;
            _length = itemlist.Count;
            _count = 0;
            _max = (ulong)Math.Pow(2, _length);
        }

        public List<List<string>> GetDataSet()
        {
            return this.trans_list;
        }
       
        public void Generate()
        {
           
            for (int i = 0; i < (int) _max -1; ++i)
            {
                
                uint rs = 0;
                string retstr = "";
                List<string> combination = new List<string>();
                while (rs < _length)
                {
                    if ((i & (1u << (int)rs)) > 0)
                    {
                        combination.Add(items_list[(int)rs]);
                    }
                    rs++;
                }
                combination.Sort();
                trans_list.Add(combination);

            }
            trans_list = trans_list.OrderBy(par => par.Count).ToList();

            for (int i = 0; i < (int) _max-1; ++i)
            {
                List<string> nt = trans_list[i];
                for (int j = 0; j < nt.Count; ++j)
                {
                    Console.Write(nt[j] + ((j == (nt.Count - 1)) ? "  " : " ,"));
                }
                Console.WriteLine();
            }

        }





    }
    public class ItemSetGen
    {
        private List<string> items_list = new List<string>();
        private List<List<string>> trans_list = new List<List<string>>();
        private int _length;
        private ulong _count;
        private ulong _max;

        public ItemSetGen(List<string> itemlist)
        {
            items_list = itemlist;
            _length = itemlist.Count;
            _count = 0;
            _max = (ulong)Math.Pow(2, _length);
        }

        public List<List<string>> GetDataSet()
        {
            return this.trans_list;
        }

        public double GetRandomNumber(Random rand, double minimum, double maximum)
        {
            // Random random = new Random(DateTime.Now.Millisecond);
            return rand.NextDouble() * (maximum - minimum) + minimum;

        }

        public void PrintRandomPermutation()
        {
            Gaussian guass = new Gaussian();
            ulong mu = _max / 2;
            ulong sd = _max / 16;
            Random ra = new Random();
            for (int i = 0; i < 8096; ++i)
            {
                if (i % 10 == 0)
                    ra = new Random(DateTime.Now.Millisecond);

                double d = GetRandomNumber(ra, 0, _max);
                ulong n = (ulong)Math.Abs(d);

                uint rs = 0;
                string retstr = "";
                List<string> basket = new List<string>();
                while (rs < _length)
                {
                    if ((n & (1u << (int)rs)) > 0)
                    {
                        basket.Add(items_list[(int)rs]);
                    }
                    rs++;
                }
                basket.Sort();
                trans_list.Add(basket);

            }
            trans_list = trans_list.OrderBy(par => par.Count).ToList();

            for (int i = 0; i < 8096; ++i)
            {
                List<string> nt = trans_list[i];
                for (int j = 0; j < nt.Count; ++j)
                {
                    Console.Write(nt[j] + ((j == (nt.Count - 1)) ? "  " : " ,"));
                }
                Console.WriteLine();
            }

        }





    }


    class TestStub
    {

        public static void SubsetTest()
        {
            List<string> items = new List<string>();

            items.Add("Banana");
            items.Add("Orange");
            items.Add("Coffee");
            items.Add("Sugar");
            items.Add("Milk");
            items.Add("Apple");
            items.Add("SoyaBean");
            items.Add("CocunutOil");

            ItemSetGen sb = new ItemSetGen(items); 
            sb.PrintRandomPermutation();
           // Console.Read();
          //  List<List<String>> trans = sb.GetDataSet();

            SubSet sb2 = new SubSet(items);
            sb2.Generate();
           // List<List<String>> comb = sb2.GetDataSet();
           // AprioriLoop aploop = new AprioriLoop(items,trans, comb);
           // aploop.PrintRules();
            Console.WriteLine();
        }

        public static void Main(String[] args)
        {
            SubsetTest();
            Console.Read();
        }

    }
}

    

