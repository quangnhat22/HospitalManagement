using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using HospitalManagement.View;

namespace HospitalManagement.Command
{
    class OpenFacilitiesFormCommand : ICommand
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
            var facilitiesForm = new FacilitiesForm();
            Application.Current.MainWindow = facilitiesForm;
            facilitiesForm.Show();
        }
    }
}