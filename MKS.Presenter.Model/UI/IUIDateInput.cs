using System;

namespace MKS.Core.Presenter.UI
{
    public interface IUIDateInput : IUIBase
    {
        DateTime Date { get; set; }
        IUILabel Label { get; set; }
    }
}