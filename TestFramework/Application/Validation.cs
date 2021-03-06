﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using MKS.Core;
using MKS.Library;

namespace TestFramework.Application
{
    public class ValidationPersonne:Validation<Personne>
    {
        public override ValidationRules GetRules()
        {
            ValidationRules lr = new ValidationRules();

            lr.Add(new RuleStringRequired("VAL_NOM",
                "Le nom est obligatoire",
                Rule.RuleSeverity.Error),
                Reflect<Personne>.GetName(c => c.Nom));
            lr.Add(new RuleCustom("VAL_CUST", "Validation personnalisé", Rule.RuleSeverity.Error),"Nom");


            return lr;
        }
        public override bool Validate(Rule rule, ValidationRule ruleProperty, Personne item, RuleResults rulesResults)
        {
            if (item.Nom.ToUpper() == "LOLO")
                return false;
            return true;
        }
    }
}
