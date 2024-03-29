﻿using System;
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
using HospitalManagement.ViewModel;
using HospitalManagement.Model;

namespace HospitalManagement.View
{
    /// <summary>
    /// Interaction logic for DoctorInformationForm.xaml
    /// </summary>
    public partial class DoctorInformationForm : Window
    {
        public DoctorInformationForm(BACSI bs)
        {
            InitializeComponent();
            this.DataContext = new DoctorInformationViewModel(bs);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
