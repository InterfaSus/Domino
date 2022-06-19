namespace DominoEngine.Utils;

///<summary>
///Generic Strategie Delegate
///</summary>
///<returns> <c>The Token to be played</c> </returns>
public delegate (Token<T>, T output) strategy<T>(GameStatus<T> status, Token<T>[] Hand, T[] AvailableOutputs) where T : IEvaluable;

///<summary>
///Generates an array with n different objects of type T
///</summary>
public delegate T[] Generator<T>(int n);

///<summary>
///Generic Evaluator Delegate
///</summary>
///<returns> <c>The value of the Token evaluating it by certain criteria</c> </returns>
public delegate int evaluator<T>(Token<T>? Token) where T : IEvaluable;

///<summary>
///Generic Victory Criteria Delegate
///</summary>
///<returns> <c>The players that had win the game, if no one has win yet, returns an empty array with Lenght = 0 </c> </returns>
public delegate string[] victoryCriteria<T>(GameStatus<T> gameStatus, Player<T>[] Players, IFilter<T>? Filter = null, int Value = 0) where T : IEvaluable;

public class CriteriaCollection<T> where T : IEvaluable
{

    List<VictoryChecker<T>> CheckersList = new List<VictoryChecker<T>>();

    public CriteriaCollection(VictoryChecker<T> v)
    {
        CheckersList.Add(v);
    }

    public void Add(VictoryChecker<T> v){ CheckersList.Add(v); }

    public string[] RunCheck(GameStatus<T> gameStatus, Player<T>[] players)
    {
        HashSet< string > winners = new HashSet< string >();

        for (int i = 0; i < CheckersList.Count; i++)
        {
            VictoryChecker<T> v = CheckersList[i];
            string[]? temp = ( v.Check(gameStatus, players));

            if(temp != null)
                for (int j = 0; j < temp.Length; j++)
                {
                   winners.Add(temp[j]); 
                }
        }

        if(winners.Count > 0) return winners.ToArray();
                              return new string[0];
    }
}