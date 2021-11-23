using HospitalManagement.View.AddStaff;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace HospitalManagement.Command.AccountListCommand
{
    public class OpenAddNewAccountForm : ICommand
    {
        public event EventHandler CanExecuteChanged
        {
            add { }
            remove { }
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            Window window = parameter as Window;
            var addNewAccountForm = new AddNewAccountForm();
            Application.Current.MainWindow = addNewAccountForm;
            addNewAccountForm.Show();
        }
    }
}
