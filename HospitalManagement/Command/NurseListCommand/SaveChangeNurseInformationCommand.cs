using HospitalManagement.Model;
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
                DataProvider.Ins?.DB?.SaveChanges();
                NotifyWindow notifyWindow = new NotifyWindow("Success", "Đã cập nhập thành công");
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

            if (string.IsNullOrWhiteSpace(cnf.txbIDTO.Text))
            {
                NotifyWindow notifyWindow = new NotifyWindow("Warning", "Vui lòng nhập id tổ");
                notifyWindow.ShowDialog();
                cnf.txbVaiTro.Focus();
                return false;
            }

            try
            {
                int idTo = int.Parse(cnf.txbIDTO.Text);
                var checkTO = DataProvider.Ins.DB.TOes.Any(x => x.ID == idTo);
                if (checkTO == false)
                {
                    NotifyWindow notifyWindow = new NotifyWindow("Warning", "Tổ không tồn tại");
                    notifyWindow.ShowDialog();
                    cnf.txbIDTO.Focus();
                    return false;
                }
            }
            catch (FormatException)
            {
                NotifyWindow notifyWindow = new NotifyWindow("Warning", "ID Tổ là một số nguyên dương");
                notifyWindow.ShowDialog();
                cnf.txbIDTO.Focus();
                return false;
            }

            return true;
        }
    }
}
