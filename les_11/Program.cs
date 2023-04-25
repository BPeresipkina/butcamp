// Сортировка подсчетом
const int THREADS_NUMBER = 4;
const int N = 10000; // размер массива
object locker = new object();

int[] array = { 100, 1001, 105, 555, -111, 1006 };
CountingSortExtended(array);

Random rand = new Random();
int[] resSerial = new int[N].Select(r => rand.Next(0, 5)).ToArray();
int[] resParallel = new int[N];

Array.Copy(resSerial, resParallel, N);

CountingSortExtended(resSerial);
PrepareParallelCountingSort(resParallel);
Console.WriteLine(EqualityMatrix(resSerial, resParallel));

// Console.WriteLine(string.Join(", ", resSerial));
// Console.WriteLine(string.Join(", ", resParallel));

void PrepareParallelCountingSort(int[] inputArray)
{
    int max = inputArray.Max();
    int min = inputArray.Min();

    int offset = -min;
    int[] counters = new int[max + offset + 1];

    int eachThreadCalc = N / THREADS_NUMBER;
    var threadsParallel = new List<Thread>();

    for (int i = 0; i < THREADS_NUMBER; i++)
    {
        int startPos = i * eachThreadCalc;
        int endPos = (i + 1) * eachThreadCalc;
        if (i == THREADS_NUMBER - 1) endPos = N;
        threadsParallel.Add(new Thread(() => CountingSortParallel(inputArray, counters, offset, startPos, endPos)));
        threadsParallel[i].Start();
    }

    foreach(var thread in threadsParallel)
    {
        thread.Join();
    }

    int index = 0;
    for (int i = 0; i < counters.Length; i++)
    {
        for (int j = 0; j < counters[i]; j++)
        {
            inputArray[index] = i - offset;
            index++;
        }
    }
}

void CountingSortParallel(int[] inputArray, int[] counters, int offset, int startPos, int endPos)
{
    for (int i = startPos; i < endPos; i++)
    {
        lock (locker) // thread 2 ждет пока инфу внесет thread 1 и наоборот
        {
        counters[inputArray[i] + offset]++;
        }
    }
}

void CountingSortExtended(int[] inputArray)
{
    int max = inputArray.Max();
    int min = inputArray.Min();

    int offset = -min;

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
            inputArray[index] = i - offset;
            index++;
        }
    }
}

bool EqualityMatrix(int[] fmatrix, int[] smatrix)
{
    bool res = true;

    for (int i = 0; i < N; i++)
    {
        res = res && (fmatrix[i] == smatrix[i]);
    }
    return res;
}