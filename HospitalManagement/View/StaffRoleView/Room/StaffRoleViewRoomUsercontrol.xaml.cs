﻿using HospitalManagement.Model;
using HospitalManagement.Utils;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HospitalManagement.View.StaffRoleView.Room
{
    /// <summary>
    /// Interaction logic for StaffRoleViewRoomUsercontrol.xaml
    /// </summary>
    public partial class StaffRoleViewRoomUsercontrol : UserControl
    {
        public StaffRoleViewRoomUsercontrol()
        {
            InitializeComponent();
            int idTo;
            using (QUANLYBENHVIENEntities dbContext = new QUANLYBENHVIENEntities())
            {
                idTo = ToUtils.GetTOID(dbContext.USERs.Find(MainWindowViewModel.User.ID));
            }
            this.DataContext = new RoomViewModel(idTo);
        }
    }
}
