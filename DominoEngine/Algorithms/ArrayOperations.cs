namespace DominoEngine.Algorithms;

public static class ArrayOperations<T> {

    public static int Find(T?[] array, T? element) {

        for (int i = 0; i < array.Length; i++) {
            if (element!.Equals(array[i])) return i;
        }

        return -1;
    }
}