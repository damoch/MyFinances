using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    public partial class AddTransaction : ModalBase
    {
        public AddTransaction(MainWindow mainWindow): base(mainWindow)
        {
            InitializeComponent();
        }
        
        private void AddTransactionButton_Click(object sender, RoutedEventArgs e)
        {
            if (TransactionDatePicker.SelectedDate != null)
            {
                DateTime date = TransactionDatePicker.SelectedDate.Value;
                var desc = TransactionDescriptionTextBox.Text;
                var ammount = ConvertUtils.StringToDecimal(AmmountTextBox.Text);
                var type = IncomeRadioButton.IsChecked != null && (bool) IncomeRadioButton.IsChecked ? TransactionType.Income : TransactionType.Outcome;
                GetController().AddTransaction(date, ammount,desc, type);
            }
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
    }
}
