#include <stdio.h>
#include <stdlib.h>
//---------------- O(n) algorithm
int sum_from_one_to_n_lineartime( int n ) {
      int sum = 0,j;
      for( j=0; j<= n; ++j) { sum += j; }
      return sum;
}
//------------------ O(1) algorithm  
int sum_from_one_to_n_constanttime( int n ) { return (n*(n+1)) >> 1; }
//------------------- User Entry Point
int main( int argc , char **argv ){
      int n = ( argc > 1 ) ? atoi(argv[1]) : 10;
      printf("%d\t%d\n",sum_from_one_to_n_lineartime(n),
			sum_from_one_to_n_constanttime(n));
      fflush(stdout);
}