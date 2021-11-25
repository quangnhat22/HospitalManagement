using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using HospitalManagement.Model;
using System.ComponentModel;
using HospitalManagement.Command;
using HospitalManagement.Utils;
using System.Collections.ObjectModel;
using HospitalManagement.Command.NurseListCommand;

namespace HospitalManagement.ViewModel
{
    class NurseViewModel : BaseViewModel, INotifyPropertyChanged
    {
        //public int CheckedCount;
        private static ObservableCollection<SelectableItem<YTA>> nurses = SelectableItem<YTA>.GetSelectableItems(DataProvider.Ins.DB.YTAs.ToList());
        public ObservableCollection<SelectableItem<YTA>> Nurses
        {
            get { return nurses; }
            set { nurses = value; OnPropertyChanged("Nurses"); }
        }
        public ICommand OpenNurseForm { get; set; }
        public ICommand AllCheckedCommand { get; set; }
        public ICommand SingleCheckedCommand { get; set; }
        public ICommand ShowNurseInfomationCommand { get; set; }
        public ICommand OpenChangeNurseForm { get; set; }
        public NurseViewModel()
        {         
            ShowNurseInfomationCommand = new ShowNurseInfomationCommand();
            OpenChangeNurseForm = new OpenChangeNurseFormCommand();
            OpenNurseForm = new OpenNurseFormCommand();
        }
    }
}
