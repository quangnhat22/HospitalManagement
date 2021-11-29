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
                BindingExpression beIDPhong = patientForm.txbIDPhong.GetBindingExpression(TextBox.TextProperty);
                beIDPhong.UpdateSource();
                BindingExpression beSoGiuong = patientForm.txbSoGiuong.GetBindingExpression(TextBox.TextProperty);
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

            if (string.IsNullOrWhiteSpace(pf.txbCMND_CCCD.Text))
            {
                NotifyWindow notifyWindow = new NotifyWindow("Warning", "Vui lòng nhập CMND_CCCD");
                notifyWindow.ShowDialog();
                pf.txbCMND_CCCD.Focus();
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


            if (string.IsNullOrWhiteSpace(pf.txbSoGiuong.Text))
            {
                NotifyWindow notifyWindow = new NotifyWindow("Warning", "Vui lòng nhập số giường");
                notifyWindow.ShowDialog();
                pf.cbxTinhTrang.Focus();
                return false;
            }
            //Kiểm tra MaBN
            //Kiem tra phong
            try
            {
                int idPhong = int.Parse(pf.txbIDPhong.Text);
                var checkPHONG = DataProvider.Ins.DB.PHONGs.Any(x => x.ID == idPhong);
                if (checkPHONG == false)
                {
                    NotifyWindow notifyWindow = new NotifyWindow("Warning", "Phòng không tồn tại");
                    notifyWindow.ShowDialog();
                    pf.txbIDPhong.Focus();
                    return false;
                }
            }
            catch (FormatException)
            {
                NotifyWindow notifyWindow = new NotifyWindow("Warning", "ID Phòng là một số nguyên dương");
                notifyWindow.ShowDialog();
                pf.txbIDPhong.Focus();
                return false;
            }
            return true;
        }
    }
}
