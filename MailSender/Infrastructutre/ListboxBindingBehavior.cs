using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Interactivity;
using MailSender.lib.Models;

namespace MailSender.Infrastructutre
{
    class ListboxBindingBehavior: Behavior<ListBoxItem>
    {
        protected override void OnAttached()
        {
            AssociatedObject.Selected += OnListBoxItemValueChanged;
        }

        public MessageSendContainer SelelectedMessage
        {
            get { return (MessageSendContainer)GetValue(ItemProperty); }
            set { SetValue(ItemProperty, value); }
        }

        public static readonly DependencyProperty ItemProperty =
            DependencyProperty.Register("SelelectedMessage", typeof(MessageSendContainer), typeof(ListboxBindingBehavior), new PropertyMetadata(null));

        private void OnListBoxItemValueChanged(object sender, RoutedEventArgs e)
        {
            var binding = BindingOperations.GetBindingExpression(this, ItemProperty);
            if (binding != null)
            {
                PropertyInfo property = binding.DataItem.GetType().GetProperty(binding.ParentBinding.Path.Path);
                if (property != null)
                    property.SetValue(binding.DataItem, AssociatedObject.Content, null);
            }
        }
    }
}
