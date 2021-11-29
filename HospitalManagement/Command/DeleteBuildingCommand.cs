using HospitalManagement.ViewModel;
using HospitalManagement.View;
using HospitalManagement.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;

namespace HospitalManagement.Command
{
    class DeleteBuildingCommand : ICommand
    {
        private RoomUsercontrolViewModel roomUsercontrolViewModel;
        public DeleteBuildingCommand(RoomUsercontrolViewModel roomUsercontrolViewModel)
        {
            this.roomUsercontrolViewModel = roomUsercontrolViewModel;
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
            string selectedBuildingName = roomUsercontrolViewModel.SelectedBuildingName;
            if (selectedBuildingName != "")
            {
                bool isExistPatientOrTeam = false;
                TOA toa = DataProvider.Ins.DB.TOAs.Where(p => p.DISPLAYNAME == selectedBuildingName).First();
                foreach (TANG tang in toa.TANGs)
                {
                    if (tang.TOes.Count > 0)
                    {
                        isExistPatientOrTeam = true;
                        break;
                    }
                    foreach (PHONG phong in tang.PHONGs)
                        if (phong.BENHNHANs.Count > 0)
                        {
                            isExistPatientOrTeam = true;
                            break;
                        }
                }
                if (isExistPatientOrTeam)
                {
                    NotifyWindow notifyWindow = new NotifyWindow("Error", "Vẫn còn bệnh nhân hoặc tổ trong toà!\n              Vui lòng kiểm tra lại!");
                    notifyWindow.Show();
                }
                else
                {
                    NotifyWindow notifyWindow = new NotifyWindow("Warning", "Bạn có chắc chắn muốn xoá toà " + selectedBuildingName + "?", "Visible");
                    notifyWindow.ShowDialog();
                    if (notifyWindow.Result == MessageBoxResult.OK)
                    {
                        List<TANG> tangs = toa.TANGs.ToList();
                        foreach (TANG tang in tangs)
                        {
                            List<PHONG> phongs = tang.PHONGs.ToList();
                            foreach (PHONG phong in phongs)
                                DataProvider.Ins.DB.PHONGs.Remove(phong);
                            DataProvider.Ins.DB.TANGs.Remove(tang);
                        }
                        DataProvider.Ins.DB.TOAs.Remove(toa);
                        DataProvider.Ins.DB.SaveChanges();
                        roomUsercontrolViewModel.Buildings = DataProvider.Ins.DB.TOAs.ToList();
                        if (roomUsercontrolViewModel.Buildings.Count > 0)
                            roomUsercontrolViewModel.SelectedBuildingName = roomUsercontrolViewModel.Buildings[0].DISPLAYNAME;
                        NotifyWindow notifyWindow1 = new NotifyWindow("Success", "Xoá thành công toà " + selectedBuildingName);
                        notifyWindow1.Show();

                        if (roomUsercontrolViewModel.Buildings.Count > 0)
                        {
                            roomUsercontrolViewModel.CurrentBuilding = 0;
                            roomUsercontrolViewModel.CanMoveBackward = false;
                            roomUsercontrolViewModel.SelectedBuildingName = roomUsercontrolViewModel.Buildings[0].DISPLAYNAME;
                            roomUsercontrolViewModel.CurrentBuildingName = roomUsercontrolViewModel.SelectedBuildingName;
                            roomUsercontrolViewModel.Floors = DataProvider.Ins.DB.TANGs.Where(p => p.TOA.DISPLAYNAME == roomUsercontrolViewModel.SelectedBuildingName).ToList();
                            if (roomUsercontrolViewModel.Buildings.Count > 1)
                                roomUsercontrolViewModel.CanMoveForward = true;
                            else
                                roomUsercontrolViewModel.CanMoveForward = false;
                        }
                    }
                }
            }
        }
    }
}
