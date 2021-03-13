using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static ReportEditor.Models.DraggableComponentModel;

namespace ReportEditor.Models
{
    public class TableComponentModel : DraggableComponentModel
    {
        public List<TableRow> Rows = new List<TableRow>();
        public List<TableColumn> Cols = new List<TableColumn>();
        public int HeaderRowHeight = 50;
        
        public TableComponentModel(string text, DraggableComponentModelType type, int defaultHeight, int defaultWidth, DraggableComponentLayoutMode layout) : base(text, type, defaultHeight, defaultWidth, layout)
        {
            int initialRowCount = 2;
            int initialColCount = 3;

            for (int i = 0; i < initialColCount; i++)
            {
                AddNewColumn();
            }

            for (int i = 0; i < initialRowCount; i++)
            {
                AddNewRow();
            }
            UpdateCellsLoaction();
        }

        public void AddNewRow()
        {
            int rowIndex = Rows.Count;
            TableRow row = new TableRow(rowIndex);

            for (int colIndex = 0; colIndex < Cols.Count; colIndex++)
            {
                TableCell cell = new TableCell(rowIndex, colIndex);
                row.Cells.Add(cell);
            }

            Rows.Add(row);
        }

        public void UpdateCellsLoaction()
        {
            int x = 0;
            int y = 0;

            for (int rowIndex = 0; rowIndex < Rows.Count; rowIndex++)
            {
                for (int colIndex = 0; colIndex < Cols.Count; colIndex++)
                {
                    Rows[rowIndex].Cells[colIndex].Model.SetLocation(x, y);
                    x += Cols[colIndex].ColWidth;
                }
                x = 0;
                y += Rows[rowIndex].RowHeight;
            }
        }

        public void AddNewColumn()
        {
            int colIndex = Cols.Count;
            TableColumn col = new TableColumn(colIndex, $"列{colIndex}");
            Cols.Add(col);

            for (int rowIndex = 0; rowIndex < Rows.Count; rowIndex++)
            {
                Rows[rowIndex].Cells.Add(new TableCell(rowIndex, colIndex));
            }
        }

    }
    public class TableRow
    {
        public int RowIndex;
        public int RowHeight = 100;
        public List<TableCell> Cells = new List<TableCell>();

        public TableRow(int index)
        {
            RowIndex = index;
        }
    }

    public class TableColumn
    {
        public int ColIndex;
        public int ColWidth = 200;
        public string Text;

        public TableColumn(int index , string text)
        {
            ColIndex = index;
            Text = text;
        }
    }

    public class TableCell
    {
        public int RowIndex;
        public int ColIndex;
        
        public ContainerComponentModel Model;

        public TableCell(int rowIndex, int colIndex)
        {
            RowIndex = rowIndex;
            ColIndex = colIndex;
            DraggableComponentModelFactory f = new DraggableComponentModelFactory();
            Model = f.Create("cell", DraggableComponentModelType.TableCell, 100, 200, DraggableComponentLayoutMode.Cell) as ContainerComponentModel;
        }
    }
}