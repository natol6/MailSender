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

namespace MailSender.Views
{
    /// <summary>
    /// Логика взаимодействия для SmtpServersView.xaml
    /// </summary>
    public partial class SmtpServersView : UserControl
    {
        public SmtpServersView()
        {
            InitializeComponent();
        }

        private void Title_Error(object sender, ValidationErrorEventArgs e)
        {
            if (e.Action == ValidationErrorEventAction.Added)
            {
                ((Control)sender).ToolTip = e.Error.ErrorContent.ToString();
            }
            else
            {
                ((Control)sender).ClearValue(ToolTipProperty);
            }
        }

        private void Password_Error(object sender, ValidationErrorEventArgs e)
        {
            if (e.Action == ValidationErrorEventAction.Added)
            {
                ((Control)sender).ToolTip = e.Error.ErrorContent.ToString();
            }
            else
            {
                ((Control)sender).ClearValue(ToolTipProperty);
            }
        }

        private void Person_Company_Error(object sender, ValidationErrorEventArgs e)
        {
            if (e.Action == ValidationErrorEventAction.Added)
            {
                ((Control)sender).ToolTip = e.Error.ErrorContent.ToString();
            }
            else
            {
                ((Control)sender).ClearValue(ToolTipProperty);
            }
        }

        private void Port_Error(object sender, ValidationErrorEventArgs e)
        {
            if (e.Action == ValidationErrorEventAction.Added)
            {
                ((Control)sender).ToolTip = e.Error.ErrorContent.ToString();
            }
            else
            {
                ((Control)sender).ClearValue(ToolTipProperty);
            }
        }

        private void AccountEmail_Error(object sender, ValidationErrorEventArgs e)
        {
            if (e.Action == ValidationErrorEventAction.Added)
            {
                ((Control)sender).ToolTip = e.Error.ErrorContent.ToString();
            }
            else
            {
                ((Control)sender).ClearValue(ToolTipProperty);
            }
        }

        private void SmtpServer_Error(object sender, ValidationErrorEventArgs e)
        {
            if (e.Action == ValidationErrorEventAction.Added)
            {
                ((Control)sender).ToolTip = e.Error.ErrorContent.ToString();
            }
            else
            {
                ((Control)sender).ClearValue(ToolTipProperty);
            }
        }

        private void SmtpAccounts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            PasswordBox.Clear();
            
        }
    }
}
