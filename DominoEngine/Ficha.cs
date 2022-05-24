namespace DominoEngine;
public class Ficha {

    public int Cara1 { get; private set; }
    public int Cara2 { get; private set; }

    public Ficha(int Cara1, int Cara2) {

        this.Cara1 = Cara1;
        this.Cara2 = Cara2;
    }

    public void Voltear() {
        
        int temp = this.Cara1;
        this.Cara1 = this.Cara2;
        this.Cara2 = temp;
    }

    public override string ToString() {
        return "[" + this.Cara1 + "-" + this.Cara2 + "]";
    }
}
