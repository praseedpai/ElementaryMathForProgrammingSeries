//////////////////////////////////////////////////////////////////////
//
// 
//
//
//
// BNF Grammar for Evaluator
//
//
// <expr> ::= <term> | <term> { '+' | '-' } <expr>
//
// <term> ::= <factor> | <factor> { '*' | '/' } <term>
//
// <factor> ::= <TOK_DOUBLE> | ( <expr> ) | { + | - } <factor>
//
//
//
//
//
//

using System;
using System.Reflection;
using System.Reflection.Emit;
using System.Threading;
using System.Collections.Generic;
///////////////////////////////////////////////////////////////////////////////
//
//
// enum TOKEN - Symbolic constants for Lexical Tokens
//
// class Lexer - Lexical Analyzer module. ( proof of concept endeavour )
//
// class CLRParser - A recursive descent parser 

namespace ExpressionEvaluator
{

   public enum OPERATOR
    {
        //--- supports +,-,*/
       ILLEGAL = -1, PLUS, MINUS, MUL, DIV
    }

    public enum ExprKind
    {
        OPERATOR , VALUE
    }

    public class ITEM_LIST
    {
        public ITEM_LIST()
        {
            op = OPERATOR.ILLEGAL;
        }
       public bool SetOperator( OPERATOR op )
       {
            this.op = op;
            this.knd = ExprKind.OPERATOR;
            return true;
       }

        public bool SetValue(double value)
        {
            this.knd = ExprKind.VALUE;
            this.Value = value;
            return true;
        }

       public ExprKind knd;
       public double Value;
       public OPERATOR op;


    }
    /// <summary>
    /// Base class for all Expression
    /// supports accept method to implement 
    /// so called "Double Dispatch". Search for 
    /// "Double Dispatch" and Visitor to understand more 
    /// about this strategy
    /// </summary>
    public abstract class Expr
    {
        public abstract double accept(IExprVisitor expr_vis);
    }
    /// <summary>
    ///  Our Visitor Interface. The Purpose of seperating Processing
    ///  Of Nodes and Data Storage (heirarchy) is for various transformations on
    ///  the composites created.
    /// </summary>
    public interface IExprVisitor
    {
        double Visit(Number num);
        double Visit(BinaryExpr bin);
        double Visit(UnaryExpr un);
    }

    /// <summary>
    ///  Class Number stores a IEEE 754 doouble precision
    ///  floating point
    /// </summary>
    public class Number : Expr
    {
        public double NUM { get; set; }
        public Number(double n) { this.NUM = n; }
        public override double accept(IExprVisitor expr_vis)
        {
            return expr_vis.Visit(this);
        }
    }
    /// <summary>
    ///  Class BinaryExpr models a binary expression of 
    ///  the form <Operand> <OPER> <Operand>
    /// </summary>
    public class BinaryExpr : Expr
    {
        public Expr Left { get; set; }
        public Expr Right { get; set; }
        public OPERATOR OP { get; set; }
        public BinaryExpr(Expr l, Expr r, OPERATOR op)
        {
            Left = l; Right = r; OP = op;
        }
        public override double accept(IExprVisitor expr_vis)
        {
            return expr_vis.Visit(this);
        }

    }
    /// <summary>
    /// Class UnaryExpr models a unary expression of the form
    /// <OPER> <OPERAND> 
    /// </summary>
    public class UnaryExpr : Expr
    {
        public Expr Right;
        public OPERATOR OP;

        public UnaryExpr(Expr r, OPERATOR op)
        {
            Right = new BinaryExpr(new Number(0),
                r, op);
        }
        public override double accept(IExprVisitor expr_vis)
        {
            return expr_vis.Visit(this);
        }
    }
    /// <summary>
    ///  Tree Evaluator - Evaluates Expression Tree through Depth 
    ///  First Traversal
    /// </summary>
    public class TreeEvaluatorVisitor : IExprVisitor
    {

        public double Visit(Number num)
        {
            return num.NUM;
        }
        public double Visit(BinaryExpr bin)
        {
            if (bin.OP == OPERATOR.PLUS)
                return bin.Left.accept(this) + bin.Right.accept(this);
            else if (bin.OP == OPERATOR.MUL)
                return bin.Left.accept(this) * bin.Right.accept(this);
            else if (bin.OP == OPERATOR.DIV)
                return bin.Left.accept(this) / bin.Right.accept(this);
            else if (bin.OP == OPERATOR.MINUS)
                return bin.Left.accept(this) - bin.Right.accept(this);

            return Double.NaN;

        }
        public double Visit(UnaryExpr un)

