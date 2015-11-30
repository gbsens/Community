using System.Collections.Generic;

namespace MKS.Core.Connector
{
    public interface ISearchContract
    {
        string Caller { get; set; }

        string Action { get; set; }

        List<IContract> Contracts { get; set; }
    }
}