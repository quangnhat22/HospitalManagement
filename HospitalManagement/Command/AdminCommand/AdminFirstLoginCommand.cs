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
    internal class AdminFirstLoginCommand : ICommand
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
            AdminForm adminForm = parameter as AdminForm;
            if (Check(adminForm))
            {
                BindingExpression beHo = adminForm.txbHo.GetBindingExpression(TextBox.TextProperty);
                beHo.UpdateSource();
                BindingExpression beTen = adminForm.txbTen.GetBindingExpression(TextBox.TextProperty);
                beTen.UpdateSource();
                BindingExpression beSDT = adminForm.txbSDT.GetBindingExpression(TextBox.TextProperty);
                beSDT.UpdateSource();
                BindingExpression beEmail = adminForm.txbEmail.GetBindingExpression(TextBox.TextProperty);
                beEmail.UpdateSource();
                BindingExpression beDiaChi = adminForm.txbDiaChi.GetBindingExpression(TextBox.TextProperty);
                beDiaChi.UpdateSource();
                BindingExpression beNGSinh = adminForm.txbNGSinh.GetBindingExpression(DatePicker.TextProperty);
                beNGSinh.UpdateSource();
                BindingExpression beGioiTinh = adminForm.cbxGioiTinh.GetBindingExpression(ComboBox.TextProperty);
                beGioiTinh.UpdateSource();
                BindingExpression beQuocTich = adminForm.txbQuocTich.GetBindingExpression(TextBox.TextProperty);
                beQuocTich.UpdateSource();
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
        public bool Check(AdminForm af)
        {
            if (af == null) return false;
            if (string.IsNullOrWhiteSpace(af.txbHo.Text))
            {
                NotifyWindow notifyWindow = new NotifyWindow("Warning", "Vui lòng nhập họ");
                notifyWindow.ShowDialog();
                af.txbHo.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(af.txbTen.Text))
            {
                NotifyWindow notifyWindow = new NotifyWindow("Warning", "Vui lòng nhập tên");
                notifyWindow.ShowDialog();
                af.txbTen.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(af.txbSDT.Text))
            {
                NotifyWindow notifyWindow = new NotifyWindow("Warning", "Vui lòng nhập SDT");
                notifyWindow.ShowDialog();
                af.txbSDT.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(af.txbEmail.Text))
            {
                NotifyWindow notifyWindow = new NotifyWindow("Warning", "Vui lòng nhập email");
                notifyWindow.ShowDialog();
                af.txbEmail.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(af.txbNGSinh.Text))
            {
                NotifyWindow notifyWindow = new NotifyWindow("Warning", "Vui lòng nhập ngày sinh");
                notifyWindow.ShowDialog();
                af.txbNGSinh.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(af.txbDiaChi.Text))
            {
                NotifyWindow notifyWindow = new NotifyWindow("Warning", "Vui lòng nhập địa chỉ");
                notifyWindow.ShowDialog();
                af.txbDiaChi.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(af.txbQuocTich.Text))
            {
                NotifyWindow notifyWindow = new NotifyWindow("Warning", "Vui lòng nhập quốc tịch");
                notifyWindow.ShowDialog();
                af.txbQuocTich.Focus();
                return false;
            }
            if (af.txbNGSinh.SelectedDate > DateTime.Now)
            {
                NotifyWindow notifyWindow = new NotifyWindow("Warning", "Ngày sinh không hợp lệ");
                notifyWindow.ShowDialog();
                af.txbNGSinh.Focus();
                return false;
            }
            return true;
        }
    }
}
