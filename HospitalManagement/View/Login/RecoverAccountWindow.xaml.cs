﻿using HospitalManagement.ViewModel;
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

namespace HospitalManagement.View.Login
{
    /// <summary>
    /// Interaction logic for RecoverAccountWindow.xaml
    /// </summary>
    public partial class RecoverAccountWindow : Window
    {
        public RecoverAccountWindow()
        {
            InitializeComponent();
            this.DataContext = new RecoverAccountViewModel();
        }
    }
}
