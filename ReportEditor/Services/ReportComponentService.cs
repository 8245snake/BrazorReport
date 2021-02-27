using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ReportEditor.Models;

namespace ReportEditor.Services
{
    public class ReportComponentService
    {
        // リストのリスト
        private List<DraggableComponentModelList> _ModelLists = new List<DraggableComponentModelList>();

        /// <summary>
        /// モデルプロパティが変化したら発火する
        /// </summary>
        public event ModelPropertyChangedEventHandler ModelPropertyChanged;
        public delegate void ModelPropertyChangedEventHandler(string sheetID, DraggableComponentModel model);

        protected virtual void OnModelPropertyChanged(string sheetID, DraggableComponentModel model)
        {
            ModelPropertyChanged?.Invoke(sheetID, model);
        }

        public DraggableComponentModelList this[string sheetID]
        {
            get => _ModelLists.Where(list => list.SheetID == sheetID).FirstOrDefault();
            set {
                var list = _ModelLists.Where(list => list.SheetID == sheetID).FirstOrDefault();
                if (list != null)
                {
                    Remove(list);
                }
                Add(value);
            }
        }

        public void Add(DraggableComponentModelList list)
        {
            list.ItemPropertyChanged += OnItemPropertyChanged;
            _ModelLists.Add(list);
        }

        public void Remove(DraggableComponentModelList list)
        {
            list.ItemPropertyChanged -= OnItemPropertyChanged;
            _ModelLists.Remove(list);
        }

        public DraggableComponentModel Find(string id)
        {
            foreach (var list in _ModelLists)
            {
                foreach (var model in list)
                {
                    if (model.ID == id)
                    {
                        return model;
                    }
                    ContainerComponentModel container = model as ContainerComponentModel;
                    if (container != null)
                    {
                        foreach (var item in container.Children())
                        {
                            if (item.ID == id)
                            {
                                return item;
                            }
                        }
                    }
                }
            }

            return null;
        }

        private void OnItemPropertyChanged(DraggableComponentModelList list, DraggableComponentModel item)
        {
            if (list != null)
            {
                OnModelPropertyChanged(list.SheetID, item);
            }
        }
    }
}
