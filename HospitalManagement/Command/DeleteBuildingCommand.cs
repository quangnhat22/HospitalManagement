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
            int selectedBuilding = roomUsercontrolViewModel.SelectedBuilding;
            if (selectedBuilding.ToString() != "")
            {
                List<TOA> buildings = roomUsercontrolViewModel.Buildings;
                bool isExistPatientOrTeam = false;
                TOA toa = DataProvider.Ins.DB.TOAs.Where(p => p.SOTOA == selectedBuilding).First();
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
                        NotifyWindow notifyWindow = new NotifyWindow("Warning", "Bạn có chắc chắn muốn xoá toà " + selectedBuilding.ToString() + "?", "Visible");
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
                            if (buildings.Count > 0)
                                roomUsercontrolViewModel.SelectedBuilding = buildings[0].IDTOA;
                            NotifyWindow notifyWindow1 = new NotifyWindow("Success", "Xoá thành công toà " + selectedBuilding.ToString());
                            notifyWindow1.Show();
                            
                            if (roomUsercontrolViewModel.Buildings.Count>0)
                            {
                                roomUsercontrolViewModel.CurrentBuilding = 1;
                                roomUsercontrolViewModel.CanMoveBackward = false;
                                roomUsercontrolViewModel.SelectedBuilding = (int)roomUsercontrolViewModel.Buildings[0].SOTOA;
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
