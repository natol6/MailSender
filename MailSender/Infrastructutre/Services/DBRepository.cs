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
    public class DBRepository<T>: IRepositoryDB<T> where T: Entity
    {
        protected readonly MailSenderDB _db;

        protected DbSet<T> Set { get; }

        public DBRepository(MailSenderDB db)
        {
            _db = db;
            Set = db.Set<T>();
        }

        public virtual IEnumerable<T> GetAll() => Set;

        //public T GetById(int id) => Set.FirstOrDefault(item => item.Id == id);
        public T GetById(int id) => Set.Find(id);

        public int Add(T item)
        {
            //Set.Add(item);
            _db.Entry(item).State = EntityState.Added;
            _db.SaveChanges();

            return item.Id;
        }

        public void Update(T item)
        {
            _db.Entry(item).State = EntityState.Modified;
            _db.SaveChanges();
        }

        public bool Remove(int id)
        {
            var item = GetById(id);
            if (item is null) return false;

            //Set.Remove(item);
            _db.Entry(item).State = EntityState.Deleted;
            _db.SaveChanges();
            return true;
        }
    }
}
