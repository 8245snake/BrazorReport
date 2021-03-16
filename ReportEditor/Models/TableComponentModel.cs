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

        public void AddNewRow(int index = -1)
        {
            int rowIndex = Rows.Count;
            TableRow row = new TableRow(rowIndex);

            for (int colIndex = 0; colIndex < Cols.Count; colIndex++)
            {
                TableCell cell = new TableCell(rowIndex, colIndex);
                row.Cells.Add(cell);
            }

            if (index == -1)
            {
                Rows.Add(row);
            }
            else
            {
                Rows.Insert(index, row);
            }

            UpdateCellsLoaction();
            UpdateIndex();
            OnPropertyChanged(new EventArgs());
        }

        public void DeleteRow(int index)
        {
            Rows.RemoveAt(index);
            UpdateCellsLoaction();
            UpdateIndex();
            OnPropertyChanged(new EventArgs());
        }


        public void AddNewColumn(int index = -1)
        {
            int colIndex = Cols.Count;
            TableColumn col = new TableColumn(colIndex, $"列{colIndex}");
            if (index == -1)
            {
                Cols.Add(col);
            }
            else
            {
                Cols.Insert(index, col);
            }

            for (int rowIndex = 0; rowIndex < Rows.Count; rowIndex++)
            {
                if (index == -1)
                {
                    Rows[rowIndex].Cells.Add(new TableCell(rowIndex, colIndex));
                }
                else
                {
                    Rows[rowIndex].Cells.Insert(index, new TableCell(rowIndex, colIndex));
                }

            }
            UpdateCellsLoaction();
            UpdateIndex();
            OnPropertyChanged(new EventArgs());
        }

        public void DeleteCol(int index)
        {
            Cols.RemoveAt(index);
            foreach (var row in Rows)
            {
                row.Cells.RemoveAt(index);
            }
            UpdateCellsLoaction();
            UpdateIndex();
            OnPropertyChanged(new EventArgs());
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

        public void UpdateIndex()
        {
            for (int rowIndex = 0; rowIndex < Rows.Count; rowIndex++)
            {
                Rows[rowIndex].RowIndex = rowIndex;

                for (int colIndex = 0; colIndex < Cols.Count; colIndex++)
                {
                    Rows[rowIndex].Cells[colIndex].RowIndex = rowIndex;
                    Rows[rowIndex].Cells[colIndex].ColIndex = colIndex;
                }
            }
        }

        public IEnumerable<Tuple<DraggableComponentModel, DraggableComponentModelList>> EnumAllModelListPairs(bool recursive)
        {
            foreach (var row in Rows)
            {
                foreach (var cell in row.Cells)
                {
                    yield return new Tuple<DraggableComponentModel, DraggableComponentModelList>(cell.Model, null);

                    foreach (var item in cell.EnumAllModelListPairs(recursive))
                    {
                        yield return item;
                    }
                }
            }
        }



        public bool Contains(DraggableComponentModel model, out TableCell containsCell)
        {
            foreach (var row in Rows)
            {
                foreach (var cell in row.Cells)
                {
                    if (cell.Model == model)
                    {
                        containsCell = cell;
                        return true;
                    }
                }
            }
            containsCell = null;
            return false;
        }

        public override DraggableComponentModel Clone()
        {
            TableComponentModel clone = (TableComponentModel)base.Clone();
            clone.Rows = new List<TableRow>(Rows.Select(row => row.Clone()));
            clone.Cols = new List<TableColumn>(Cols.Select(col => col.Clone()));
            clone.HeaderRowHeight = HeaderRowHeight;
            return clone;
        }
    }
    public class TableRow
    {
        public int RowIndex;
        public int RowHeight = 50;
        public List<TableCell> Cells = new List<TableCell>();

        public TableRow(int index)
        {
            RowIndex = index;
        }
        public TableRow Clone()
        {
            var clone = new TableRow(RowIndex);
            clone.RowHeight = RowHeight;
            clone.Cells = new List<TableCell>(Cells.Select(cell => cell.Clone()));
            return clone;
        }
    }

    public class TableColumn
    {
        public int ColIndex;
        public int ColWidth = 100;
        public string Text;

        public TableColumn(int index , string text)
        {
            ColIndex = index;
            Text = text;
        }

        public TableColumn Clone()
        {
            var clone = new TableColumn(ColIndex, Text);
            return clone;
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

        public IEnumerable<Tuple<DraggableComponentModel, DraggableComponentModelList>> EnumAllModelListPairs(bool recursive)
        {
            foreach (var item in Model.EnumAllModelListPairs(recursive))
            {
                yield return item;
            }
        }

        public TableCell Clone()
        {
            var clone = new TableCell(RowIndex, ColIndex);
            clone.Model = (ContainerComponentModel)this.Model.Clone();
            return clone;
        }
    }


}