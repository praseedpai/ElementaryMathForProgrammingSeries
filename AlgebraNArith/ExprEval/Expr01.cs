using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SLANG_DOT_NET{   
    
    /// <summary>
    ///      One can store the stack frame inside this class
    /// </summary>
    public class RUNTIME_CONTEXT{
        public RUNTIME_CONTEXT() {}
    }
    /// <summary>
    ///    Enumeration for operators
    /// </summary>
    public enum OPERATOR
    {
        ILLEGAL = -1,PLUS,MINUS, DIV,MUL
    }
    /// <summary>
    ///     Expression is what you evaluates for it's Value
    /// </summary>
    /// 
    public abstract class Exp {
        public abstract double Evaluate(RUNTIME_CONTEXT cont);
    }
    /// <summary>
    ///      one can store number inside the class
    /// </summary>
   public class NumericConstant : Exp {
        private double _value;
        ///     Construction does not do much , just keeps the 
        ///     value assigned to the private variable
        public NumericConstant(double value){_value = value;}
        /// <summary>
        ///     While evaluating a numeric constant , return the _value
        /// </summary>
        /// <param name="cont"></param>
        /// <returns></returns>
        public override double Evaluate(RUNTIME_CONTEXT cont) { return _value;}

    }

    /// <summary>
    ///     This class supports Binary Operators like + , - , / , *
    /// </summary>
    public class BinaryExp : Exp
    {
        private Exp _ex1, _ex2;
        private OPERATOR _op;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="op"></param>

        public BinaryExp(Exp a, Exp b, OPERATOR op)
        {
            _ex1 = a;
            _ex2 = b;
            _op = op;
        }

        /// <summary>
        ///     While evaluating a numeric constant , return the _value
        /// </summary>
        /// <param name="cont"></param>
        /// <returns></returns>
        public override double Evaluate(RUNTIME_CONTEXT cont)
        {


            switch (_op)
            {
                case OPERATOR.PLUS:
                    return _ex1.Evaluate(cont) + _ex2.Evaluate(cont);
                case OPERATOR.MINUS:
                    return _ex1.Evaluate(cont) - _ex2.Evaluate(cont);
                case OPERATOR.DIV:
                    return _ex1.Evaluate(cont) / _ex2.Evaluate(cont);
                case OPERATOR.MUL:
                    return _ex1.Evaluate(cont) * _ex2.Evaluate(cont);

            }

            return Double.NaN;

        }

    }

    /// <summary>
    ///     This class supports Unary Operators like + , - , / , *
    /// </summary>
    public class UnaryExp : Exp
    {
        private Exp _ex1;
        private OPERATOR _op;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="op"></param>

        public UnaryExp(Exp a, OPERATOR op)
        {
            _ex1 = a;
            _op = op;
        }

        /// <summary>
        ///     While evaluating a numeric constant , return the _value
        /// </summary>
        /// <param name="cont"></param>
        /// <returns></returns>
        public override double Evaluate(RUNTIME_CONTEXT cont)
        {

            switch (_op)
            {
                case OPERATOR.PLUS:
                    return _ex1.Evaluate(cont);
                case OPERATOR.MINUS:
                    return -_ex1.Evaluate(cont);
            }

            return Double.NaN;

        }

    }


  class Program
    {
        static void Main(string[] args)
        {
            // Abstract Syntax Tree (AST) for 5*10
            Exp e = new BinaryExp(new NumericConstant(5),
                                   new NumericConstant(10),
                                   OPERATOR.MUL);

            //
            // Evaluate the Expression
            //
            //
            Console.WriteLine(e.Evaluate(null));

            // AST for  -(10 + (30 + 50 ) )

            e = new UnaryExp(
                         new BinaryExp(new NumericConstant(10),
                             new BinaryExp(new NumericConstant(30),
                                           new NumericConstant(50),
                                  OPERATOR.PLUS),
                         OPERATOR.PLUS),
                     OPERATOR.MINUS);

            //
            // Evaluate the Expression
            //
            Console.WriteLine(e.Evaluate(null));

            //
            // Pause for a key stroke
            //
            Console.Read();

        }
    }

}
