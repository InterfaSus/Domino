using DominoEngine.Utils;

namespace DominoEngine;

///<summary>
///A class to encapsulate a filter and a value
///</summary>
public class Condition<T> where T : IEvaluable {
    

    public tokenFilter<T> Filter { get; private set; }
    public int Value { get; private set; }

    public Condition(tokenFilter<T> filter, int value)
    {
        Filter = filter;
        Value = value;
    }
}


