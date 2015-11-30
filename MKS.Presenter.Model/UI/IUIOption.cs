namespace MKS.Core.Presenter.UI
{
    public interface IUIOption : IUIBase
    {
        IUILabel Texte { get; set; }
        Select Selection { get; set; }
    }
}