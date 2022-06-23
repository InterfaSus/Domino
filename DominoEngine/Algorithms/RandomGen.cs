namespace DominoEngine.Algorithms;

///<summary>
/// A class to generate random numbers using a substractive generator algorithm
///</summary>
public class RandomGen {

    private readonly int MAX = 1000000000;
    private int seed;
    private int[] state;
    private int pos;
 
    private int mod(int n) {
        return ((n % MAX) + MAX) % MAX;
    }

    public RandomGen() {
        
        state = new int[55];
        this.seed = (int)(DateTime.Now.ToFileTimeUtc() % MAX);

        int[] temp = new int[55];
        temp[0] = mod(seed);
        temp[1] = 1;

        for(int i = 2; i < 55; ++i)
            temp[i] = mod(temp[i - 2] - temp[i - 1]);
 
        for(int i = 0; i < 55; ++i)
            state[i] = temp[(34 * (i + 1)) % 55];
 
        pos = 54;
        for(int i = 55; i < 220; ++i)
            Next();
    }

    ///<summary>
    /// Returns a random number
    ///</summary>
    ///<param name="exclusiveUpper">The exclusive upper bound of the number to generate</param>
    public int Next(int exclusiveUpper = 1000000000) {
        
        int temp = mod(state[(pos + 1) % 55] - state[(pos + 32) % 55]);
        pos = (pos + 1) % 55;
        state[pos] = temp;
        return temp % exclusiveUpper;
    }
}