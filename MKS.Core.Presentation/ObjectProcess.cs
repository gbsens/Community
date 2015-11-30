using MKS.Core.Presenter;
using MKS.Core.Presenter.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKS.Core.Presenter
{
    public class ObjectProcess<TView> 
        where TView:IView   
    {
        TView _view;
        IOperation<TView> _process;

        public ObjectProcess(TView view, IOperation<TView> process, IPresenter presenter)
        {
            _view = view;
            _process = process;
            Presenter = presenter;
        }
        public IOperation<TView> GetProcess { get { return _process; } }
        public TView GetView { get { return _view; } }
        
        public string Command { get; set; }
        public CommandEventArgsCustom Args { get; set; }
        public IPresenter Presenter {get;set;}
    }
}
