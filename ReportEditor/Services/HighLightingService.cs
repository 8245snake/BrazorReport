using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReportEditor.Services
{
    public class HighLightingService
    {
        /// <summary>
        /// ハイライト中のコンポーネントが変化したときに発火する
        /// </summary>
        public event EventHandler HighLightChanged;
        protected virtual void OnHighLightChanged(EventArgs e)
        {
            HighLightChanged?.Invoke(this, e);
        }

        private string _HighLightingID = "";

        public string HighLightingID
        {
            get => _HighLightingID;
            set {
                if (value != _HighLightingID)
                {
                    _HighLightingID = value;
                    OnHighLightChanged(new EventArgs());
                }
            }
        }
    }
}
