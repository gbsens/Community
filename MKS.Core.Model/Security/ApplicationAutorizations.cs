using System.Collections.Generic;

namespace MKS.Core.Security
{
    public class ApplicationAutorizations : IApplicationAuthorization
    {
        private List<IPermissionAuthorization> _permissionList = new List<IPermissionAuthorization>();

        public List<IPermissionAuthorization> Permissions
        {
            get { return _permissionList; }
            set { _permissionList = value; }
        }

        /// <summary>
        ///     Valide si la permissiond de l'utilisateur est dans la liste de permission de l'application
        /// </summary>
        /// <param name="p_lstPermission">Permission a verifier</param>
        /// <returns></returns>
        public bool IsUserAuthorized(List<IPermission> lstPermission)
        {
            foreach (var item in lstPermission)
            {
                foreach (var item2 in Permissions)
                {
                    if (item2.Permission.Code == item.Code)
                        return true;
                }
            }
            return false;
        }
    }
}