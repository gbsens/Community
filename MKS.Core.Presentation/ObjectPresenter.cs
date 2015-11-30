using MKS.Core.Presenter;
using MKS.Core.Presenter.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKS.Core.Presenter
{
    public class ObjectPresenter<TView> 
        where TView:IView
    {
        TView _view;
        IOperation<TView> _process;

        public ObjectPresenter(TView view, IPresenter presenter)
        {
            _view = view;            
            Presenter = presenter;
        }
        
        public TView GetView { get { return _view; } }

        public IOperation<TView> Process { get { return _process; } set { _process = value; } }

        public IPresenter Presenter { get; private set; }
    }
}
