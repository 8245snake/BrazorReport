using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReportEditor.Models
{
    public class TextComponentModel : DraggableComponentModel
    {


        public TextComponentModel(string text, DraggableComponentModelType type, int defaultHeight, int defaultWidth, DraggableComponentLayoutMode layout) : base(text, type, defaultHeight, defaultWidth, layout)
        {
        }
    }
}
