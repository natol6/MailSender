using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailSender.lib.Models;

namespace MailSender.lib.Interfaces
{
    public interface IRepositoryDB<T> where T: Entity
    {
        IEnumerable<T> GetAll();

        T GetById(int id);

        int Add(T item);

        void Update(T item);

        bool Remove(int id);
    }
}
