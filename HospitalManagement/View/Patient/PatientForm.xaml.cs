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
    /// Interaction logic for PatientForm.xaml
    /// </summary>
    public partial class PatientForm : Window
    {
        private List<int> listIDGiuong = new List<int>();
        public PatientForm(PatientViewModel patientViewModel)
        {
            InitializeComponent();
            this.DataContext = new PatientFormViewModel(patientViewModel);
            PatientFormViewModel informationViewModel = new PatientFormViewModel(patientViewModel);
            Closing += informationViewModel.OnWindowClosing;
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
