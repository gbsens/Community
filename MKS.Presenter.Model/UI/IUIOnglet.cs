using System.Collections.Generic;

namespace MKS.Core.Presenter.UI
{
    public interface IUIOnglet : IUIBase
    {
    }

    public interface IUIOngletDynamic<TView> where TView : IUIForm
    {
        List<IUITab<TView>> Tabs { get; set; }
    }
}