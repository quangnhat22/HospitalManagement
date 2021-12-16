using HospitalManagement.Command;
using HospitalManagement.Model;
using HospitalManagement.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HospitalManagement.ViewModel
{
    internal class DoctorFormViewModel : BaseViewModel
    {
        public DataProvider db;
        private DoctorViewModel doctorViewModel;
        public ICommand AddDoctor { get; set; }
        public DoctorFormViewModel(DoctorViewModel doctorViewModel)
        {
            db = new DataProvider();
            this.doctorViewModel = doctorViewModel;
            AddDoctor = new AddDoctorCommand(this, this.doctorViewModel);

        }
        public void OnWindowClosing(object sender, CancelEventArgs e)
        {
            if (isFormChange(sender))
            {
                NotifyWindow notifyWindow = new NotifyWindow("Warning", "Bác sĩ mới chưa được thêm có chắc chắn muốn thoát?", "Visible", 500);
                notifyWindow.ShowDialog();
                if (notifyWindow.Result == System.Windows.MessageBoxResult.Cancel)
                {
                    e.Cancel = true;
                }
            }
        }
        private bool isFormChange(object parameter)
        {
            DoctorForm doctorForm = parameter as DoctorForm;
            if(doctorForm.txbCMND_CCCD.Text != string.Empty)
            {
                BACSI bs = DataProvider.Ins.DB.BACSIs.Find(doctorForm.txbCMND_CCCD.Text);
                if (bs != null)
                {
                    DataProvider.Ins.DB.Entry<BACSI>(bs).Reload();
                    bool gioitinh;
                    if (doctorForm.cbxGioiTinh.Text == "Nữ")
                    {
                        gioitinh = true;
                    }
                    else gioitinh = false;
                    DateTime text = (DateTime)bs.NGSINH;
                    string date = text.ToString("dd/MM/yyyy");
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
                else
                {
                    return true;
                }    
            }
            else
            {
                if (doctorForm.txbHo.Text != string.Empty || doctorForm.txbTen.Text != string.Empty || doctorForm.txbChuyenKhoa.Text != string.Empty ||
                        doctorForm.txbQuocTich.Text != string.Empty || doctorForm.txbDiaChi.Text != string.Empty || doctorForm.txbEmail.Text != string.Empty ||
                         doctorForm.txbGhiChu.Text != string.Empty || doctorForm.txbSDT.Text != string.Empty || doctorForm.cbxVaiTro.Text != string.Empty ||
                         doctorForm.cbxIDTO.Text != string.Empty || doctorForm.cbxGioiTinh.Text != string.Empty || doctorForm.txbNGSinh.Text != string.Empty)
                {
                    return true;
                }
                else
                {
                    return false;
                }    
            }              
        }
        private string NullToString(object Value)
        {
            return Value == null ? "" : Value.ToString();
        }
    }
}