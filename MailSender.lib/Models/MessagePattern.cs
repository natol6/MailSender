using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MailSender.lib.Models
{
    public class MessagePattern : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private int _Id;
        public int Id
        {
            get => _Id;
            set
            {
                _Id = value;
                OnPropertyChanged("Id");
            }
        }
        private string _Subject;
        public string Subject
        {
            get => _Subject;
            set
            {
                _Subject = value;
                OnPropertyChanged("Subject");
            }
        }
        private string _Body;
        public string Body
        {
            get => _Body;
            set
            {
                _Body = value;
                OnPropertyChanged("Body");
            }
        }
        private void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
