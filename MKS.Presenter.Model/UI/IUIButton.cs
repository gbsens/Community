namespace MKS.Core.Presenter.UI
{
    public interface IUIButton : IUIBase
    {
        string Text { get; set; }
        string Command { get; set; }
    }
}