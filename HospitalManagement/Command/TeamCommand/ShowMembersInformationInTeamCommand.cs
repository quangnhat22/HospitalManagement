using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using HospitalManagement.Model;
using HospitalManagement.ViewModel;
using HospitalManagement.Utils;
using HospitalManagement.View;
using HospitalManagement.View.Staff;

namespace HospitalManagement.Command.TeamCommand
{
    class ShowMembersInformationInTeamCommand : ICommand
    {
        private TeamViewmodel teamViewmodel;

        public ShowMembersInformationInTeamCommand(TeamViewmodel teamViewmodel)
        {
            this.teamViewmodel = teamViewmodel;
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
            int index = (int)parameter;
            StaffInformation member = teamViewmodel.Members[index];
            if (member.PhanLoai== "Tổ Trưởng"|| member.PhanLoai == "Bác Sĩ")
            {
                BACSI bs = new BACSI();
                bs.CMND_CCCD = member.Cmnd_cccd;
                bs.HO = member.Ho;
                bs.TEN = member.Ten;
                bs.SDT = member.Sdt;
                bs.EMAIL = member.Email;
                bs.QUOCTICH = member.QuocTich;
                bs.DIACHI = member.DiaChi;
                bs.NGSINH = member.NgSinh;
                bs.GIOITINH = member.GioiTinh;
                bs.VAITRO = member.VaiTro;
                bs.CHUYENKHOA = member.ChuyenKhoa;
                bs.GHICHU = member.GhiChu;
                bs.IDTO = member.IdTo;
                DoctorInformationForm dif = new DoctorInformationForm(bs);
                dif.Show();
            }
            else
            {
                YTA yta = new YTA();
                yta.CMND_CCCD = member.Cmnd_cccd;
                yta.HO = member.Ho;
                yta.TEN = member.Ten;
                yta.SDT = member.Sdt;
                yta.EMAIL = member.Email;
                yta.QUOCTICH = member.QuocTich;
                yta.DIACHI = member.DiaChi;
                yta.NGSINH = member.NgSinh;
                yta.GIOITINH = member.GioiTinh;
                yta.VAITRO = member.VaiTro;
                yta.CHUYENKHOA = member.ChuyenKhoa;
                yta.GHICHU = member.GhiChu;
                yta.IDTO = member.IdTo;
                NurseInformationForm nif = new NurseInformationForm(yta);
                nif.Show();
            }
        }
    }
}
