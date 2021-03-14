using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReportEditor.Models
{
    public class TextComponentModel : DraggableComponentModel
    {
        public string BindingPath { get; set; } = "";

        public string FontName { get; set; } = "ＭＳ ゴシック";

        public int FontSize { get; set; } = 14;

        public string FontStyle
        {
            get
            {
                string style = $" font-family:'{FontName}'; ";
                style += $"font-size:{FontSize}px; ";
                return style;
            }
        }



        public TextComponentModel(string text, DraggableComponentModelType type, int defaultHeight, int defaultWidth, DraggableComponentLayoutMode layout) : base(text, type, defaultHeight, defaultWidth, layout)
        {
        }
    }
}
