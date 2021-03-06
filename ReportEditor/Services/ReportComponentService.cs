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
        private List<ContainerComponentModel> _Papers = new List<ContainerComponentModel>();

        /// <summary>
        /// モデルプロパティが変化したら発火する
        /// </summary>
        public event ModelPropertyChangedEventHandler ModelPropertyChanged;
        public delegate void ModelPropertyChangedEventHandler(string sheetID, DraggableComponentModel model);

        protected virtual void OnModelPropertyChanged(string sheetID, DraggableComponentModel model)
        {
            ModelPropertyChanged?.Invoke(sheetID, model);
        }

        public ContainerComponentModel this[string sheetID]
        {
            get => _Papers.Where(paper => paper.ID == sheetID).FirstOrDefault();
            set {
                var list = _Papers.Where(paper => paper.ID == sheetID).FirstOrDefault();
                if (list != null)
                {
                    Remove(list);
                }
                Add(value);
            }
        }

        public void Add(ContainerComponentModel paper)
        {
            paper.Models.ItemPropertyChanged += OnItemPropertyChanged;
            _Papers.Add(paper);
        }

        public void Remove(ContainerComponentModel paper)
        {
            paper.Models.ItemPropertyChanged -= OnItemPropertyChanged;
            _Papers.Remove(paper);
        }

        public DraggableComponentModel Find(string id)
        {

            DraggableComponentModel item;
            DraggableComponentModelList parent;

            if (Find(id, out item, out parent))
            {
                return item;
            }
            return null;
        }

        private bool Find(string id, out DraggableComponentModel item, out DraggableComponentModelList parent)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                item = null;
                parent = null;
                return false;
            }

            foreach (var paper in _Papers)
            {
                if (paper.ID == id)
                {
                    item = paper;
                    parent = null;
                    return true;
                }

                foreach (var model in paper.Models)
                {
                    if (model.ID == id)
                    {
                        item = model;
                        parent = paper.Models;
                        return true;
                    }
                    ContainerComponentModel container = model as ContainerComponentModel;
                    if (container != null)
                    {
                        foreach (var child in container.Children())
                        {
                            if (child.ID == id)
                            {
                                item = child;
                                parent = container.Models;
                                return true;
                            }
                        }
                    }
                }
            }

            item = null;
            parent = null;
            return false;
        }

        public void Remove(DraggableComponentModel model)
        {
            DraggableComponentModel item;
            DraggableComponentModelList parent;

            if (Find(model.ID, out item, out parent))
            {
                parent.Remove(item);
            }
        }

        private void OnItemPropertyChanged(DraggableComponentModelList list, DraggableComponentModel item)
        {
            if (list != null)
            {
                OnModelPropertyChanged(list.ContainerID, item);
            }
        }
    }
}
