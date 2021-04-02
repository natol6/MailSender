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
    public class MessagePattern : Entity
    {
        [Required]
        public string Subject { get; set; }
        [Required]
        public string Body { get; set; }
        
    }
}
