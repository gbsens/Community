using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace MKS.Core.Security
{
    /// <summary>
    ///     Classe contenant les informations de sécurité reliées à un Business.
    /// </summary>
    [ServiceContract]
    public class SecurityInfo
    {
        private readonly List<IPermission> lstPermissionAdd = new List<IPermission>();
        private readonly List<IPermission> lstPermissionDelete = new List<IPermission>();
        private readonly List<IPermission> lstPermissionExecute = new List<IPermission>();
        private readonly List<IPermission> lstPermissionSelect = new List<IPermission>();
        private readonly List<IPermission> lstPermissionUpdate = new List<IPermission>();
        private readonly List<IPermission> lstPermissionEdit = new List<IPermission>();

        /// <summary>
        ///     Liste des permissions pour l'action Execute.
        /// </summary>
        [DataMember(Name = "PermissionExecute")]
        public List<IPermission> PermissionExecute
        {
            get { return (lstPermissionExecute); }
        }

        /// <summary>
        ///     Liste des permissions pour l'action Add.
        /// </summary>
        [DataMember(Name = "PermissionAdd")]
        public List<IPermission> PermissionAdd
        {
            get { return (lstPermissionAdd); }
        }

        /// <summary>
        ///     Liste des permission pour l'action Update.
        /// </summary>
        [DataMember(Name = "PermissionUpdate")]
        public List<IPermission> PermissionUpdate
        {
            get { return (lstPermissionUpdate); }
        }

        /// <summary>
        ///     Liste des permission pour l'action Selected.
        /// </summary>
        [DataMember(Name = "PermissionSelect")]
        public List<IPermission> PermissionSelect
        {
            get { return (lstPermissionSelect); }
        }

        /// <summary>
        ///     Liste des permission pour l'action Delete.
        /// </summary>
        [DataMember(Name = "PermissionDelete")]
        public List<IPermission> PermissionDelete
        {
            get { return (lstPermissionDelete); }
        }
        /// <summary>
        /// Liste des permissions de l'action Edit
        /// </summary>
        [DataMember(Name = "PermissionEdit")]
        public List<IPermission> PermissionEdit
        {
            get { return (lstPermissionEdit); }
        }
        /// <summary>
        ///     Liste des permissions.
        /// </summary>
        public List<IPermission> PermissionList()
        {
            var pl = new List<IPermission>();
            pl = pl.Union(lstPermissionAdd).ToList();
            pl = pl.Union(lstPermissionUpdate).ToList();
            pl = pl.Union(lstPermissionSelect).ToList();
            pl = pl.Union(lstPermissionDelete).ToList();
            pl = pl.Union(lstPermissionExecute).ToList();
            pl = pl.Union(lstPermissionEdit).ToList();
            return (pl);
        }
    }
}