using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReportEditor.Services
{
    public class DragAndDropService
    {
        /// <summary>
        /// ドロップされるデータ
        /// </summary>
        public object Data { get; set; }

        /// <summary>
        /// ドラッグ元のデータ
        /// </summary>
        public object DataSource { get; set; }


    }
}
