using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReportEditor.Models
{
    public class TextComponentModel : DraggableComponentModel
    {
        private string _BindingPath = "";
        private int _FontSize = 14;

        public string BindingPath { get => _BindingPath; set => _BindingPath = value; }


        public string FontName { get; set; } = "ＭＳ ゴシック";


        public int FontSize
        {
            get => _FontSize;
            set
            {
                _FontSize = value;
                OnPropertyChanged(new EventArgs());
            }
        }

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
