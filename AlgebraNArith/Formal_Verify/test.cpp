#include <stdio.h>
#include <iostream>

#include <string.h>


using namespace std;

class IUnknown {
 public:
   virtual bool QueryInterface( string iname , void **ppv ) = 0;

};



class  IDisplay : virtual public IUnknown
{
     public:
       virtual bool Display() = 0;


};


class  IStore : virtual public IUnknown
{
     public:
       virtual bool Set(int set) = 0;


};

class IErase :  virtual public IUnknown
{
    public:
       virtual bool DoErase() = 0;


};


class UberComponent : public IErase, IStore, IDisplay
{
       int _value;
   public:
       UberComponent(int r) : _value(r) { }
       bool DoErase() { _value= 0; return false; }
       bool Set(int set) { _value = set; return false; }
       bool Display() { cout << _value << endl;  return true; }
       bool QueryInterface( string iname , void **ppv )  { 

               const char *Iname = iname.c_str();
               *ppv = 0;

               if ( stricmp(Iname,"IERASE" ) == 0 )
               		*ppv = (void *) ((IErase *)this);
               else if (  stricmp(Iname,"ISTORE") == 0 ) {
			*ppv = (void *) ((IStore *)this);
                        if ( *ppv != 0 )
                             cout << "Retrieved the IStore Interface " << endl; 
               }
               else if (  stricmp(Iname,"IDISPLAY" ) == 0 )
			*ppv = (void *) ((IDisplay *)this);
               else if ( stricmp(Iname,"IUNKNOWN" ) == 0 )
			*ppv = (void *) ((IUnknown *)this);
               else {
                       *ppv = 0;
                       cout << "Wrong Interface name " << endl;
                       return false;

               }

  
               
               return true; 

      }
       

};


bool TestIunknown(IUnknown  *rf) {

   cout << "Entered the Test Suite " << endl;

   // test for reflexivity
   IErase *IA;
   IStore *IB;
   IDisplay *IC;
   IErase *TempIA;
   IStore *TenpIB;

   IDisplay *TempIC;


  // IA = IB = IC = Temp = 0;

   if (! rf->QueryInterface("IERASE",(void **)&IA) )
   {
        cout << "Failed to Retrieve IA " << endl;
        return false;

   }
   cout << " Passed the first state  " << endl;

   if (! IA->QueryInterface("ISTORE",(void **)&IB) )
   {

        cout << "Failed to Retrieve IB " << endl;
        return false;

   }
   cout << " Passed the second state  " << endl;

   if ( IB == 0 ) { 
        cout << "Failed to retrieve the COM interface" << endl;
        return false;

   }

   IB->Set(10);

   cout << " Passed the third state  " << endl;
   IStore *TempIB2;
   if ( ! IB->QueryInterface("ISTORE",(void **)&TempIB2) )
   {
        cout << "Failed to Retrieve IB " << endl;
        return false;

   }
   cout << " Passed the thrid state  " << endl;
 
   if ( TempIB2 == IB ) { cout << "Agrees to Symmetric Law" << endl; }


   if ( ! IB->QueryInterface("IDISPLAY", (void **)&IC) )
   {
	cout << "Failed to Retrieve IC " << endl;
        return false;
   }
    cout << " Passed the fourth state  " << endl;
   if (! IA->QueryInterface("IDISPLAY", (void **)&TempIC) )
   {

	cout << "Failed to Retrieve IDisplay " << endl;
        return false;
   }

   if ( TempIC == IC ) { cout << "Agrees to Transitive Law" << endl; }

   if (! IA->QueryInterface("IERASE",(void **) &TempIA ) )
   {
	cout << "Failed to Retrieve Irase" << endl;
        return false;

   }

   if ( IA == TempIA ) { cout << "Agrees to Reflexive Law" << endl; }

  
   return true;
   

}


int main( int argc, char **argv )
{

    UberComponent * te = new UberComponent(10);   
    te->Display();
    TestIunknown((IUnknown *)te);
    delete te;


}

