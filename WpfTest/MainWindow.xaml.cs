using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfTest.ViewModels;

namespace WpfTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Quit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void AddAddress_Click(object sender, RoutedEventArgs e)
        {
            MailSenderViewModels msvm = (MailSenderViewModels)DataContext;
            msvm.AddEmailAddress();
        }

        private void DeleteAddress_Click(object sender, RoutedEventArgs e)
        {
            MailSenderViewModels msvm = (MailSenderViewModels)DataContext;
            msvm.DeleteEmailAddress();
        }

        private void SaveEmail_Click(object sender, RoutedEventArgs e)
        {
            MailSenderViewModels msvm = (MailSenderViewModels)DataContext;
            msvm.UpdateEmailAddress();
        }

        private void AddMessage_Click(object sender, RoutedEventArgs e)
        {
            MailSenderViewModels msvm = (MailSenderViewModels)DataContext;
            msvm.AddMessagePattern();
        }

        private void DeleteMessage_Click(object sender, RoutedEventArgs e)
        {
            MailSenderViewModels msvm = (MailSenderViewModels)DataContext;
            msvm.DeleteMessagePattern();
        }

        private void SaveMessage_Click(object sender, RoutedEventArgs e)
        {
            MailSenderViewModels msvm = (MailSenderViewModels)DataContext;
            msvm.UpdateMessagePattern();
        }

        private void AddMailService_Click(object sender, RoutedEventArgs e)
        {
            MailSenderViewModels msvm = (MailSenderViewModels)DataContext;
            msvm.AddMailServise();
        }

        private void DeleteMailService_Click(object sender, RoutedEventArgs e)
        {
            MailSenderViewModels msvm = (MailSenderViewModels)DataContext;
            msvm.DeleteMailServise();
        }

        private void SaveMailService_Click(object sender, RoutedEventArgs e)
        {
            MailSenderViewModels msvm = (MailSenderViewModels)DataContext;
            msvm.UpdateMailService();
        }

        private void AddAccount_Click(object sender, RoutedEventArgs e)
        {
            MailSenderViewModels msvm = (MailSenderViewModels)DataContext;
            msvm.AddAccount();
        }

        private void DeleteAccount_Click(object sender, RoutedEventArgs e)
        {
            MailSenderViewModels msvm = (MailSenderViewModels)DataContext;
            msvm.DeleteAccount();
        }

        private void SaveAccount_Click(object sender, RoutedEventArgs e)
        {
            MailSenderViewModels msvm = (MailSenderViewModels)DataContext;
            msvm.UpdateAccount();
        }

        private void LoadAB_DB_Click(object sender, RoutedEventArgs e)
        {
            MailSenderViewModels msvm = (MailSenderViewModels)DataContext;
            msvm.LoadAddressBook_DB();
        }

        private void LoadAB_Other_Click(object sender, RoutedEventArgs e)
        {
            MailSenderViewModels msvm = (MailSenderViewModels)DataContext;
            msvm.LoadAddressBook_Other();
        }
    }
}
