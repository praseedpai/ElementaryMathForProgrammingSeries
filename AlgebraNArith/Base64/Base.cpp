///////////////////////////////
// Base64 encoding technique demo
// Written by Praseed Pai K.T.
//

#include <stdio.h>
#include <windows.h>
///////////////////////////////////
//
// Legal Base64 characters
// Encoder looks up in the table using
// a 6 bit index.
//
BYTE Base64tab[]={
'A','B','C','D','E','F','G','H',
'I','J','K','L','M','N','O','P',
'Q','R','S','T','U','V','W','X',
'Y','Z','a','b','c','d','e','f',
'g','h','i','j','k','l', 'm','n',
'o','p','q','r','s','t','u','v',
'w','x','y','z','0','1','2','3',
'4','5','6','7','8','9','+','/'
};
////////////////////////////////
//
// Get the index of the character from
// the Base 64 table. This routine is
// used by the Decoder .. This can be
// optimized by using a Base 64 reverse
// table...
//
int ConvertAlphabetToIndex( BYTE alpha ){
	for(int i=0; i < 63; ++i )
		if ( Base64tab[i] == alpha )
			return i;
	return -1;
}

/////////////////////////////////////////////
// Encode three bytes (in ) into four bytes
// of data (out) .. The algorithm converts
// three bytes of data (24 bits ) into 4
// 6 bit indexes and uses the index to emit
// the character from the Base64 table into
// output (out)

void EncodeThree( BYTE *inp , BYTE *out){
	long val = inp[0];
	val = val | inp[1] << 8;
	val = val | inp[2] << 16;
	out[0] = Base64tab[val&63];
	out[1] = Base64tab[( val >> 6 )&63];
	out[2] = Base64tab[(val >> 12 )&63];
	out[3] = Base64tab[(val >> 18 ) &63];
}
///////////////////////////////////
//
// Takes four bytes of Base64 encoded data and
// converts to three bytes of data ...
// Convert the Character into the index of Base
// 64 table and assemble four indexes into a
// long value and extract the lowest three bytes
//
void DecodeThree( BYTE *inp , BYTE *out ){
	long val;
	val = ConvertAlphabetToIndex(inp[0]);
	val |= ConvertAlphabetToIndex(inp[1]) << 6;
	val |= ConvertAlphabetToIndex(inp[2]) << 12;
	val |= ConvertAlphabetToIndex(inp[3]) << 18;
	out[0] = val&255;
	out[1] = (val >> 8 )& 255;
	out[2] = (val >> 16)& 255;
}
//////////////////////////////////////////
// A driver program....
//
int main( int argc , char **argv ){
	BYTE in[3];
	BYTE out[4];
	in[0]='B';
	in[1]='O';
	in[2]='B';
	EncodeThree(in,out);
	printf("%c%c%c%c\n",out[0],out[1],out[2],out[3]);
	DecodeThree(out,in);
	printf("%c%c%c\n",in[0],in[1],in[2]);
}