        {
            if (un.OP == OPERATOR.PLUS)
                return +un.Right.accept(this);
            else if (un.OP == OPERATOR.MINUS)
                return -un.Right.accept(this);
            return Double.NaN;

        }

    }
    /// <summary>
    ///  A Visitor implementation which converts Infix expression to 
    ///  a Reverse Polish Notation ( RPN) 
    /// </summary>
    public class ReversePolishEvaluator : IExprVisitor
    {

        public double Visit(Number num)
        {
            Console.Write(num.NUM + " ");
            return 0;
        }
        public double Visit(BinaryExpr bin)
        {
            bin.Left.accept(this);
            bin.Right.accept(this);

            if (bin.OP == OPERATOR.PLUS)
                Console.Write(" + ");
            else if (bin.OP == OPERATOR.MUL)
                Console.Write(" * ");
            else if (bin.OP == OPERATOR.DIV)
                Console.Write(" / ");
            else if (bin.OP == OPERATOR.MINUS)
                Console.Write(" - ");

            return Double.NaN;

        }
        public double Visit(UnaryExpr un)
        {

            un.Right.accept(this);
            if (un.OP == OPERATOR.PLUS)
                Console.Write("  + ");
            else if (un.OP == OPERATOR.MINUS)
                Console.Write("  - ");
            return Double.NaN;

        }

    }
    /// <summary>
    ///  A Visitor which evaluates the Infix expression using a Stack
    ///  We will leverage stack implementation available with .NET 
    ///  collections API
    /// </summary>
    public class StackEvaluator : IExprVisitor
    {
        private Stack<double> eval_stack = new Stack<double>();

        public double get_value() { return eval_stack.Pop(); }
        public StackEvaluator()
        {
            eval_stack.Clear();
        }
        public double Visit(Number num)
        {
            eval_stack.Push(num.NUM);
            return 0;
        }
        public double Visit(BinaryExpr bin)
        {
            bin.Left.accept(this);
            bin.Right.accept(this);

            if (bin.OP == OPERATOR.PLUS)
                eval_stack.Push(eval_stack.Pop() + eval_stack.Pop());
            else if (bin.OP == OPERATOR.MUL)
                eval_stack.Push(eval_stack.Pop() * eval_stack.Pop());
            else if (bin.OP == OPERATOR.DIV)
                eval_stack.Push(eval_stack.Pop() / eval_stack.Pop());
            else if (bin.OP == OPERATOR.MINUS)
                eval_stack.Push(eval_stack.Pop() - eval_stack.Pop());

            return Double.NaN;

        }
        public double Visit(UnaryExpr un)
        {

            un.Right.accept(this);
            if (un.OP == OPERATOR.PLUS)
                eval_stack.Push(eval_stack.Pop());
            else if (un.OP == OPERATOR.MINUS)
                eval_stack.Push(-eval_stack.Pop());
            return Double.NaN;

        }

    }


    public class FlattenVisitor : IExprVisitor
    {
        List<ITEM_LIST> ils = null;

        private ITEM_LIST MakeListItem(double num)
        {
            ITEM_LIST temp = new ITEM_LIST();
            temp.SetValue(num);
    
            return temp;
        }

        private ITEM_LIST MakeListItem(OPERATOR op)
        {
            ITEM_LIST temp = new ITEM_LIST();
            temp.SetOperator(op);

            return temp;
        }

        public List<ITEM_LIST> FlattenedExpr()
        {
            return ils;
        }
        public FlattenVisitor()
        {
            ils = new List<ITEM_LIST>();
        }
        public double Visit(Number num)
        {
        
            ils.Add(MakeListItem(num.NUM));
            return 0;
        }
        public double Visit(BinaryExpr bin)
        {
            bin.Left.accept(this);
            bin.Right.accept(this);

               ils.Add(MakeListItem(bin.OP));
            

            return Double.NaN;

        }
        public double Visit(UnaryExpr un)
        {

            un.Right.accept(this);
           // ils.Add(MakeListItem(un.OP));
            return 0;
          

        }

    }



    public class FlattenVisitor2 : IExprVisitor
    {
        List<ITEM_LIST> ils = null;

        private ITEM_LIST MakeListItem(double num)
        {
            ITEM_LIST temp = new ITEM_LIST();
            temp.SetValue(num);
    
            return temp;
        }

        private ITEM_LIST MakeListItem(OPERATOR op)
        {
            ITEM_LIST temp = new ITEM_LIST();
            temp.SetOperator(op);

            return temp;
        }

