using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Drawing;

namespace ReportEditor.Models
{
    [Serializable]
    public class DraggableComponentModel
    {
        public event ModelPropertyChangedEventHandler PropertyChanged;
        public delegate void ModelPropertyChangedEventHandler(DraggableComponentModel model);

        protected static int ComponentIdNumber = 0;

        public enum DraggableComponentModelType : int
        {
            TextBlock,
            Table,
            Picture,
            Container,
            TableCell,
            Dummy,
        }

        public enum DraggableComponentLayoutMode : int
        {
            Absolute,
            Relative,
            Stack,
            Cell,
        }

        public enum VerticalAlign : int
        {
            Center,
            Top,
            Bottom
        }

        public enum HorizontalAlign : int
        {
            Center,
            Left,
            Right
        }

        private string _ID;
        public string ID
        {
            get => _ID;
            set
            {
                _ID = value;
                OnPropertyChanged(new EventArgs());
            }
        }

        private string _Name;

        public string Name
        {
            get => _Name;
            set
            {
                _Name = value;
                OnPropertyChanged(new EventArgs());
            }
        }


        public string Text { get; set; }

        public DraggableComponentModelType ModelType { get; set; }
        public DraggableComponentLayoutMode LayoutMode { get; set; }


        public Rectangle ComponentRect;


        private bool _IsHighLighting;

        public bool IsHighLighting
        {
            get => _IsHighLighting;
            set
            {
                if (_IsHighLighting != value)
                {
                    _IsHighLighting = value;
                    OnPropertyChanged(new EventArgs());
                }

            }
        }


        private bool _IsGrabbing;

        public bool IsGrabbing
        {
            get => _IsGrabbing;
            set
            {
                if (_IsGrabbing != value)
                {
                    _IsGrabbing = value;
                    OnPropertyChanged(new EventArgs());
                }
            }
        }

        public double GrabbingShiftX { get; set; }
        public double GrabbingShiftY { get; set; }
        public bool IsDraggable
        {
            get {
                if (LayoutMode != DraggableComponentLayoutMode.Stack)
                {
                    // いまのところスタックタイプのみドラッグ可能
                    return false;
                }
                if (ModelType == DraggableComponentModelType.TableCell)
                {
                    // テーブルセルはドラッグ禁止
                    return false;
                }
                // ハイライトしているもののみドラッグ可能
                return IsHighLighting;
            }
        }

        protected virtual void OnPropertyChanged(EventArgs e)
        {
            PropertyChanged?.Invoke(this);
        }

        public DraggableComponentModel()
        {
            Text = "unknown";
            ModelType = DraggableComponentModelType.Dummy;
            LayoutMode = DraggableComponentLayoutMode.Stack;
            ComponentRect.Height = 50;
            ComponentRect.Width = 100;
            ID = $"component{ComponentIdNumber}-{ModelType.Name()}";
            Name = ID;
            ComponentIdNumber++;
        }

        public DraggableComponentModel(string text, DraggableComponentModelType type, int defaultHeight, int defaultWidth, DraggableComponentLayoutMode layout)
        {
            Text = text;
            ModelType = type;
            LayoutMode = layout;
            ComponentRect.Height = defaultHeight;
            ComponentRect.Width = defaultWidth;
            ID = $"component{ComponentIdNumber}-{ModelType.Name()}";
            Name = ID;
            ComponentIdNumber++;
        }

        public void SetRect(int x, int y, int height, int width)
        {
            ComponentRect.X = x;
            ComponentRect.Y = y;
            ComponentRect.Height = height;
            ComponentRect.Width = width;
            OnPropertyChanged(new EventArgs());
        }

        public void SetLocation(int x, int y)
        {
            int oldX = ComponentRect.X;
            ComponentRect.X = x;

            int oldY = ComponentRect.Y;
            ComponentRect.Y = y;

            if (oldX != x || oldY != y)
            {
                OnPropertyChanged(new EventArgs());
            }
        }

        public void SetSize(int height, int width)
        {
            int oldHeight = ComponentRect.Height;
            ComponentRect.Height = height;

            int oldWidth = ComponentRect.Width;
            ComponentRect.Width = width;

            if (oldHeight != height || oldWidth != width)
            {
                OnPropertyChanged(new EventArgs());
            }
        }

        public string GetRectStyle()
        {
            if (ModelType == DraggableComponentModelType.TableCell)
            {
                return "height:100%; width:100%;";
            }
            int x = ComponentRect.X;
            int y = ComponentRect.Y;
            int height = ComponentRect.Height;
            int width = ComponentRect.Width;
            string unit = "px";
            return $"height:{height}{unit}; width:{width}{unit}; left:{x}{unit}; top:{y}{unit};";
        }


        public virtual DraggableComponentModel Clone()
        {
            var clone = (DraggableComponentModel)this.MemberwiseClone();
            clone.ID = $"component{ComponentIdNumber}-{clone.ModelType.Name()}";
            clone.Name = clone.ID;
            ComponentIdNumber++;
            return clone;
        }
    }

}
