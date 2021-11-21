using GongSolutions.Wpf.DragDrop;
using HospitalManagement.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HospitalManagement.ViewModel
{
    class TeamViewmodel : BaseViewModel, IDropTarget
    {
        // start
        private ObservableCollection<DragableItem<BACSI>> to1;
        private ObservableCollection<DragableItem<BACSI>> to2;

        public TeamViewmodel()
        {
            to1 = DragableItem<BACSI>.GetDragableItems((DataProvider.Ins.DB.BACSIs.Where(p => p.TO.ID == 1).ToList()));
            to2 = DragableItem<BACSI>.GetDragableItems((DataProvider.Ins.DB.BACSIs.Where(p => p.TO.ID == 2).ToList()));
        }

        public ObservableCollection<DragableItem<BACSI>> To1 { get => to1; set => to1 = value; }
        public ObservableCollection<DragableItem<BACSI>> To2 { get => to2; set => to2 = value; }

        void IDropTarget.DragOver(IDropInfo dropInfo)
        {
            DragableItem<BACSI> sourceItem = dropInfo.Data as DragableItem<BACSI>;
            DragableItem<BACSI> targetItem = dropInfo.TargetItem as DragableItem<BACSI>;

            if (sourceItem != null && targetItem != null)
            {
                dropInfo.DropTargetAdorner = DropTargetAdorners.Insert;
                dropInfo.Effects = DragDropEffects.Move;
            }
        }

        void IDropTarget.Drop(IDropInfo dropInfo)
        {
            DragableItem<BACSI> sourceItem = dropInfo.Data as DragableItem<BACSI>;
            DragableItem<BACSI> targetItem = dropInfo.TargetItem as DragableItem<BACSI>;
            if (sourceItem.Value.IDTO == 1)
            {
                sourceItem.Value.IDTO = 2;
                To1.Remove(sourceItem);
                To2.Add(sourceItem);
            }
            else
            {
                sourceItem.Value.IDTO = 1;
                To2.Remove(sourceItem);
                To1.Add(sourceItem);
            }
            DataProvider.Ins.DB.SaveChanges();
        }
    }

    public class DragableItem<T> : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public bool CanAcceptChildren { get; set; }

        public T Value { get; set; }

        public static ObservableCollection<DragableItem<T>> GetDragableItems(List<T> ls)
        {
            ObservableCollection<DragableItem<T>> selectableItems = new ObservableCollection<DragableItem<T>>();
            foreach (T t in ls)
            {
                DragableItem<T> selectableItem = new DragableItem<T>();
                selectableItem.Value = t;
                selectableItems.Add(selectableItem);
            }
            return selectableItems;
        }
    }
}
