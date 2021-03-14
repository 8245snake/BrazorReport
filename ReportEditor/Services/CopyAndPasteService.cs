using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ReportEditor.Models;

namespace ReportEditor.Services
{
    public class CopyAndPasteService
    {

        private  DraggableComponentModel _CopySource;

        public DraggableComponentModel CopySource { get => _CopySource; set => _CopySource = value; }

    }
}
