using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailSender.lib.Interfaces.Base
{
    public interface IDatabaseTable<T>
    {
        public T AddDb(T obj);
        public void UpdateDb(T obj);
        public void DeleteDb(T obj);
    }
}
