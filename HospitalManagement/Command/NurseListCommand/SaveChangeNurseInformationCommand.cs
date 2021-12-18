using HospitalManagement.Model;
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
    internal class SaveChangeNurseInformationCommand: ICommand
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
            ChangeNurseInformationForm nurseForm = parameter as ChangeNurseInformationForm;
            if (Check(nurseForm))
            {
                BindingExpression beHo = nurseForm.txbHo.GetBindingExpression(TextBox.TextProperty);
                beHo.UpdateSource();
                BindingExpression beTen = nurseForm.txbTen.GetBindingExpression(TextBox.TextProperty);
                beTen.UpdateSource();
                BindingExpression beSDT = nurseForm.txbSDT.GetBindingExpression(TextBox.TextProperty);
                beSDT.UpdateSource();
                BindingExpression beEmail = nurseForm.txbEmail.GetBindingExpression(TextBox.TextProperty);
                beEmail.UpdateSource();
                BindingExpression beDiaChi = nurseForm.txbDiaChi.GetBindingExpression(TextBox.TextProperty);
                beDiaChi.UpdateSource();
                BindingExpression beNGSinh = nurseForm.txbNGSinh.GetBindingExpression(DatePicker.TextProperty);
                beNGSinh.UpdateSource();
                BindingExpression beGioiTinh = nurseForm.cbxGioiTinh.GetBindingExpression(ComboBox.TextProperty);
                beGioiTinh.UpdateSource();
                BindingExpression beQuocTich = nurseForm.txbQuocTich.GetBindingExpression(TextBox.TextProperty);
                beQuocTich.UpdateSource();
                BindingExpression beChuyenKhoa = nurseForm.txbChuyenKhoa.GetBindingExpression(TextBox.TextProperty);
                beChuyenKhoa.UpdateSource();
                BindingExpression beVaiTro = nurseForm.txbVaiTro.GetBindingExpression(TextBox.TextProperty);
                beVaiTro.UpdateSource();
                YTA yt = DataProvider.Ins.DB.YTAs.Find(nurseForm.txbCMND_CCCD.Text);
                if(nurseForm.cbxIDTO.Text != yt.IDTO.ToString())
                {
                    yt.YTALIENQUANs.Clear();
                }
                BindingExpression beIDTO = nurseForm.cbxIDTO.GetBindingExpression(ComboBox.TextProperty);
                beIDTO.UpdateSource();
                BindingExpression beGhiChu = nurseForm.txbGhiChu.GetBindingExpression(TextBox.TextProperty);
                beGhiChu.UpdateSource();
                DataProvider.Ins?.DB?.SaveChanges();
                NotifyWindow notifyWindow = new NotifyWindow("Success", "Đã cập nhật thành công");
                notifyWindow.ShowDialog();
            }
        }
        public bool Check(ChangeNurseInformationForm cnf)
        {
            if (cnf == null) return false;


            if (string.IsNullOrWhiteSpace(cnf.txbHo.Text))
            {
                NotifyWindow notifyWindow = new NotifyWindow("Warning", "Vui lòng nhập họ");
                notifyWindow.ShowDialog();
                cnf.txbHo.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(cnf.txbTen.Text))
            {
                NotifyWindow notifyWindow = new NotifyWindow("Warning", "Vui lòng nhập tên");
                notifyWindow.ShowDialog();
                cnf.txbTen.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(cnf.txbSDT.Text))
            {
                NotifyWindow notifyWindow = new NotifyWindow("Warning", "Vui lòng nhập SDT");
                notifyWindow.ShowDialog();
                cnf.txbSDT.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(cnf.txbEmail.Text))
            {
                NotifyWindow notifyWindow = new NotifyWindow("Warning", "Vui lòng nhập email");
                notifyWindow.ShowDialog();
                cnf.txbEmail.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(cnf.txbDiaChi.Text))
            {
                NotifyWindow notifyWindow = new NotifyWindow("Warning", "Vui lòng nhập địa chỉ");
                notifyWindow.ShowDialog();
                cnf.txbDiaChi.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(cnf.txbNGSinh.Text))
            {
                NotifyWindow notifyWindow = new NotifyWindow("Warning", "Vui lòng nhập ngày sinh");
                notifyWindow.ShowDialog();
                cnf.txbNGSinh.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(cnf.cbxGioiTinh.Text.ToString()))
            {
                NotifyWindow notifyWindow = new NotifyWindow("Warning", "Vui lòng chọn giới tính");
                notifyWindow.ShowDialog();
                cnf.cbxGioiTinh.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(cnf.txbQuocTich.Text))
            {
                NotifyWindow notifyWindow = new NotifyWindow("Warning", "Vui lòng nhập quốc tịch");
                notifyWindow.ShowDialog();
                cnf.txbQuocTich.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(cnf.txbChuyenKhoa.Text))
            {
                NotifyWindow notifyWindow = new NotifyWindow("Warning", "Vui lòng nhập chuyên Khoa");
                notifyWindow.ShowDialog();
                cnf.txbChuyenKhoa.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(cnf.txbVaiTro.Text))
            {
                NotifyWindow notifyWindow = new NotifyWindow("Warning", "Vui lòng nhập vai trò");
                notifyWindow.ShowDialog();
                cnf.txbVaiTro.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(cnf.cbxIDTO.Text))
            {
                NotifyWindow notifyWindow = new NotifyWindow("Warning", "Vui lòng nhập id tổ");
                notifyWindow.ShowDialog();
                cnf.cbxIDTO.Focus();
                return false;
            }
            if (cnf.txbNGSinh.SelectedDate > DateTime.Now)
            {
                NotifyWindow notifyWindow = new NotifyWindow("Warning", "Ngày sinh không hợp lệ");
                notifyWindow.ShowDialog();
                cnf.txbNGSinh.Focus();
                return false;
            }
            if (!CheckCombobox(cnf.cbxIDTO.Text, cnf.cbxIDTO))
            {
                NotifyWindow notifyWindow = new NotifyWindow("Warning", "ID To không hợp lệ");
                notifyWindow.ShowDialog();
                cnf.cbxIDTO.Focus();
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
