using System;
//using System.Net;
//using System.Net.Mail;
using System.Windows;
using MailKit;
using MimeKit;
using MailKit.Net.Smtp;
using System.Threading;
using static System.Net.Mime.MediaTypeNames;
using System.Diagnostics;

namespace ConsoleTest
{
    public delegate void Callback(long count);
    class Program
    {
        

        static void Main(string[] args)
        {
            Console.Write("Введите число для рассчета факториала этого числа и суммы целых чисел от нуля до этого числа: ");
            long num = Int64.Parse(Console.ReadLine());
            
            long ansFactorial = 0;
            long ansSummaNum = 0;
            
            var threadFactorial = new Thread(() => ansFactorial = Factorial(num));
            var threadSummaNum = new Thread(() => ansSummaNum = SummaNum(num));
            Stopwatch stopwatch = new Stopwatch();
            Console.WriteLine("\nРассчет в разных потоках:");
            
            stopwatch.Start();
            threadFactorial.Start();
            threadSummaNum.Start();
            threadFactorial.Join();
            threadSummaNum.Join();
            stopwatch.Stop();
            
            Console.WriteLine($"Факториал введенного числа равен: {ansFactorial}");
            Console.WriteLine($"Сумма целых чисел от нуля до введенного числа равна: {ansSummaNum}");
            Console.WriteLine($"Время рассчета в разных потоках: {stopwatch.ElapsedMilliseconds} миллисекунд");
            
            Console.WriteLine("\nРассчет в одном потоке:");
            
            stopwatch.Start();
            Console.WriteLine($"Факториал введенного числа равен: {Factorial(num)}");
            Console.WriteLine($"Сумма целых чисел от нуля до введенного числа равна: {SummaNum(num)}");
            stopwatch.Stop();
            Console.WriteLine($"Время рассчета в одном потоке: {stopwatch.ElapsedMilliseconds} миллисекунд");
        }
        public static long Factorial(long val)
        {
            Console.WriteLine($"Поток {Thread.CurrentThread.ManagedThreadId} рассчет факториала");
            long ans = 1;
            for (int i = 2; i <= val; i++)
            {
                ans *= i;
            }
            return ans;
        }
        public static long SummaNum(long val)
        {
            Console.WriteLine($"Поток {Thread.CurrentThread.ManagedThreadId} рассчет суммы");
            long ans = 1;
            for (int i = 2; i <= val; i++)
            {
                ans += i;
            }
            return ans;
        }
        
    }
}
