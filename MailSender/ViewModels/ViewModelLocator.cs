﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using MailSender.lib.Service;

namespace MailSender.ViewModels
{
    internal class ViewModelLocator
    {
        public DBMailSenderService DBMailSender => App.Services
            .GetRequiredService<DBMailSenderService>();
        public MainWindowViewModel MainWindowModel => App.Services
            .GetRequiredService<MainWindowViewModel>();
        
    }
}
