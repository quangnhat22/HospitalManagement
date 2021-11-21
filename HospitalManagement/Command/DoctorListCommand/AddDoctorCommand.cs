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

namespace HospitalManagement.Command
{
    class AddDoctorCommand : ICommand
    {
        private DoctorFormViewModel doctorFormViewModel;
        public AddDoctorCommand(DoctorFormViewModel doctorFormViewModel)
        {
            this.doctorFormViewModel = doctorFormViewModel;
        }


        public event EventHandler CanExecuteChanged
        {
            add { }
            remove { }
        }

        public bool CanExecute(object parameter)
        {
            return parameter == null ? false : true;
        }

        public void Execute(object parameter)
        {
            DoctorForm doctorForm = parameter as DoctorForm;
            string gioiTinh = doctorForm.cbxGioiTinh.SelectedValue as string;
            var doctorInput = new BACSI
            {
                HO = doctorForm.txtHo.Text,
                TEN = doctorForm.txtTen.Text,
                SDT = doctorForm.txtSDT.Text,
                EMAIL = doctorForm.txtEmail.Text,
                DIACHI = doctorForm.txtDiaChi.Text,
                NGSINH = doctorForm.txtNGSinh.DisplayDate,
                GIOITINH = (gioiTinh == "Nam"),
                QUOCTICH = doctorForm.txtQuocTich.Text,
                CMND_CCCD = doctorForm.txtCMND_CCCD.Text,
                CHUYENKHOA = doctorForm.txtChuyenKhoa.Text,
                VAITRO = doctorForm.txtVaiTro.Text,
                IDTO = Convert.ToInt32(doctorForm.txtIDTO.Text),
                GHICHU = doctorForm.txtGhiChu.Text,
            };
            DataProvider.Ins.DB.BACSIs.Add(doctorInput);
            DataProvider.Ins.DB.SaveChanges();
        }

    }
}