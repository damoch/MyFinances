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
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static System.Windows.Forms.DialogResult;
using ListBox = System.Windows.Controls.ListBox;

namespace MyFinances
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public  Controller Controller { get; set; }
        private List<int> _ids;
        private Window _currentModal;
        private bool _modalWindowOpened;
        public MainWindow()
        {
            Controller = new Controller();
            _ids = new List<int>();
            _modalWindowOpened = false;
            InitializeComponent();
            EditTransactionButton.IsEnabled = false;
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
            _ids = new List<int>();
            foreach (var transaction in Controller.GetTransactionsList(null, null))
            {
                TransactionsListBox.Items.Add(transaction);
                _ids.Add(transaction.Id);
            }
            EditTransactionButton.IsEnabled = false;
            RemoveTransactionButton.IsEnabled = false;
            MoneyAmmountTextBox.Text = Controller.GetAmmount().ToString();
            PrognosisTextBox.Text = Controller.GetEndOfMonthPrognosis().ToString();
            AverageOutcomeTextBox.Text = Controller.GetAverageOutcomeValue().ToString();
        }

        public void ModalWindowClosed()
        {
            _modalWindowOpened = false;
            RefreshCurrentView();
        }

        private void TransactionsListBox_OnPreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.OriginalSource == null) return;
            var item = ItemsControl.ContainerFromElement(sender as ListBox, e.OriginalSource as DependencyObject) as ListBoxItem;
            if (item != null)
            {
                EditTransactionButton.IsEnabled = true;
                RemoveTransactionButton.IsEnabled = true;
            }
        }

        private void DataWindow_Closing(object sender, CancelEventArgs e)
        {
            _currentModal.Close();
        }

        private void EditTransactionButton_Click(object sender, RoutedEventArgs e)
        {
            var id = _ids[TransactionsListBox.SelectedIndex];
            var transaction = Controller.GetById(id);
            if (!_modalWindowOpened)
            {
                _currentModal = new AddTransaction(this, transaction);
                _currentModal.Show();
                _modalWindowOpened = true;
            }
        }

        private void RemoveTransactionButton_Click(object sender, RoutedEventArgs e)
        {
            var id = _ids[TransactionsListBox.SelectedIndex];
            Controller.RemoveTransaction(id);
            RefreshCurrentView();
        }

        private void SaveDataModelButton_Click(object sender, RoutedEventArgs e)
        {
            var saveDialog = new SaveFileDialog();
            if(saveDialog.ShowDialog() == OK)
                Controller.SaveDataModel(saveDialog.FileName);
        }

        private void LoadDataModelButton_Click(object sender, RoutedEventArgs e)
        {
            var loadDialog = new OpenFileDialog();
            if (loadDialog.ShowDialog() == OK)
            {
                Controller.LoadDataModel(loadDialog.FileName);
                RefreshCurrentView();
            }
        }
    }
}
