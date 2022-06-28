namespace DominoEngine.Utils;

///<summary>
///Generic Strategie Delegate
///</summary>
///<returns> <c>The Token to be played</c> </returns>
public delegate Tuple<Token<T>, T> strategy<T>(GameStatus<T> status, Token<T>[] Hand, T[] AvailableOutputs) where T : IEvaluable;

///<summary>
///Generates an array with n different objects of type T
///</summary>
public delegate T[] Generator<T>(int n);

///<summary>
///Generic Evaluator Delegate
///</summary>
///<returns> <c>The value of the Token evaluating it by certain criteria</c> </returns>
public delegate int evaluator<T>(Token<T>? Token) where T : IEvaluable;

public delegate bool tokenFilter<T>(Token<T> token) where T : IEvaluable;

///<summary>
///Generic Victory Criteria Delegate
///</summary>
///<returns> <c>The players that had win the game, if no one has win yet, returns a null array </c> </returns>
public delegate string[]? victoryCriteria<T>(GameStatus<T> gameStatus, Player<T>[] Players, int Value = 0) where T : IEvaluable;

public class CriteriaCollection<T> where T : IEvaluable
{

    List<VictoryChecker<T>> CheckersList = new List<VictoryChecker<T>>();

    public CriteriaCollection(VictoryChecker<T> v)
    {
        CheckersList.Add(v);
    }

    public CriteriaCollection(VictoryChecker<T>[] v)
    {   
        if (v.Length == 0) throw new ArgumentException("At least one victory checker must be passed");
        CheckersList.AddRange(v);
    }

    public void Add(VictoryChecker<T> v){ CheckersList.Add(v); }

    public string[]? RunCheck(GameStatus<T> gameStatus, Player<T>[] players)
    {
        HashSet< string > winners = new HashSet< string >();
        bool noOneWon = false;

        for (int i = 0; i < CheckersList.Count; i++)
        {
            VictoryChecker<T> v = CheckersList[i];
            string[]? temp = ( v.Check(gameStatus, players));

            if(temp != null) {
                if (temp.Length == 0) noOneWon = true;
                for (int j = 0; j < temp.Length; j++)
                {
                   winners.Add(temp[j]); 
                }
            }
        }

        if(winners.Count > 0) return winners.ToArray();
        if (noOneWon) return new string[0];
        else return null;
    }
}