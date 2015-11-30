namespace MKS.Core.Connector
{
    public interface IKeyContract
    {
        string Caller { get; set; }

        string Action { get; set; }

        IContract Contract { get; set; }
    }
}