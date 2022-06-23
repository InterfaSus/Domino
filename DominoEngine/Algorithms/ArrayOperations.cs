namespace DominoEngine.Algorithms;

///<summary>
///Static class containing basic algorithms on arrays
///</summary>
public static class ArrayOperations {

    ///<summary>
    ///A method to find an element of type T on an array of type T. If the element exists, returns its position, otherwise returns -1
    ///</summary>
    ///<param name="array">The array where the element must be found</param>
    ///<param name="element">The element to find</param>
    public static int Find<T>(T?[] array, T? element) {

        for (int i = 0; i < array.Length; i++) {
            if (element!.Equals(array[i])) return i;
        }

        return -1;
    }

    ///<summary>
    ///Randomly shuffles the elements in an array
    ///</summary>
    ///<param name="array">The array to be shuffled</param>
    public static void RandomShuffle<T>(T[] array) {

        RandomGen r = new RandomGen();
        for (int i = 0; i < array.Length; i++) {

            int pos = r.Next(array.Length);
            T temp = array[i];
            array[i] = array[pos];
            array[pos] = temp;
        }
    }
}