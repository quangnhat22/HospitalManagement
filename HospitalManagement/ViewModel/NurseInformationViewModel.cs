using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using HospitalManagement.Command;
using HospitalManagement.Model;
using HospitalManagement.View;
using HospitalManagement.View.Staff;

namespace HospitalManagement.ViewModel
{
    class NurseInformationViewModel : INotifyPropertyChanged
    {
        private YTA nurse;

        public YTA Nurse { get => nurse; set => nurse = value; }
        public event PropertyChangedEventHandler PropertyChanged;
        public ICommand SaveChange { get; set; }
        public NurseInformationViewModel(YTA yt)
        {
            this.Nurse = yt;
            SaveChange = new SaveChangeNurseInformationCommand();
        }      
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(name));
           
        }

        public void OnWindowClosing(object sender, CancelEventArgs e)
        {
            if (isFormChange(sender))
            {
                NotifyWindow notifyWindow = new NotifyWindow("Warning", "Thông tin y tá có sự thay đổi bạn có chắc chắn muốn thoát?", "Visible", 500);
                notifyWindow.ShowDialog();
                if (notifyWindow.Result == System.Windows.MessageBoxResult.Cancel)
                {
                    e.Cancel = true;
                }
            }
        }
        private bool isFormChange(object parameter)
        {
            ChangeNurseInformationForm nurseForm = parameter as ChangeNurseInformationForm;
            YTA yt = DataProvider.Ins.DB.YTAs.Find(nurseForm.txbCMND_CCCD.Text);
            DataProvider.Ins.DB.Entry<YTA>(yt).Reload();
            bool gioitinh;
            if (nurseForm.cbxGioiTinh.Text == "Nữ")
            {
                gioitinh = true;
            }
            else gioitinh = false;
            DateTime text = (DateTime)yt.NGSINH;
            string date = text.ToString("dd/MM/yyyy");
            if (nurseForm.txbHo.Text != NullToString(yt.HO) || nurseForm.txbTen.Text != NullToString(yt.TEN) || nurseForm.txbChuyenKhoa.Text != NullToString(yt.CHUYENKHOA) ||
                nurseForm.txbQuocTich.Text != NullToString(yt.QUOCTICH) || nurseForm.txbDiaChi.Text != NullToString(yt.DIACHI) || nurseForm.txbEmail.Text != NullToString(yt.EMAIL) ||
                 nurseForm.txbGhiChu.Text != NullToString(yt.GHICHU) || nurseForm.txbSDT.Text != NullToString(yt.SDT) || nurseForm.txbVaiTro.Text != NullToString(yt.VAITRO) ||
                 nurseForm.txbIDTO.Text != yt.IDTO.ToString() || gioitinh != (bool)yt.GIOITINH || nurseForm.txbNGSinh.Text != date)
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
