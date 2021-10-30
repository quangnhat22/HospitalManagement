﻿using HospitalManagement.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HospitalManagement.ViewModel
{
    internal class ForgotPasswordFormViewModel : BaseViewModel
    {
        public ICommand OpenLoginWindow { get; set; }
        public ForgotPasswordFormViewModel()
        {
            OpenLoginWindow = new OpenLoginWindowCommand();
        }
    }
}