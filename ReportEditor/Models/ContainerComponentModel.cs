using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReportEditor.Models
{
    public class ContainerComponentModel : DraggableComponentModel
    {

        public DraggableComponentModelList Models = new DraggableComponentModelList();

        public ContainerComponentModel(string text, DraggableComponentModelType type, int defaultHeight, int defaultWidth, DraggableComponentLayoutMode layout) : base(text, type, defaultHeight, defaultWidth, layout)
        {
        }

        public IEnumerable<DraggableComponentModel> Children(bool recursive)
        {
            foreach (var tuple in EnumAllModelListPairs(recursive))
            {
                yield return tuple.Item1;
            }
        }

        /// <summary>
        /// 配下の要素とその親の参照をタプルで返す
        /// </summary>
        /// <param name="recursive">再帰的に探索するか</param>
        /// <returns></returns>
        public IEnumerable<Tuple<DraggableComponentModel, DraggableComponentModelList>> EnumAllModelListPairs(bool recursive)
        {
            foreach (DraggableComponentModel item in Models)
            {
                yield return new Tuple<DraggableComponentModel, DraggableComponentModelList>(item, Models);

                if (recursive)
                {
                    ContainerComponentModel container = item as ContainerComponentModel;
                    if (container != null)
                    {
                        foreach (var child in container.EnumAllModelListPairs(recursive))
                        {
                            yield return child;
                        }
                    }
                }
            }
        }

        public override DraggableComponentModel Clone()
        {
            ContainerComponentModel clone = this.MemberwiseClone() as ContainerComponentModel;

            clone.Models = new DraggableComponentModelList();
            clone.Models.AddRange(this.Models.Select(model => model.Clone()));
            clone.ID = $"component{ComponentIdNumber}-{clone.ModelType.Name()}";
            clone.Name = clone.ID;
            ComponentIdNumber++;
            return clone;
        }

    }
}