        public List<ITEM_LIST> FlattenedExpr()
        {
            return ils;
        }
        public FlattenVisitor2()
        {
            ils = new List<ITEM_LIST>();
        }
        public double Visit(Number num)
        {
        
            ils.Add(MakeListItem(num.NUM));
            Console.Write( "  " + num.NUM + " " );
            return 0;
        }
        public double Visit(BinaryExpr bin)
        {
            ils.Add(MakeListItem(bin.OP));
            if (bin.OP == OPERATOR.PLUS)
               Console.Write( "(" + "add " );
            else if (bin.OP == OPERATOR.MUL)
                 Console.Write( "(" + "mul " );
            else if (bin.OP == OPERATOR.DIV)
                Console.Write( "(" + "div " );
            else if (bin.OP == OPERATOR.MINUS)
                Console.Write( "(" + "minus " );

            
          
            bin.Left.accept(this);
            bin.Right.accept(this);

            Console.Write( " ) " );  
            

            return Double.NaN;

        }
        public double Visit(UnaryExpr un)
        {
            ils.Add(MakeListItem(un.OP));
            Console.Write( "( " + ( ( un.OP == OPERATOR.MUL ) ? "mul " : "add "));
            un.Right.accept(this);
            Console.Write( " ) " );
            return 0;
          

        }

    }
  ////////////////////////////////////////////////
  //
  // Lexical Tokens
  //
  //
  //
  public enum TOKEN
  {  
    ILLEGAL_TOKEN=-1, // Not a Token
    TOK_PLUS=1, // '+'
    TOK_MUL, // '*'
    TOK_DIV, // '/'
    TOK_SUB, // '-'
    TOK_OPAREN, // '('
    TOK_CPAREN, // ')'
    TOK_DOUBLE, // '('
    TOK_NULL // End of string
  }
  //////////////////////////////////////////////////////////
  //
  // A naive Lexical analyzer which looks for operators , Parenthesis
  // and number. All numbers are treated as IEEE doubles. Only numbers
  // without decimals can be entered. Feel free to modify the code
  // to accomodate LONG and Double values
  public class Lexer
  {
    String IExpr; // Expression string
    int index ; // index into a character
    int length; // Length of the string
    double number; // Last grabbed number from the stream
    /////////////////////////////////////////////
    //
    // Ctor
    //
    //
    public Lexer(String Expr)
    {
      IExpr = Expr;
      length = IExpr.Length;
      index = 0;
    }
    /////////////////////////////////////////////////////
    // Grab the next token from the stream
    //
    //
    //
    //
    public TOKEN GetToken()
    {
      TOKEN tok = TOKEN.ILLEGAL_TOKEN;
      ////////////////////////////////////////////////////////////
      //
      // Skip the white space
      //
      while (index < length &&
      (IExpr[index] == ' ' || IExpr[index]== '\t') )
        index++;
      //////////////////////////////////////////////
      //
      // End of string ? return NULL;
      //
      if ( index == length)
        return TOKEN.TOK_NULL;
      /////////////////////////////////////////////////
      //
      //
      //
      switch(IExpr[index])
      {
        case '+':
          tok = TOKEN.TOK_PLUS;
          index++;
          break;
        case '-':
          tok = TOKEN.TOK_SUB;
          index++;
          break;
        case '/':
          tok = TOKEN.TOK_DIV;
          index++;
          break;
        case '*':
          tok = TOKEN.TOK_MUL;
          index++;
          break;
        case '(':
          tok = TOKEN.TOK_OPAREN;
          index++;
          break;
        case ')':
          tok = TOKEN.TOK_CPAREN;
          index++;
          break;
        case '0':
        case '1':
        case '2':
        case '3':
        case '4':
        case '5':
        case '6':
        case '7':
        case '8':
        case '9':
        {
          String str="";
          while ( index < length &&
          ( IExpr[index]== '0' ||
          IExpr[index]== '1' ||
          IExpr[index]== '2' ||
          IExpr[index]== '3' ||
          IExpr[index]== '4' ||
          IExpr[index]== '5' ||
          IExpr[index]== '6' ||
          IExpr[index]== '7' ||
          IExpr[index] == '8'||
          IExpr[index]== '9' ))
          {
            str += Convert.ToString(IExpr[index]);
            index ++;
          }
          number = Convert.ToDouble(str);
          tok = TOKEN.TOK_DOUBLE;
        }
        break;
        default:
          Console.WriteLine("Error While Analyzing Tokens");
          throw new Exception();
      }
    return tok;
    }


    public double GetNumber() { return number; }
}


public static class Extensions {
        public static Expr ParseOne()
        {
            Expr r = new BinaryExpr(new Number(2),
               new BinaryExpr(new Number(3), new Number(4), OPERATOR.MUL),
               OPERATOR.PLUS);
            return r;
        }

        public static Expr ParseTwo()
        {
            Expr r = new BinaryExpr(new Number(3), new Number(4), OPERATOR.MUL);
          
            return r;
        }

        public static List<ITEM_LIST> FlattenExprToList(this Expr e)
        {
            FlattenVisitor2 fl = new FlattenVisitor2();
            e.accept(fl);
            return fl.FlattenedExpr();
        }
         
