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
    class OpenDoctorFormCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return parameter == null ? false : true;
        }

        public void Execute(object parameter)
        {
            Window window = parameter as Window;
            var doctorForm = new DoctorForm();
            Application.Current.MainWindow = doctorForm;
            doctorForm.Show();
        }
    }
}
