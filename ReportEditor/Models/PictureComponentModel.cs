using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReportEditor.Models
{
    public class PictureComponentModel : DraggableComponentModel
    {

        public string ImagePath { get; set; }



        public PictureComponentModel(string text, DraggableComponentModelType type, int defaultHeight, int defaultWidth, DraggableComponentLayoutMode layout) : base(text, type, defaultHeight, defaultWidth, layout)
        {
            ImagePath = @"./resource/sample-picture.png";
        }
    }
}
