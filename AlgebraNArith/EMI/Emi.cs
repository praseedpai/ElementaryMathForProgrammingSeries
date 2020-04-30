using System;

namespace Test
{

public class Emi
{


  public static double Get_Emi( double principal , double rate , long n )
  {
     

       double percent_rate = rate/(12.0*100.0);

 

       return  principal/(( 1 - Math.Pow(1+percent_rate,-n*12.0))/percent_rate);

  }

 


 

  public static void Main(String [] args )
  {

        if ( args.Length != 3 )
        {

           Console.WriteLine("Emi  <Principal> <rate> <period> ");

           return;
        }

        double principal = 0;
        double rate   = 0;
        long n         = 0; 

        try {
           principal = Double.Parse(args[0]);
           rate      = Double.Parse(args[1]);
           n         = Int32.Parse(args[2]);
        }
        catch(Exception  ) {
         
          Console.WriteLine("Exception while parsing Command Line argument");
          Console.WriteLine("Emi  <Principal> <rate> <period> ");
          return;
        }


        
         
        double emi =  Get_Emi(principal,rate,n);




        double temp_principal = principal;
        long months = n*12;

 

        long curr_month = 0;
 

        double curpri =  principal; 
 
        while ( curr_month < months )

        {

          

          double interest = curpri*(rate/(12*100));

          Console.WriteLine(curpri.ToString() + " "+ emi.ToString() + " " + (emi-interest).ToString() + " " + interest.ToString() );    

          curpri -= (emi-interest); 

          curr_month++;

        }  

 

 

    return;   
        

  }




}

}
