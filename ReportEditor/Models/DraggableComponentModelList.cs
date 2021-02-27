using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReportEditor.Models
{
    public class DraggableComponentModelList : IList<DraggableComponentModel>
    {
        private static int SheetIdNumber = 0;
        private List<DraggableComponentModel> Items = new List<DraggableComponentModel>();

        public string SheetID { get; set; }

        public event ItemPropertyChangedEventHandler ItemPropertyChanged;
        public delegate void ItemPropertyChangedEventHandler(DraggableComponentModelList list, DraggableComponentModel item);

        protected virtual void OnItemPropertyChanged(DraggableComponentModel item)
        {
            ItemPropertyChanged?.Invoke(this, item);
        }

        private void PropertyChanged(DraggableComponentModel sender)
        {
            OnItemPropertyChanged(sender);
        }

        #region "IList"
        public DraggableComponentModel this[int index] { get => Items[index]; set => Items[index] = value; }

        public int Count => Items.Count;

        public bool IsReadOnly => false;

        public void Add(DraggableComponentModel item)
        {
            item.PropertyChanged += PropertyChanged;
            Items.Add(item);
        }

        public void Clear()
        {
            Items.Clear();
        }

        public bool Contains(DraggableComponentModel item)
        {
            return Items.Contains(item);
        }

        public void CopyTo(DraggableComponentModel[] array, int arrayIndex)
        {
            Items.CopyTo(array, arrayIndex);
        }

        public IEnumerator<DraggableComponentModel> GetEnumerator()
        {
            return Items.GetEnumerator();
        }

        public int IndexOf(DraggableComponentModel item)
        {
            return Items.IndexOf(item);
        }

        public void Insert(int index, DraggableComponentModel item)
        {
            item.PropertyChanged += PropertyChanged;
            Items.Insert(index, item);
        }

        public bool Remove(DraggableComponentModel item)
        {
            item.PropertyChanged -= PropertyChanged;
            return Items.Remove(item);
        }

        public void RemoveAt(int index)
        {
            var item = this[index];
            Items.Remove(item);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Items.GetEnumerator();
        }

        #endregion



        public DraggableComponentModelList()
        {
            SheetIdNumber++;
            SheetID = $"sheet-{SheetIdNumber}";
        }

        public void AddRange(IEnumerable<DraggableComponentModel> items)
        {
            Items.AddRange(items);
        }

    }
}
