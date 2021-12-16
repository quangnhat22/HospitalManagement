using HospitalManagement.Model;
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

namespace HospitalManagement.Command
{
    internal class NurseFirstLoginUpdateCommand : ICommand
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
            NurseForm nurseForm = parameter as NurseForm;
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
                BindingExpression beIDTO = nurseForm.cbxIDTO.GetBindingExpression(ComboBox.TextProperty);
                beIDTO.UpdateSource();
                BindingExpression beGhiChu = nurseForm.txbGhiChu.GetBindingExpression(TextBox.TextProperty);
                beGhiChu.UpdateSource();
                DataProvider.Ins?.DB?.SaveChanges();
                Window window = System.Windows.Application.Current.MainWindow as Window;
                MainWindowViewModel.User = FirstLoginViewModel.User;
                MainWindow mainWindow = new MainWindow();
                System.Windows.Application.Current.MainWindow = mainWindow;
                System.Windows.Application.Current.MainWindow.Show();
                window.Close();
                Thread windowThread = new Thread(new ThreadStart(() =>
                {
                    window.Closed += (s, e) =>
                    Dispatcher.CurrentDispatcher.BeginInvokeShutdown(DispatcherPriority.Background);
                    System.Windows.Threading.Dispatcher.Run();
                }));
                windowThread.SetApartmentState(ApartmentState.STA);
                windowThread.IsBackground = true;
                windowThread.Start();
                NotifyWindow notifyWindow = new NotifyWindow("Success", "Đã cập nhật thành công");
                notifyWindow.ShowDialog();
            }
        }
        public bool Check(NurseForm nf)
        {
            if (nf == null) return false;


            if (string.IsNullOrWhiteSpace(nf.txbHo.Text))
            {
                NotifyWindow notifyWindow = new NotifyWindow("Warning", "Vui lòng nhập họ");
                notifyWindow.ShowDialog();
                nf.txbHo.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(nf.txbTen.Text))
            {
                NotifyWindow notifyWindow = new NotifyWindow("Warning", "Vui lòng nhập tên");
                notifyWindow.ShowDialog();
                nf.txbTen.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(nf.txbSDT.Text))
            {
                NotifyWindow notifyWindow = new NotifyWindow("Warning", "Vui lòng nhập SDT");
                notifyWindow.ShowDialog();
                nf.txbSDT.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(nf.txbEmail.Text))
            {
                NotifyWindow notifyWindow = new NotifyWindow("Warning", "Vui lòng nhập email");
                notifyWindow.ShowDialog();
                nf.txbEmail.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(nf.txbDiaChi.Text))
            {
                NotifyWindow notifyWindow = new NotifyWindow("Warning", "Vui lòng nhập địa chỉ");
                notifyWindow.ShowDialog();
                nf.txbDiaChi.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(nf.txbNGSinh.Text))
            {
                NotifyWindow notifyWindow = new NotifyWindow("Warning", "Vui lòng nhập ngày sinh");
                notifyWindow.ShowDialog();
                nf.txbNGSinh.Focus();
                return false;
            }

            //if (string.IsNullOrWhiteSpace(nf.cbxGioiTinh.Text.ToString()))
            //{
            //    NotifyWindow notifyWindow = new NotifyWindow("Warning", "Vui lòng chọn giới tính");
            //    notifyWindow.ShowDialog();
            //    nf.cbxGioiTinh.Focus();
            //    return false;
            //}

            if (string.IsNullOrWhiteSpace(nf.txbQuocTich.Text))
            {
                NotifyWindow notifyWindow = new NotifyWindow("Warning", "Vui lòng nhập quốc tịch");
                notifyWindow.ShowDialog();
                nf.txbQuocTich.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(nf.txbChuyenKhoa.Text))
            {
                NotifyWindow notifyWindow = new NotifyWindow("Warning", "Vui lòng nhập chuyên Khoa");
                notifyWindow.ShowDialog();
                nf.txbChuyenKhoa.Focus();
                return false;
            }

            //if (string.IsNullOrWhiteSpace(nf.txbVaiTro.Text))
            //{
            //    NotifyWindow notifyWindow = new NotifyWindow("Warning", "Vui lòng nhập vai trò");
            //    notifyWindow.ShowDialog();
            //    nf.txbVaiTro.Focus();
            //    return false;
            //}

            //if (string.IsNullOrWhiteSpace(nf.cbxIDTO.Text))
            //{
            //    NotifyWindow notifyWindow = new NotifyWindow("Warning", "Vui lòng nhập id tổ");
            //    notifyWindow.ShowDialog();
            //    nf.cbxIDTO.Focus();
            //    return false;
            //}
            //if (!CheckCombobox(nf.cbxIDTO.Text, nf.cbxIDTO))
            //{
            //    NotifyWindow notifyWindow = new NotifyWindow("Warning", "ID To không hợp lệ");
            //    notifyWindow.ShowDialog();
            //    nf.cbxIDTO.Focus();
            //    return false;
            //}

            return true;
        }
        //private bool CheckCombobox(string text, ComboBox cbx)
        //{
        //    foreach (var item in cbx.Items)
        //    {
        //        if (item.ToString() == text)
        //        {
        //            return true;
        //        }
        //    }
        //    return false;
        //}
    }
}
