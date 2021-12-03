using HospitalManagement.Model;
using HospitalManagement.Utils;
using HospitalManagement.View;
using HospitalManagement.View.Staff;
using HospitalManagement.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace HospitalManagement.Command
{
    class AddNurseCommand : ICommand
    {
        private NurseFormViewModel nurseFormViewModel;
        private NurseViewModel nurseViewModel;
        public AddNurseCommand(NurseFormViewModel nurseFormViewModel, NurseViewModel nurseViewModel)
        {
            this.nurseFormViewModel = nurseFormViewModel;
            this.nurseViewModel = nurseViewModel;
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
            NurseForm nurseForm = parameter as NurseForm;

            if (Check(nurseForm))
            {
                string gioiTinh = nurseForm.cbxGioiTinh.SelectedValue as string;
                var nurseInput = new YTA
                {
                    HO = nurseForm.txbHo.Text,
                    TEN = nurseForm.txbTen.Text,
                    SDT = nurseForm.txbSDT.Text,
                    EMAIL = nurseForm.txbEmail.Text,
                    DIACHI = nurseForm.txbDiaChi.Text,
                    NGSINH = nurseForm.txbNGSinh.DisplayDate,
                    GIOITINH = (gioiTinh == "Nam"),
                    QUOCTICH = nurseForm.txbQuocTich.Text,
                    CMND_CCCD = nurseForm.txbCMND_CCCD.Text,
                    CHUYENKHOA = nurseForm.txbChuyenKhoa.Text,
                    VAITRO = nurseForm.txbVaiTro.Text,
                    IDTO = Convert.ToInt32(nurseForm.txbIDTO.Text),
                    GHICHU = nurseForm.txbGhiChu.Text,
                };
                DataProvider.Ins.DB.YTAs.Add(nurseInput);
                DataProvider.Ins.DB.SaveChanges();
                NotifyWindow notifyWindow = new NotifyWindow("Success", "Thêm thành công!");
                notifyWindow.Show();
                nurseViewModel.Nurses = SelectableItem<YTA>.GetSelectableItems(DataProvider.Ins.DB.YTAs.ToList());
            }

        }
        public bool Check(NurseForm nf)
        {
            if (nf == null) return false;
           
            if (string.IsNullOrWhiteSpace(nf.txbHo.Text))
            {
                NotifyWindow notifyWindow = new NotifyWindow("Warning", "Vui lòng nhập họ");
                notifyWindow.ShowDialog();
                nf.txbHo.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(nf.txbTen.Text))
            {
                NotifyWindow notifyWindow = new NotifyWindow("Warning", "Vui lòng nhập tên");
                notifyWindow.ShowDialog();
                nf.txbTen.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(nf.txbSDT.Text))
            {
                NotifyWindow notifyWindow = new NotifyWindow("Warning", "Vui lòng nhập SDT");
                notifyWindow.ShowDialog();
                nf.txbSDT.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(nf.txbEmail.Text))
            {
                NotifyWindow notifyWindow = new NotifyWindow("Warning", "Vui lòng nhập email");
                notifyWindow.ShowDialog();
                nf.txbEmail.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(nf.txbDiaChi.Text))
            {
                NotifyWindow notifyWindow = new NotifyWindow("Warning", "Vui lòng nhập địa chỉ");
                notifyWindow.ShowDialog();
                nf.txbDiaChi.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(nf.txbNGSinh.Text))
            {
                NotifyWindow notifyWindow = new NotifyWindow("Warning", "Vui lòng nhập ngày sinh");
                notifyWindow.ShowDialog();
                nf.txbNGSinh.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(nf.cbxGioiTinh.Text.ToString()))
            {
                NotifyWindow notifyWindow = new NotifyWindow("Warning", "Vui lòng chọn giới tính");
                notifyWindow.ShowDialog();
                nf.cbxGioiTinh.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(nf.txbQuocTich.Text))
            {
                NotifyWindow notifyWindow = new NotifyWindow("Warning", "Vui lòng nhập quốc tịch");
                notifyWindow.ShowDialog();
                nf.txbQuocTich.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(nf.txbCMND_CCCD.Text))
            {
                NotifyWindow notifyWindow = new NotifyWindow("Warning", "Vui lòng nhập CMND_CCCD");
                notifyWindow.ShowDialog();
                nf.txbCMND_CCCD.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(nf.txbChuyenKhoa.Text))
            {
                NotifyWindow notifyWindow = new NotifyWindow("Warning", "Vui lòng nhập chuyên Khoa");
                notifyWindow.ShowDialog();
                nf.txbChuyenKhoa.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(nf.txbVaiTro.Text))
            {
                NotifyWindow notifyWindow = new NotifyWindow("Warning", "Vui lòng nhập vai trò");
                notifyWindow.ShowDialog();
                nf.txbVaiTro.Focus();
                return false;
            }
            
            if (string.IsNullOrWhiteSpace(nf.txbIDTO.Text))
            {
                NotifyWindow notifyWindow = new NotifyWindow("Warning", "Vui lòng nhập id tổ");
                notifyWindow.ShowDialog();
                nf.txbVaiTro.Focus();
                return false;
            }
            //Kiem tra cmnnd//
            if (DataProvider.Ins.DB.BENHNHANs.Any(x => x.CMND_CCCD == nf.txbCMND_CCCD.Text))
            {
                NotifyWindow notifyWindow = new NotifyWindow("Warning", "Trùng mã CMND_CCCD");
                notifyWindow.ShowDialog();
                nf.txbCMND_CCCD.Focus();
                return false;
            }
            if (DataProvider.Ins.DB.BACSIs.Any(x => x.CMND_CCCD == nf.txbCMND_CCCD.Text))
            {
                NotifyWindow notifyWindow = new NotifyWindow("Warning", "Trùng mã CMND_CCCD");
                notifyWindow.ShowDialog();
                nf.txbCMND_CCCD.Focus();
                return false;
            }
            if (DataProvider.Ins.DB.YTAs.Any(x => x.CMND_CCCD == nf.txbCMND_CCCD.Text))
            {
                NotifyWindow notifyWindow = new NotifyWindow("Warning", "Trùng mã CMND_CCCD");
                notifyWindow.ShowDialog();
                nf.txbCMND_CCCD.Focus();
                return false;
            }
            //Kiểm tra IDTO//
            try
            {
                int idTo = int.Parse(nf.txbIDTO.Text);
                var checkTO = DataProvider.Ins.DB.TOes.Any(x => x.ID == idTo);
                if (checkTO == false)
                {
                    NotifyWindow notifyWindow = new NotifyWindow("Warning", "Tổ không tồn tại");
                    notifyWindow.ShowDialog();
                    nf.txbIDTO.Focus();
                    return false;
                }
            }
            catch (FormatException)
            {
                NotifyWindow notifyWindow = new NotifyWindow("Warning", "ID Tổ là một số nguyên dương");
                notifyWindow.ShowDialog();
                nf.txbIDTO.Focus();
                return false;
            }

            return true;
        }
    }
}
