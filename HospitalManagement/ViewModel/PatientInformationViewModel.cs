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

namespace HospitalManagement.ViewModel
{
    class PatientInformationViewModel : INotifyPropertyChanged
    {
        private BENHNHAN benhnhan;
        private List<int> listIDPhong = new List<int>();
        public ICommand SaveChange { get; set; }
        public List<int> ListIDPhong { get => listIDPhong; set => listIDPhong = value; }
        public PatientInformationViewModel(BENHNHAN bn)
        {
            this.BenhNhan = bn;
            SaveChange = new SaveChangePatientInformationCommand();
            var groupListData = DataProvider.Ins.DB.PHONGs.ToList().ConvertAll(itemGroup => itemGroup.ID);
            ListIDPhong.AddRange(groupListData);
        }

        public BENHNHAN BenhNhan { get => benhnhan; set => benhnhan = value; }
        
        public event PropertyChangedEventHandler PropertyChanged;
        

        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
        public void OnWindowClosing(object sender, CancelEventArgs e)
        {
            if (isFormChange(sender))
            {
                NotifyWindow notifyWindow = new NotifyWindow("Warning", "Thông tin bệnh nhân có sự thay đổi bạn có chắc chắn muốn thoát?", "Visible", 500);
                notifyWindow.ShowDialog();
                if (notifyWindow.Result == System.Windows.MessageBoxResult.Cancel)
                {
                    e.Cancel = true;
                }
            }
        }
        private bool isFormChange(object parameter)
        {
            ChangePatientInformationForm patientForm = parameter as ChangePatientInformationForm;
            BENHNHAN bn = DataProvider.Ins.DB.BENHNHANs.Find(patientForm.txbCMND_CCCD.Text);
            DataProvider.Ins.DB.Entry<BENHNHAN>(bn).Reload();
            bool gioitinh;
            if (patientForm.cbxGioiTinh.Text == "Nữ")
            {
                gioitinh = true;
            }
            else gioitinh = false;
            DateTime text = (DateTime)bn.NGSINH;
            string dateBirth = text.ToString("dd/MM/yyyy");
            text = (DateTime)bn.NGNHAPVIEN;
            string dateNV = text.ToString("dd/MM/yyyy");
            if (patientForm.txbHo.Text != NullToString(bn.HO) || patientForm.txbTen.Text != NullToString(bn.TEN) || patientForm.txbBenhNen.Text != NullToString(bn.BENHNEN) ||
                patientForm.txbQuocTich.Text != NullToString(bn.QUOCTICH) || patientForm.txbDiaChi.Text != NullToString(bn.DIACHI) || patientForm.txbEmail.Text != NullToString(bn.EMAIL) ||
                 patientForm.txbGhiChu.Text != NullToString(bn.GHICHU) || patientForm.txbSDT.Text != NullToString(bn.SDT) || patientForm.cbxTinhTrang.Text != NullToString(bn.TINHTRANG) ||
                  gioitinh != (bool)bn.GIOITINH || patientForm.txbNGSinh.Text != dateBirth || patientForm.txbNGNhapVien.Text != dateNV || patientForm.cbxIDPhong.Text != bn.IDPHONG.ToString() ||
                   patientForm.cbxSoGiuong.Text != bn.GIUONGBENH)
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
