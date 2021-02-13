using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace WpfTest.Models
{
    class MessagePattern : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private int id;
        public int Id
        {
            get => id;
            set
            {
                id = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Id)));
            }
        }
        private string subject;
        public string Subject
        {
            get => subject;
            set
            {
                subject = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Subject)));
            }
        }
        private string body;
        public string Body
        {
            get => body;
            set
            {
                body = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Body)));
            }
        }
    }
}
