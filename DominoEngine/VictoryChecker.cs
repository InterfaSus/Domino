using DominoEngine.Utils;

namespace DominoEngine
{
    public class VictoryChecker<T> where T : IEvaluable
    {
        private readonly victoryCriteria<T> criteria;
        private readonly tokenFilter<T>? filter;
        private readonly int value;
        public VictoryChecker(victoryCriteria<T> Criteria, tokenFilter<T>? Filter = null, int Value = 0)
        {
            criteria = Criteria;
            filter = Filter;
            value = Value;
        }

        public string[]? Check(GameStatus<T> gameStatus, Player<T>[] Players)
        {
            return criteria(gameStatus, Players, value);
        }
    }
}