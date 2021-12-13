using HospitalManagement.Model;
using HospitalManagement.Utils;
using HospitalManagement.View;
using HospitalManagement.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace HospitalManagement.Command.AccountListCommand
{
    internal class DeleteAccountListCommand: ICommand
    {
        private AccountListViewModel accountListViewModel;
        public DeleteAccountListCommand(AccountListViewModel accountListViewModel)
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
            var selectableItems = accountListViewModel.Users.Where(p => p.IsSelected).Select(x => x.Value);
            NotifyWindow notifyWindow;
            if (selectableItems.Any(p => p.PhanLoai == "admin"))
            {
                if (!MainWindowViewModel.IsSuperAdmin)
                {
                    NotifyWindow adminNotifyWindow = new NotifyWindow("Error", "Bạn không có quyền hạn thực hiện thao tác này");
                    adminNotifyWindow.ShowDialog();
                }
            }
            else if (selectableItems.Any())
            {
                notifyWindow = new NotifyWindow("Warning", "Thao tác này sẽ xóa nhân viên tương ứng\n               Bạn có muốn tiếp tục?", "Visible");
                notifyWindow.ShowDialog();
                if (notifyWindow.Result == MessageBoxResult.OK)
                {
                    foreach (StaffInformation si in selectableItems)
                    {
                        if (si.PhanLoai == "admin")
                        {
                            if (MainWindowViewModel.IsSuperAdmin)
                            {
                                var ad = DataProvider.Ins.DB.ADMINs.Where(p => p.ID == si.Cmnd_cccd);
                                foreach (ADMIN adMin in ad)
                                {
                                    DataProvider.Ins.DB.ADMINs.Remove(adMin);
                                }
                                DataProvider.Ins.DB.SaveChanges();
                                accountListViewModel.StaffAccounts.Remove(si);
                            }
                        }
                        else
                        {
                            if (si.PhanLoai == "yta")
                            {
                                var yt = DataProvider.Ins.DB.YTAs.Where(p => p.CMND_CCCD == si.Cmnd_cccd);
                                foreach (YTA yTA in yt)
                                {
                                    DataProvider.Ins.DB.YTAs.Remove(yTA);
                                }
                                DataProvider.Ins.DB.SaveChanges();
                            }
                            else
                            {
                                var bs = DataProvider.Ins.DB.BACSIs.Where(p => p.CMND_CCCD == si.Cmnd_cccd);
                                foreach (BACSI bACSI in bs)
                                {
                                    DataProvider.Ins.DB.BACSIs.Remove(bACSI);
                                }
                                DataProvider.Ins.DB.SaveChanges();
                            }
                            accountListViewModel.StaffAccounts.Remove(si);
                        }  
                    }            
                }
            }
            accountListViewModel.IsCheckedAll = false;
            accountListViewModel.Users = SelectableItem<StaffInformation>.GetSelectableItems(accountListViewModel.StaffAccounts);
        }
    }
}
