using HospitalManagement.View;
using HospitalManagement.View.Others;
using HospitalManagement.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HospitalManagement.Command
{
    internal class SaveChangeAccountCommand : ICommand
    {
        private ChangeAccountViewModel changeAccountViewModel;
        public event EventHandler CanExecuteChanged
        {
            add { }
            remove { }
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public SaveChangeAccountCommand()
        {
            changeAccountViewModel = new ChangeAccountViewModel();
        }
        public void Execute(object parameter)
        {
            throw new NotImplementedException();
        }
        public bool Check(ChangeAccountWindow cw)
        {
            if (cw == null) return false;
            //if (string.IsNullOrWhiteSpace(cw.txbNewPassword.Text) && string.IsNullOrWhiteSpace(cw.txbNewRepassword.Text))
            //{
            //    if(string.IsNullOrWhiteSpace(cw.txbLastName.Text))
            //    {
            //        NotifyWindow notifyWindow = new NotifyWindow("Warning", "Vui lòng nhập Họ");
            //        notifyWindow.ShowDialog();
            //        cw.txbLastName.Focus();
            //        return false;
            //    }
            //    if (string.IsNullOrWhiteSpace(cw.txbFistName.Text))
            //    {
            //        NotifyWindow notifyWindow = new NotifyWindow("Warning", "Vui lòng nhập Tên");
            //        notifyWindow.ShowDialog();
            //        cw.txbFistName.Focus();
            //        return false;
            //    }

            //    if (string.IsNullOrWhiteSpace(cw.tbDateTimePicker.Text))
            //    {
            //        NotifyWindow notifyWindow = new NotifyWindow("Warning", "Vui lòng nhập ngày sinh");
            //        notifyWindow.ShowDialog();
            //        cw.txbFistName.Focus();
            //        return false;
            //    }
            //    if (string.IsNullOrWhiteSpace(cw.tbDateTimePicker.Text))
            //    {
            //        NotifyWindow notifyWindow = new NotifyWindow("Warning", "Vui lòng nhập ngày sinh");
            //        notifyWindow.ShowDialog();
            //        cw.txbFistName.Focus();
            //        return false;
            //    }

            //    return false;
            //    //if (string.IsNullOrWhiteSpace(cw.cbSex.Text))
            //    //{
            //    //    NotifyWindow notifyWindow = new NotifyWindow("Warning", "Vui lòng nhập họ tên");
            //    //    notifyWindow.ShowDialog();
            //    //    cw.txbNameUser.Focus();
            //    //    return false;
            //    //}
            //    //if (string.IsNullOrWhiteSpace(cw.txbEmail.Text))
            //    //{
            //    //    NotifyWindow notifyWindow = new NotifyWindow("Warning", "Vui lòng nhập địa chỉ Email");
            //    //    notifyWindow.ShowDialog();
            //    //    cw.txbNameUser.Focus();
            //    //    return false;
            //    //}
            //}
            //var users = forgotPasswordFormViewModel?.db?.DB?.USERs.Where(x => x.USERNAME == mw.tbUsername.Text && x.EMAIL == mw.tbMailAddress.Text);
            //if (users == null || users.Count() == 0)
            //{
            //    NotifyWindow notifyWindow = new NotifyWindow("Warning", "Vui lòng kiểm tra lại thông tin nhập!");
            //    notifyWindow.ShowDialog();
            //    mw.tbUsername.Focus();
            //    return false;
            //}
            return true;
        }
    }
}
