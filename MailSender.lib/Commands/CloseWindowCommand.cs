using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailSender.lib.Commands.Base;
using System.Windows;

namespace MailSender.lib.Commands
{
    public class CloseWindowCommand: Command
    {
        public override void Execute(object p)
        {
            var window = p as Window;
            if (window is null)
                window = Application.Current.Windows
                    .Cast<Window>()
                    .FirstOrDefault(w => w.IsFocused);
            if (window is null)
                window = Application.Current.Windows
                .Cast<Window>()
                .FirstOrDefault(w => w.IsActive);
            window?.Close();
        }

    }
}
