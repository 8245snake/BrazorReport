using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReportEditor.Models
{
    public class TableComponentModel : DraggableComponentModel
    {
        public List<TableRow> Rows = new List<TableRow>();
        public List<TableColumn> Cols = new List<TableColumn>();

        
        public TableComponentModel(string text, DraggableComponentModelType type, int defaultHeight, int defaultWidth, DraggableComponentLayoutMode layout) : base(text, type, defaultHeight, defaultWidth, layout)
        {
            Cols.Add(new TableColumn() { ColIndex = 0 });
            Cols.Add(new TableColumn() { ColIndex = 1 });
            Cols.Add(new TableColumn() { ColIndex = 2 });

            Rows.Add(new TableRow { RowIndex = 0 });
            Rows.Add(new TableRow { RowIndex = 1 });
        }
    }
    public class TableRow
    {
        public int RowIndex;


    }

    public class TableColumn
    {
        public int ColIndex;


    }
    public class TableCell
    {
        public int RowIndex;
        public int ColIndex;


    }
}