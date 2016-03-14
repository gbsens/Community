using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MKS.Core.Business;

namespace TestFramework.Application
{
    public class ProcessAddPersonne:BusinessProcessAdd<Personne>
    {
        public override List<RuleBusiness> GetProcessRules()
        {
            List<RuleBusiness> rl = new List<RuleBusiness>();
            rl.Add(new RuleBusiness("NOM_INTERDIT", "Il est interdit d'utiliser ce mot", MKS.Core.Rule.RuleSeverity.Error));
            return rl;
        }

        public override Process DoBusinessProcess(RuleBusiness rule, BusinessObjectAdd<Personne> businessObject)
        {
            switch (rule.CodeMessage)
            {
                case "NOM_INTERDIT":
                    if (businessObject.Parameter.Nom.ToUpper() == "STEPHANE")
                        return Process.FailedThrow;
                    break;

            }
            return Process.Succeed;
        }
    }
}
