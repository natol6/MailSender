using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MatrixMultiplication
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Задайте начальную размерность квадратных матриц для умножения:");
            int begin = Int32.Parse(Console.ReadLine());
            Console.Write("Задайте конечную размерность квадратных матриц для умножения:");
            int end = Int32.Parse(Console.ReadLine());
            Console.Write("Задайте шаг изменения размерности квадратных матриц для умножения:");
            int step = Int32.Parse(Console.ReadLine());
            TimeSpan time;
            Console.WriteLine("\n\n");
            int num = 5;
            Random rnd = new Random();
            Stopwatch stopwatch = new Stopwatch();
            int quantity = (end - begin) / step + 1;
            double[,] rezultNoParallel = new double[num + 2, quantity];
            double[,] rezultParallel = new double[num + 2, quantity];
            for (int m = 0; m < quantity; m++)
            {
                int temp = begin + m * step;
                rezultNoParallel[0, m] = temp;
                rezultParallel[0, m] = temp;
                for (int k = 0; k < num; k++)
                {
                    int[,] matrix1 = new int[temp, temp];
                    int[,] matrix2 = new int[temp, temp];
                    int[,] matrixMultiplication = new int[temp, temp];
                    int[,] matrixThread = new int[temp, temp];

                    for (int i = 0; i < temp; i++)
                    {
                        for (int j = 0; j < temp; j++)
                        {
                            matrix1[i, j] = rnd.Next(0, 10);
                            matrix2[i, j] = rnd.Next(0, 10);
                        }
                    }
                    /*Console.WriteLine("Сгенерированная первая матрица:\n");
                    for (int i = 0; i < n; i++)
                    {
                        for (int j = 0; j < n; j++)
                        {
                            Console.Write($"{matrix1[i, j]} ");
                        }
                        Console.WriteLine();
                    }
                    Console.WriteLine("\n\nСгенерированная вторая матрица:\n");
                    for (int i = 0; i < n; i++)
                    {
                        for (int j = 0; j < n; j++)
                        {
                            Console.Write($"{matrix2[i, j]} ");
                        }
                        Console.WriteLine();
                    }*/
                    stopwatch.Restart();
                    for (int i = 0; i < temp; i++)
                    {
                        for (int j = 0; j < temp; j++)
                        {
                            Multiplicate(matrix1, matrix2, matrixMultiplication, matrixThread, i, j);
                        }

                    }
                    stopwatch.Stop();
                    time = stopwatch.Elapsed;
                    rezultNoParallel[k + 1, m] = time.TotalSeconds;
                    rezultNoParallel[num + 1, m] += time.TotalSeconds;
                    /*
                    Console.WriteLine("\n\nМатрица, полученная в результате умножения первой матрицы на вторую без применения параллельных потоков:");
                    Console.WriteLine($"(Время выполнения {stopwatch.ElapsedMilliseconds} миллисекунд)\n");
                    for (int i = 0; i < n; i++)
                    {
                        for (int j = 0; j < n; j++)
                        {
                            Console.Write($"{matrixMultiplication[i, j]} ");
                        }
                        Console.WriteLine();
                    }
                    Console.WriteLine("\n\nМатрица потоков:");
                    for (int i = 0; i < n; i++)
                    {
                        for (int j = 0; j < n; j++)
                        {
                            Console.Write($"{matrixThread[i, j]} ");
                        }
                        Console.WriteLine();
                    }*/
                    stopwatch.Restart();
                    Parallel.For(0, temp, i => Parallel.For(0, temp, j => Multiplicate(matrix1, matrix2, matrixMultiplication, matrixThread, i, j)));
                    stopwatch.Stop();
                    time = stopwatch.Elapsed;
                    rezultParallel[k + 1, m] = time.TotalSeconds;
                    rezultParallel[num + 1, m] += time.TotalSeconds;
                    //Console.WriteLine("\n\nМатрица, полученная в результате умножения первой матрицы на вторую с применением параллельных потоков:");
                    //Console.WriteLine($"(Время выполнения {stopwatch.ElapsedMilliseconds} миллисекунд)\n");
                    /*for (int i = 0; i < n; i++)
                    {
                        for (int j = 0; j < n; j++)
                        {
                            Console.Write($"{matrixMultiplication[i, j]} ");
                        }
                        Console.WriteLine();
                    }
                    Console.WriteLine("\n\nМатрица потоков:");
                    for (int i = 0; i < n; i++)
                    {
                        for (int j = 0; j < n; j++)
                        {
                            Console.Write($"{matrixThread[i, j]} ");
                        }
                        Console.WriteLine();
                    }*/
                }
                rezultNoParallel[num + 1, m] /= num;
                rezultParallel[num + 1, m] /= num;
            }
            
            RezToString(rezultNoParallel, rezultParallel);
            RezToFile(rezultNoParallel, rezultParallel);

            Console.ReadLine();
        }
        private static void Multiplicate(int[,] arr1, int[,] arr2, int[,] arrans, int[,] arrThread, int ii, int jj)
        {
            int ans = 0;
            for (int i = 0; i < arr1.GetLength(0); i++)
            {
                ans += arr1[ii, i] * arr2[i, jj];
            }
            arrans[ii, jj] = ans;
            arrThread[ii, jj] = Thread.CurrentThread.ManagedThreadId;
                        
        }
        private static void RezToString(double[,] arrNoPar, double[,] arrPar)
        {
            int num = arrPar.GetLength(0);
            int quantity = arrPar.GetLength(1);
            string[] title = new string[num];
            title[0] = "Размерность матриц:";
            for (int i = 1; i < title.Length - 1; i++)
            {
                title[i] = $"Время операции - {i}:";
            }
            title[num - 1] = "Среднее время:";
            Console.Write($"{"Один поток:",40}");
            Console.WriteLine($"{"Параллельные потоки:",30}\n");
            for (int i = 0; i < quantity; i++)
            {
                Console.Write($"{title[0],-20}");
                Console.WriteLine($"{arrPar[0,i]}\n");
                for (int j = 1; j < num; j++)
                {
                    Console.Write($"{title[j],-20}");
                    Console.Write($"{arrNoPar[j, i],20:F7} ");
                    Console.WriteLine($"{arrPar[j, i],30:F7} ");
                }
                Console.WriteLine();
            }

        }
        private static void RezToFile(double[,] arrNoPar, double[,] arrPar)
        {
            string FileName = "OutputData";
            using var writer = new StreamWriter(FileName, false, Encoding.UTF8);

            writer.WriteLine("Студент: Нестеров Олег Николаевич");
            writer.WriteLine("Операционная система (номер версии):  {0}", Environment.OSVersion);
            writer.WriteLine("Разрядность процессора:  {0}", Environment.GetEnvironmentVariable("PROCESSOR_ARCHITECTURE"));
            writer.WriteLine("Модель процессора:  {0}", Environment.GetEnvironmentVariable("PROCESSOR_IDENTIFIER"));
            writer.WriteLine("Процессор:  Intel(R) Core(TM) i5-5300U CPU @ 2.30GHz");
            writer.WriteLine("Оперативная память:  8.00 ГБ");
            writer.WriteLine("Среда программирования: Microsoft Visual Studio Community 2019, C#, консольное приложение .NET Core 5.0\n");
            writer.WriteLine("\nРезультаты сравнения времени умножения квадратных матриц в одном потоке (1 столбец) и параллельных потоках (2 столбец). Время работы в секундах:\n");
            int num = arrPar.GetLength(0);
            int quantity = arrPar.GetLength(1);
            string[] title = new string[num];
            title[0] = "Размерность матриц:";
            for (int i = 1; i < title.Length - 1; i++)
            {
                title[i] = $"Время операции - {i}:";
            }
            title[num - 1] = "Среднее время:";
            writer.Write($"{"Один поток:",40}");
            writer.WriteLine($"{"Параллельные потоки:",30}\n");
            for (int i = 0; i < quantity; i++)
            {
                writer.Write($"{title[0],-20}");
                writer.WriteLine($"{arrPar[0, i]}\n");
                for (int j = 1; j < num; j++)
                {
                    writer.Write($"{title[j],-20}");
                    writer.Write($"{arrNoPar[j, i],20:F7} ");
                    writer.WriteLine($"{arrPar[j, i],30:F7} ");
                }
                writer.WriteLine();
            }
        }
    }
}
