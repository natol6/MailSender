using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailSender.lib.Models;
using Microsoft.EntityFrameworkCore;

namespace MailSender.lib.Data
{
    public class DBMailSender : DbContext
    {
        public DbSet<MessageSendContainer> MessageSendContainers { get; set; }
        public DbSet<MessagePattern> MessagePatterns { get; set; }
        public DbSet<SmtpServer> SmtpServers { get; set; }
        public DbSet<SmtpAccount> SmtpAccounts { get; set; }
        public DbSet<EmailAddress> EmailAddresses { get; set; }
        public DBMailSender(DbContextOptions<DBMailSender> options) : base(options)
        {

        }
    }
}
