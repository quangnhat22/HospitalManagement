using HospitalManagement.Model;
using HospitalManagement.Utils;
using HospitalManagement.View;
using HospitalManagement.View.Staff;
using HospitalManagement.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace HospitalManagement.Command
{
    internal class ChangeDoctorInformationClosingCommand : ICommand
    {
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
            ChangeDoctorInformationForm doctorForm = parameter as ChangeDoctorInformationForm;
            BACSI bs = DataProvider.Ins.DB.BACSIs.Find(doctorForm.txbCMND_CCCD.Text);
            DataProvider.Ins.DB.Entry<BACSI>(bs).Reload();
            doctorForm.txbHo.Text = bs.HO;
            doctorForm.txbTen.Text = bs.TEN;
            doctorForm.txbChuyenKhoa.Text = bs.CHUYENKHOA;
            doctorForm.txbQuocTich.Text = bs.QUOCTICH;
            doctorForm.txbDiaChi.Text = bs.DIACHI;
            doctorForm.txbEmail.Text = bs.EMAIL;
            doctorForm.txbGhiChu.Text = bs.GHICHU;
            doctorForm.txbSDT.Text = bs.SDT;
            doctorForm.txbVaiTro.Text = bs.VAITRO;
            doctorForm.txbIDTO.Text = bs.IDTO.ToString();
            if ((bool)bs.GIOITINH)
            {
                doctorForm.cbxGioiTinh.Text = "Nữ";
            }
            else doctorForm.cbxGioiTinh.Text = "Nam";
            DateTime text = (DateTime)bs.NGSINH;
            string date = text.ToString("dd/MM/yyyy");          
        }
    }
}
