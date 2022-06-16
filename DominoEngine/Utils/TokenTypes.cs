namespace DominoEngine.Utils.TokenTypes;

///<summary>
///An adapter to use numbers as output types
///</summary>
public class Number : IEvaluable {

    private readonly int Num;

    public Number(int num) {
        this.Num = num;
    }

    public int Value => Num;

    public static Number[] Generate(int n) {

        Number[] result = new Number[n];

        for (int i = 0; i < n; i++) {
            result[i] = new Number(i);
        }

        return result;
    }

    public override bool Equals(object? obj) {
        
        if (obj == null) return false;
        if (obj is Number n) return Value == n.Value;
        return false;
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    public override string ToString()
    {
        return Value.ToString();
    }

}