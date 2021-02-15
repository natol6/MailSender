using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WpfTest.Models;

namespace WpfTest.Data
{
    class DBMailSender : DbContext
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<MessagePattern> MessagePatterns { get; set; }
        public DbSet<MailService> MailServices { get; set; }
        public DBMailSender(DbContextOptions<DBMailSender> options) : base(options)
        {

        }
    }
}
