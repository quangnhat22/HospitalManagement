﻿using HospitalManagement.Model;
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
            DoctorInformationViewModel informationViewModel = new DoctorInformationViewModel(bs);
            Closing += informationViewModel.OnWindowClosing;
        }

        
    }
}
