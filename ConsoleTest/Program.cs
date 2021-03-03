using System;
//using System.Net;
//using System.Net.Mail;
using System.Windows;
using MailKit;
using MimeKit;
using MailKit.Net.Smtp;
using System.Threading;
using static System.Net.Mime.MediaTypeNames;

namespace ConsoleTest
{
    public delegate void Callback(long count);
    class Program
    {
        

        static void Main(string[] args)
        {
            Console.Write("Введите число для рассчета факториала этого числа и суммы целых чисел от нуля до этого числа: ");
            long num = Int64.Parse(Console.ReadLine());
            DataValue factorial = new DataValue(num, PrintFactorial);
            DataValue summanum = new DataValue(num, PrintSummaNum);
            
            var threadFactorial = new Thread(() => factorial.ThreadFactorial());
            var threadSummaNum = new Thread(() => summanum.ThreadSummaNum());
            Console.WriteLine("\nРассчет в разных потоках:");
            DateTime begin = DateTime.Now;
            threadFactorial.Start();
            threadSummaNum.Start();
            threadFactorial.Join();
            threadSummaNum.Join();
            DateTime end = DateTime.Now;
            Console.WriteLine($"Время рассчета в разных потоках: {end - begin}");
            
            Console.WriteLine("\nРассчет в одном потоке:");
            begin = DateTime.Now;
            Console.WriteLine($"Факториал введенного числа равен: {Factorial(num)}");
            Console.WriteLine($"Сумма целых чисел от нуля до введенного числа равна: {SummaNum(num)}");
            end = DateTime.Now;
            Console.WriteLine($"Время рассчета в одном потоке: {end - begin}");
        }
        public static long Factorial(long val)
        {
            long ans = 1;
            for (int i = 2; i <= val; i++)
            {
                ans *= i;
            }
            return ans;
        }
        public static long SummaNum(long val)
        {
            long ans = 1;
            for (int i = 2; i <= val; i++)
            {
                ans += i;
            }
            return ans;
        }
        public static void PrintFactorial(long val)
        {
            Console.WriteLine($"Факториал введенного числа равен: {val}");
        }
        public static void PrintSummaNum(long val)
        {
            Console.WriteLine($"Сумма целых чисел от нуля до введенного числа равна: {val}");
        }
    }
}
