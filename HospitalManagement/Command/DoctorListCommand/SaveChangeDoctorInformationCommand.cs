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
using System.Windows.Input;

namespace HospitalManagement.Command
{
    internal class SaveChangeDoctorInformationCommand: ICommand
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
            if (Check(doctorForm))
            {
                DataProvider.Ins?.DB?.SaveChanges();
                NotifyWindow notifyWindow = new NotifyWindow("Success", "Đã cập nhập thành công");
                notifyWindow.ShowDialog();
            }
        }
        public bool Check(ChangeDoctorInformationForm cdf)
        {
            if (cdf == null) return false;


            if (string.IsNullOrWhiteSpace(cdf.txbHo.Text))
            {
                NotifyWindow notifyWindow = new NotifyWindow("Warning", "Vui lòng nhập họ");
                notifyWindow.ShowDialog();
                cdf.txbHo.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(cdf.txbTen.Text))
            {
                NotifyWindow notifyWindow = new NotifyWindow("Warning", "Vui lòng nhập tên");
                notifyWindow.ShowDialog();
                cdf.txbTen.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(cdf.txbSDT.Text))
            {
                NotifyWindow notifyWindow = new NotifyWindow("Warning", "Vui lòng nhập SDT");
                notifyWindow.ShowDialog();
                cdf.txbSDT.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(cdf.txbEmail.Text))
            {
                NotifyWindow notifyWindow = new NotifyWindow("Warning", "Vui lòng nhập email");
                notifyWindow.ShowDialog();
                cdf.txbEmail.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(cdf.txbDiaChi.Text))
            {
                NotifyWindow notifyWindow = new NotifyWindow("Warning", "Vui lòng nhập địa chỉ");
                notifyWindow.ShowDialog();
                cdf.txbDiaChi.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(cdf.txbNGSinh.Text))
            {
                NotifyWindow notifyWindow = new NotifyWindow("Warning", "Vui lòng nhập ngày sinh");
                notifyWindow.ShowDialog();
                cdf.txbNGSinh.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(cdf.cbxGioiTinh.Text.ToString()))
            {
                NotifyWindow notifyWindow = new NotifyWindow("Warning", "Vui lòng chọn giới tính");
                notifyWindow.ShowDialog();
                cdf.cbxGioiTinh.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(cdf.txbQuocTich.Text))
            {
                NotifyWindow notifyWindow = new NotifyWindow("Warning", "Vui lòng nhập quốc tịch");
                notifyWindow.ShowDialog();
                cdf.txbQuocTich.Focus();
                return false;
            }
            
            if (string.IsNullOrWhiteSpace(cdf.txbChuyenKhoa.Text))
            {
                NotifyWindow notifyWindow = new NotifyWindow("Warning", "Vui lòng nhập chuyên Khoa");
                notifyWindow.ShowDialog();
                cdf.txbChuyenKhoa.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(cdf.txbVaiTro.Text))
            {
                NotifyWindow notifyWindow = new NotifyWindow("Warning", "Vui lòng nhập vai trò");
                notifyWindow.ShowDialog();
                cdf.txbVaiTro.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(cdf.txbIDTO.Text))
            {
                NotifyWindow notifyWindow = new NotifyWindow("Warning", "Vui lòng nhập id tổ");
                notifyWindow.ShowDialog();
                cdf.txbVaiTro.Focus();
                return false;
            }
            //Kiểm tra CMND nha mày do nó liên quan tới Nurse vs Patient nữa @@//
            //Kiem tra cmnnd//            
            //Kiểm tra IDTO//
            try
            {
                int idTo = int.Parse(cdf.txbIDTO.Text);
                var checkTO = DataProvider.Ins.DB.TOes.Any(x => x.ID == idTo);
                if (checkTO == false)
                {
                    NotifyWindow notifyWindow = new NotifyWindow("Warning", "Tổ không tồn tại");
                    notifyWindow.ShowDialog();
                    cdf.txbIDTO.Focus();
                    return false;
                }
            }
            catch (FormatException)
            {
                NotifyWindow notifyWindow = new NotifyWindow("Warning", "ID Tổ là một số nguyên dương");
                notifyWindow.ShowDialog();
                cdf.txbIDTO.Focus();
                return false;
            }

            return true;
        }
        
    }
}
