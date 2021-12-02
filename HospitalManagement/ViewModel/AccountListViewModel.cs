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
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace HospitalManagement.ViewModel
{
    public class AccountListViewModel : BaseViewModel, INotifyPropertyChanged
    {
        private static ObservableCollection<SelectableItem<StaffInformation>> users;
        private static List<StaffInformation> staffAccounts;
        private Visibility dataGridVisibility;
        private Visibility progressbarVisibility;
        public List<StaffInformation> StaffAccounts
        {
            get { return staffAccounts; }
            set { staffAccounts = value; OnPropertyChanged("StaffAccounts"); }
        }
        public ObservableCollection<SelectableItem<StaffInformation>> Users
        {
            get { return users; }
            set { users = value; OnPropertyChanged("Users"); }
        }

        private bool? isCheckedAll;
        private List<String> filterList = new List<string> { "CMND/CCCD", "Họ", "Tên", "Email", "Tên đăng nhập", "Tổ" };
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

            }
        }
        public Visibility DataGridVisibility
        {
            get => dataGridVisibility;
            set
            {
                dataGridVisibility = value;
                OnPropertyChanged("DataGridVisibility");
            }
        }
        public Visibility ProgressbarVisibility 
        { 
            get => progressbarVisibility; 
            set
            {
                progressbarVisibility = value;
                OnPropertyChanged("ProgressbarVisibility");
            }
        }

        public ICommand AllCheckedCommand { get; set; }
        public ICommand SingleCheckedCommand { get; set; }
        public ICommand OpenAddAccountListForm { get; set; }
        public ICommand SearchAccountListCommand { get; set; }
        public ICommand DeleteAccountListCommand { get; set; }

        public AccountListViewModel()
        {
            initAccountList();
            OpenAddAccountListForm = new OpenAddNewAccountForm();
            DeleteAccountListCommand = new DeleteAccountListCommand(this);
            SearchAccountListCommand = new SearchAccountListCommand(this);
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

        private async void initAccountList()
        {
            ProgressbarVisibility = Visibility.Visible;
            DataGridVisibility = Visibility.Collapsed;
            StaffAccounts = await StaffInformation.InitAccountList();
            Users = SelectableItem<StaffInformation>.GetSelectableItems(StaffAccounts);
            ProgressbarVisibility = Visibility.Collapsed;
            DataGridVisibility = Visibility.Visible;
        }
    }
}
