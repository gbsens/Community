namespace MKS.Core.Presenter.UI
{
    public interface IUINumericInput : IUIBase
    {
        int Text { get; set; }
        IUILabel Label { get; set; }
    }
    public interface IUINumericInputLong : IUIBase
    {
        long Text { get; set; }
        IUILabel Label { get; set; }
    }
    public interface IUINumericInputFloat : IUIBase
    {
        float Text { get; set; }
        IUILabel Label { get; set; }
    }
    public interface IUINumericInputDouble : IUIBase
    {
        double Text { get; set; }
        IUILabel Label { get; set; }
    }
    public interface IUINumericInputDecimal : IUIBase
    {
        decimal Text { get; set; }
        IUILabel Label { get; set; }
    }
}