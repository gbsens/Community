namespace MKS.Core.Presenter.UI
{
    public interface IUICheck : IUIBase
    {
        Label Texte { get; set; }
        Select Selection { get; set; }
    }
}