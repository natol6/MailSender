using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.ComponentModel.DataAnnotations;

namespace MailSender.lib.Entities
{
    public class SmtpAccount : Entity
    {
        [Required]
        public string AccountEmail { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Person_Company { get; set; }
        [Required]
        public SmtpServer Smtp_Server { get; set; }
        
    }
}
