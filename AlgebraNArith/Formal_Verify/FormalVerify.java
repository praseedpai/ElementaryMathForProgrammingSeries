
interface IA { void SayHello(String s); }
interface IB { void SayHello2(String s); }
interface IC { void SayHello3(String s); }
class UberComponent implements IA,IB,IC{
   public void SayHello(String s) { System.out.println("IA"); }
   public  void SayHello2(String s) {System.out.println("IB");}
   public  void SayHello3(String s) {System.out.println("IC");}
}
public class FormalVerify {
    static public void main(String[] args){
         //--------------- Check For Reflexivity IA = IA
         IA ia = new UberComponent(); IA ia2 = (IA) ia; ia2.SayHello(",");
         //--------------------- Check For Symmetry
         IB ia3 = (IB) ia; ia2 = (IA) ia3; ia2.SayHello(","); ia3.SayHello2(",");
         //----------- Check For Transitivity
         IC ia4 = (IC) ia3; IA ia5 = (IA) ia4; ia5.SayHello(","); a4.SayHello3("m");
    }
}