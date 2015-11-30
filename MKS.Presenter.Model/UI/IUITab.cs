namespace MKS.Core.Presenter.UI
{
    public interface IUITab<TView> : IUIBase where TView : IUIForm
    {
        IUILabel Title { get; set; }
        TView View { get; set; }
    }
}