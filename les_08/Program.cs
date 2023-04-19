const int N = 1001; // размер матрицы
const int THREADS_NUMBER = 10;

int[,] serialMulRes = new int[N, N]; // результат выполнения умножения в однопотоке
int[,] treadMulRes = new int[N, N]; // результат параллельного умножения матриц

int[,] firstMatrix = MatrixGeneration(N, N);
int[,] secondMatrix = MatrixGeneration(N, N);

SerialMatruxMul(firstMatrix, secondMatrix);
PrepareParallelMatrix(firstMatrix, secondMatrix);

Console.WriteLine(EqualityMatrix(serialMulRes, treadMulRes));

int[,] MatrixGeneration(int rows, int columns)
{
    Random _rand = new Random();
    int[,] res = new int[rows, columns];
    for (int i = 0; i < res.GetLength(0); i++)
    {
        for (int j = 0; j < res.GetLength(1); j++)
        {
            res[i, j] = _rand.Next(-100, 100);
        }
    }
    return res;
}

void SerialMatruxMul(int[,] a, int[,] b)
{
    if (a.GetLength(1) != b.GetLength(0)) throw new System.Exception("Нельзя умножить такие матрицы");
    for (int i = 0; i < a.GetLength(0); i++)
    {
        for (int j = 0; j < b.GetLength(1); j++)
        {
            for (int k = 0; k < b.GetLength(0); k++)
            {
                serialMulRes[i, j] += a[i, k] * b[k, j];
            }
        }
    }
}

void PrepareParallelMatrix(int[,] a, int[,] b)
{
    if (a.GetLength(1) != b.GetLength(0)) throw new System.Exception("Нельзя умножить такие матрицы");
    int eachThreadCalc = N / THREADS_NUMBER;
    var threadsList = new List<Thread>();
    for (int i = 0; i < THREADS_NUMBER; i++)
    {
        int startPos = i * eachThreadCalc;
        int endPos = (i + 1) * eachThreadCalc;
        // если последний поток
        if (i == THREADS_NUMBER - 1) endPos = N;
        threadsList.Add(new Thread(() => ParallelMatruxMul(a, b, startPos, endPos)));
        threadsList[i].Start();
    }
    for (int i = 0; i < THREADS_NUMBER; i++)
    {
        threadsList[i].Join();
    }
}

void ParallelMatruxMul(int[,] a, int[,] b, int startPos, int endPos)
{
    for (int i = startPos; i < endPos; i++)
    {
        for (int j = 0; j < b.GetLength(1); j++)
        {
            for (int k = 0; k < b.GetLength(0); k++)
            {
                treadMulRes[i, j] += a[i, k] * b[k, j];
            }
        }
    }
}

bool EqualityMatrix(int[,] fmatrix, int[,] smatrix)
{
    bool res = true;

    for (int i = 0; i < fmatrix.GetLength(0); i++)
    {
        for (int j = 0; j < fmatrix.GetLength(1); j++)
        {
            res = res && (fmatrix[i, j] == smatrix[i, j]);
        }
    }
    return res;
}

