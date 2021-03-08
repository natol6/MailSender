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
        public static IHost Hosting => __Hosting ??= CreateHostBuilder(Environment.GetCommandLineArgs()).Build();
        
        public static IServiceProvider Services => Hosting.Services;
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration(opt => opt.AddJsonFile("appsettings.json", false, true))
            .ConfigureServices(ConfigureServices);
        private static void ConfigureServices(
            HostBuilderContext host,
            IServiceCollection services)
        {
            services.AddDbContext<MailSenderDB>(opt => opt.UseSqlServer(host.Configuration.GetConnectionString("Default")));
            services.AddSingleton<MainWindowViewModel>();
            //services.AddScoped(typeof(IRepositoryDB<>), typeof(DBRepository<>));
            services.AddScoped<IRepositoryDB<EmailAddress>, DBRepository<EmailAddress>>();
            services.AddScoped<IRepositoryDB<MessagePattern>, DBRepository<MessagePattern>>();
            services.AddScoped<IRepositoryDB<MessageSendContainer>, DBRepository<MessageSendContainer>>();
            services.AddScoped<IRepositoryDB<SmtpAccount>, DBRepository<SmtpAccount>>();
            services.AddScoped<IRepositoryDB<SmtpServer>, DBRepositorySmtpServers>();
            services.AddTransient<ITextEncoder, TextEncoder>();
            services.AddSingleton<IMailsender, MailSenderService>();
            // Здесь нам надо добавить все сервисы нашего приложения
            // в коллекцию services
            // В переменной host хранится информация, на пример,
            // о пути запуска нашего приложения

        }

    }
}
