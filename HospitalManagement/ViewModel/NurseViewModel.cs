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

        //public event PropertyChangedEventHandler PropertyChanged;

        //protected virtual void OnPropertyChanged(string name)
        //{
        //    if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(name));
        //}



        private bool? isCheckedAll;

        public bool? IsCheckedAll
        {
            get { return isCheckedAll; }
            set { isCheckedAll = value; OnPropertyChanged("IsCheckedAll"); }
        }

        public ICommand OpenNurseForm { get; set; }
        public ICommand AllCheckedCommand { get; set; }
        public ICommand SingleCheckedCommand { get; set; }
        public ICommand ShowNurseInfomationCommand { get; set; }

        public NurseViewModel()
        {
            IsCheckedAll = false;
            AllCheckedCommand = new RelayCommand<CheckBox>((p) => { return p == null ? false : true; }, (p) =>
            {
                bool allcheckbox = (p.IsChecked == true);
                for (int i = 0; i < Nurses.Count; i++)
                    Nurses[i].IsSelected = allcheckbox;
                p.IsChecked = allcheckbox;
            });

            SingleCheckedCommand = new RelayCommand<CheckBox>((p) => { return p == null ? false : true; }, (p) =>
            {
                IsCheckedAll = null;

                if (Nurses.Where(nurse => nurse.IsSelected).Count() == nurses.Count)
                    IsCheckedAll = true;
                else
                    if (Nurses.Where(nurse => nurse.IsSelected).Count() == 0)
                    IsCheckedAll = false;
            });
            ShowNurseInfomationCommand = new ShowNurseInfomationCommand();
            OpenNurseForm = new OpenNurseFormCommand();
        }
    }
}
