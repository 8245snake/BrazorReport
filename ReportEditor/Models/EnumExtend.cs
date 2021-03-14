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
                case DraggableComponentModel.DraggableComponentModelType.TableCell:
                    return "cell";
            }

            return "other";
        }

        public static string NameJP(this DraggableComponentModel.DraggableComponentModelType self)
        {
            switch (self)
            {
                case DraggableComponentModel.DraggableComponentModelType.TextBlock:
                    return "テキスト";
                case DraggableComponentModel.DraggableComponentModelType.Table:
                    return "表";
                case DraggableComponentModel.DraggableComponentModelType.Picture:
                    return "画像";
                case DraggableComponentModel.DraggableComponentModelType.Container:
                    return "コンテナ";
                case DraggableComponentModel.DraggableComponentModelType.TableCell:
                    return "セル";
            }

            return "その他";
        }

        public static string StyleName(this DraggableComponentModel.DraggableComponentModelType self)
        {
            switch (self)
            {
                case DraggableComponentModel.DraggableComponentModelType.TextBlock:
                    return "xrp2-text";
                case DraggableComponentModel.DraggableComponentModelType.Table:
                    return "xrp2-table";
                case DraggableComponentModel.DraggableComponentModelType.Picture:
                    return "xrp2-picture";
                case DraggableComponentModel.DraggableComponentModelType.Container:
                    return "xrp2-group";
                case DraggableComponentModel.DraggableComponentModelType.TableCell:
                    return "xrp2-cell";
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

            return "position-relative";
        }
    }
}
