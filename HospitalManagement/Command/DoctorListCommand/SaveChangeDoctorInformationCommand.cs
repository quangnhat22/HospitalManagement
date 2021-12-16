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
using System.Windows.Controls;
using System.Windows.Data;
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
                BindingExpression beHo = doctorForm.txbHo.GetBindingExpression(TextBox.TextProperty);
                beHo.UpdateSource();
                BindingExpression beTen = doctorForm.txbTen.GetBindingExpression(TextBox.TextProperty);
                beTen.UpdateSource();
                BindingExpression beSDT = doctorForm.txbSDT.GetBindingExpression(TextBox.TextProperty);
                beSDT.UpdateSource();
                BindingExpression beEmail = doctorForm.txbEmail.GetBindingExpression(TextBox.TextProperty);
                beEmail.UpdateSource();
                BindingExpression beDiaChi = doctorForm.txbDiaChi.GetBindingExpression(TextBox.TextProperty);
                beDiaChi.UpdateSource();
                BindingExpression beNGSinh = doctorForm.txbNGSinh.GetBindingExpression(DatePicker.TextProperty);
                beNGSinh.UpdateSource();
                BindingExpression beGioiTinh = doctorForm.cbxGioiTinh.GetBindingExpression(ComboBox.TextProperty);
                beGioiTinh.UpdateSource();
                BindingExpression beQuocTich = doctorForm.txbQuocTich.GetBindingExpression(TextBox.TextProperty);
                beQuocTich.UpdateSource();
                BindingExpression beChuyenKhoa = doctorForm.txbChuyenKhoa.GetBindingExpression(TextBox.TextProperty);
                beChuyenKhoa.UpdateSource();
                BindingExpression beVaiTro = doctorForm.txbVaiTro.GetBindingExpression(TextBox.TextProperty);
                beVaiTro.UpdateSource();
                BindingExpression beIDTO = doctorForm.cbxIDTO.GetBindingExpression(ComboBox.TextProperty);
                beIDTO.UpdateSource();
                BindingExpression beGhiChu = doctorForm.txbGhiChu.GetBindingExpression(TextBox.TextProperty);
                beGhiChu.UpdateSource();
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

            if (string.IsNullOrWhiteSpace(cdf.cbxIDTO.Text))
            {
                NotifyWindow notifyWindow = new NotifyWindow("Warning", "Vui lòng nhập id tổ");
                notifyWindow.ShowDialog();
                cdf.txbVaiTro.Focus();
                return false;
            }
            if (cdf.txbNGSinh.SelectedDate > DateTime.Now)
            {
                NotifyWindow notifyWindow = new NotifyWindow("Warning", "Ngày sinh không hợp lệ");
                notifyWindow.ShowDialog();
                cdf.txbNGSinh.Focus();
                return false;
            }
            if (!CheckCombobox(cdf.cbxIDTO.Text, cdf.cbxIDTO))
            {
                NotifyWindow notifyWindow = new NotifyWindow("Warning", "ID To không hợp lệ");
                notifyWindow.ShowDialog();
                cdf.cbxIDTO.Focus();
                return false;
            }
            return true;
        }
        private bool CheckCombobox(string text, ComboBox cbx)
        {
            foreach (var item in cbx.Items)
            {
                if (item.ToString() == text)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
