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

///<summary>
///A type where every letter in the spanish alphabet its assigned to a value guide by the Scrabble rules
///</summary>
public class Letter : IEvaluable
{
    private static readonly Dictionary<char, int> charValues = new Dictionary<char, int>{
        {'a', 1}, {'b', 3}, {'c', 3}, {'d', 2}, {'e', 1}, {'f', 4}, 
        {'g', 2}, {'h', 4}, {'i', 1}, {'j', 8}, {'k', 5}, {'l', 1}, 
        {'m', 3}, {'n', 1}, {'ñ', 20}, {'o', 1}, {'p', 3}, {'q', 10}, 
        {'r', 1}, {'s', 1}, {'t', 1}, {'u', 1}, {'v', 1}, {'w', 4}, 
        {'x', 8}, {'y', 4}, {'z', 10}
    };
    private readonly char _letter;

    public Letter(char letter)
    {
        if(!charValues.ContainsKey(letter)) throw new Exception("Character must be between a and z");
        _letter = letter;
    }

    public static Letter[] Generate(int n) {

        if(n > charValues.Count) throw new  Exception("Value n can't be higher that the amount of letters on the dictionary");
        char[] letterCollection = charValues.Keys.ToArray();
        List<Letter> Letters = new List<Letter>();
        
        for (int i = 0; i < n; i++)
        {
            Letters.Add(new Letter(letterCollection[i]));
        }


        return Letters.ToArray();
    }

    public override bool Equals(object? obj) {
        
        if (obj == null) return false;
        if (obj is Letter a) return a._letter == this._letter;
        return false;
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    public override string ToString()
    {
        return _letter.ToString();
    }

    public int Value => charValues[_letter];
}