using System.Text;

namespace DominoEngine
{
    ///<summary>
    ///Class <c>Token</c> Represents a Token with n(integer) possibles values
    ///</summary>
    public class Token<T> : IToken<T> where T : IEvaluable
    {
        private readonly T[] outputs;
        private readonly bool[] AvailableMask;
        public Token(T[] outputs)
        {
            this.outputs = (T[])outputs.Clone();
            Array.Sort(this.outputs, (x, y) => x.Value - y.Value);
            this.AvailableMask = new bool[outputs.Length];

            for (int i = 0; i < AvailableMask.Length; i++)
            {
                AvailableMask[i] = true;
            }
        }

        ///<summary>
        ///Represent to place a token on a defined position
        ///</summary>
        ///<param name="number"> The output's value where the other token will be placed</param>
        internal void PlaceTokenOn(T? value)
        {   
            if (value == null) {
                throw new ArgumentException("Argument \"value\" cannot be null");
            }

            for (int i = 0; i < outputs.Length; i++)
            {
                if(outputs[i]!.Equals(value) && AvailableMask[i]) { 
                    AvailableMask[i] = false;
                    return;
                }
            }

            throw new Exception("The value " + value + " does not exist on token or is not currently available");
        }
        
        ///<summary>
        ///Returns all the outputs of the token
        ///</summary>
        ///<returns> <c>An array of type T with every token output</c> </returns>
        public T[] Outputs 
        { 
            get
            {
                return (T[])outputs.Clone();
            } 
        }
    
        ///<summary>
        ///Returns all the availables outputs of the token
        ///</summary>
        ///<returns> <c>An array of type T with every token free output</c> </returns>
        public T[] FreeOutputs 
        { 
            get
            {
                List<T> temp = new List<T>();

                for (int i = 0; i < outputs.Length; i++)
                {
                    if(AvailableMask[i]) temp.Add(outputs[i]);
                }

                return temp.ToArray();
            } 
        }

        ///<summary>
        ///Tells if the given output its available on the Token or not
        ///</summary>
        ///<param name="output">The output to check</param>
        ///<returns>A boolean indicating if the output is available</returns>
        public bool HasOutput(T output) {
            return Algorithms.ArrayOperations.Find<T>(this.FreeOutputs , output) != -1;
        }

        public override string ToString() {
            
            StringBuilder result = new StringBuilder("[");

            foreach (var item in outputs) {
                result.Append(item + "-");
            }
            result.Remove(result.Length - 1, 1);
            result.Append("]");
            return result.ToString();
        }
    }
}