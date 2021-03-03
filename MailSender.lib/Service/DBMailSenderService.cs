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
        public IEnumerable<SmtpAccount> DbGetAccounts()
        {
            return db.SmtpAccounts.ToArray();
        }
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
        public SmtpAccount AddBdSmtpAccount(SmtpAccount ac)
        {
            db.SmtpAccounts.Add(ac);
            db.SaveChanges();
            return db.SmtpAccounts.OrderBy(d => d.Id).LastOrDefault();
        }
        public void DeleteBdSmtpAccount(int id)
        {
            SmtpAccount ac = db.SmtpAccounts.FirstOrDefault(d => d.Id == id);
            if (ac != null)
            {
                db.SmtpAccounts.Remove(ac);
                db.SaveChanges();
            }
        }
        public void UpdateBdAccount(SmtpAccount ac)
        {
            SmtpAccount acdb = db.SmtpAccounts.FirstOrDefault(d => d.Id == ac.Id);
            if (acdb != null)
            {
                acdb.AccountEmail = ac.AccountEmail;
                acdb.Password = ac.Password;
                acdb.Person_Company = ac.Person_Company;
                db.SmtpAccounts.Update(acdb);
                db.SaveChanges();
            }

        }
        public SmtpServer AddBdSmtpServer(SmtpServer serv)
        {
            db.SmtpServers.Add(serv);
            db.SaveChanges();
            return db.SmtpServers.OrderBy(d => d.Id).LastOrDefault();
        }
        public void DeleteBdSmtpServer(int id)
        {
            SmtpServer ms = db.SmtpServers.FirstOrDefault(d => d.Id == id);
            if (ms != null)
            {
                db.SmtpServers.Remove(ms);
                db.SaveChanges();
            }
        }
        public void UpdateBdSmtpServer(SmtpServer serv)
        {
            SmtpServer servdb = db.SmtpServers.FirstOrDefault(d => d.Id == serv.Id);
            if (servdb != null)
            {
                servdb.Title = serv.Title;
                servdb.SmtpServ = serv.SmtpServ;
                servdb.Port = serv.Port;
                servdb.UseSSL = serv.UseSSL;
                db.SmtpServers.Update(servdb);
                db.SaveChanges();
            }

        }
        public MessageSendContainer AddBdMessageSendContainer(MessageSendContainer msc)
        {
            db.MessageSendContainers.Add(msc);
            db.SaveChanges();
            return db.MessageSendContainers.OrderBy(d => d.Id).LastOrDefault();
        }
        public void DeleteBdMessageSendContainer(int id)
        {
            MessageSendContainer msc = db.MessageSendContainers.FirstOrDefault(d => d.Id == id);
            if (msc != null)
            {
                db.MessageSendContainers.Remove(msc);
                db.SaveChanges();
            }
        }
        public void UpdateBdMessageSendContaine(MessageSendContainer msc)
        {
            MessageSendContainer mscdb = db.MessageSendContainers.FirstOrDefault(d => d.Id == msc.Id);
            if (mscdb != null)
            {
                mscdb.SmtpServerUse = msc.SmtpServerUse;
                mscdb.PortUse = msc.PortUse;
                mscdb.SSLUse = msc.SSLUse;
                mscdb.SmtpAccountEmailUse = msc.SmtpAccountEmailUse;
                mscdb.SmtpAccountPasswordUse = msc.SmtpAccountPasswordUse;
                mscdb.SmtpAccountPerson_CompanyUse = msc.SmtpAccountPerson_CompanyUse;
                mscdb.EmailAddressesTo = msc.EmailAddressesTo;
                mscdb.Subject = msc.Subject;
                mscdb.Body = msc.Body;
                mscdb.SendDate = msc.SendDate;
                mscdb.Status = msc.Status;
                db.MessageSendContainers.Update(mscdb);
                db.SaveChanges();
            }

        }
    }
}
