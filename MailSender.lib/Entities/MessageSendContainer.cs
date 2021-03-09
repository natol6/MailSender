using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace MailSender.lib.Entities
{
    public class MessageSendContainer : Entity
    {
        [Required]
        public string SmtpServerUse { get; set; }
        [Required]
        public int PortUse { get; set; }
        [Required]
        public bool SSLUse { get; set; }
        [Required]
        public string SmtpAccountEmailUse { get; set; }
        [Required]
        public string SmtpAccountPasswordUse { get; set; }
        [Required]
        public string SmtpAccountPerson_CompanyUse { get; set; }
        [Required]
        public string EmailAddressesTo { get; set; }
        [Required]
        public string Subject { get; set; }
        [Required]
        public string Body { get; set; }
        [Required]
        public DateTime? SendDate { get; set; }
        [Required]
        public string Status { get; set; }
        
    }
}
