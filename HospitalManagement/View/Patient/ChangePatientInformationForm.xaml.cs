using HospitalManagement.Model;
using HospitalManagement.ViewModel;
using System;
using System.Collections.Generic;
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

namespace HospitalManagement.View
{
    /// <summary>
    /// Interaction logic for ChangePatientInformationForm.xaml
    /// </summary>
    public partial class ChangePatientInformationForm : Window
    {
        private List<int> listIDGiuong = new List<int>();
        public ChangePatientInformationForm(BENHNHAN bn)
        {
            InitializeComponent();
            this.DataContext = new PatientInformationViewModel(bn);
            PatientInformationViewModel informationViewModel = new PatientInformationViewModel(bn);           
            Closing += informationViewModel.OnWindowClosing;
            PHONG p = DataProvider.Ins.DB.PHONGs.Find(bn.IDPHONG);
            if (p != null)
            {
                for (int i = 0; i < p.SUCCHUA; i++)
                {
                    listIDGiuong.Add(i + 1);
                }
                cbxSoGiuong.ItemsSource = listIDGiuong;
            }
        }
        private void cbxIDPhong_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            listIDGiuong.Clear();
            cbxSoGiuong.ItemsSource = null;
            PHONG p = DataProvider.Ins.DB.PHONGs.Find(cbxIDPhong.SelectedItem);
            if (p != null)
            {
                for (int i = 0; i < p.SUCCHUA; i++)
                {
                    listIDGiuong.Add(i + 1);
                }
                cbxSoGiuong.ItemsSource = listIDGiuong;
            }
        }
    }
}
