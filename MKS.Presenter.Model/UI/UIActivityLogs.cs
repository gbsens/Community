using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKS.Core.Presenter.UI
{

    public class UIActivityLogs
    {
        private List<UIActivityLog> _activityLog = new List<UIActivityLog>();
        public List<UIActivityLog> UI
        {
            get { return _activityLog; }
            set { _activityLog = value; }
        }
    }
}
