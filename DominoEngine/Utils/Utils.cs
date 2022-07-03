namespace DominoEngine.Utils;

///<summary>
///Generic Strategie Delegate
///</summary>
///<returns> <c>The Token to be played</c> </returns>
public delegate Tuple<Token<T>, T> strategy<T>(PlayData<T>[] status, evaluator<T> evaluator, Token<T>[] Hand, T[] AvailableOutputs) where T : IEvaluable;

///<summary>
///Generates an array with n different objects of type T
///</summary>
public delegate T[] Generator<out T>(int n);

///<summary>
///Generic Evaluator Delegate
///</summary>
///<returns> <c>The value of the Token evaluating it by certain criteria</c> </returns>
public delegate int evaluator<T>(Token<T> Token) where T : IEvaluable;

public delegate bool tokenFilter<T>(Token<T> token) where T : IEvaluable;

///<summary>
///Generic Victory Criteria Delegate
///</summary>
///<returns> <c>The players that had win the game, if no one has win yet, returns a null array </c> </returns>
public delegate string[]? victoryCriteria<T>(PlayData<T>[] movesHistory, evaluator<T> evaluator, Tuple<string, Token<T>[]>[] PlayersTokens, int Value = 0) where T : IEvaluable;