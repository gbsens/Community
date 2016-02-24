using MKS.Core.Security;
using System.Collections.Generic;

namespace MKS.Core.Presenter.UI
{
    public interface IUIBase
    {
        bool Enabled { get; set; }
        string Info { get; set; }
        string Tips { get; set; }
        bool Visible { get; set; }
        UIVAlidation UIValidations { get; set; }
        List<Permission> Permissions { get; set; }
        
    }

    //public interface IUIBase<TViewBase>:IUIBase
    //{
    //    UIValidation<TViewBase> UIValidations { get; set; }

    //    //void SetValidation<TViewBase>(TViewBase objectinstance, UIValidation<TViewBase> validation);

    //}
}