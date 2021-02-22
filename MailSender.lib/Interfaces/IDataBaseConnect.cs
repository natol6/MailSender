using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailSender.lib.Models;

namespace MailSender.lib.Interfaces
{
    public interface IDatabaseTable<T>
    {
        public T AddDb(T obj);
        public void UpdateDb(T obj);
        public void DeleteDb(T obj);
    }
    public interface IDataBaseConnect : IDatabaseTable<SmtpAccount>, IDatabaseTable<SmtpServer>, IDatabaseTable<MessagePattern>
        , IDatabaseTable<EmailAddress>, IDatabaseTable<MessageSendContainer>
    {
        public IEnumerable<SmtpAccount> DBGetSmtpAccounts();
        public IEnumerable<SmtpServer> DBGetSmtpServers();
        public IEnumerable<MessagePattern> DBGetMessagePatterns();
        public IEnumerable<EmailAddress> DBGetEmailAddresses();
        public IEnumerable<MessageSendContainer> DBGetMessageSendContainers();
    }
}
