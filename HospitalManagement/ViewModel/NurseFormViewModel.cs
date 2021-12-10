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
    internal class NurseFormViewModel: BaseViewModel
    {
        public DataProvider db;
        private NurseViewModel nurseViewModel;
        public ICommand AddNurse { get; set; }
        public NurseFormViewModel(NurseViewModel nurseViewModel)
        {
            db = new DataProvider();
            this.nurseViewModel = nurseViewModel;
            AddNurse = new AddNurseCommand(this, this.nurseViewModel);
        }
        public void OnWindowClosing(object sender, CancelEventArgs e)
        {
            if (isFormChange(sender))
            {
                NotifyWindow notifyWindow = new NotifyWindow("Warning", "Y tá mới chưa được thêm có chắc chắn muốn thoát?", "Visible", 500);
                notifyWindow.ShowDialog();
                if (notifyWindow.Result == System.Windows.MessageBoxResult.Cancel)
                {
                    e.Cancel = true;
                }
            }
        }
        private bool isFormChange(object parameter)
        {
            NurseForm nurseForm = parameter as NurseForm;
            if (nurseForm.txbCMND_CCCD.Text != string.Empty)
            {
                YTA yt = DataProvider.Ins.DB.YTAs.Find(nurseForm.txbCMND_CCCD.Text);
                if (yt != null)
                {
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
                else
                {
                    return true;
                }
            }
            else
            {
                if (nurseForm.txbHo.Text != string.Empty || nurseForm.txbTen.Text != string.Empty || nurseForm.txbChuyenKhoa.Text != string.Empty ||
                        nurseForm.txbQuocTich.Text != string.Empty || nurseForm.txbDiaChi.Text != string.Empty || nurseForm.txbEmail.Text != string.Empty ||
                         nurseForm.txbGhiChu.Text != string.Empty || nurseForm.txbSDT.Text != string.Empty || nurseForm.txbVaiTro.Text != string.Empty ||
                         nurseForm.txbIDTO.Text != string.Empty || nurseForm.cbxGioiTinh.Text != string.Empty || nurseForm.txbNGSinh.Text != string.Empty)
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
