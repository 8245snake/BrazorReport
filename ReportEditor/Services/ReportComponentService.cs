using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ReportEditor.Models;

namespace ReportEditor.Services
{
    public class ReportComponentService
    {
        public string ParameterUnit = "px";

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

        private void OnItemPropertyChanged(DraggableComponentModelList list, DraggableComponentModel item)
        {
            if (list != null)
            {
                OnModelPropertyChanged(list.ContainerID, item);
            }
        }

        public void Add(ContainerComponentModel paper)
        {
            if (!paper.Models.Contains(paper))
            {
                paper.Models.ItemPropertyChanged += OnItemPropertyChanged;
                _Papers.Add(paper);
            }
        }

        public void Remove(ContainerComponentModel paper)
        {
            paper.Models.ItemPropertyChanged -= OnItemPropertyChanged;
            _Papers.Remove(paper);
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
                    parent = new DraggableComponentModelList(_Papers);
                    return true;
                }

                foreach (var tuple in paper.EnumAllModelListPairs(true))
                {
                    if (tuple.Item1.ID == id)
                    {
                        item = tuple.Item1;
                        parent = tuple.Item2;
                        return true;
                    }
                }
            }

            item = null;
            parent = null;
            return false;
        }

        public IEnumerable<DraggableComponentModel> EnumAllModels()
        {
            foreach (var paper in _Papers)
            {
                foreach (var tuple in paper.EnumAllModelListPairs(true))
                {
                    yield return tuple.Item1;
                }
            }
        }

    }
}
