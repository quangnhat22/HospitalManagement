using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using HospitalManagement.Model;
using HospitalManagement.Utils;
using HospitalManagement.View;
using HospitalManagement.View.Staff;
using HospitalManagement.ViewModel;

namespace HospitalManagement.Command.NurseListCommand
{
    internal class OpenChangeNurseFormCommand: ICommand
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
            SelectableItem<YTA> selectableItem = parameter as SelectableItem<YTA>;
            YTA yt = selectableItem.Value as YTA;
            ChangeNurseInformationForm changeNurseInformationForm = new ChangeNurseInformationForm(yt);
            changeNurseInformationForm.Show();
        }
    }
}
