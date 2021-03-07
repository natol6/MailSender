using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Net;
using System.Net.Mail;
using System.ComponentModel.DataAnnotations;

namespace MailSender.lib.Models
{
    public class EmailAddress : Entity
    {
        [Required]
        public string Person_Company { get; set; }
        [Required]
        public string Email { get; set; }
        
    }
}
