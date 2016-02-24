namespace MKS.Core.Presenter.UI
{
    public interface IUIOption : IUIBase
    {
        Label Texte { get; set; }
        Select Selection { get; set; }
    }
}