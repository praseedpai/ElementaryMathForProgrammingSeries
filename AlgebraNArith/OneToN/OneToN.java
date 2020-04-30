import java.math.BigInteger; 
import java.util.Scanner; 
  
public class OneToN
{ 
    // Returns Products of First  N 
    static BigInteger Product(int N) { 
        BigInteger f = new BigInteger("1"); 
        for (int i = 2; i <= N; i++) 
            f = f.multiply(BigInteger.valueOf(i)); 
  	return f; 
    } 
     // Returns Sigma of First  N 
    static BigInteger Sigma(int N) { 
        BigInteger f = new BigInteger("0"); 
        for (int i = 1; i <= N; i++) 
            f = f.add(BigInteger.valueOf(i)); 
        return f; 
    } 
  
    // Returns Sigma of First  N WithoutLoop
    static BigInteger SigmaWithOutLoop(int N) { 
        BigInteger f = BigInteger.valueOf(N); 
        // N*(N+1)/2
        f = f.multiply(f.add( BigInteger.valueOf(1))).divide(new BigInteger("2")); 
        return f; 
    } 
    // Driver method 
    public static void main(String args[]) throws Exception 
    { 
        int N = 200; 
        
        int len = args.length;
	if(len==0) {
			System.out.println("No args");
			return; 
	 }
	
         N = Integer.parseInt(args[0]);
	 System.out.println(Product(N)); 
         System.out.println(Sigma(N)); 
	 System.out.println(SigmaWithOutLoop(N)); 
		
    } 
} 