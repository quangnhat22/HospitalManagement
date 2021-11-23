using HospitalManagement.Command.AccountListCommand;
using HospitalManagement.Model;
using HospitalManagement.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace HospitalManagement.ViewModel
{
    public class AccountListViewModel : BaseViewModel, INotifyPropertyChanged
    {
        private static ObservableCollection<SelectableItem<StaffAccount>> users = SelectableItem<StaffAccount>.GetSelectableItems(StaffAccount.staffAccountsList);
        public ObservableCollection<SelectableItem<StaffAccount>> Users
        {
            get { return users; }
            set { users = value; OnPropertyChanged("Users"); }
        }

        private bool? isCheckedAll;
        private List<String> filterList = new List<string> { "Tên tài khoản","Tổ" };
        private string selectedFilter;
        private string searchBox;

        public string SelectedFilter 
        { 
            get => selectedFilter; 
            set => selectedFilter = value; 
        }
        public string SearchBox
        {  
            get => searchBox; 
            set => searchBox = value; 
        }
        public List<string> FilterList 
        { 
            get => filterList; 
            set => filterList = value; 
        }

        public bool? IsCheckedAll 
        { 
            get => isCheckedAll;
            set 
            { 
                isCheckedAll = value; 
                OnPropertyChanged("IsCheckedAll"); 
            } 
        }

        public ICommand AllCheckedCommand { get; set; }
        public ICommand SingleCheckedCommand { get; set; }
        public ICommand OpenAddAccountListForm { get; set; }
        public ICommand SearchAccountListCommand { get; set; }
        public ICommand DeleteAccountListCommand { get; set; }
        
        public AccountListViewModel ()
        {
            StaffAccount.InitAccountList();
            OpenAddAccountListForm = new OpenAddNewAccountForm();
            //DeleteAccountListCommand = new DeleteAccountListCommand(this);
            //SearchAccountListCommand = new SearchAccountListCommand(this);
            if (FilterList.Count > 0)
            {
                SelectedFilter = FilterList[0];
            }

            IsCheckedAll = false;
            AllCheckedCommand = new RelayCommand<CheckBox>((p) => { return p == null ? false : true; }, (p) =>
            {
                bool allcheckbox = (p.IsChecked == true);
                for (int i = 0; i < Users.Count; i++)
                    Users[i].IsSelected = allcheckbox;
                p.IsChecked = allcheckbox;
            });

            SingleCheckedCommand = new RelayCommand<CheckBox>((p) => { return p == null ? false : true; }, (p) =>
            {
                IsCheckedAll = null;
                IsCheckedAll = null;

                if (Users.Where(f => f.IsSelected).Count() == Users.Count)
                    IsCheckedAll = true;
                else
                    if (Users.Where(f => f.IsSelected).Count() == 0)
                    IsCheckedAll = false;
            });
        }

    }
}
