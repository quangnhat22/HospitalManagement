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

namespace HospitalManagement.Command.DoctorListCommand
{
    class DeleteDoctorCommand: ICommand
    {
        private DoctorViewModel doctorViewModel;
        public DeleteDoctorCommand(DoctorViewModel doctorViewModel)
        {
            this.doctorViewModel = doctorViewModel;
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
            var selectableItems = doctorViewModel.Doctors.Where(p => p.IsSelected).Select(x => x.Value);
            if (selectableItems.Count() > 0)
            {
                NotifyWindow notifyWindow;
                if (selectableItems.Count() == 1)
                {
                    notifyWindow = new NotifyWindow("Check", "Bạn có chắc chắn muốn xoá bác sĩ này?", "Visible", 400);
                }
                else
                {
                    notifyWindow = new NotifyWindow("Check", "Bạn có chắc chắn muốn xoá những bác sĩ này?", "Visible", 400);
                }
                notifyWindow.ShowDialog();
                if (notifyWindow.Result == System.Windows.MessageBoxResult.OK)
                {
                    foreach (BACSI bs in selectableItems)
                    {
                        foreach (TO to in DataProvider.Ins.DB.TOes.Where(p => p.ID == bs.IDTO))
                        {
                            to.BACSIs.Remove(bs);
                        }
                        DataProvider.Ins.DB.BACSIs.Remove(bs);
                    }
                    DataProvider.Ins.DB.SaveChanges();
                    doctorViewModel.IsCheckedAll = false;
                    doctorViewModel.Doctors = SelectableItem<BACSI>.GetSelectableItems(DataProvider.Ins.DB.BACSIs.ToList());
                    notifyWindow = new NotifyWindow("Success", "Đã xóa thành công!");
                    notifyWindow.Show();
                }
            }
                    
        }
    }
}
