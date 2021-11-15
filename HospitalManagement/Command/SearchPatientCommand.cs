using HospitalManagement.Model;
using HospitalManagement.Utils;
using HospitalManagement.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HospitalManagement.Command
{
    class SearchPatientCommand : ICommand
    {
        private PatientViewModel patientViewModel;

        public SearchPatientCommand(PatientViewModel patientViewModel)
        {
            this.patientViewModel = patientViewModel;
        }

        public event EventHandler CanExecuteChanged
        {
            add { }
            remove { }
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            patientViewModel.SearchBox = patientViewModel.SearchBox.Trim();
            if (patientViewModel.SearchBox == string.Empty || patientViewModel.SearchBox == null)
            {
                patientViewModel.Patients = DataProvider.Ins.DB.BENHNHANs.ToList();
            }
            else
            {
                string RemoveSignString = VietnameseSign.convertToUnSign2(patientViewModel.SelectedFilter).ToLower();
                if (RemoveSignString == "cmnd")
                {
                    patientViewModel.Patients = DataProvider.Ins.DB.BENHNHANs.Where(p => p.CMND_CCCD.Contains(patientViewModel.SearchBox)).ToList();
                }
                else if (RemoveSignString == "ho")
                {
                    //Patients = DataProvider.Ins.DB.BENHNHANs.Where(p => p.HO.Contains(searchBox)).ToList();
                    patientViewModel.Patients = DataProvider.Ins.DB.BENHNHANs.Where(delegate (BENHNHAN bn)
                    {
                        return VietnameseSign.ContainsUnsigned(bn.HO, patientViewModel.SearchBox);
                    }).AsQueryable().ToList();
                }
                else if (RemoveSignString == "ten")
                {
                    //Patients = DataProvider.Ins.DB.BENHNHANs.Where(p => p.TEN.Contains(searchBox)).ToList();
                    patientViewModel.Patients = DataProvider.Ins.DB.BENHNHANs.Where(delegate (BENHNHAN bn)
                    {
                        return VietnameseSign.ContainsUnsigned(bn.TEN, patientViewModel.SearchBox);
                    }).AsQueryable().ToList();
                }
                else if (RemoveSignString == "ho va ten")
                {
                    patientViewModel.Patients = DataProvider.Ins.DB.BENHNHANs.Where(delegate (BENHNHAN bn)
                    {
                        // Has some problems
                        return VietnameseSign.ContainsUnsigned(bn.HO + " " + bn.TEN, patientViewModel.SearchBox);
                    }).AsQueryable().ToList();
                }
            }
        }
    }
}
