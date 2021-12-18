using HospitalManagement.Model;
using HospitalManagement.Utils;
using HospitalManagement.View;
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
            if (selectableItems.Count() > 0)
            {
                NotifyWindow notifyWindow;
                if (selectableItems.Count() == 1)
                {
                    notifyWindow = new NotifyWindow("Check", "Bạn có chắc chắn muốn xoá y tá này?", "Visible", 400);
                }
                else
                {
                    notifyWindow = new NotifyWindow("Check", "Bạn có chắc chắn muốn xoá những y tá này?", "Visible", 400);
                }
                notifyWindow.ShowDialog();
                if (notifyWindow.Result == System.Windows.MessageBoxResult.OK)
                {

                    foreach (YTA yt in selectableItems)
                    {
                        yt.YTALIENQUANs.Clear();
                        foreach (TO to in DataProvider.Ins.DB.TOes.Where(p => p.ID == yt.IDTO))
                        {
                            to.YTAs.Remove(yt);
                        }
                        DataProvider.Ins.DB.YTAs.Remove(yt);
                    }
                    DataProvider.Ins.DB.SaveChanges();
                    nurseViewModel.IsCheckedAll = false;
                    nurseViewModel.Nurses = SelectableItem<YTA>.GetSelectableItems(DataProvider.Ins.DB.YTAs.ToList());
                    notifyWindow = new NotifyWindow("Success", "Đã xóa thành công!");
                    notifyWindow.Show();
                }
            }
        }
    }
}
