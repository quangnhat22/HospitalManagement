using HospitalManagement.Model;
using HospitalManagement.Utils;
using HospitalManagement.View;
using HospitalManagement.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HospitalManagement.Command
{
    class AddPatientCommand : ICommand
    {
        private PatientViewModel patientViewModel;
        private PatientFormViewModel patientFormViewModel;
        public AddPatientCommand(PatientFormViewModel patientFormViewModel, PatientViewModel patientViewModel)
        {
            this.patientFormViewModel = patientFormViewModel;
            this.patientViewModel = patientViewModel;
        }


        public event EventHandler CanExecuteChanged
        {
            add { }
            remove { }
        }

        public bool CanExecute(object parameter)
        {
            return parameter == null ? false : true;
        }

        public void Execute(object parameter)
        {
            PatientForm patientForm = parameter as PatientForm;

            if (Check(patientForm))
            {
                string gioiTinh = patientForm.cbxGioiTinh.SelectedValue as string;
                var patientInput = new BENHNHAN
                {
                    HO = patientForm.txbHo.Text,
                    TEN = patientForm.txbTen.Text,
                    MABENHNHAN = patientForm.txbMaBN.Text,
                    SDT = patientForm.txbSDT.Text,
                    EMAIL = patientForm.txbEmail.Text,
                    DIACHI = patientForm.txbDiaChi.Text,
                    NGSINH = patientForm.txbNGSinh.DisplayDate,
                    GIOITINH = (gioiTinh == "Nam"),
                    QUOCTICH = patientForm.txbQuocTich.Text,
                    CMND_CCCD = patientForm.txbCMND_CCCD.Text,
                    NGNHAPVIEN = patientForm.txbNGNhapVien.DisplayDate,
                    BENHNEN = patientForm.txbBenhNen.Text,
                    TINHTRANG = patientForm.cbxTinhTrang.Text,
                    IDPHONG = Convert.ToInt32(patientForm.txbIDPhong.Text),
                    GIUONGBENH = patientForm.txbSoGiuong.Text,
                    GHICHU = patientForm.txbGhiChu.Text,
                };
                DataProvider.Ins.DB.BENHNHANs.Add(patientInput);
                DataProvider.Ins.DB.SaveChanges();
                NotifyWindow notifyWindow = new NotifyWindow("Success", "Thêm thành công!");
                notifyWindow.Show();
                patientViewModel.Patients = SelectableItem<BENHNHAN>.GetSelectableItems(DataProvider.Ins.DB.BENHNHANs.ToList());
            }

        }
        public bool Check(PatientForm pf)
        {
            if (pf == null) return false;            
            if (string.IsNullOrWhiteSpace(pf.txbHo.Text))
            {
                NotifyWindow notifyWindow = new NotifyWindow("Warning", "Vui lòng nhập họ");
                notifyWindow.ShowDialog();
                pf.txbHo.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(pf.txbTen.Text))
            {
                NotifyWindow notifyWindow = new NotifyWindow("Warning", "Vui lòng nhập tên");
                notifyWindow.ShowDialog();
                pf.txbTen.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(pf.txbSDT.Text))
            {
                NotifyWindow notifyWindow = new NotifyWindow("Warning", "Vui lòng nhập SDT");
                notifyWindow.ShowDialog();
                pf.txbSDT.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(pf.txbEmail.Text))
            {
                NotifyWindow notifyWindow = new NotifyWindow("Warning", "Vui lòng nhập email");
                notifyWindow.ShowDialog();
                pf.txbEmail.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(pf.txbDiaChi.Text))
            {
                NotifyWindow notifyWindow = new NotifyWindow("Warning", "Vui lòng nhập địa chỉ");
                notifyWindow.ShowDialog();
                pf.txbDiaChi.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(pf.txbNGSinh.Text))
            {
                NotifyWindow notifyWindow = new NotifyWindow("Warning", "Vui lòng nhập ngày sinh");
                notifyWindow.ShowDialog();
                pf.txbNGSinh.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(pf.cbxGioiTinh.Text.ToString()))
            {
                NotifyWindow notifyWindow = new NotifyWindow("Warning", "Vui lòng chọn giới tính");
                notifyWindow.ShowDialog();
                pf.cbxGioiTinh.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(pf.txbQuocTich.Text))
            {
                NotifyWindow notifyWindow = new NotifyWindow("Warning", "Vui lòng nhập quốc tịch");
                notifyWindow.ShowDialog();
                pf.txbQuocTich.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(pf.txbCMND_CCCD.Text))
            {
                NotifyWindow notifyWindow = new NotifyWindow("Warning", "Vui lòng nhập CMND_CCCD");
                notifyWindow.ShowDialog();
                pf.txbCMND_CCCD.Focus();
                return false;
            }

            
            if (string.IsNullOrWhiteSpace(pf.txbNGNhapVien.Text))
            {
                NotifyWindow notifyWindow = new NotifyWindow("Warning", "Vui lòng chọn ngày nhập viện");
                notifyWindow.ShowDialog();
                pf.txbNGNhapVien.Focus();
                return false;
            }
            // ko có bệnh nền //
            //if (string.IsNullOrWhiteSpace(pf.txbBenhNen.Text))
            //{
            //    NotifyWindow notifyWindow = new NotifyWindow("Warning", "Vui lòng nhập bệnh nền");
            //    notifyWindow.ShowDialog();
            //    pf.txbBenhNen.Focus();
            //    return false;
            //}
            if (string.IsNullOrWhiteSpace(pf.cbxTinhTrang.Text.ToString()))
            {
                NotifyWindow notifyWindow = new NotifyWindow("Warning", "Vui lòng chọn tình trạng");
                notifyWindow.ShowDialog();
                pf.cbxTinhTrang.Focus();
                return false;
            }
            

            if (string.IsNullOrWhiteSpace(pf.txbSoGiuong.Text))
            {
                NotifyWindow notifyWindow = new NotifyWindow("Warning", "Vui lòng nhập số giường");
                notifyWindow.ShowDialog();
                pf.cbxTinhTrang.Focus();
                return false;
            }
            //Kiểm tra MaBN
            if (DataProvider.Ins.DB.BENHNHANs.Any(x => x.MABENHNHAN == pf.txbMaBN.Text))
            {           
                NotifyWindow notifyWindow = new NotifyWindow("Warning", "Trùng mã bệnh nhân");
                notifyWindow.ShowDialog();
                pf.txbMaBN.Focus();
                return false;
                
            }
            //Kiểm tra CMND nha mày do nó liên quan tới Nurse vs Patient nữa @@//
            if (DataProvider.Ins.DB.BENHNHANs.Any(x => x.CMND_CCCD == pf.txbCMND_CCCD.Text))
            {
                NotifyWindow notifyWindow = new NotifyWindow("Warning", "Trùng mã CMND_CCCD");
                notifyWindow.ShowDialog();
                pf.txbCMND_CCCD.Focus();
                return false;
            }
            if (DataProvider.Ins.DB.BACSIs.Any(x => x.CMND_CCCD == pf.txbCMND_CCCD.Text))
            {
                NotifyWindow notifyWindow = new NotifyWindow("Warning", "Trùng mã CMND_CCCD");
                notifyWindow.ShowDialog();
                pf.txbCMND_CCCD.Focus();
                return false;
            }
            if (DataProvider.Ins.DB.YTAs.Any(x => x.CMND_CCCD == pf.txbCMND_CCCD.Text))
            {
                NotifyWindow notifyWindow = new NotifyWindow("Warning", "Trùng mã CMND_CCCD");
                notifyWindow.ShowDialog();
                pf.txbCMND_CCCD.Focus();
                return false;
            }
            //Kiem tra phong
            try
            {
                int idPhong = int.Parse(pf.txbIDPhong.Text);
                var checkPHONG = DataProvider.Ins.DB.PHONGs.Any(x => x.ID == idPhong);
                if (checkPHONG == false)
                {
                    NotifyWindow notifyWindow = new NotifyWindow("Warning", "Phòng không tồn tại");
                    notifyWindow.ShowDialog();
                    pf.txbIDPhong.Focus();
                    return false;
                }
            }
            catch (FormatException)
            {
                NotifyWindow notifyWindow = new NotifyWindow("Warning", "ID Phòng là một số nguyên dương");
                notifyWindow.ShowDialog();
                pf.txbIDPhong.Focus();
                return false;
            }
            return true;
        }
    }
}
