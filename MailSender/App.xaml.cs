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
using MailSender.lib.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using MailSender.Data;
using MailSender.Infrastructutre.Services;

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
        public static IHostBuilder CreatehostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration(opt => opt.AddJsonFile("appsetting.json", false, true))
            .ConfigureServices(ConfigureServices);
        private static void ConfigureServices(
            HostBuilderContext host,
            IServiceCollection services)
        {
            services.AddSingleton<MainWindowViewModel>();
            services.AddDbContext<MailSenderDB>(opt => opt.UseSqlServer(host.Configuration.GetConnectionString("Default")));
            //services.AddSingleton<IRepository<EmailAddress>, RepositoryEmailaddresses>();
            //services.AddSingleton<IRepository<MessagePattern>, RepositoryMessagePatterns>();
            //services.AddSingleton<IRepository<SmtpServer>, RepositorySmtpServers>();
            //services.AddSingleton<IRepository<SmtpAccount>, RepositorySmtpAccounts>();
            //services.AddSingleton<IRepository<MessageSendContainer>, RepositoryMessageSendContainers>();
            services.AddScoped(typeof(IRepositoryDB<>), typeof(DBRepository<>));
            services.AddSingleton<IRepositoryDB<SmtpServer>, DBRepositorySmtpServers>();
            services.AddTransient<ITextEncoder, TextEncoder>();
            services.AddSingleton<IMailsender, MailSenderService>();
            // Здесь нам надо добавить все сервисы нашего приложения
            // в коллекцию services
            // В переменной host хранится информация, на пример,
            // о пути запуска нашего приложения

        }

    }
}
