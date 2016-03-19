using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using MKS.Core;
using MKS.Library;

namespace TestFramework.Application
{
    public class ValidationSearchPersonne:Validation<SearchPersonne>
    {
        public override ValidationRules GetRules()
        {
            ValidationRules lr = new ValidationRules();

            lr.Add(new RuleStringRequired("VAL_NOM",
                "Le nom est obligatoire",
                Rule.RuleSeverity.Error),
                Reflect<SearchPersonne>.GetName(c => c.Nom));
            lr.Add(new RuleCustom("VAL_CUST", "Validation personnalisé", Rule.RuleSeverity.Error),"Nom");


            return lr;
        }
        public override bool Validate(Rule rule, ValidationRule ruleProperty, SearchPersonne item, RuleResults rulesResults)
        {
            if (item.Nom.ToUpper() == "LOLO")
                return false;
            return true;
        }
    }
}
