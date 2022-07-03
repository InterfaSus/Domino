using DominoEngine.Utils;

namespace DominoEngine;

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

    public string[]? RunCheck(PlayData<T>[] history, evaluator<T> evaluator, Tuple<string, Token<T>[]>[] PlayersTokens)
    {
        HashSet< string > winners = new HashSet< string >();
        bool noOneWon = false;

        for (int i = 0; i < CheckersList.Count; i++)
        {
            VictoryChecker<T> v = CheckersList[i];
            string[]? temp = ( v.Check(history, evaluator, PlayersTokens));

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