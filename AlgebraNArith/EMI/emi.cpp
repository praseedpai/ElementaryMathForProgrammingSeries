#include <iostream>

#include <cmath>

#include <cstdlib>

using namespace std;

 

 

double   Get_emi( double principal , double rate , int number_of_year )

{

 

       double percent_rate = rate/(12*100);

 

       return  principal/(( 1 - pow(1+percent_rate,-number_of_year*12))/percent_rate);

 

}

 

 

void main(int argc , char **argv)

{

 

 

    if ( argc != 4 ) {

 

         fprintf(stdout,"Usage: EmiSched <principal> <rate> <period> \n");

         return;

 

    }

 

    double principal = atof(argv[1]);

    double rate      = atof(argv[2]);

    long   period    = atol(argv[3]);

    

    double emi =  Get_emi(principal,rate,period);

 

    double temp_principal = principal;

 

 

    long months = period*12;

 

    long curr_month = 0;

 

 

    double curpri =  principal; 

    while ( curr_month < months )

    {

          

          double interest = curpri*(rate/(12*100));

          fprintf(stdout,"%f\t%f\t%f\t%f\n",curpri,emi,emi-interest,interest);

          curpri -= (emi-interest); 

          curr_month++;

    }  

 

 

    return; 

 

 

 

}

