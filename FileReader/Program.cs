using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FileReader
{
    class Program
    {
        static object Locker = new object();
        static void Main(string[] args)
        {
            //GenerateFiles(100);

            string[] files = Directory.GetFiles("DataCatalog");
            Parallel.For(0, files.Length, i => FileAction(files[i]));
            foreach (string file in files)
            {
                Console.WriteLine(file);
            }
        }
        private static void FileAction(string filename)
        {
            string FileNameOut = "result.dat";
            using var reader = new StreamReader(filename);
            string[] data = reader.ReadToEnd().Split(" ");
            lock (Locker)
            {
                
                using var writer = new StreamWriter(FileNameOut, true, Encoding.UTF8);
                double temp1 = Double.Parse(data[1]);
                double temp2 = Double.Parse(data[2]);
                if (Int32.Parse(data[0]) == 1) writer.WriteLine("Файл - {0}:  ({1:F2}) * ({2:F2}) = {3:F2}", filename, temp1, temp2, temp1 * temp2);
                else writer.WriteLine("Файл - {0}:  ({1:F2}) / ({2:F2}) = {3:F2}", filename, temp1, temp2, (temp1 / temp2));
                Console.WriteLine("Вычисление выражения из файла {0} в потоке {1}", filename, Thread.CurrentThread.ManagedThreadId);
            }
            
            
        }

        private static void GenerateFiles(int quantity)
        {
            Random rnd = new Random();
            Directory.CreateDirectory("DataCatalog");
            for (int i = 1; i <= quantity; i++)
            {
                FileWriter($"DataCatalog\\Data_{i}.txt", $"{rnd.Next(1,3)} {(double)rnd.Next(-1000, 1001) + rnd.NextDouble()} {(double)rnd.Next(-1000, 1001) + rnd.NextDouble()}");
            }
            
        }

        private static void FileWriter(string FileName, string str)
        {
           File.WriteAllText(FileName, str);
 
        }
    }
}
