namespace MKS.Core.Presenter.UI
{
    public interface IUICheck : IUIBase
    {
        IUILabel Texte { get; set; }
        Select Selection { get; set; }
    }
}