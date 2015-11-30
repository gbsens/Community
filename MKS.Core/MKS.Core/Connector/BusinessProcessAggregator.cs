using MKS.Core.Business;
using MKS.Core.Business.Interfaces;
using MKS.Core.Model;
using System.Collections.Generic;

namespace MKS.Core.Connector
{
    public abstract class BusinessProcessAggregator<TBusinessObject> : IBusinessProcess where TBusinessObject : IBusinessObject
    {
        public List<IContract> contracts { get; set; }

        public abstract Process DoBusinessProcess(RuleBusiness rule, TBusinessObject businessObject, List<IContract> contractCollection);

        public abstract List<RuleBusiness> GetProcessRules();

        public virtual void Dispose()
        {
        }

        public Process DoBusinessProcess(RuleBusiness rule, ref IBusinessObject businessObject)
        {
            TBusinessObject businessObjectAdd = (TBusinessObject)businessObject;
            return DoBusinessProcess(rule, businessObjectAdd, contracts);
        }
    }

    public abstract class BusinessProcessAddAggregator<TObject> : BusinessProcessAggregator<BusinessObjectAdd<TObject>>
    {
    }

    public abstract class BusinessProcessUpdateAggregator<TObject> : BusinessProcessAggregator<BusinessObjectUpdate<TObject>>
    {
    }

    public abstract class BusinessProcessDeleteAggregator<TObject> : BusinessProcessAggregator<BusinessObjectDelete<TObject>>
    {
    }

    public abstract class BusinessProcessDeleteAggregator<TObject, TKey> : BusinessProcessAggregator<BusinessObjectDelete<TObject, TKey>>
        where TKey : IKey
    {
    }

    public abstract class BusinessProcessDeleteAggregator<TObject, TResult, TSearch> : BusinessProcessAggregator<BusinessObjectDelete<TObject, TResult, TSearch>>
        where TSearch : ISearch
    {
    }

    public abstract class BusinessProcessSelectAggregator<TObject> : BusinessProcessAggregator<BusinessObjectSelect<TObject>>
    {
    }

    public abstract class BusinessProcessSelectAggregator<TObject, TKey> : BusinessProcessAggregator<BusinessObjectSelect<TObject, TKey>>
        where TKey : IKey
    {
    }

    public abstract class BusinessProcessSelectAggregator<TObject, TResult, TSearch> : BusinessProcessAggregator<BusinessObjectSelect<TObject, TResult, TSearch>>
        where TSearch : ISearch
    {
    }

    public abstract class BusinessProcessExecuteAggregator<TObject> : BusinessProcessAggregator<BusinessObjectExecute<TObject>>
    {
    }
}