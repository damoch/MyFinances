using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MyFinances
{
    /// <summary>
    /// Interaction logic for AddTransaction.xaml
    /// </summary>
    public partial class AddTransaction
    {
        private Transaction _transaction;

        public AddTransaction(MainWindow mainWindow): base(mainWindow)
        {
            _transaction = null;
            InitializeComponent();
            TransactionDatePicker.SelectedDate = DateTime.Now;
        }

        public AddTransaction(MainWindow mainWindow, Transaction transaction) : this(mainWindow)
        {
            InitializeComponent();
            SetTransactionValues(transaction);
        }

        private void SetTransactionValues(Transaction transaction)
        {
            _transaction = transaction;
            TransactionDatePicker.SelectedDate = transaction.DateTime;
            TransactionDescriptionTextBox.Text = transaction.Title;
            AmmountTextBox.Text = transaction.Ammount.ToString();
            if (transaction.TransactionType == TransactionType.Income)
                IncomeRadioButton.IsChecked = true;
            else
                OutcomeRadioButton.IsChecked = true;
            
        }

        private void AddTransactionButton_Click(object sender, RoutedEventArgs e)
        {
            if (_transaction != null && TransactionValid())
            {
                _transaction.DateTime = TransactionDatePicker.SelectedDate.Value;
                _transaction.Title = TransactionDescriptionTextBox.Text;
                _transaction.Ammount = ConvertUtils.StringToDecimal(AmmountTextBox.Text);
                _transaction.TransactionType = IncomeRadioButton.IsChecked != null && (bool)IncomeRadioButton.IsChecked ? TransactionType.Income : TransactionType.Outcome;
                GetController().ModifyTransaction(_transaction);
            }
            else if (TransactionValid())
            {
                var date = TransactionDatePicker.SelectedDate.Value;
                var desc = TransactionDescriptionTextBox.Text;
                var ammount = ConvertUtils.StringToDecimal(AmmountTextBox.Text);
                var type = IncomeRadioButton.IsChecked != null && (bool) IncomeRadioButton.IsChecked ? TransactionType.Income : TransactionType.Outcome;
                GetController().AddTransaction(date, ammount,desc, type);
            }
            Close();
        }

        private bool TransactionValid()
        {
            return TransactionDatePicker.SelectedDate != null && CheckTextBox(TransactionDescriptionTextBox) && CheckTextBox(AmmountTextBox);

        }

        private bool CheckTextBox(TextBox box)
        {
            return box.Text != "";
        }


        private void AmmountTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            
        }

        private void PreviewTextInpuxt(object sender, TextCompositionEventArgs e)
        {
            var input = e.Text;
            var current = AmmountTextBox.Text;
            //kinda crappy
            e.Handled = !(Regex.IsMatch(input, @"^\d+$") || (input.Equals(".") && !current.Contains(".")));
            
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
