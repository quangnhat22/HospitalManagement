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
                    HO = doctorForm.txbHo.Text,
                    TEN = doctorForm.txbTen.Text,
                    SDT = doctorForm.txbSDT.Text,
                    EMAIL = doctorForm.txbEmail.Text,
                    DIACHI = doctorForm.txbDiaChi.Text,
                    NGSINH = doctorForm.txbNGSinh.DisplayDate,
                    GIOITINH = (gioiTinh == "Nam"),
                    QUOCTICH = doctorForm.txbQuocTich.Text,
                    CMND_CCCD = doctorForm.txbCMND_CCCD.Text,
                    CHUYENKHOA = doctorForm.txbChuyenKhoa.Text,
                    VAITRO = doctorForm.txbVaiTro.Text,
                    IDTO = Convert.ToInt32(doctorForm.txbIDTO.Text),
                    GHICHU = doctorForm.txbGhiChu.Text,
                };
                DataProvider.Ins.DB.BACSIs.Add(doctorInput);
                DataProvider.Ins.DB.SaveChanges();
                NotifyWindow notifyWindow = new NotifyWindow("Success", "Thêm thành công!");
            }          
            
        }
        public bool Check(DoctorForm df)
        {
            if (df == null) return false;
           
            
            if (string.IsNullOrWhiteSpace(df.txbHo.Text))
            {
                NotifyWindow notifyWindow = new NotifyWindow("Warning", "Vui lòng nhập họ");
                notifyWindow.ShowDialog();
                df.txbHo.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(df.txbTen.Text))
            {
                NotifyWindow notifyWindow = new NotifyWindow("Warning", "Vui lòng nhập tên");
                notifyWindow.ShowDialog();
                df.txbTen.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(df.txbSDT.Text))
            {
                NotifyWindow notifyWindow = new NotifyWindow("Warning", "Vui lòng nhập SDT");
                notifyWindow.ShowDialog();
                df.txbSDT.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(df.txbEmail.Text))
            {
                NotifyWindow notifyWindow = new NotifyWindow("Warning", "Vui lòng nhập email");
                notifyWindow.ShowDialog();
                df.txbEmail.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(df.txbDiaChi.Text))
            {
                NotifyWindow notifyWindow = new NotifyWindow("Warning", "Vui lòng nhập địa chỉ");
                notifyWindow.ShowDialog();
                df.txbDiaChi.Focus();
                return false;
            }            

            if (string.IsNullOrWhiteSpace(df.txbNGSinh.Text))
            {
                NotifyWindow notifyWindow = new NotifyWindow("Warning", "Vui lòng nhập ngày sinh");
                notifyWindow.ShowDialog();
                df.txbNGSinh.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(df.cbxGioiTinh.Text.ToString()))
            {
                NotifyWindow notifyWindow = new NotifyWindow("Warning", "Vui lòng chọn giới tính");
                notifyWindow.ShowDialog();
                df.cbxGioiTinh.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(df.txbQuocTich.Text))
            {
                NotifyWindow notifyWindow = new NotifyWindow("Warning", "Vui lòng nhập quốc tịch");
                notifyWindow.ShowDialog();
                df.txbQuocTich.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(df.txbCMND_CCCD.Text))
            {
                NotifyWindow notifyWindow = new NotifyWindow("Warning", "Vui lòng nhập CMND_CCCD");
                notifyWindow.ShowDialog();
                df.txbCMND_CCCD.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(df.txbChuyenKhoa.Text))
            {
                NotifyWindow notifyWindow = new NotifyWindow("Warning", "Vui lòng nhập chuyên Khoa");
                notifyWindow.ShowDialog();
                df.txbChuyenKhoa.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(df.txbVaiTro.Text))
            {
                NotifyWindow notifyWindow = new NotifyWindow("Warning", "Vui lòng nhập vai trò");
                notifyWindow.ShowDialog();
                df.txbVaiTro.Focus();
                return false;
            }
            
            if (string.IsNullOrWhiteSpace(df.txbIDTO.Text))
            {
                NotifyWindow notifyWindow = new NotifyWindow("Warning", "Vui lòng nhập id tổ");
                notifyWindow.ShowDialog();
                df.txbVaiTro.Focus();
                return false;
            }
            //Kiểm tra CMND nha mày do nó liên quan tới Nurse vs Patient nữa @@//
            //Kiem tra cmnnd//
            if (DataProvider.Ins.DB.BENHNHANs.Any(x => x.CMND_CCCD == df.txbCMND_CCCD.Text))
            {
                NotifyWindow notifyWindow = new NotifyWindow("Warning", "Trùng mã CMND_CCCD");
                notifyWindow.ShowDialog();
                df.txbCMND_CCCD.Focus();
                return false;
            }
            if (DataProvider.Ins.DB.BACSIs.Any(x => x.CMND_CCCD == df.txbCMND_CCCD.Text))
            {
                NotifyWindow notifyWindow = new NotifyWindow("Warning", "Trùng mã CMND_CCCD");
                notifyWindow.ShowDialog();
                df.txbCMND_CCCD.Focus();
                return false;
            }
            if (DataProvider.Ins.DB.YTAs.Any(x => x.CMND_CCCD == df.txbCMND_CCCD.Text))
            {
                NotifyWindow notifyWindow = new NotifyWindow("Warning", "Trùng mã CMND_CCCD");
                notifyWindow.ShowDialog();
                df.txbCMND_CCCD.Focus();
                return false;
            }
            //Kiểm tra IDTO//
            try
            {
                int idTo = int.Parse(df.txbIDTO.Text);
                var checkTO = DataProvider.Ins.DB.TOes.Any(x => x.ID == idTo);
                if (checkTO == false)
                {
                    NotifyWindow notifyWindow = new NotifyWindow("Warning", "Tổ không tồn tại");
                    notifyWindow.ShowDialog();
                    df.txbIDTO.Focus();
                    return false;
                }
            }
            catch (FormatException)
            {
                NotifyWindow notifyWindow = new NotifyWindow("Warning", "ID Tổ là một số nguyên dương");
                notifyWindow.ShowDialog();
                df.txbIDTO.Focus();
                return false;
            }

            return true;
        }
    }
    
}