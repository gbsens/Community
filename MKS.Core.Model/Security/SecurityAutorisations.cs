using System.Collections.Generic;

namespace MKS.Core.Security
{
    /// <summary>
    ///     Cette classe dois etre surchargé afin de la connecter avec un systeme d'authorisation.
    /// </summary>
    public class SecurityAutorisations : ISecurityAdapter
    {
        /// <summary>
        ///     Retourne l'ensemble des authorisation d'une application
        /// </summary>
        /// <param name="userCode">Identification du demandeur d'authorisation (pour les appel aux service distant)</param>
        /// <param name="applicationCode">Identification de l'application demande pour les authorisations</param>
        /// <returns>
        ///     Les authorisation de l'application en fonction du code de l'application et selon l'authorisation du code du
        ///     demandeur.
        /// </returns>
        public virtual IApplicationAuthorization GetApplicationAuthorization(string userCode, string applicationCode)
        {
            var auth = new ApplicationAuthorisation();
            return auth;
        }
    }

    /// <summary>
    ///     Cette classe contien l'ensemble des permissions d'une application
    /// </summary>
    public class ApplicationAuthorisation : IApplicationAuthorization
    {
        private List<IPermissionAuthorization> _Permissions = new List<IPermissionAuthorization>();
        //Liste des permissions de l'ensemble de l'application
        public List<IPermissionAuthorization> Permissions
        {
            get { return _Permissions; }

            set { _Permissions = value; }
        }

        /// <summary>
        ///     Verifie si l'utilisateur possede la permission en fonction des persmission de l'application
        /// </summary>
        /// <param name="p_lstPermission">Liste des permisssions de l'utilisateur</param>
        /// <returns></returns>
        public bool IsUserAuthorized(List<IPermission> p_lstPermission)
        {
            foreach (var item in _Permissions)
            {
                foreach (var item2 in p_lstPermission)
                {
                    //Compare les permission
                    if (item.Permission.Code == item2.Code)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }

    /// <summary>
    ///     Classe de permission de l'utilisateur
    ///     Elle indique si l'utilisateur a acces ou non
    /// </summary>
    public class PermissionAuthorisation : IPermissionAuthorization
    {
        private bool _IsUserAuthorized = true;
        private IPermission _Permission = new Permission();

        public PermissionAuthorisation(IPermission permission)
        {
            Permission = permission;
        }

        public bool IsUserAuthorized
        {
            get { return _IsUserAuthorized; }
            set { _IsUserAuthorized = value; }
        }

        public IPermission Permission
        {
            get { return _Permission; }
            set { _Permission = value; }
        }
    }

    /// <summary>
    ///     Objet d'une permission doit etre unique pour l'ensemble des systemes
    /// </summary>
    public class Permission : IPermission
    {
        public Permission(string code, string description, int? id)
        {
            Code = code;
            Description = description;
            Id = id;
        }

        public Permission()
        {
        }

        public string Code { get; set; }

        public string Description { get; set; }

        public int? Id { get; set; }
    }
}