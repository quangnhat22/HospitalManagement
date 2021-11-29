﻿using HospitalManagement.Command.TeamTaskCommand;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HospitalManagement.ViewModel.StaffViewViewModel.TeamTask
{
    class AddToDoFormViewModel : BaseViewModel
    {
        private string subjectText;
        private string infoText;
        private string locationText;
        private DateTime startDate;
        private DateTime endDate;

        public string SubjectText { get => subjectText; set => subjectText = value; }
        public string InfoText { get => infoText; set => infoText = value; }
        public string LocationText { get => locationText; set => locationText = value; }
        public DateTime StartDate { get => startDate; set => startDate = value; }
        public DateTime EndDate { get => endDate; set => endDate = value; }

        public ICommand AddTaskCommand { get; set; }

        public AddToDoFormViewModel()
        {
            AddTaskCommand = new AddTaskCommand(this);
            StartDate = EndDate = DateTime.Now;
        }
    }
}