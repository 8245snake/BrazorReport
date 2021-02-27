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

        public IEnumerable<DraggableComponentModel> Children()
        {
            foreach (DraggableComponentModel item in Models)
            {
                yield return item;

                ContainerComponentModel container = item as ContainerComponentModel;
                if (container != null)
                {
                    foreach (var child in container.Children())
                    {
                        yield return child;
                    }
                }
            }
        }

    }
}