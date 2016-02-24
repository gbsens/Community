using System;

namespace MKS.Core.Presenter.UI
{
    public interface IUIDateInput : IUIBase
    {
        DateTime Date { get; set; }
        Label Label { get; set; }
    }
}