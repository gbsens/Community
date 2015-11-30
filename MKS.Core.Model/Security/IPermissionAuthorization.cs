namespace MKS.Core.Security
{
    public interface IPermissionAuthorization
    {
        IPermission Permission { get; set; }
        bool IsUserAuthorized { get; set; }
    }
}