using HospitalManagement.Command;
using HospitalManagement.Model;
using HospitalManagement.View;
using HospitalManagement.View.Staff;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HospitalManagement.ViewModel
{
    class AdminInformationViewModel : INotifyPropertyChanged
    {
        private ADMIN admin;
        public ADMIN Admin { get => admin; set => admin = value; }
        public ICommand UpdateInformation { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
        public AdminInformationViewModel(ADMIN admin)
        {
            this.Admin = admin;
            UpdateInformation = new AdminFirstLoginCommand();
        }
        public void OnWindowFormClosing(object sender, CancelEventArgs e)
        {
            if (isAdminFormChange(sender))
            {
                NotifyWindow notifyWindow = new NotifyWindow("Check", "Thông tin có sự thay đổi bạn có chắc chắn muốn thoát?", "Visible", 500);
                notifyWindow.ShowDialog();
                if (notifyWindow.Result == System.Windows.MessageBoxResult.Cancel)
                {
                    e.Cancel = true;
                }
            }
        }
        private bool isAdminFormChange(object parameter)
        {
            AdminForm adminForm = parameter as AdminForm;
            ADMIN ad = DataProvider.Ins.DB.ADMINs.Find(adminForm.txbCMND_CCCD.Text);
            DataProvider.Ins.DB.Entry<ADMIN>(ad).Reload();
            bool gioitinh;
            if (adminForm.cbxGioiTinh.Text == "Nữ")
            {
                gioitinh = true;
            }
            else gioitinh = false;
            string date = "";
            if (ad.NGSINH != null)
            {
                DateTime text = (DateTime)ad.NGSINH;
                date = text.ToString("dd/MM/yyyy");
            }
            if (adminForm.txbHo.Text != NullToString(ad.HO) || adminForm.txbTen.Text != NullToString(ad.TEN) ||
                adminForm.txbQuocTich.Text != NullToString(ad.QUOCTICH) || adminForm.txbDiaChi.Text != NullToString(ad.DIACHI) ||
                 adminForm.txbSDT.Text != NullToString(ad.SDT) || gioitinh != (bool)ad.GIOITINH || adminForm.txbNGSinh.Text != date)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private string NullToString(object Value)
        {
            return Value == null ? "" : Value.ToString();
        }
    }
}
