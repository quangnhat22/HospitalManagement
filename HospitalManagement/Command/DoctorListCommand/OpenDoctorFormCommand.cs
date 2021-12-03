using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using HospitalManagement.View;
using HospitalManagement.ViewModel;

namespace HospitalManagement.Command
{
    class OpenDoctorFormCommand : ICommand
    {
        private DoctorViewModel doctorViewModel;
        public OpenDoctorFormCommand(DoctorViewModel doctorViewModel)
        {
            this.doctorViewModel = doctorViewModel;
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
            var doctorForm = new DoctorForm(doctorViewModel);
            Application.Current.MainWindow = doctorForm;
            doctorForm.Show();
        }
    }
}
