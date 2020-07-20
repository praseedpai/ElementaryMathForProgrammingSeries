// PSRenderer.cpp : Defines the entry point for the application.
//

#include <windows.h>
#include <math.h>
#include "Matrix2D.h"

#define MAX_LOADSTRING 100
#define IDT_TIMER1 (WM_USER + 1067 )
//////////////////////////////////
//
// constant to represent PI
//
//
const double PI = 3.14159;

void Transform(HWND m_hWnd, int *px , int *py){
        RECT _rect;
        ::GetClientRect(m_hWnd,&_rect);
        int width = (_rect.right-_rect.left)/2;
        int height = (_rect.bottom-_rect.top)/2;
       // *px = *px + width;
       // *py = height - *py;

       Matrix2D mat;
       mat.SetIdentity();
       mat.Translate(width,height);
       mat.Scale(2,-1);
       mat.Rotate(-90);
       double ppx = *px;
       double ppy = *py;
       mat.Transform(ppx,ppy);
       *px = (int)ppx;
       *py = (int)ppy;
}

//////////////////////////////////
//
// Every Window has got a WindowClass Name ...
//   Button has got classname BUTTON , list box "LISTBOX etc
//
// Class name is they key which Windows uses to retrieve the
// specification to create a window (sounds familiar ..oop guys)
//

const char g_szClassName[] = "myWindowClass";

///////////////////////////////
//
// Customize this program by adding code to the paint routine...
//
//
void CallPaintRoutine( HWND hwnd , HDC paintdc ){
        int x1 = -200,y1=-200,
            x2 =  200,y2=-200,
            x3 =  200,y3=200,
            x4 =  -200,y4=200;
        Transform(hwnd,&x1,&y1); Transform(hwnd,&x2,&y2);       
         Transform(hwnd,&x3,&y3); Transform(hwnd,&x4,&y4); 
        POINT rect[4];
        rect[0].x = x1;
        rect[0].y = y1;
        rect[1].x = x2;
        rect[1].y = y2;
        rect[2].x= x3;
        rect[2].y= y3; //  
        rect[3].x= x4;
        rect[3].y= y4; //  = { x1,y1,x2,y2};
        HBRUSH hbr = CreateSolidBrush(RGB(100,149,237));
        SelectObject(paintdc,hbr);
        Polygon(paintdc, rect,4);

#if 0
        HPEN hpen = CreatePen(PS_SOLID,2,RGB(153,0,0));
        SelectObject(paintdc,hpen);

        SYSTEMTIME systime;
        GetLocalTime(&systime);
 
        int fhour,fmin,fsec ;
        fhour = systime.wHour%12; 
        fmin = systime.wMinute;
        fsec = systime.wMinute;

        fhour += fmin/60;
        fhour = fhour * 360 / 12;
        fmin = fmin * 360 / 60;
        fsec = fsec * 360 / 60;

        int xs =  120 * cos((-fsec * PI / 180.0)+ PI/2.0);
        int ys =  120 * sin((-fsec * PI / 180.0)+ PI/2.0);

        int xm =  100 * cos((-fmin * PI / 180.0)+ PI/2.0);
        int ym =  100 * sin((-fmin * PI / 180.0)+ PI/2.0);


        int xh =  60 * cos((-fhour * PI / 180.0)+ PI/2.0);
        int yh =  60 * sin((-fhour * PI / 180.0)+ PI/2.0);
        
        int x = 0,y=0;
                    

        Transform(hwnd,&xh,&yh);
        Transform(hwnd,&x,&y);
        MoveToEx(paintdc,x,y,NULL);
        LineTo(paintdc,xh,yh);
        Transform(hwnd,&xm,&ym);
        MoveToEx(paintdc,x,y,NULL);
        LineTo(paintdc,xm,ym);
        Transform(hwnd,&xs,&ys);
        MoveToEx(paintdc,x,y,NULL);
        LineTo(paintdc,xs,ys);

   
        char *str[] = {"3","2","1","12","11","10","9","8","7","6","5","4"};
        HFONT hFont = CreateFont(48,0,0,0,FW_DONTCARE,FALSE,FALSE,FALSE,DEFAULT_CHARSET,OUT_OUTLINE_PRECIS,
                CLIP_DEFAULT_PRECIS,CLEARTYPE_QUALITY, VARIABLE_PITCH,TEXT("Times New Roman"));
        SelectObject(paintdc,hFont);
        SetTextColor(paintdc,RGB(204,0,0));
        SetBkMode(paintdc,TRANSPARENT);
        Matrix2D mat;
	 RECT _rect;
        ::GetClientRect(hwnd ,&_rect);
        int width = (_rect.right-_rect.left)/2;
        int height = (_rect.bottom-_rect.top)/2;
        for(int i = 0;i < 12;i++){
          int x = 200 * cos(i * PI / 6),y= 200 * sin(i * PI / 6);
          //  double x = 200,  y =200;
           Transform(hwnd,&x,&y);
          // mat.SetIdentity();
           
         // mat.Translate(width,height);
           // mat.Scale(1,-1);
           //  mat.Rotate(i*30.0);
          
       
         //  mat.Transform(x,y);
            TextOut(paintdc,(int)x,(int)y,str[i],strlen(str[i]));
	}

    #endif

}

