using System.Collections.Generic;

namespace MKS.Core.Security
{
    public interface IApplicationAuthorization
    {
        List<IPermissionAuthorization> Permissions { get; set; }
        //bool IsUserAuthorized(params string[] p_lstPermission);
        bool IsUserAuthorized(List<IPermission> lstPermission);
    }
}