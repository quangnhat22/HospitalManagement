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
            
            if(Check(doctorForm))
            {
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
        public bool Check(DoctorForm df)
        {
            if (df == null) return false;
            List<BACSI> doctors = doctorFormViewModel?.db?.DB?.BACSIs?.ToList();
            
            if (string.IsNullOrWhiteSpace(df.txtHo.Text))
            {
                NotifyWindow notifyWindow = new NotifyWindow("Warning", "Vui lòng nhập họ");
                notifyWindow.ShowDialog();
                df.txtHo.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(df.txtTen.Text))
            {
                NotifyWindow notifyWindow = new NotifyWindow("Warning", "Vui lòng nhập tên");
                notifyWindow.ShowDialog();
                df.txtTen.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(df.txtSDT.Text))
            {
                NotifyWindow notifyWindow = new NotifyWindow("Warning", "Vui lòng nhập SDT");
                notifyWindow.ShowDialog();
                df.txtSDT.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(df.txtEmail.Text))
            {
                NotifyWindow notifyWindow = new NotifyWindow("Warning", "Vui lòng nhập email");
                notifyWindow.ShowDialog();
                df.txtEmail.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(df.txtDiaChi.Text))
            {
                NotifyWindow notifyWindow = new NotifyWindow("Warning", "Vui lòng nhập địa chỉ");
                notifyWindow.ShowDialog();
                df.txtDiaChi.Focus();
                return false;
            }            

            if (string.IsNullOrWhiteSpace(df.txtNGSinh.Text))
            {
                NotifyWindow notifyWindow = new NotifyWindow("Warning", "Vui lòng nhập ngày sinh");
                notifyWindow.ShowDialog();
                df.txtNGSinh.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(df.cbxGioiTinh.SelectedValue.ToString()))
            {
                NotifyWindow notifyWindow = new NotifyWindow("Warning", "Vui lòng chọn giới tính");
                notifyWindow.ShowDialog();
                df.cbxGioiTinh.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(df.txtQuocTich.Text))
            {
                NotifyWindow notifyWindow = new NotifyWindow("Warning", "Vui lòng nhập quốc tịch");
                notifyWindow.ShowDialog();
                df.txtQuocTich.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(df.txtCMND_CCCD.Text))
            {
                NotifyWindow notifyWindow = new NotifyWindow("Warning", "Vui lòng nhập CMND_CCCD");
                notifyWindow.ShowDialog();
                df.txtCMND_CCCD.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(df.txtChuyenKhoa.Text))
            {
                NotifyWindow notifyWindow = new NotifyWindow("Warning", "Vui lòng nhập chuyên Khoa");
                notifyWindow.ShowDialog();
                df.txtChuyenKhoa.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(df.txtVaiTro.Text))
            {
                NotifyWindow notifyWindow = new NotifyWindow("Warning", "Vui lòng nhập vai trò");
                notifyWindow.ShowDialog();
                df.txtVaiTro.Focus();
                return false;
            }
            //Kiểm tra IDTO//
            if (string.IsNullOrWhiteSpace(df.txtIDTO.Text))
            {
                NotifyWindow notifyWindow = new NotifyWindow("Warning", "Vui lòng nhập id tổ");
                notifyWindow.ShowDialog();
                df.txtVaiTro.Focus();
                return false;
            }

            foreach (BACSI doctor in doctors)
            {
                if (doctor.CMND_CCCD == df.txtCMND_CCCD.Text)
                {
                    NotifyWindow notifyWindow = new NotifyWindow("Warning", "Trùng mã CMND_CCCD");
                    notifyWindow.ShowDialog();
                    df.txtCMND_CCCD.Focus();
                    return false;
                }
            }

            return true;
        }
    }
    
}