using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using HospitalManagement.Model;
using System.ComponentModel;
using HospitalManagement.Command;

namespace HospitalManagement.ViewModel
{
    class FacilitiesViewModel : BaseViewModel, INotifyPropertyChanged
    {
        public int CheckedCount;
        public List<Facility> facilities = new List<Facility>();

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

        public List<Facility> Facilities
        {
            get { return facilities; }
            set { facilities = value; OnPropertyChanged("Facilities"); }
        }

        private bool? isCheckedAll;

        public bool? IsCheckedAll
        {
            get { return isCheckedAll; }
            set { isCheckedAll = value; OnPropertyChanged("IsCheckedAll"); }
        }
        public ICommand AllCheckedCommand { get; set; }
        public ICommand SingleCheckedCommand { get; set; }

        public FacilitiesViewModel()
        {
            Facilities.Add(new Facility()
            {
                ID = 1,
                DisplayName = "Máy thở Bellavista",
                Type = "Thiết bị",
                ManufactureDate = (new DateTime(2021, 1, 1)).ToString("dd/MM/yyyy"),
                Amount = 200,
                Unit = "Bộ",
                Note=""
            });
            Facilities.Add(new Facility()
            {
                ID = 2,
                DisplayName = "Bình chứa oxi",
                Type = "Thiết bị",
                ManufactureDate = (new DateTime(2021, 4, 15)).ToString("dd/MM/yyyy"),
                Amount = 50,
                Unit = "Bình",
                Note = ""
            });
            Facilities.Add(new Facility()
            {
                ID = 3,
                DisplayName = "Panadol Extra",
                Type = "Thuốc",
                ManufactureDate = (new DateTime(2021, 2, 7)).ToString("dd/MM/yyyy"),
                Amount = 500,
                Unit = "Hộp",
                Note = "Hạn sử dụng 2 năm"
            });
            Facilities.Add(new Facility()
            {
                ID = 4,
                DisplayName = "Hapacol",
                Type = "Thuốc",
                ManufactureDate = (new DateTime(2021, 2, 3)).ToString("dd/MM/yyyy"),
                Amount = 200,
                Unit = "Hộp",
                Note = "Hạn sử dụng 2 năm",
            });
            Facilities.Add(new Facility()
            {
                ID = 5,
                DisplayName = "BreFence Go",
                Type = "Thiết bị",
                ManufactureDate = (new DateTime(2021, 1, 3)).ToString("dd/MM/yyyy"),
                Amount = 10,
                Unit = "Bộ",
                Note = "Bảo trì: 6 tháng/lần",
            });
            CheckedCount = 0;
            IsCheckedAll = false;


            AllCheckedCommand = new RelayCommand<CheckBox>((p) => { return p == null ? false : true; }, (p) =>
            {
                bool allcheckbox = (p.IsChecked == true);
                for (int i = 0; i < Facilities.Count; i++)
                    Facilities[i].IsChecked = allcheckbox;
            });

            SingleCheckedCommand = new RelayCommand<CheckBox>((p) => { return p == null ? false : true; }, (p) =>
            {
                IsCheckedAll = null;
                if (p.IsChecked == true)
                    CheckedCount++;
                else
                    CheckedCount--;

                if (CheckedCount == facilities.Count)
                    IsCheckedAll = true;
                else
                    if (CheckedCount == 0)
                    IsCheckedAll = false;
            });
        }
    }
}
