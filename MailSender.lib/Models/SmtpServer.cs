using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace MailSender.lib.Models
{
    public class SmtpServer : Entity
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string SmtpServ { get; set; }
        [Required]
        public int Port { get; set; }
        [Required]
        public bool UseSSL { get; set; }
       
        public ICollection<SmtpAccount> SmtpAccounts { get; set; } = new ObservableCollection<SmtpAccount>();
        
    }
}
