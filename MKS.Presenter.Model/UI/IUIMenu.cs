using System;
using System.Collections.Generic;

namespace MKS.Core.Presenter.UI
{
    public interface IUIMenu : IUIBase
    {
        string Command { get; set; }
        string Text { get; set; }
        Uri Url { get; set; }
        List<Menu> Menus { get; set; }
    }
}