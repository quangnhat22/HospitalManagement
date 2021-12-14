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
    internal class PatientFormViewModel : BaseViewModel
    {
        public DataProvider db;
        private PatientViewModel patientViewModel;
        private List<int> listIDPhong = new List<int>();
        public ICommand AddPatient { get; set; }
        public List<int> ListIDPhong { get => listIDPhong; set => listIDPhong = value; }
        public PatientFormViewModel(PatientViewModel patientViewModel)
        {
            db = new DataProvider();
            this.patientViewModel = patientViewModel;
            AddPatient = new AddPatientCommand(this, this.patientViewModel);
            var groupListData = DataProvider.Ins.DB.PHONGs.ToList().ConvertAll(itemGroup => itemGroup.ID);
            ListIDPhong.AddRange(groupListData);
        }
        public void OnWindowClosing(object sender, CancelEventArgs e)
        {
            if (isFormChange(sender))
            {
                NotifyWindow notifyWindow = new NotifyWindow("Warning", "Bệnh nhân mới chưa được thêm có chắc chắn muốn thoát?", "Visible", 500);
                notifyWindow.ShowDialog();
                if (notifyWindow.Result == System.Windows.MessageBoxResult.Cancel)
                {
                    e.Cancel = true;
                }
            }
        }
        private bool isFormChange(object parameter)
        {
            PatientForm patientForm = parameter as PatientForm;
            if (patientForm.txbCMND_CCCD.Text != string.Empty)
            {
                BENHNHAN bn = DataProvider.Ins.DB.BENHNHANs.Find(patientForm.txbCMND_CCCD.Text);
                if (bn != null)
                {                  
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
                else
                {
                    return true;
                }
            }
            else
            {
                if (patientForm.txbHo.Text != string.Empty || patientForm.txbTen.Text != string.Empty || patientForm.txbBenhNen.Text != string.Empty ||
                        patientForm.txbQuocTich.Text != string.Empty || patientForm.txbDiaChi.Text != string.Empty || patientForm.txbEmail.Text != string.Empty ||
                         patientForm.txbGhiChu.Text != string.Empty || patientForm.txbSDT.Text != string.Empty || patientForm.cbxTinhTrang.Text != string.Empty ||
                          patientForm.cbxGioiTinh.Text != string.Empty || patientForm.txbNGSinh.Text != string.Empty || patientForm.txbNGNhapVien.Text != string.Empty || 
                          patientForm.cbxIDPhong.Text != string.Empty || patientForm.cbxSoGiuong.Text != string.Empty)
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
