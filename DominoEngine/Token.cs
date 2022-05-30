using System.Text;

namespace DominoEngine
{
    ///<summary>
    ///Class <c>Token</c> Represents a Token with n(integer) possibles values
    ///</summary>
    public class Token : IToken
    {
        private readonly int[] outputs;
        private readonly bool[] AvailableMask;
        public Token(int[] outputs)
        {
            this.outputs = (int[])outputs.Clone();
            this.AvailableMask = new bool[outputs.Length];

            for (int i = 0; i < AvailableMask.Length; i++)
            {
                AvailableMask[i] = true;
            }
        }

    ///<summary>
    ///Represent to place a token on a defined position
    ///</summary>
    ///<param name="number"> The output's number where the other token will be placed</param>
        public void PlaceTokenOn(int number)
        {
            for (int i = 0; i < outputs.Length; i++)
            {
                if(outputs[i] == number && AvailableMask[i]){ 
                    AvailableMask[i] = false;
                    return;
                }
            }

            throw new Exception("The number " + number + " does not exist on token or is not currently available");
        }
    ///<summary>
    ///Returns all the outputs of the token
    ///</summary>
    ///<returns> <c>int[] outputs</c> </returns>
        public int[] Outputs 
        { 
            get
            {
                return (int[])outputs.Clone();
            } 
        }
    
    ///<summary>
    ///Returns all the availables outputs of the token
    ///</summary>
    ///<returns> <c>int[] outputs</c> </returns>

        public int[] FreeOutputs 
        { 
            get
            {
                List<int> temp = new List<int>();

                for (int i = 0; i < outputs.Length; i++)
                {
                    if(AvailableMask[i]) temp.Add(outputs[i]);
                }

                return temp.ToArray();
            } 
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