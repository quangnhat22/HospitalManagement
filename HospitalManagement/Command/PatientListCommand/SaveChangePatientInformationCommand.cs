using HospitalManagement.Model;
using HospitalManagement.View;
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
    internal class SaveChangePatientInformationCommand: ICommand
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
            ChangePatientInformationForm patientForm = parameter as ChangePatientInformationForm;
            if (Check(patientForm))
            {
                BindingExpression beHo = patientForm.txbHo.GetBindingExpression(TextBox.TextProperty);
                beHo.UpdateSource();
                BindingExpression beTen = patientForm.txbTen.GetBindingExpression(TextBox.TextProperty);
                beTen.UpdateSource();
                BindingExpression beSDT = patientForm.txbSDT.GetBindingExpression(TextBox.TextProperty);
                beSDT.UpdateSource();
                BindingExpression beEmail = patientForm.txbEmail.GetBindingExpression(TextBox.TextProperty);
                beEmail.UpdateSource();
                BindingExpression beDiaChi = patientForm.txbDiaChi.GetBindingExpression(TextBox.TextProperty);
                beDiaChi.UpdateSource();
                BindingExpression beNGSinh = patientForm.txbNGSinh.GetBindingExpression(DatePicker.TextProperty);
                beNGSinh.UpdateSource();
                BindingExpression beGioiTinh = patientForm.cbxGioiTinh.GetBindingExpression(ComboBox.TextProperty);
                beGioiTinh.UpdateSource();
                BindingExpression beQuocTich = patientForm.txbQuocTich.GetBindingExpression(TextBox.TextProperty);
                beQuocTich.UpdateSource();
                BindingExpression beNGNhapVien = patientForm.txbNGNhapVien.GetBindingExpression(DatePicker.TextProperty);
                beNGNhapVien.UpdateSource();
                BindingExpression beBenhNen = patientForm.txbBenhNen.GetBindingExpression(TextBox.TextProperty);
                beBenhNen.UpdateSource();
                BindingExpression beTinhTrang = patientForm.cbxTinhTrang.GetBindingExpression(ComboBox.TextProperty);
                beTinhTrang.UpdateSource();
                BindingExpression beIDPhong = patientForm.cbxIDPhong.GetBindingExpression(ComboBox.TextProperty);
                beIDPhong.UpdateSource();
                BindingExpression beSoGiuong = patientForm.cbxSoGiuong.GetBindingExpression(ComboBox.TextProperty);
                beSoGiuong.UpdateSource();
                BindingExpression beGhiChu = patientForm.txbGhiChu.GetBindingExpression(TextBox.TextProperty);
                beGhiChu.UpdateSource();
                DataProvider.Ins?.DB?.SaveChanges();
                NotifyWindow notifyWindow = new NotifyWindow("Success", "Đã cập nhập thành công");
                notifyWindow.ShowDialog();
            }
        }
        public bool Check(ChangePatientInformationForm pf)
        {
            if (pf == null) return false;
            if (string.IsNullOrWhiteSpace(pf.txbHo.Text))
            {
                NotifyWindow notifyWindow = new NotifyWindow("Warning", "Vui lòng nhập họ");
                notifyWindow.ShowDialog();
                pf.txbHo.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(pf.txbTen.Text))
            {
                NotifyWindow notifyWindow = new NotifyWindow("Warning", "Vui lòng nhập tên");
                notifyWindow.ShowDialog();
                pf.txbTen.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(pf.txbSDT.Text))
            {
                NotifyWindow notifyWindow = new NotifyWindow("Warning", "Vui lòng nhập SDT");
                notifyWindow.ShowDialog();
                pf.txbSDT.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(pf.txbEmail.Text))
            {
                NotifyWindow notifyWindow = new NotifyWindow("Warning", "Vui lòng nhập email");
                notifyWindow.ShowDialog();
                pf.txbEmail.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(pf.txbDiaChi.Text))
            {
                NotifyWindow notifyWindow = new NotifyWindow("Warning", "Vui lòng nhập địa chỉ");
                notifyWindow.ShowDialog();
                pf.txbDiaChi.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(pf.txbNGSinh.Text))
            {
                NotifyWindow notifyWindow = new NotifyWindow("Warning", "Vui lòng nhập ngày sinh");
                notifyWindow.ShowDialog();
                pf.txbNGSinh.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(pf.cbxGioiTinh.Text.ToString()))
            {
                NotifyWindow notifyWindow = new NotifyWindow("Warning", "Vui lòng chọn giới tính");
                notifyWindow.ShowDialog();
                pf.cbxGioiTinh.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(pf.txbQuocTich.Text))
            {
                NotifyWindow notifyWindow = new NotifyWindow("Warning", "Vui lòng nhập quốc tịch");
                notifyWindow.ShowDialog();
                pf.txbQuocTich.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(pf.txbNGNhapVien.Text))
            {
                NotifyWindow notifyWindow = new NotifyWindow("Warning", "Vui lòng chọn ngày nhập viện");
                notifyWindow.ShowDialog();
                pf.txbNGNhapVien.Focus();
                return false;
            }
            // ko có bệnh nền //
            //if (string.IsNullOrWhiteSpace(pf.txbBenhNen.Text))
            //{
            //    NotifyWindow notifyWindow = new NotifyWindow("Warning", "Vui lòng nhập bệnh nền");
            //    notifyWindow.ShowDialog();
            //    pf.txbBenhNen.Focus();
            //    return false;
            //}
            if (string.IsNullOrWhiteSpace(pf.cbxTinhTrang.Text.ToString()))
            {
                NotifyWindow notifyWindow = new NotifyWindow("Warning", "Vui lòng chọn tình trạng");
                notifyWindow.ShowDialog();
                pf.cbxTinhTrang.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(pf.cbxSoGiuong.Text))
            {
                NotifyWindow notifyWindow = new NotifyWindow("Warning", "Vui lòng nhập số giường");
                notifyWindow.ShowDialog();
                pf.cbxTinhTrang.Focus();
                return false;
            }
            
            if (pf.txbNGSinh.SelectedDate > DateTime.Now)
            {
                NotifyWindow notifyWindow = new NotifyWindow("Warning", "Ngày sinh không hợp lệ");
                notifyWindow.ShowDialog();
                pf.txbNGSinh.Focus();
                return false;
            }
            if (pf.txbNGSinh.SelectedDate >= pf.txbNGNhapVien.SelectedDate || pf.txbNGNhapVien.SelectedDate > DateTime.Now)
            {
                NotifyWindow notifyWindow = new NotifyWindow("Warning", "Ngày nhập viện không hợp lệ");
                notifyWindow.ShowDialog();
                pf.txbNGNhapVien.Focus();
                return false;
            }
            if (!CheckCombobox(pf.cbxIDPhong.Text, pf.cbxIDPhong))
            {
                NotifyWindow notifyWindow = new NotifyWindow("Warning", "ID phòng không hợp lệ");
                notifyWindow.ShowDialog();
                pf.cbxIDPhong.Focus();
                return false;
            }
            if (!CheckCombobox(pf.cbxSoGiuong.Text, pf.cbxSoGiuong))
            {
                NotifyWindow notifyWindow = new NotifyWindow("Warning", "Số giường không hợp lệ");
                notifyWindow.ShowDialog();
                pf.cbxSoGiuong.Focus();
                return false;
            }
            //Kiem tra phong
            int idPhong = int.Parse(pf.cbxIDPhong.Text);
            if (DataProvider.Ins.DB.BENHNHANs.Any(x => x.IDPHONG == idPhong && x.GIUONGBENH == pf.cbxSoGiuong.Text && x.CMND_CCCD != pf.txbCMND_CCCD.Text))
            {
                NotifyWindow notifyWindow = new NotifyWindow("Warning", "Giường bệnh đã có bệnh nhân");
                notifyWindow.ShowDialog();
                pf.cbxSoGiuong.Focus();
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
