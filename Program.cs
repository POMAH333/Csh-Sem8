// Урок 8. Двумерные массивы. Продолжение

//-----------------------------------------------------------------------------------

while (true)
{
    Console.WriteLine();
    Console.WriteLine();
    Console.WriteLine();
    Console.WriteLine("Введите номер задачи:");
    Console.WriteLine("54) Задача 54: Задайте двумерный массив. Напишите программу, которая упорядочит по убыванию элементы каждой строки двумерного массива.");
    Console.WriteLine("56) Задача 56: Задайте прямоугольный двумерный массив. Напишите программу, которая будет находить строку с наименьшей суммой элементов.");
    Console.WriteLine("60) Задача 60. Сформируйте трёхмерный массив из неповторяющихся двузначных чисел. Напишите программу, которая будет построчно выводить массив, добавляя индексы каждого элемента.");
    Console.WriteLine("62) Задача 62. Напишите программу, которая заполнит спирально массив 4 на 4.");
    Console.WriteLine("58) Задача 58(исключённая): Задайте две матрицы. Напишите программу, которая будет находить произведение двух матриц.");
    Console.WriteLine();
    Console.WriteLine("0) Или введите 0 для выхода из программы");
    int menuNum = SetNumber("");
    Console.WriteLine();
    Console.WriteLine();
    Console.WriteLine();

    switch (menuNum)
    {
        case 0: return; break;
        case 54: Task54(); break;
        case 56: Task56(); break;
        case 60: Task60(); break;
        case 62: Task62(); break;
        case 58: Task58(); break;
        default: Console.WriteLine($"Задачи №{menuNum}, не существует"); break;
    }
}

//-----------------------------------------------------------------------------------

int SetNumber(string messageEnt)
{
    Console.WriteLine(messageEnt);
    int num = int.Parse(Console.ReadLine());
    return num;
}

void PrintMatrix(int[,] matrix)
{
    for (int i = 0; i < matrix.GetLength(0); i++)
    {
        for (int j = 0; j < matrix.GetLength(1); j++)
        {
            Console.Write($"{matrix[i, j]} ");
        }
        Console.WriteLine();
    }
}

int[,] GetMatrix(int rows, int columns, int minValue, int maxValue)
{
    int[,] matrix = new int[rows, columns];

    Random random = new Random();

    for (int i = 0; i < rows; i++)
    {
        for (int j = 0; j < columns; j++)
        {
            matrix[i, j] = random.Next(minValue, maxValue + 1);
        }
    }
    return matrix;
}



// Задача 54: Задайте двумерный массив. Напишите программу, которая упорядочит по убыванию элементы каждой строки двумерного массива.
// Например, задан массив:
// 1 4 7 2
// 5 9 2 3
// 8 4 2 4
// В итоге получается вот такой массив:
// 7 4 2 1
// 9 5 3 2
// 8 4 4 2

int[,] SortMatrix(int[,] matrix)
{
    int temp = 0;

    for (int i = 0; i < matrix.GetLength(0); i++)
    {
        for (int j = 0; j < matrix.GetLength(1) - 1; j++)
        {
            for (int k = 0; k < matrix.GetLength(1) - j - 1; k++)
            {
                if (matrix[i, k] < matrix[i, k + 1])
                {
                    temp = matrix[i, k];
                    matrix[i, k] = matrix[i, k + 1];
                    matrix[i, k + 1] = temp;
                }
            }
        }
    }
    return matrix;
}

void Task54()
{
    int rows = SetNumber("Введите количество строк:");
    int columns = SetNumber("Введите количество столбцов:");
    int min = SetNumber("Введите min:");
    int max = SetNumber("Введите max:");
    Console.WriteLine();

    int[,] matrix = GetMatrix(rows, columns, min, max);
    PrintMatrix(matrix);

    Console.WriteLine();
    Console.WriteLine();

    matrix = SortMatrix(matrix);
    PrintMatrix(matrix);
}



// Задача 56: Задайте прямоугольный двумерный массив. Напишите программу, которая будет находить строку с наименьшей суммой элементов.

// Например, задан массив:
// 1 4 7 2
// 5 9 2 3
// 8 4 2 4
// 5 2 6 7
// Программа считает сумму элементов в каждой строке и выдаёт номер строки с наименьшей суммой элементов: 1 строка

int MatrixMinSumRow(int[,] matrix)
{
    int minSumRow = 0;
    int minSum = 0;
    int sum = 0;

    for (int i = 0; i < matrix.GetLength(0); i++)
    {
        sum = 0;
        for (int j = 0; j < matrix.GetLength(1); j++)
        {
            sum += matrix[i, j];
        }
        if (i == 0) minSum = sum;
        if (sum < minSum)
        {
            minSum = sum;
            minSumRow = i;
        }
    }
    return minSumRow;
}

void Task56()
{
    int rows = SetNumber("Введите количество строк:");
    int columns = SetNumber("Введите количество столбцов:");
    int min = SetNumber("Введите min:");
    int max = SetNumber("Введите max:");
    Console.WriteLine();

    int[,] matrix = GetMatrix(rows, columns, min, max);

    PrintMatrix(matrix);

    Console.WriteLine();
    Console.WriteLine($"Наименьшая сумма элементов в {MatrixMinSumRow(matrix) + 1} строке"); // Строки нумеруются с 1, согласно примера решения
}



