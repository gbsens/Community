namespace MKS.Core.Presenter.UI
{
    public interface IUITab<TView> : IUIBase where TView : IUIForm
    {
        Label Title { get; set; }
        TView View { get; set; }
    }
}