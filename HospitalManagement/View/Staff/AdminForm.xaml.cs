﻿using HospitalManagement.Model;
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

namespace HospitalManagement.View.Staff
{
    /// <summary>
    /// Interaction logic for AdminForm.xaml
    /// </summary>
    public partial class AdminForm : Window
    {
        public AdminForm(ADMIN ad)
        {
            InitializeComponent();
            this.DataContext = new AdminInformationViewModel(ad);
            AdminInformationViewModel informationViewModel = new AdminInformationViewModel(ad);
            Closing += informationViewModel.OnWindowFormClosing;
        }
    }
}
