using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailSender.lib.Models;
using MailSender.lib.Interfaces;
using MailSender.Data;
using Microsoft.EntityFrameworkCore;

namespace MailSender.Infrastructutre.Services
{
    public class DBRepositorySmtpServers : DBRepository<SmtpServer>
    {
        public DBRepositorySmtpServers(MailSenderDB db) : base( db)
        {

        }
        public override IEnumerable<SmtpServer> GetAll() => Set.Include(s => s.SmtpAccounts);
    }
}
