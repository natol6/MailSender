using DataRead.Service;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace DataRead
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        public record Person(int Id, string LastName, string Name, string Patronymic, string Address);
        private void OpenFile_Click(object sender, RoutedEventArgs e)
        {
            var ui_thread_id = Thread.CurrentThread.ManagedThreadId;

            var open_dialog = new OpenFileDialog
            {
                Filter = "Excel (*.xlsx)|*.xlsx|Все файлы (*.*)|*.*",
                InitialDirectory = Environment.CurrentDirectory,
                Title = "Выбор файла для чтения"
            };

            if (open_dialog.ShowDialog() != true) return;

            var file_name = open_dialog.FileName;

            if (!File.Exists(file_name)) return;

            //var load_data_thread = new Thread(() => LoadData(file_name));
            //load_data_thread.Start();
            LoadData(file_name);
        }

        private void LoadData(string FileName)
        {
            var persons = GetPersons(FileName);
            var work_thread_id = Thread.CurrentThread.ManagedThreadId;

            //Application.Current.Dispatcher.Invoke(() =>
            //{
                //var ui_thread_id = Thread.CurrentThread.ManagedThreadId;
                //return Data.ItemsSource = persons;
            //});
            Data.Dispatcher.Invoke(() => Data.ItemsSource = persons);
        }

        private static IEnumerable<Person> GetPersons(string FileName)
        {
            var persons = new List<Person>();
            var thread_id = Thread.CurrentThread.ManagedThreadId;

            foreach (var row in Excel.File(FileName)["senders"].Skip(1))
            {
                var values = row.Values.ToArray();
                persons.Add(new Person(
                    int.Parse(values[0]),
                    values[1],
                    values[2],
                    values[3],
                    values[4]
                ));
            }
            return persons;
        }

        private void CreateFile_Click(object sender, RoutedEventArgs e)
        {
            ExcelCreator.Create();
            MessageBox.Show("Список адресатов сгенерирован и сохранен в файле 'senders.csv'", "Генерация списка адресатов", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
