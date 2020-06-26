#include <stdio.h>
#include <math.h>

double m_abs( double arg ) {
	 if ( arg < 0.0 ) { arg = -1*arg;}
	 return arg;
}

double m_dtor(double x ) { return ( x * 3.14159/180.0 ); }

double m_sin(double arg ) {
	double term = arg;
    	double i = 1;
    	double x2 = arg*arg;
	double tsin = arg;
	while ( m_abs(term) > 0.0000001 ){
        	 i = i + 2;
       	         term = -term*x2/(i*(i-1));
         	 tsin = tsin + term; 
	}
	return tsin;
}

double m_cos(double arg ) { return m_sin( (2*3.14159/4.0) - arg ); }

int main(int argc, char **argv) {
   double dval = (argc == 1 )? 45.0 : atof(argv[1]);
   printf("\n\n\n\nValue is %e\n\n" ,dval*3.14159/180.0);
   printf("my sine = %f\t\t\tmath.h sin = %f\n",
		m_sin(m_dtor(dval)),sin(m_dtor(dval)));
   printf("my cosine = %f\t\t\tmath.h cosine = %f\n",
		m_cos(m_dtor(dval)),cos(m_dtor(dval)));
   
}