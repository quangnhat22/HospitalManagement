﻿using HospitalManagement.Model;
using HospitalManagement.Utils;
using HospitalManagement.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HospitalManagement.Command
{
    class DeleteNurseCommand : ICommand
    {
        private NurseViewModel nurseViewModel;
        public DeleteNurseCommand(NurseViewModel nurseViewModel)
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
            var selectableItems = nurseViewModel.Nurses.Where(p => p.IsSelected).Select(x => x.Value);
            foreach (YTA yt in selectableItems)
            {
                foreach (TO to in DataProvider.Ins.DB.TOes.Where(p => p.ID == yt.IDTO))
                {
                    to.YTAs.Remove(yt);
                }
                DataProvider.Ins.DB.YTAs.Remove(yt);
            }
            DataProvider.Ins.DB.SaveChanges();
            nurseViewModel.IsCheckedAll = false;
            nurseViewModel.Nurses = SelectableItem<YTA>.GetSelectableItems(DataProvider.Ins.DB.YTAs.ToList());
        }
    }
}