//////////////////////////////////////////////////////
//
//
//
LRESULT CALLBACK	WndProc(HWND, UINT, WPARAM, LPARAM);
LRESULT CALLBACK	About(HWND, UINT, WPARAM, LPARAM);

int APIENTRY WinMain(HINSTANCE hInstance,
                     HINSTANCE hPrevInstance,
                     LPTSTR    lpCmdLine,
                     int       nCmdShow)
{
   WNDCLASSEX wc;
   HWND hwnd;
   MSG Msg;

   //
   // Write the specification of the Window
   // and Register it with Window manager (desktop manager)
   //
   wc.cbSize        = sizeof(WNDCLASSEX);
   wc.style         =  CS_HREDRAW | CS_VREDRAW;

   /////////////////////////////////////
   //
   // lpfnWndProc - LONG POINTER TO FUNCTION WndProc
   // give the address of your window procedure here  
   //
   wc.lpfnWndProc   = WndProc;

   /////////////////////////////////////////////
   // When i started windows programming , there was something called
   // VBX ( Visual Basic Extensions.. Later superceded by OCX/ActiveX)
   // We used to use cbWndExtra a lot ( Window Extra bytes...used to store
   // a pointer to a structure )
   //
   wc.cbClsExtra    = 0;
   wc.cbWndExtra    = 0;
   wc.hInstance     = hInstance;

   /////////////////////////////////////////
   //
   // Load standard icons ...if the first parameter is null...
   //
   wc.hIcon         = LoadIcon(NULL, IDI_APPLICATION);
   wc.hCursor       = LoadCursor(NULL, IDC_ARROW);
   /////////////////////////////
   //
   //  Set color attributes ...
   //
   wc.hbrBackground = (HBRUSH)(COLOR_WINDOW+1);
   wc.lpszMenuName  = NULL;
   wc.lpszClassName = g_szClassName;
   wc.hIconSm       = LoadIcon(NULL, IDI_APPLICATION);

   if(!RegisterClassEx(&wc))
   {
       MessageBox(NULL, "Window Registration Failed!", "Error!",
           MB_ICONEXCLAMATION | MB_OK);
       return 0;
   }

   ////////////////////////////////////////////////////
   // Windows looks up in the Wndclass table by using g_szClassName as
   // a key ..and retrieves the WNDCLASS and goes to action..
   hwnd = CreateWindowEx(
       WS_EX_CLIENTEDGE,
       g_szClassName,
       "The title of my window",
       WS_OVERLAPPEDWINDOW,
       CW_USEDEFAULT, CW_USEDEFAULT, 1024, 768,
       NULL, NULL, hInstance, NULL);

   if(hwnd == NULL)
   {
       MessageBox(NULL, "Window Creation Failed!", "Error!",
           MB_ICONEXCLAMATION | MB_OK);
       return 0;
   }

   ShowWindow(hwnd, nCmdShow);
   UpdateWindow(hwnd);

   ///////////////////////////////////////////
   //
   // GetMessage stops , when it gets WM_QUIT message
   //
   while(GetMessage(&Msg, NULL, 0, 0) > 0)
   {
       TranslateMessage(&Msg);
       DispatchMessage(&Msg);
   }
   //////////////////////////////
   // We are finished...send ...status to the shell !
   //
   return Msg.wParam;
}







	
    



//
//  FUNCTION: WndProc(HWND, unsigned, WORD, LONG)
//
//  PURPOSE:  Processes messages for the main window.
//
//  WM_COMMAND	- process the application menu
//  WM_PAINT	- Paint the main window
//  WM_DESTROY	- post a quit message and return
//
//
LRESULT CALLBACK WndProc(HWND hWnd, UINT message, WPARAM wParam, LPARAM lParam)
{
	int wmId, wmEvent;
	PAINTSTRUCT ps;
	HDC hdc;

	switch (message) 
	{
        case WM_CREATE:
             SetTimer(hWnd,IDT_TIMER1,1000,(TIMERPROC) NULL);     // no timer callback 
             return DefWindowProc(hWnd, message, wParam, lParam);
	case WM_COMMAND:
		wmId    = LOWORD(wParam); 
		wmEvent = HIWORD(wParam); 
		// Parse the menu selections:
		switch (wmId)
		{
		
		default:
			return DefWindowProc(hWnd, message, wParam, lParam);
		}
		break;
	case WM_PAINT:
		{
		hdc = BeginPaint(hWnd, &ps);
		CallPaintRoutine( hWnd , hdc );
		EndPaint(hWnd, &ps);
		}
		break;
        case WM_TIMER:
                InvalidateRect(hWnd, NULL, TRUE); 
                break;
	case WM_DESTROY:
		PostQuitMessage(0);
		break;
	default:
		return DefWindowProc(hWnd, message, wParam, lParam);
	}
	return 0;
}

