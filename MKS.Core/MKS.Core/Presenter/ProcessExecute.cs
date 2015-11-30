using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MKS.Core.Business;

namespace MKS.Core.Presentation
{
    internal class ProcessExecute:BusinessProcessExecute<TView>
    {
        public override List<RuleBusiness> GetProcessRules()
        {
            List<RuleBusiness> rb = new List<RuleBusiness>();
            rb.Add(new RuleBusiness("PREZ-INIT01", "Phase assignation des valeurs par défaut", Severity.Error));
            rb.Add(new RuleBusiness("PREZ-INIT02", "Phase assignation des validations", Severity.Error));
            rb.Add(new RuleBusiness("PREZ-INIT03", "Phase assignation des libellés", Severity.Warning));
            rb.Add(new RuleBusiness("PREZ-INIT04", "Phase assignation des états", Severity.Warning));
            rb.Add(new RuleBusiness("PREZ-INIT05", "Phase assignation de la sécurité", Severity.Error));

        }

        public override Process DoBusinessProcess(RuleBusiness rule, BusinessObjectExecute<TView> businessObject)
        {
            switch (rule.CodeMessage)
            {
                case "PREZ-INIT01":
                    break;
                case "PREZ-INIT02":
                    break;
                case "PREZ-INIT03":
                    break;
                case "PREZ-INIT04":
                    break;
                case "PREZ-INIT05":
                    break;
                case "PREZ-INIT06":
                    break;
                default:
                    break;
            }
        }
    }
}
