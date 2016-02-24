namespace MKS.Core.Presenter.UI
{
    public interface IUIInput : IUIBase
    {
        string Text { get; set; }
        Label Label { get; set; }
    }
}