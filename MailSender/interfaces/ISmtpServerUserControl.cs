using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using MailSender.lib.Models;

namespace MailSender.interfaces
{
    public interface ISmtpServerUserControl
    {
        public ObservableCollection<SmtpServer> SmtpServers { get; set; }
        public void LoadSmtpServers();
    }
}
