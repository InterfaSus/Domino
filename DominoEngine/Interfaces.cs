namespace DominoEngine
{
    public interface IToken
    {
        int[] Outputs { get; }
        int[] FreeOutputs { get; }
    }
}