        public static double Evaluate( this List<ITEM_LIST> ls)
        {
            Stack<double> stk = new Stack<double>();

            foreach( ITEM_LIST s in ls )
            {
                if (s.knd == ExprKind.VALUE)
                    stk.Push(s.Value);
                else
                {
                    switch(s.op) {
                        case OPERATOR.PLUS:
                            stk.Push(stk.Pop() + stk.Pop());
                            break;
                        case OPERATOR.MINUS:
                            stk.Push(stk.Pop() - stk.Pop());
                            break;
                        case OPERATOR.DIV:
                            double n = stk.Pop();
                            stk.Push(stk.Pop() / n);
                            break;
                        case OPERATOR.MUL:
                            stk.Push(stk.Pop() * stk.Pop());

                            break;

                    }


                }


            }
            return stk.Pop();
        }
 }

 /// <summary>
    /// 
    /// </summary>
    public class RDParser : Lexer
    {
        TOKEN Current_Token;


        public RDParser(String str)
            : base(str)
        {


        }

        /// <summary>
        ///      
        /// </summary>
        /// <returns></returns>
        public Expr CallExpr()
        {
            Current_Token = GetToken();
            return Expr();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Expr Expr()
        {
            TOKEN l_token;
            Expr RetValue = Term();
            while (Current_Token == TOKEN.TOK_PLUS || Current_Token == TOKEN.TOK_SUB)
            {
                l_token = Current_Token;
                Current_Token = GetToken();
                Expr e1 = Expr();
                RetValue = new BinaryExpr(RetValue, e1,
                    l_token == TOKEN.TOK_PLUS ? OPERATOR.PLUS : OPERATOR.MINUS);

            }

            return RetValue;

        }
        /// <summary>
        /// 
        /// </summary>
        public Expr Term()
        {
            TOKEN l_token;
            Expr RetValue = Factor();

            while (Current_Token == TOKEN.TOK_MUL || Current_Token == TOKEN.TOK_DIV)
            {
                l_token = Current_Token;
                Current_Token = GetToken();


                Expr e1 = Term();
                RetValue = new BinaryExpr(RetValue, e1,
                    l_token == TOKEN.TOK_MUL ? OPERATOR.MUL : OPERATOR.DIV);

            }

            return RetValue;
        }

        /// <summary>
        ///    
        /// </summary>
        public Expr Factor()
        {
            TOKEN l_token;
            Expr RetValue = null;
            if (Current_Token == TOKEN.TOK_DOUBLE)
            {

                RetValue = new Number(GetNumber());
                Current_Token = GetToken();

            }
            else if (Current_Token == TOKEN.TOK_OPAREN)
            {

                Current_Token = GetToken();

                RetValue = Expr();  // Recurse

                if (Current_Token != TOKEN.TOK_CPAREN)
                {
                    Console.WriteLine("Missing Closing Parenthesis\n");
                    throw new Exception();

                }
                Current_Token = GetToken();
            }

            else if (Current_Token == TOKEN.TOK_PLUS || Current_Token == TOKEN.TOK_SUB)
            {
                l_token = Current_Token;
                Current_Token = GetToken();
                RetValue = Factor();

                RetValue = new UnaryExpr(RetValue,
                     l_token == TOKEN.TOK_PLUS ? OPERATOR.PLUS : OPERATOR.MINUS);
            }
            else
            {

                Console.WriteLine("Illegal Token");
                throw new Exception();
            }


            return RetValue;

        }

 
//////////////////////////////////////////////////////////////////
//
// Entry point for the Test Driver
//
//
//
public static void Main(string[] args)
{
  if ( args.Length == 0 || args.Length > 1 )
  {  
    Console.WriteLine("Usage : Expr \"<expr>\" \n");
    Console.WriteLine(" eg:- Expr \"2*3+4\" \n");
    Console.WriteLine(" Expr \"-2-3\" \n");
    return;
  }




  try {
    RDParser parser = new RDParser (args[0]);
    Expr nd  = parser.CallExpr();
    Console.WriteLine("The Evaluated Value is {0} \n",nd );
  
    double n = nd.accept(new TreeEvaluatorVisitor());
    Console.WriteLine(n);
             nd.accept(new ReversePolishEvaluator());
              Console.WriteLine();
              StackEvaluator s = new StackEvaluator();
              nd.accept(s);
              Console.WriteLine(s.get_value());
              Console.WriteLine();

            nd.FlattenExprToList();
          
      
            Console.Read();
          
             
  }
  catch(Exception e )
  {
    Console.WriteLine("Error Evaluating Expression\n");
    Console.WriteLine(e);
    return;
  }
}




  }

}



