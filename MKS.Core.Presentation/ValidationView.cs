using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MKS.Core;
using MKS.Core.Presenter.Interfaces;

namespace MKS.Core.Presenter
{
    /// <summary>
    /// NOTE RELEASED
    /// </summary>
    /// <typeparam name="TView"></typeparam>
    public class ValidationView<TView>:Validation<TView> where TView:IView
    {

        public override ValidationRules GetRules()
        {
            if (base.ObjectInstance != null)
            { }
            return null;
        }
        public override bool Validate(Rule rule, ValidationRule ruleProperty, TView item, RuleResults rulesResults)
        {
            rulesResults.Add(item.Validations.Validate<TView>(base.ObjectInstance));
            return true; 
        }
    }
}
