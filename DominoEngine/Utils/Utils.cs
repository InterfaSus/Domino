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
public delegate Player<T>[]? victoryCriteria<T>(GameStatus<T> gameStatus, Player<T>[] Players, Condition<T> condition) where T : IEvaluable;

public class FuseCriteria<T> where T : IEvaluable
{

    List<victoryCriteria<T>> CriteriasList = new List<victoryCriteria<T>>();

    public FuseCriteria(victoryCriteria<T> v1, victoryCriteria<T> v2)
    {
        CriteriasList.Add(v1);
        CriteriasList.Add(v2);
    }

    public void Add(victoryCriteria<T> v3){ CriteriasList.Add(v3); }

    public Player<T>[]? Run(GameStatus<T> gameStatus, Player<T>[] Players, Condition<T> condition)
    {
        HashSet< Player<T> > winners = new HashSet< Player<T>>();

        for (int i = 0; i < CriteriasList.Count; i++)
        {
            victoryCriteria<T> v = CriteriasList[i];
            Player<T>[]? temp = (v(gameStatus,Players,condition));

            if(temp != null)
                for (int j = 0; j < temp.Length; j++)
                {
                   winners.Add(temp[j]); 
                }
        }

        if(winners.Count > 0) return winners.ToArray();
                              return null;
    }
}