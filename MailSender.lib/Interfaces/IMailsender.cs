using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailSender.lib.Entities;

namespace MailSender.lib.Interfaces
{
    public interface IMailsender
    {
        public string SendMessage(MessageSendContainer msc);
    }
}
