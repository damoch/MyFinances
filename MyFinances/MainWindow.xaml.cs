using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace MyFinances
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public  Controller Controller { get; set; }
        private Window _currentModal;
        private bool _modalWindowOpened;
        public MainWindow()
        {
            Controller = new Controller();
            _modalWindowOpened = false;
            InitializeComponent();
            RefreshCurrentView();
        }

        private void AddTransactionButton_Click(object sender, RoutedEventArgs e)
        {
            if (!_modalWindowOpened)
            {
                _currentModal = new AddTransaction(this);
                _currentModal.Show();
                _modalWindowOpened = true;
            }

        }

        private void RefreshCurrentView()
        {
            TransactionsListBox.Items.Clear();
            foreach (var transaction in Controller.GetTransactionsList(null, null))
            {
                TransactionsListBox.Items.Add(transaction.DateTime);
            }
        }

        public void ModalWindowClosed()
        {
            _modalWindowOpened = false;
            RefreshCurrentView();
        }

        private void DataWindow_Closing(object sender, CancelEventArgs e)
        {
            _currentModal.Close();
        }
    }
}
