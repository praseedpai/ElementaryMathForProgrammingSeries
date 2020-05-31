#include <stdio.h>
#include <math.h>


long isleap(long year ) {

  return ( (year % 400 == 0 ) ||
         ( (year%4 == 0 ) && 
         (year%100 != 0 )));
}



int main() 
{

	long month , day , year;
	long tempcomp;
	long oddday = 0;

	long accum[12] = { 0,3,3,6,8,11,13,16,19,21,24,26};

    	const char * days[] = { "Sunday",
		               "Monday",
					   "Tuesday",
					   "Wednesday",
					   "Thursday",
					   "Friday",
					   "Saturday",0 };


	printf("Enter day month and year\n\n");

	scanf("%d%d%d",&day , &month , &year );


	if ( month < 1 && month >12 )
	{
        printf("You  have entered an illegal month\n");
        return -1;
	}


	if ( day < 1 && day > 31 ) {
		
       printf("u have entered an illeagal day \n" );
	   return -1;
	   
	}

	if ( year < 0 ) {
               printf("Year should not be a negative number"); 
               return -1;
	}


   	 if ( ( day == 29 ) && ( month == 2 ) && !isleap(year) ){
       		printf("u have entered an illeagal date : not a leap year\n");
       		return -1	; 
	}

    	
	if ( ( day > 29 ) && ( month == 2 ) )
	{
      		 printf("u have entered an illeagal date \n");
                 return -1;
 
	}


        //--- Take the Previous Year , Take Modulo 400 
    	tempcomp = year-1;
	tempcomp = tempcomp%400;
        //--- Depending upon reminder, odd days has to be assigned
        if (tempcomp >= 300 ) { tempcomp = tempcomp%300; oddday = 1;}
	else if ( tempcomp >= 200 ) { tempcomp = tempcomp%200; oddday = 3; }
        else if ( tempcomp >= 100 ) { tempcomp = tempcomp%100; oddday = 3;}
        else  { oddday = 0; }

        //---- Adjust the Number of Year and Number of Leap Years
	oddday = ( oddday + ( tempcomp + (long)(tempcomp/4) )); 
        //------- Adjust for the month 
        oddday = ( oddday + accum[ month- 1 ]);
        //---- Adjust for the Leap Year 
	if ( month > 2 && isleap(year) ) { oddday +=1; }
        //----------- Adjust the Odd days for DAYs and take modulo 7
        oddday = ( oddday + day ) % 7;
        //------------- Look up in the days array to find CDOW
        printf("The day is %s\n" , days[oddday]);
        return 0;
}
