using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using HospitalManagement.View.Staff;
using HospitalManagement.ViewModel;

namespace HospitalManagement.Command
{
    class OpenNurseFormCommand : ICommand
    {
        private NurseViewModel nurseViewModel;
        public OpenNurseFormCommand(NurseViewModel nurseViewModel)
        {
            this.nurseViewModel = nurseViewModel;
        }
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
            var nurseForm = new NurseForm(nurseViewModel);
            Application.Current.MainWindow = nurseForm;
            nurseForm.Show();
        }
    }
}
