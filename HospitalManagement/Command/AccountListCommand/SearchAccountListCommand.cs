using HospitalManagement.Model;
using HospitalManagement.Utils;
using HospitalManagement.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HospitalManagement.Command.AccountListCommand
{
    public class SearchAccountListCommand : ICommand
    {
        private AccountListViewModel accountListViewModel;

        public SearchAccountListCommand(AccountListViewModel accountListViewModel)
        {
            this.accountListViewModel = accountListViewModel;   
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
            //cmnd,ten,email,tendangnhap,tocongtac
            accountListViewModel.SearchBox = accountListViewModel.SearchBox.Trim();
            if (accountListViewModel.SearchBox == string.Empty || accountListViewModel.SearchBox == null)
            {
                accountListViewModel.Users = SelectableItem<StaffInformation>.GetSelectableItems(accountListViewModel.StaffAccounts);
            }
            else
            {
                string RemoveSignString = VietnameseSign.convertToUnSign2(accountListViewModel.SelectedFilter).ToLower();
                if (RemoveSignString == "cmnd/cccd")
                {
                    accountListViewModel.Users = SelectableItem<StaffInformation>.GetSelectableItems(
                                                                                            accountListViewModel.StaffAccounts.Where(
                                                                                                p => p.Cmnd_cccd.Contains(accountListViewModel.SearchBox)).ToList());
                }
                else if (RemoveSignString == "ho")
                {
                    accountListViewModel.Users = SelectableItem<StaffInformation>.GetSelectableItems(
                        accountListViewModel.StaffAccounts.Where(delegate (StaffInformation bn)
                        {
                            return VietnameseSign.ContainsUnsigned(bn.Ho, accountListViewModel.SearchBox);
                        }).AsQueryable().ToList()
                        );
                }
                else if (RemoveSignString == "ten")
                {
                    accountListViewModel.Users = SelectableItem<StaffInformation>.GetSelectableItems(
                        accountListViewModel.StaffAccounts.Where(delegate (StaffInformation bn)
                        {
                            return VietnameseSign.ContainsUnsigned(bn.Ten, accountListViewModel.SearchBox);
                        }).AsQueryable().ToList()
                        );
                }
                else if (RemoveSignString == "email")
                {
                    accountListViewModel.Users = SelectableItem<StaffInformation>.GetSelectableItems(
                        accountListViewModel.StaffAccounts.Where(delegate (StaffInformation bn)
                        {
                            return VietnameseSign.ContainsUnsigned(bn.Email, accountListViewModel.SearchBox);
                        }).AsQueryable().ToList()
                        );
                }
                else if (RemoveSignString == "ten dang nhap")
                {
                    accountListViewModel.Users = SelectableItem<StaffInformation>.GetSelectableItems(
                        accountListViewModel.StaffAccounts.Where(delegate (StaffInformation bn)
                        {
                            return VietnameseSign.ContainsUnsigned(bn.UserName , accountListViewModel.SearchBox);
                        }).AsQueryable().ToList()
                        );
                }
                else if (RemoveSignString == "to")
                {
                    accountListViewModel.Users = SelectableItem<StaffInformation>.GetSelectableItems(
                        accountListViewModel.StaffAccounts.Where(delegate (StaffInformation bn)
                        {
                            return VietnameseSign.ContainsUnsigned(bn.IdTo.ToString() , accountListViewModel.SearchBox);
                        }).AsQueryable().ToList()
                        );
                }
            }
        }
    }
}
