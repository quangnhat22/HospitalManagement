using HospitalManagement.Model;
using HospitalManagement.Utils;
using HospitalManagement.View;
using HospitalManagement.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            foreach (StaffInformation si in selectableItems)
            {
                if(si.PhanLoai=="Bác Sĩ"||si.PhanLoai=="Tổ Trưởng")
                {
                    var bs = DataProvider.Ins.DB.BACSIs.Where(p => p.CMND_CCCD == si.Cmnd_cccd);
                    foreach(BACSI bacSi in bs)
                    {
                        foreach(TO to in DataProvider.Ins.DB.TOes.Where(p => p.ID == bacSi.IDTO))
                        {
                            to.BACSIs.Remove(bacSi);                
                        }  
                    }
                }
                else if(si.PhanLoai=="Y Tá")
                {
                    var yt = DataProvider.Ins.DB.YTAs.Where(p => p.CMND_CCCD == si.Cmnd_cccd);
                    foreach (YTA yTa in yt)
                    {
                        foreach (TO to in DataProvider.Ins.DB.TOes.Where(p => p.ID == yTa.IDTO))
                        {
                            to.YTAs.Remove(yTa);   
                        }
                    }
                }
                else if (si.PhanLoai == "admin")
                {
                    if (MainWindowViewModel.IsSuperAdmin)
                    {
                        var ad = DataProvider.Ins.DB.ADMINs.Where(p => p.ID == si.Cmnd_cccd);
                        foreach (ADMIN adMin in ad)
                        {
                            DataProvider.Ins.DB.ADMINs.Remove(adMin);
                        }
                    }
                    else
                    {
                        NotifyWindow notifyWindow = new NotifyWindow("Error", "Bạn không có quyền hạn thực hiện thao tác này");
                        notifyWindow.ShowDialog();
                    }
                }
                DataProvider.Ins.DB.SaveChanges();
                accountListViewModel.StaffAccounts.Remove(si);
            }
            accountListViewModel.IsCheckedAll = false;
            accountListViewModel.Users = SelectableItem<StaffInformation>.GetSelectableItems(accountListViewModel.StaffAccounts);
        }
    }
}
