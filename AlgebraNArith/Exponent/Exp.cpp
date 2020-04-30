
// e => ( n + (1/n))^n 
//          as 
//      lim n => INFINITY  
//   
//
#include <stdio.h>
#include <stdlib.h>
#include <math.h>


int main( int argc , char **argv )

{

   if ( argc == 1 )

     return 0;

   int n = atoi(argv[1]);

   //////////////////////

   //

   // Compute the Kernel ( 1 + 1/n);

   //

   double nucleus = 1.0 + (1.0  /(double)n); 

   //////////////////////////////////////

   // Compute EXP for one 

   //

   double exp_one   = exp(1.0);

   ////////////////////////////////

   //

   // Compute  nucleus ^ n 

   double exp_brute = pow(nucleus,(double)n);

   ///////////////////////////

   //

   // Spit the result on to the console

   //

   printf("CRT exp = %g\t BRUTE exp = %g\n",

          exp_one,exp_brute);


}
