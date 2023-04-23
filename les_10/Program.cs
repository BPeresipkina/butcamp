// Сортировка подсчетом

int[] array = { 100, 1001, 105, 555, -111, 1006 };
int[] sortingArray = CountingSortExtended(array);
Console.WriteLine(string.Join(", ", sortingArray));
// Console.WriteLine(array.Max());

void CountingSort(int[] inputArray) // Метод для цифр (от 0 до 9)
{
    int[] counters = new int[10]; // Массив повторений
    for (int i = 0; i < inputArray.Length; i++)
    {
        counters[inputArray[i]]++;
        // int ourNumber = inputArray[i];
        // counters[ourNumber]++;
    }
    int index = 0;
    for (int i = 0; i < counters.Length; i++)
    {
        for (int j = 0; j < counters[i]; j++)
        {
            inputArray[index] = i;
            index++;
        }
    }
}

int[] CountingSortExtended(int[] inputArray)
{
    int max = inputArray.Max();
    int min = inputArray.Min();

    int offset = -min;

    int[] sortedArray = new int[inputArray.Length];

    int[] counters = new int[max + 1 + offset];
    for (int i = 0; i < inputArray.Length; i++)
    {
        counters[inputArray[i] + offset]++;
    }
    int index = 0;
    for (int i = 0; i < counters.Length; i++)
    {
        for (int j = 0; j < counters[i]; j++)
        {
            sortedArray[index] = i - offset;
            index++;
        }
    }
    return sortedArray;
}