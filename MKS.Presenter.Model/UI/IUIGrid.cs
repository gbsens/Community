using MKS.Core;

namespace MKS.Core.Presenter.UI
{
    public interface IUIGrid : IUIBase
    {
        VirtualSkip VirtualPaging { get; set; }
    }
}