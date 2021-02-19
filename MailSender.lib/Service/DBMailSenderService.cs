using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using MailSender.lib.Models;
using MailSender.lib.Data;

namespace MailSender.lib.Service
{
    public class DBMailSenderService
    {
        public static IConfiguration Configuration { get; } = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", false, true)
            .Build();
        public static string ConnectionString => Configuration.GetConnectionString("Default");
        private DBMailSender db = new DBMailSender(new DbContextOptionsBuilder<DBMailSender>()
            .UseSqlServer(ConnectionString).Options);
        public DBMailSenderService()
        {
            db.Database.EnsureCreated();
        }
        //public IEnumerable<SmtpAccount> DbGetAccounts()
        //{
            //return db.Account.ToArray();
        //}
        public IEnumerable<SmtpServer> DbGetMailServices()
        {
            return db.SmtpServers.ToArray();
        }
        public IEnumerable<MessagePattern> DbGetMessagePattern()
        {
            return db.MessagePatterns.ToArray();
        }
        public IEnumerable<MessageSendContainer> DbGetMessageSendContainer()
        {
            return db.MessageSendContainers.ToArray();
        }
        public MessagePattern AddBdMessagePattern(MessagePattern mp)
        {
            db.MessagePatterns.Add(mp);
            db.SaveChanges();
            return db.MessagePatterns.OrderBy(d => d.Id).LastOrDefault();
        }
        public void DeleteBdMessagePattern(int id)
        {
            MessagePattern mp = db.MessagePatterns.FirstOrDefault(d => d.Id == id);
            if (mp != null)
            {
                db.MessagePatterns.Remove(mp);
                db.SaveChanges();
            }
        }
        public void UpdateBdMessagePattern(MessagePattern mp)
        {
            MessagePattern mpdb = db.MessagePatterns.FirstOrDefault(d => d.Id == mp.Id);
            if (mpdb != null)
            {
                mpdb.Subject = mp.Subject;
                mpdb.Body = mp.Body;
                db.MessagePatterns.Update(mpdb);
                db.SaveChanges();
            }

        }
        /*public Account AddBdAccount(Account ac)
        {
            db.Accounts.Add(ac);
            db.SaveChanges();
            return db.Accounts.OrderBy(d => d.Id).LastOrDefault();
        }
        public void DeleteBdAccount(int id)
        {
            Account ac = db.Accounts.FirstOrDefault(d => d.Id == id);
            if (ac != null)
            {
                db.Accounts.Remove(ac);
                db.SaveChanges();
            }
        }
        public void UpdateBdAccount(Account ac)
        {
            Account acdb = db.Accounts.FirstOrDefault(d => d.Id == ac.Id);
            if (acdb != null)
            {
                acdb.Login = ac.Login;
                acdb.SecurePassword = ac.SecurePassword;
                acdb.Person = ac.Person;
                db.Accounts.Update(acdb);
                db.SaveChanges();
            }

        }
        public MailService AddBdMailService(MailService ms)
        {
            db.MailServices.Add(ms);
            db.SaveChanges();
            return db.MailServices.OrderBy(d => d.Id).LastOrDefault();
        }
        public void DeleteBdMailService(int id)
        {
            MailService ms = db.MailServices.FirstOrDefault(d => d.Id == id);
            if (ms != null)
            {
                db.MailServices.Remove(ms);
                db.SaveChanges();
            }
        }
        public void UpdateBdMailService(MailService ms)
        {
            MailService msdb = db.MailServices.FirstOrDefault(d => d.Id == ms.Id);
            if (msdb != null)
            {
                msdb.Title = ms.Title;
                msdb.SmtpServer = ms.SmtpServer;
                msdb.DomainName = ms.DomainName;
                db.MailServices.Update(msdb);
                db.SaveChanges();
            }

        }*/
    }
}
