using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagement.Utils
{
    class SelectableItem<T> : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private bool isSelected;
        public bool IsSelected
        {
            get => isSelected;
            set
            {
                isSelected = value;
                OnPropertyChanged("IsSelected");
            }
        }

    public T Value { get; set; }

        public static ObservableCollection<SelectableItem<T>> GetSelectableItems(List<T> ls)
        {
            ObservableCollection<SelectableItem<T>> selectableItems = new ObservableCollection<SelectableItem<T>>();
            foreach(T t in ls)
            {
                SelectableItem<T> selectableItem = new SelectableItem<T>();
                selectableItem.Value = t;
                selectableItems.Add(selectableItem);
            }
            return selectableItems;
        }
    }
}
