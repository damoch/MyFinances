using System.ComponentModel;
using System.Windows;

namespace MyFinances
{
    public abstract class ModalBase : Window
    {
        public ModalBase(MainWindow parent)
        {
            Parent = parent;
        }
        private new MainWindow Parent { get; set; }

        protected void DataWindow_Closing(object sender, CancelEventArgs e)
        {
            this.Parent.ModalWindowClosed();
        }

        protected Controller GetController()
        {
            return Parent.Controller;
        }
    }
}