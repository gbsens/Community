using System;
using System.Collections.Generic;

namespace MKS.Core.Business.Interfaces
{
    public interface IBusinessProcess : IDisposable
    {
        List<RuleBusiness> GetProcessRules();

        Process DoBusinessProcess(RuleBusiness rule, ref IBusinessObject businessObject);
    }
}