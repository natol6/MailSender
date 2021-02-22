using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MailSender.ViewModels;
using MailSender.lib.Service;
using MailSender.lib.Interfaces;

namespace MailSender
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static IHost __Hosting;
        public static IHost Hosting
        {
            get
            {
                if (__Hosting != null) return __Hosting;
                var host_builder = Host
                .CreateDefaultBuilder(Environment.GetCommandLineArgs());
                host_builder.ConfigureServices(ConfigureServices);
                return __Hosting = host_builder.Build();
            }
        }
        public static IServiceProvider Services => Hosting.Services;
        private static void ConfigureServices(
            HostBuilderContext host,
            IServiceCollection services)
        {
            services.AddSingleton<MainWindowViewModel>();
            services.AddSingleton<IDataBaseConnect, DBMailSenderService>();
            // Здесь нам надо добавить все сервисы нашего приложения
            // в коллекцию services
            // В переменной host хранится информация, на пример,
            // о пути запуска нашего приложения
        }

    }
}
