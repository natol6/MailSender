using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataRead.ViewModels.Base;
using System.Collections.ObjectModel;
using DataRead.Models;
using DataRead.Commands;
using System.Windows.Input;
using System.Security;
using System.Windows;
using System.ComponentModel;
using System.Threading;
using DataRead.Service;
using Microsoft.Win32;
using System.IO;

namespace DataRead.ViewModels
{
    class MainWindowViewModel : ViewModel
    {
        private string _Title = "Адресная книга";
        public string Title
        {
            get => _Title;
            set => Set(ref _Title, value);
        }
        private ObservableCollection<Person> _Persons;
        public ObservableCollection<Person> Persons
        {
            get => _Persons;
            set => Set(ref _Persons, value);
        }

        private Person _SelectedPerson;
        public Person SelectedPerson
        {
            get => _SelectedPerson;
            set => Set(ref _SelectedPerson, value);

        }

        public MainWindowViewModel()
        {

        }
        
        private static IEnumerable<Person> LoadDataExcel(string FileName)
        {
            var persons = new List<Person>();
            var thread_id = Thread.CurrentThread.ManagedThreadId;

            foreach (var row in Excel.File(FileName)["senders"].Skip(1))
            {
                var values = row.Values.ToArray();
                persons.Add(new Person {
                    Id = int.Parse(values[0]),
                    LastName = values[1],
                    Name = values[2],
                    Patronymic = values[3],
                    Address = values[4]
                });
            }
            MessageBox.Show($"Поток {thread_id} завершил загрузку данных."

               , "Загрузка данных", MessageBoxButton.OK, MessageBoxImage.Information);
            return persons;
        }
        #region Commands

        private ICommand _OpenFileExcel;
        public ICommand OpenFileExcel => _OpenFileExcel ??= new LambdaCommand(OnOpenFileExcelExecuted);
        
        private void OnOpenFileExcelExecuted(object p)
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

            var load_data_thread = new Thread(() => Persons = new ObservableCollection<Person>(LoadDataExcel(file_name)));
            load_data_thread.Start();
        }
        private ICommand _OpenFileScv;
        public ICommand OpenFileScv => _OpenFileScv ??= new LambdaCommand(OnOpenFileScvExecuted);
        
        private void OnOpenFileScvExecuted(object p)
        {
            var open_dialog = new OpenFileDialog
            {
                Filter = "Csv (*.csv)|*.csv|Все файлы (*.*)|*.*",
                InitialDirectory = Environment.CurrentDirectory,
                Title = "Выбор файла для чтения"
            };

            if (open_dialog.ShowDialog() != true) return;

            var file_name = open_dialog.FileName;

            if (!File.Exists(file_name)) return;

            var load_data_Csv_thread = new Thread(() => Persons = new ObservableCollection<Person>(LoadDataCsv(file_name)));
            load_data_Csv_thread.Start();
            
        }
        private static IEnumerable<Person> LoadDataCsv(string FileName)
        {
            var persons = new List<Person>();
            var thread_id = Thread.CurrentThread.ManagedThreadId;
            FileStream fs = new FileStream(FileName, FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(fs);
            string[] title = sr.ReadLine().Split(';');
            while (!sr.EndOfStream)
            {
                string[] str = sr.ReadLine().Split(';');
                persons.Add(new Person
                {
                    Id = int.Parse(str[0]),
                    LastName = str[1],
                    Name = str[2],
                    Patronymic = str[3],
                    Address = str[4]
                });

            }
            sr.Close();
            fs.Close();
            MessageBox.Show($"Поток {thread_id} завершил загрузку данных."

               , "Загрузка данных", MessageBoxButton.OK, MessageBoxImage.Information);
            return persons;
        }
        private ICommand _SaveTxt;
        public ICommand SaveTxt => _SaveTxt ??= new LambdaCommand(OnSaveTxtExecuted, CanSaveTxtExecuted);
        private bool CanSaveTxtExecuted(object p)
        {
            return Persons != null &&
                Persons.Count() > 0;
        }
        private void OnSaveTxtExecuted(object p)
        {
            string FileName = "OutputData";
            var save_data_thread_txt = new Thread(() => SaveDataTxt(FileName));
            save_data_thread_txt.Start();
        }
        private void SaveDataTxt(string FileName)
        {
            var thread_id = Thread.CurrentThread.ManagedThreadId;
                        
            using var writer = new StreamWriter(FileName, false, Encoding.UTF8);
            writer.WriteLine("id: Фамилия: Имя: Отчество: Адрес:");
            for (int i = 0; i < Persons.Count(); i++)
            {
                writer.WriteLine($"{Persons[i].Id} {Persons[i].LastName} {Persons[i].Name} {Persons[i].Patronymic} {Persons[i].Address}");
            }
            MessageBox.Show($"Поток {thread_id} завершил сохранение данных. Данные сохранены в файле OutputData.txt"

               , "Загрузка данных", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        private ICommand _GenerateData;
        public ICommand GenerateData => _GenerateData ??= new LambdaCommand(OnGenerateDataExecuted);
       
        private void OnGenerateDataExecuted(object p)
        {
            
            var generate_data_thread_txt = new Thread(() => ExcelCreator.Create());
            generate_data_thread_txt.Start();
            
        }
        private ICommand _Exit;
        public ICommand Exit => _Exit ??= new LambdaCommand(OnExitExecuted);
        
        private void OnExitExecuted(object p)
        {
            var window = p as Window;
            if (window is null)
                window = Application.Current.Windows
                    .Cast<Window>()
                    .FirstOrDefault(w => w.IsFocused);
            if (window is null)
                window = Application.Current.Windows
                .Cast<Window>()
                .FirstOrDefault(w => w.IsActive);
            window?.Close();
        }
        #endregion

    }
}