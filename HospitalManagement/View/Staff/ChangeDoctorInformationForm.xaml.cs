using HospitalManagement.Model;
using HospitalManagement.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace HospitalManagement.View.Staff
{
    /// <summary>
    /// Interaction logic for ChageDoctorInformationForm.xaml
    /// </summary>
    public partial class ChangeDoctorInformationForm : Window
    {
        public ChangeDoctorInformationForm(BACSI bs)
        {
            InitializeComponent();
            this.DataContext = new DoctorInformationViewModel(bs);
        }

        private void ChangeDoctorInformationFormWindow_Closing(object sender, CancelEventArgs e)
        {
            BACSI bs = DataProvider.Ins.DB.BACSIs.Find(txbCMND_CCCD.Text);
            DataProvider.Ins.DB.Entry<BACSI>(bs).Reload();           
            txbHo.Text = bs.HO;
            txbTen.Text = bs.TEN;
            txbChuyenKhoa.Text = bs.CHUYENKHOA;
            txbQuocTich.Text = bs.QUOCTICH;
            txbDiaChi.Text = bs.DIACHI;
            txbEmail.Text = bs.EMAIL;
            txbGhiChu.Text = bs.GHICHU;
            txbSDT.Text = bs.SDT;
            txbVaiTro.Text = bs.VAITRO;
            txbIDTO.Text = bs.IDTO.ToString();
            if ((bool)bs.GIOITINH)
            {
                cbxGioiTinh.Text = "Nữ";
            }
            else cbxGioiTinh.Text = "Nam";
            DateTime text = (DateTime)bs.NGSINH;
            string date = text.ToString("dd/MM/yyyy");
            txbNGSinh.Text = date;
        }
    }
}
