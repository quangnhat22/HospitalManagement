using HospitalManagement.Command.AccountListCommand;
using HospitalManagement.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HospitalManagement.ViewModel.AddAccountVM
{
    public class AddNewAccountViewModel : BaseViewModel
    {
        private List<String> accountList;
        private string selectedAccount;
        private List<int> groupList = new List<int>();
        private string selectedGroup;
        public ICommand AddNewAccountCommand { get; set; }
        public List<string> AccountList { get => accountList; set => accountList = value; }
        public string SelectedAccount { get => selectedAccount; set => selectedAccount = value; }
        public List<int> GroupList { get => groupList; set => groupList = value; }
        public string SelectedGroup { get => selectedGroup; set => selectedGroup = value; }

        public AddNewAccountViewModel()
        {
            if(MainWindowViewModel.User.ROLE == "sudo")
            {
                accountList = new List<string> { "Quản lý nhân sự", "Nhóm trưởng", "Bác sĩ", "Y tá" };
            }
            else
            {
                accountList = new List<string> {"Nhóm trưởng", "Bác sĩ", "Y tá" };
            }
            AddNewAccountCommand = new AddNewAccountCommand();
            SelectedAccount = AccountList[0];
            var groupListData = DataProvider.Ins.DB.TOes.ToList().ConvertAll(itemGroup => itemGroup.ID);
            GroupList.AddRange(groupListData);
        }
    }
}
