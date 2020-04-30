////////////////////////////////////////////////////
//
// The C# code is adapted ( copied! ) from Mark Seaman's blog
// https://github.com/ploeh/ChurchEncoding/tree/8d1e7501f486351e748646c915f0bd334332e386
//
//

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace KJUGMathematics {
    //-------------- Interface for Natural Number
    public interface INaturalNumber {
	T Match<T>(T zero, Func<INaturalNumber, T> succ);
    }
    //-------------- Interface for Boolean Value
    public interface IChurchBoolean {
	T Match<T>(T trueCase, T falseCase);
    }
    //--------------------- Definition of Zero
    public class Zero : INaturalNumber  {
        public T Match<T>(T zero, Func<INaturalNumber, T> succ) { return zero;}
    }
    //--------------- Successor Function
    public class Successor : INaturalNumber {
        private readonly INaturalNumber predecessor;
        public Successor(INaturalNumber n) { predecessor = n; }
        public T Match<T>(T zero, Func<INaturalNumber, T> succ)  { return succ(predecessor);}
    }
    //------------------- Defintion of True amd False
    public class ChurchTrue : IChurchBoolean {
         public T Match<T>(T trueCase, T falseCase) { return trueCase; }
    }
    public class ChurchFalse : IChurchBoolean {
	 public T Match<T>(T trueCase, T falseCase) {  return falseCase;}
    }
    //------------------------- Boolean  And 
    public class ChurchAnd : IChurchBoolean {
        private readonly IChurchBoolean x;
        private readonly IChurchBoolean y;
        public ChurchAnd(IChurchBoolean x, IChurchBoolean y) {
              this.x = x; this.y = y;
        }
        public T Match<T>(T trueCase, T falseCase) {
              return x.Match(y.Match(trueCase, falseCase), falseCase);
        }
     }
     //-------------------------- Boolean Or
     public class ChurchOr : IChurchBoolean {
        private readonly IChurchBoolean x;
        private readonly IChurchBoolean y;
        public ChurchOr(IChurchBoolean x, IChurchBoolean y) {
               this.x = x; this.y = y;
        }
        public T Match<T>(T trueCase, T falseCase) {
               return x.Match(trueCase, y.Match(trueCase, falseCase));
        }
     }
     //------------------------------- Boolean Not
     public class ChurchNot : IChurchBoolean {
        private readonly IChurchBoolean b;
        public ChurchNot(IChurchBoolean b) { this.b = b; }
        public T Match<T>(T trueCase, T falseCase) {
               return b.Match(falseCase, trueCase);
        }
     }

     //------------------ Extension Methods for Simplifying Boolean Expression
     public static class ChurchBoolean {
        public static bool ToBool(this IChurchBoolean b) {
               return b.Match(true, false);
        }
        public static IChurchBoolean ToChurchBoolean(this bool b) {
               if ( b ) 
                    return   new ChurchTrue();
               else
                    return   new ChurchFalse();
        }
     }

    public static class NaturalNumber {

        public static INaturalNumber  Zero = new Zero();
        public static INaturalNumber   One = new Successor(Zero);
        public static INaturalNumber   Two = new Successor(One);
        public static INaturalNumber Three = new Successor(Two);
        public static INaturalNumber  Four = new Successor(Three);
        public static INaturalNumber  Five = new Successor(Four);
        public static INaturalNumber   Six = new Successor(Five);
        public static INaturalNumber Seven = new Successor(Six);
        public static INaturalNumber Eight = new Successor(Seven);
        public static INaturalNumber  Nine = new Successor(Eight);
	public static INaturalNumber  Ten =  new Successor(Nine);
        public static INaturalNumber  Eleven = new Successor(Ten);
        public static INaturalNumber  Twelve = new Successor(Eleven);
        public static INaturalNumber Thirteen = new Successor(Twelve);
        public static INaturalNumber  Fourteen = new Successor(Thirteen);
        public static INaturalNumber  Fifteen = new Successor(Fourteen);
        public static INaturalNumber   Sixteen = new Successor(Fifteen);
        public static INaturalNumber Seventeen = new Successor(Sixteen);
        public static INaturalNumber Eighteen = new Successor(Seventeen);
        public static INaturalNumber  Nineteen = new Successor(Eighteen);


        // More memmbers go here...



        public static int Count(this INaturalNumber n) {
               return n.Match( zero: 0, succ: p => 1 + p.Count());
        }

        public static INaturalNumber Add(this INaturalNumber x,INaturalNumber y) {
	       return x.Match(zero: y, succ: p => new Successor(p.Add(y)));
        }



        // The formula used here is

        // x * y = 1 + (x - 1) + (y - 1) + ((x - 1) * (y - 1))

        // It follows like this:

        // x* y =

        // (x - 1 + 1) * (y - 1 + 1) =

        // ((x - 1) + 1) * ((y - 1) + 1) =

        // ((x - 1) * (y - 1)) + ((x - 1) * 1) + ((y - 1) * 1) + 1 * 1 =

        // ((x - 1) * (y - 1)) + (x - 1) + (y - 1) + 1

        public static INaturalNumber Multiply(

            this INaturalNumber x,

            INaturalNumber y)

        {

            return x.Match(  zero: new Zero(),

                succ: px => y.Match(

                    zero: new Zero(),

                    succ: py =>

                        One

                        .Add(px)

                        .Add(py)

                        .Add(px.Multiply(py))));

        }



        public static IChurchBoolean IsZero(this INaturalNumber n) {

            return n.Match<IChurchBoolean>( zero: new ChurchTrue(), succ: _ => new ChurchFalse());
        }



        public static IChurchBoolean IsEven(this INaturalNumber n)

        {

            return n.Match(

                zero: new ChurchTrue(),        // 0 is even, so true

                succ: p1 => p1.Match(          // Match previous

                    zero: new ChurchFalse(),   // If 0 then successor was 1

                    succ: p2 => p2.IsEven())); // Eval previous' previous

        }



        public static IChurchBoolean IsOdd(this INaturalNumber n) {
                          return new ChurchNot(n.IsEven());
        }

    }
    public class Test { 
   	 public static void Main(string[] args) { 
               var two = new Successor(new Successor(new Zero()));
               var three =
                new Successor(new Successor(new Successor(new Zero())));
               var actual = two.Add(three);
               Console.WriteLine(actual.Count());

               var one = new Successor(new Zero());
               two = new Successor(new Successor(new Zero()));
               three =
                new Successor(new Successor(new Successor(new Zero())));
                actual = one.Add(two).Add(three);


               Console.WriteLine(actual.Count());

               actual = one.Multiply(two).Multiply(three);

               Console.WriteLine( actual.Count() );
         }
    }

}