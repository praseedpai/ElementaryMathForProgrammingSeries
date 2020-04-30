import java.lang.*;



public class Emi
{


  public static double Get_Emi( double principal , double rate , long n )
  {
     

       double percent_rate = rate/(12.0*100.0);

 

       return  principal/(( 1 - Math.pow(1+percent_rate,-n*12.0))/percent_rate);

  }

 


 

  public static void main(String [] args )
  {

        if ( args.length != 3 )
        {

           System.out.println("Emi  <Principal> <rate> <period> ");

           return;
        }

        double principal = 0;
        double rate   = 0;
        long n         = 0; 

        try {
           principal = Double.parseDouble(args[0]);
           rate      = Double.parseDouble(args[1]);
           n         = Integer.parseInt(args[2]);
        }
        catch(Exception e ) {
          System.out.println("Exception while parsing Command Line argument");
          System.out.println("Emi  <Principal> <rate> <period> ");
          return;
        }

        System.out.println( principal + "  " + rate  + " " + n ); 
         
        double emi =  Get_Emi(principal,rate,n);

        System.out.println("emi is " + emi); 


        double temp_principal = principal;
        long months = n*12;

 

        long curr_month = 0;
 

        double curpri =  principal; 
 
        while ( curr_month < months )

        {

          

          double interest = curpri*(rate/(12*100));

          System.out.println(Math.round(curpri) + " "+ Math.round(emi) + " " + Math.round(emi-interest) + " " + Math.round(interest) );    

          curpri -= (emi-interest); 

          curr_month++;

        }  

 

 

    return;   
        

  }




}