namespace MKS.Core.Presenter.UI
{
    public interface IUIInput : IUIBase
    {
        string Text { get; set; }
        IUILabel Label { get; set; }
    }
}