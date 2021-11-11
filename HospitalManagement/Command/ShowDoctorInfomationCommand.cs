using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using HospitalManagement.View;
using HospitalManagement.Model;
using System.Windows;

namespace HospitalManagement.Command
{
    class ShowDoctorInfomationCommand : ICommand
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
            //DataGrid dg = parameter as DataGrid;
            //Doctor d = dg.SelectedItem as Doctor;
            //DoctorInformationForm doctorif = new DoctorInformationForm(d);
            //doctorif.Show();
        }
    }
}
