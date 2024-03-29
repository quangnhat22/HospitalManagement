﻿using HospitalManagement.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using HospitalManagement.View;
using HospitalManagement.View.Staff;
using HospitalManagement.Utils;

namespace HospitalManagement.Command
{
    class ShowNurseInfomationCommand : ICommand
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
            NurseInformationForm nurseInformationForm = new NurseInformationForm(yt);
            nurseInformationForm.Show();
        }
    }
}