// Задача 60. ...Сформируйте трёхмерный массив из неповторяющихся двузначных чисел. Напишите программу, которая будет построчно выводить массив, добавляя индексы каждого элемента.
// Массив размером 2 x 2 x 2
// 66(0,0,0) 25(0,1,0)
// 34(1,0,0) 41(1,1,0)
// 27(0,0,1) 90(0,1,1)
// 26(1,0,1) 55(1,1,1)

int[,,] GetMatrix3D(int rows, int columns, int sites, int minValue, int maxValue)
{
    int[,,] matrix3D = new int[rows, columns, sites];

    for (int i = 0; i < rows; i++)
    {
        for (int j = 0; j < columns; j++)
        {
            for (int k = 0; k < sites; k++)
            {
                matrix3D[i, j, k] = FindUnique(matrix3D, minValue, maxValue);
            }
        }
    }
    return matrix3D;
}

int FindUnique(int[,,] matrix3D, int min, int max)
{
    Random random = new Random();
    bool unique = true;
    int result = 0;

    while (unique)
    {
        result = random.Next(min, max + 1);
        unique = false;
        for (int i = 0; i < matrix3D.GetLength(0); i++)
        {
            for (int j = 0; j < matrix3D.GetLength(1); j++)
            {
                for (int k = 0; k < matrix3D.GetLength(2); k++)
                {
                    if (matrix3D[i, j, k] == result) unique = true;
                }
            }
        }
    }
    return result;
}

void PrintMatrix3D(int[,,] matrix3D)
{
    for (int k = 0; k < matrix3D.GetLength(2); k++)
    {
        for (int i = 0; i < matrix3D.GetLength(0); i++)
        {
            for (int j = 0; j < matrix3D.GetLength(1); j++)
            {
                Console.Write($"{matrix3D[i, j, k]}({i},{j},{k}) ");
            }
            Console.WriteLine();
        }

    }
}

void Task60()
{
    int rows = SetNumber("Введите количество строк:");
    int columns = SetNumber("Введите количество столбцов:");
    int sites = SetNumber("Введите количество страниц:");
    Console.WriteLine();

    if ((rows * columns * sites) > 90)
    {
        Console.WriteLine("Массив такой размерности невозможно заполнить уникальными двузначными числами");
        return;
    }

    int[,,] matrix3D = GetMatrix3D(rows, columns, sites, 10, 99);
    PrintMatrix3D(matrix3D);
}



// Задача 62. Напишите программу, которая заполнит спирально массив 4 на 4.
// Например, на выходе получается вот такой массив:
// 01 02 03 04
// 12 13 14 05
// 11 16 15 06
// 10 09 08 07

int[,] GetMatrixSpiral(int rows, int columns)
{
    int[,] matrix = new int[rows, columns];
    int num = 0;
    int startRow = 0;
    int endRow = rows - 1;
    int startCol = 0;
    int endCol = columns - 1;

    for (; endRow >= startRow && endCol >= startCol;)
    {
        for (int j = startCol; j <= endCol; j++)
        {
            num++;
            matrix[startRow, j] = num;
        }

        startRow++;

        for (int i = startRow; i <= endRow; i++)
        {
            num++;
            matrix[i, endCol] = num;
        }

        endCol--;

        for (int j = endCol; j >= startCol && endRow >= startRow; j--)
        {
            num++;
            matrix[endRow, j] = num;
        }

        endRow--;

        for (int i = endRow; i >= startRow && endCol >= startCol; i--)
        {
            num++;
            matrix[i, startCol] = num;
        }

        startCol++;
    }

    return matrix;
}

void Task62()
{
    int[,] matrix = GetMatrixSpiral(4, 4);
    PrintMatrix(matrix);
}



// Задача 58: Задайте две матрицы. Напишите программу, которая будет находить произведение двух матриц.
// Например, даны 2 матрицы:
// 2 4 | 3 4
// 3 2 | 3 3
// Результирующая матрица будет:
// 18 20
// 15 18

int[,] CompositionMatrix(int[,] matrix1, int[,] matrix2)
{
    int[,] result = new int[matrix1.GetLength(0), matrix2.GetLength(1)];

    for (int i = 0; i < result.GetLength(0); i++)
    {
        for (int j = 0; j < result.GetLength(1); j++)
        {
            for (int r = 0; r < matrix1.GetLength(1); r++)
            {
                result[i, j] += matrix1[i, r] * matrix2[r, j];
            }
        }
    }
    return result;
}

void Task58()
{
    int rows1 = SetNumber("Введите количество строк матрицы 1:");
    int columns1 = SetNumber("Введите количество столбцов матрицы 1:");
    int rows2 = SetNumber("Введите количество строк матрицы 2:");
    int columns2 = SetNumber("Введите количество столбцов матрицы 2:");
    int min = SetNumber("Введите min:");
    int max = SetNumber("Введите max:");
    Console.WriteLine();

    if (columns1 != rows2)
    {
        Console.WriteLine("Невозможно найти произведение таких матриц");
        return;
    }

    int[,] matrix1 = GetMatrix(rows1, columns1, min, max);
    PrintMatrix(matrix1);
    Console.WriteLine();

    int[,] matrix2 = GetMatrix(rows2, columns2, min, max);
    PrintMatrix(matrix2);
    Console.WriteLine();

    int[,] matrixComp = CompositionMatrix(matrix1, matrix2);

    PrintMatrix(matrixComp);

    Console.WriteLine();
    Console.WriteLine();
}


