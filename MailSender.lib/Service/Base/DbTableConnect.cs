using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using MailSender.lib.Models;
using MailSender.lib.Data;

namespace MailSender.lib.Service.Base
{
    public abstract class DbTableConnect
    {
        public static IConfiguration Configuration { get; } = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", false, true)
            .Build();
        public static string ConnectionString => Configuration.GetConnectionString("Default");
        protected DBMailSender db = new DBMailSender(new DbContextOptionsBuilder<DBMailSender>()
            .UseSqlServer(ConnectionString).Options);
        public DbTableConnect()
        {
            db.Database.EnsureCreated();
        }
    }
}
