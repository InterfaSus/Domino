using DominoEngine;

public class Number : IEvaluable {

    private readonly int Num;

    public Number(int num) {
        this.Num = num;
    }

    public int Value {
        get {
            return Num;
        }
    }

    public override bool Equals(object? obj) {
        
        if (obj == null) return false;
        if (obj is Number n) return Value == n.Value;
        return false;
    }

    public override string ToString()
    {
        return Value.ToString();
    }
}