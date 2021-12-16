using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using HospitalManagement.Model;
using HospitalManagement.Command;
using HospitalManagement.View.Staff;
using HospitalManagement.View;
using System.Windows;

namespace HospitalManagement.ViewModel
{
     class DoctorInformationViewModel : INotifyPropertyChanged
    {
        private BACSI doctor;        
        public BACSI Doctor { get => doctor; set => doctor = value;}
        private List<int> listIDTo = new List<int>();
        public ICommand UpdateInformation { get; set; }
        public ICommand SaveChange { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
        public List<int> ListIDTo { get => listIDTo; set => listIDTo = value; }
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
        public DoctorInformationViewModel(BACSI bs)
        {
            this.Doctor = bs;
            UpdateInformation = new DoctorFirstLoginUpdateCommand();
            SaveChange = new SaveChangeDoctorInformationCommand();
            var groupListData = DataProvider.Ins.DB.TOes.ToList().ConvertAll(itemGroup => itemGroup.ID);
            ListIDTo.AddRange(groupListData);
        }
        public void OnWindowFormClosing(object sender, CancelEventArgs e)
        {
            if (isDoctorFormChange(sender))
            {
                NotifyWindow notifyWindow = new NotifyWindow("Warning", "Thông tin có sự thay đổi bạn có chắc chắn muốn thoát?", "Visible", 500);
                notifyWindow.ShowDialog();
                if (notifyWindow.Result == System.Windows.MessageBoxResult.Cancel)
                {
                    e.Cancel = true;
                }
            }
        }
        public void OnWindowClosing(object sender, CancelEventArgs e)
        {
            if(isFormChange(sender))
            {
                NotifyWindow notifyWindow = new NotifyWindow("Warning", "Thông tin bác sĩ có sự thay đổi bạn có chắc chắn muốn thoát?", "Visible", 500);
                notifyWindow.ShowDialog();
                if(notifyWindow.Result == System.Windows.MessageBoxResult.Cancel)
                {
                    e.Cancel = true;
                }   
            }            
        }
        private bool isFormChange(object parameter)
        {
            ChangeDoctorInformationForm doctorForm = parameter as ChangeDoctorInformationForm;
            BACSI bs = DataProvider.Ins.DB.BACSIs.Find(doctorForm.txbCMND_CCCD.Text);
            DataProvider.Ins.DB.Entry<BACSI>(bs).Reload();
            bool gioitinh;
            if(doctorForm.cbxGioiTinh.Text == "Nữ")
            {
                gioitinh = true;
            }
            else gioitinh = false;
            string date = "";
            if(bs.NGSINH != null)
            {
                DateTime text = (DateTime)bs.NGSINH;
                date = text.ToString("dd/MM/yyyy");
            }              
            if (doctorForm.txbHo.Text != NullToString(bs.HO) || doctorForm.txbTen.Text != NullToString(bs.TEN) || doctorForm.txbChuyenKhoa.Text != NullToString(bs.CHUYENKHOA) ||
                doctorForm.txbQuocTich.Text != NullToString(bs.QUOCTICH) || doctorForm.txbDiaChi.Text != NullToString(bs.DIACHI) || doctorForm.txbEmail.Text != NullToString(bs.EMAIL) ||
                 doctorForm.txbGhiChu.Text != NullToString(bs.GHICHU) || doctorForm.txbSDT.Text != NullToString(bs.SDT) || doctorForm.cbxVaiTro.Text != NullToString(bs.VAITRO) ||
                 doctorForm.cbxIDTO.Text != bs.IDTO.ToString() || gioitinh != (bool)bs.GIOITINH || doctorForm.txbNGSinh.Text != date)
            {
                return true;
            }
            else
            {
                return false;
            }             
        }
        private bool isDoctorFormChange(object parameter)
        {
            DoctorForm doctorForm = parameter as DoctorForm;
            BACSI bs = DataProvider.Ins.DB.BACSIs.Find(doctorForm.txbCMND_CCCD.Text);
            DataProvider.Ins.DB.Entry<BACSI>(bs).Reload();
            bool gioitinh;
            if (doctorForm.cbxGioiTinh.Text == "Nữ")
            {
                gioitinh = true;
            }
            else gioitinh = false;
            string date = "";
            if (bs.NGSINH != null)
            {
                DateTime text = (DateTime)bs.NGSINH;
                date = text.ToString("dd/MM/yyyy");
            }
            if (doctorForm.txbHo.Text != NullToString(bs.HO) || doctorForm.txbTen.Text != NullToString(bs.TEN) || doctorForm.txbChuyenKhoa.Text != NullToString(bs.CHUYENKHOA) ||
                doctorForm.txbQuocTich.Text != NullToString(bs.QUOCTICH) || doctorForm.txbDiaChi.Text != NullToString(bs.DIACHI) || doctorForm.txbEmail.Text != NullToString(bs.EMAIL) ||
                 doctorForm.txbGhiChu.Text != NullToString(bs.GHICHU) || doctorForm.txbSDT.Text != NullToString(bs.SDT) || doctorForm.cbxVaiTro.Text != NullToString(bs.VAITRO) ||
                 doctorForm.cbxIDTO.Text != bs.IDTO.ToString() || gioitinh != (bool)bs.GIOITINH || doctorForm.txbNGSinh.Text != date)
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
