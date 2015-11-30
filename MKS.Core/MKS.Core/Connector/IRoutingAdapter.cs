using System.Collections.Generic;

namespace MKS.Core.Connector
{
    public interface IRoutingAdapter
    {
        ISearchContract SearchContract { get; set; }

        List<IContract> GetContractAddresses(ISearchContract searchContract);

        IContract GetContractAddress(IKeyContract keyContract);
    }
}