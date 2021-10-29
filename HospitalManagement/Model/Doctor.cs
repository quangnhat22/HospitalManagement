using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagement.Model
{
    public enum SexType { Nam, Nữ }

    public class Doctor : INotifyPropertyChanged
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

        public int Age { get; set; }

        public string Phone { get; set; }

        public string Mail { get; set; }

        public SexType Sex { get; set; }

        public string Birthday { get; set; }

        public string Specialization { get; set; }

        public string Rule { get; set; }
    }
}
