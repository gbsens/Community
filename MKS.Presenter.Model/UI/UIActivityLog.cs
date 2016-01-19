using MKS.Core.Presenter.Interfaces;
using MKS.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MKS.Core.Presenter.UI
{
    public class UIActivityLog
    { }

    public class UIActivityLog<TView>:UIActivityLog where TView:IView
    {
        public string ObjectPropertyName { get; set; }
        public UIActivityLog(Expression<Func<TView, object>> property)
        {
            ObjectPropertyName = Reflect<TView>.GetName(property);
        }
    }
}
