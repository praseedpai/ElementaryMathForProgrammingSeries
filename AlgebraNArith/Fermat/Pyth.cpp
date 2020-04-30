//////////////////////////////////////////////////
//
//  Program to Print Pythagorean Triplets
//
//
//  Pythagorean Triplets are 3 numbers which obey
//  the law x^2 + y ^2 = z^2. 
//
//
//
//
//
#include <stdio.h>


///////////////////////////////////////////////
//
//
//  entry point 
//
//
//
//


int main() 
{

	float x , y , z ;



    for( x = 1.0 ; x <= 100; x=x+1.0 )
	   for( y = 1.0 ; y <= 100 ; y=y+1.)
		   for( z = 1.0 ; z <=100; z=z+1. )
		   {

               if ( x*x + y*y == z*z ) 
                 printf(" triplets %d\t\t%d\t\t%d\n",(long)x,(long)y,(long)z);

			   if ( x*x*x + y*y*y == z*z*z )
			   {

                    printf("cube =%d\t\t%d\t\t%d\n",(long)x,(long)y,(long)z);


			   }



		   }




		   













}

