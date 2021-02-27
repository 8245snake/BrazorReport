using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReportEditor.Models
{
    static class EnumExtend
    {
        public static string Name(this DraggableComponentModel.DraggableComponentModelType self)
        {
            switch (self)
            {
                case DraggableComponentModel.DraggableComponentModelType.TextBlock:
                    return "textblock";
                case DraggableComponentModel.DraggableComponentModelType.Table:
                    return "table";
                case DraggableComponentModel.DraggableComponentModelType.Picture:
                    return "picture";
                case DraggableComponentModel.DraggableComponentModelType.Container:
                    return "group";
            }

            return "other";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static string PositionClass(this DraggableComponentModel.DraggableComponentLayoutMode self)
        {
            switch (self)
            {
                case DraggableComponentModel.DraggableComponentLayoutMode.Absolute:
                    return "position-absolute";
                case DraggableComponentModel.DraggableComponentLayoutMode.Relative:
                    return "position-static";
                case DraggableComponentModel.DraggableComponentLayoutMode.Stack:
                    return "position-static";
            }

            return "position-absolute";
        }
    }
}
