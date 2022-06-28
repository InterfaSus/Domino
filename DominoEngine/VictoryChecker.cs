using DominoEngine.Utils;

namespace DominoEngine
{
    public class VictoryChecker<T> where T : IEvaluable
    {
        private readonly victoryCriteria<T> criteria;
        private readonly int value;
        public VictoryChecker(victoryCriteria<T> Criteria, int Value = 0)
        {
            criteria = Criteria;
            value = Value;
        }

        public string[]? Check(GameStatus<T> gameStatus, Player<T>[] Players)
        {
            return criteria(gameStatus, Players, value);
        }
    }
}