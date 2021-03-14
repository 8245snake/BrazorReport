using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static ReportEditor.Models.DraggableComponentModel;

namespace ReportEditor.Models
{
    public class DraggableComponentModelFactory
    {
        public DraggableComponentModel Create(string text, DraggableComponentModelType type, int defaultHeight = 0, int defaultWidth = 0, DraggableComponentLayoutMode layout = DraggableComponentLayoutMode.Relative)
        {

            switch (type)
            {
                case DraggableComponentModelType.TextBlock:
                    return new TextComponentModel(text, type, defaultHeight, defaultWidth, layout);
                case DraggableComponentModelType.Table:
                    return new TableComponentModel(text, type, defaultHeight, defaultWidth, layout);
                case DraggableComponentModelType.Picture:
                    return new PictureComponentModel(text, type, defaultHeight, defaultWidth, layout);
                case DraggableComponentModelType.Container:
                case DraggableComponentModelType.TableCell:
                    return new ContainerComponentModel(text, type, defaultHeight, defaultWidth, layout);
                default:
                    break;
            }

            return new DraggableComponentModel(text, type, defaultHeight, defaultWidth, layout);
        }
    }
}
