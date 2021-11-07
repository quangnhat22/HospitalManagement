using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using HospitalManagement.View.Staff;

namespace HospitalManagement.Command
{
    class OpenNurseFormCommand : ICommand
    {
        public event EventHandler CanExecuteChanged
        {
            add { }
            remove { }
        }

        public bool CanExecute(object parameter)
        {
            return parameter == null ? false : true;
        }

        public void Execute(object parameter)
        {
            Window window = parameter as Window;
            var nurseForm = new NurseForm();
            Application.Current.MainWindow = nurseForm;
            nurseForm.Show();
        }
    }
}
