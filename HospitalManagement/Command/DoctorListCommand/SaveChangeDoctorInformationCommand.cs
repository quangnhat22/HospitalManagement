﻿using HospitalManagement.Model;
using HospitalManagement.Utils;
using HospitalManagement.View;
using HospitalManagement.View.Staff;
using HospitalManagement.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Threading;
using static System.Net.Mime.MediaTypeNames;

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
                BindingExpression beVaiTro = doctorForm.cbxVaiTro.GetBindingExpression(ComboBox.TextProperty);
                beVaiTro.UpdateSource();
                BACSI bs = DataProvider.Ins.DB.BACSIs.Find(doctorForm.txbCMND_CCCD.Text);
                if(doctorForm.cbxVaiTro.Text == "Tổ trưởng")
                {
                    bs.USER.ROLE = "leader";
                }    
                else
                {
                    bs.USER.ROLE = "doctor";
                }
                if (doctorForm.cbxIDTO.Text != bs.IDTO.ToString())
                {
                    bs.BACSILIENQUANs.Clear();
                }
                BindingExpression beIDTO = doctorForm.cbxIDTO.GetBindingExpression(ComboBox.TextProperty);
                beIDTO.UpdateSource();
                BindingExpression beGhiChu = doctorForm.txbGhiChu.GetBindingExpression(TextBox.TextProperty);
                beGhiChu.UpdateSource();
                DataProvider.Ins?.DB?.SaveChanges();               
                NotifyWindow notifyWindow = new NotifyWindow("Success", "Đã cập nhật thành công");
                notifyWindow.ShowDialog();                
            }
        }
        public bool Check(ChangeDoctorInformationForm df)
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
            
            if (string.IsNullOrWhiteSpace(df.txbChuyenKhoa.Text))
            {
                NotifyWindow notifyWindow = new NotifyWindow("Warning", "Vui lòng nhập chuyên Khoa");
                notifyWindow.ShowDialog();
                df.txbChuyenKhoa.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(df.cbxVaiTro.Text))
            {
                NotifyWindow notifyWindow = new NotifyWindow("Warning", "Vui lòng nhập vai trò");
                notifyWindow.ShowDialog();
                df.cbxVaiTro.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(df.cbxIDTO.Text))
            {
                NotifyWindow notifyWindow = new NotifyWindow("Warning", "Vui lòng nhập id tổ");
                notifyWindow.ShowDialog();
                df.cbxIDTO.Focus();
                return false;
            }
            if (df.txbNGSinh.SelectedDate > DateTime.Now)
            {
                NotifyWindow notifyWindow = new NotifyWindow("Warning", "Ngày sinh không hợp lệ");
                notifyWindow.ShowDialog();
                df.txbNGSinh.Focus();
                return false;
            }
               
            if (!CheckCombobox(df.cbxIDTO.Text, df.cbxIDTO))
            {
                NotifyWindow notifyWindow = new NotifyWindow("Warning", "ID To không hợp lệ");
                notifyWindow.ShowDialog();
                df.cbxIDTO.Focus();
                return false;
            }
            int idTO = int.Parse(df.cbxIDTO.Text);
            if (df.cbxVaiTro.Text == "Tổ trưởng" && DataProvider.Ins.DB.BACSIs.Any(x => x.VAITRO == "Tổ trưởng" && x.CMND_CCCD != df.txbCMND_CCCD.Text && x.IDTO == idTO))
            {
                NotifyWindow notifyWindow = new NotifyWindow("Warning", "Tổ đã có nhóm trưởng");
                notifyWindow.ShowDialog();
                df.cbxVaiTro.Focus();
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
