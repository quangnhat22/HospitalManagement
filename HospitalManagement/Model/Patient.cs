using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace HospitalManagement.Model
{
    public class Patient : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

        private bool isChecked;
        public bool IsChecked
        {
            get { return isChecked; }
            set { isChecked = value; OnPropertyChanged("IsChecked"); }
        }

        public int ID { get; set; }
        public string Name { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PatientCode { get; set; }
        public string Phone { get; set; }
        public string Mail { get; set; }
        public string Address { get; set; }
        public string Birthday { get; set; }
        public SexType Sex { get; set; }
        public string BedNum { get; set; }
        public string DateCheckIn { get; set; }
        public string Nationality { get; set; }
        public string IDCard { get; set; }
        public string Note { get; set; }
        public string Status { get; set; }
        public string BackgroundDisease { get; set; }
    }
}
