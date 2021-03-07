using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Net;
using System.Net.Mail;

namespace MailSender.lib.Models
{
    public class Entity //: INotifyPropertyChanged
    {
        //public event PropertyChangedEventHandler PropertyChanged;
        //protected virtual void OnPropertyChanged([CallerMemberName] string PropertyName = null)
        //{
            //PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        //}
        //protected virtual void Set<T>(ref T field, T value, [CallerMemberName] string PropertyName = null)
        //{
            //if (Equals(field, value)) return;
            //field = value;
            //OnPropertyChanged(PropertyName);
        //}
        
        public int Id { get; set; }
        
        
        
    }
}
