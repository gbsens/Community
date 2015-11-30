namespace MKS.Core.Security
{
    /// <summary>
    ///     Cette classe assigne les permissions par defaut en fonction du code de l'application et la fonction du systeme
    /// </summary>
    public abstract class SecurityCRUDE : ISecurityPermission
    {
        private readonly string _applicationcode = string.Empty;
        private readonly string _business = string.Empty;
        public bool USE_ADD = true;
        public bool USE_DELETE = true;
        public bool USE_EXECUTE = true;
        public bool USE_SELECT = true;
        public bool USE_UPDATE = true;
        public bool USE_EDIT = true;

        /// <summary>
        ///     Constructeur pour assigner le code application et la function
        /// </summary>
        /// <param name="applicationCode">Represente le code du systeme inscrit dans le fichier de configuration</param>
        /// <param name="business">Represente la fonction sur laquel on application les permissions</param>
        public  SecurityCRUDE(string applicationCode, string business)
        {
            _applicationcode = applicationCode;
            _business = business;
        }

        //Permission de base pour les opérations du CRUDE
        public virtual SecurityInfo SecurityPermissions
        {
            get
            {
                var s = new SecurityInfo();
                if (USE_ADD)
                    s.PermissionAdd.Add(new Permission(_applicationcode + _business + "_ADD", _business, null));
                if (USE_DELETE)
                    s.PermissionDelete.Add(new Permission(_applicationcode + _business + "_DELETE", _business, null));
                if (USE_EXECUTE)
                    s.PermissionExecute.Add(new Permission(_applicationcode + _business + "_EXECUTE", _business, null));
                if (USE_SELECT)
                    s.PermissionSelect.Add(new Permission(_applicationcode + _business + "_SELECT", _business, null));
                if (USE_UPDATE)
                    s.PermissionUpdate.Add(new Permission(_applicationcode + _business + "_UPDATE", _business, null));
                if (USE_EDIT)
                    s.PermissionUpdate.Add(new Permission(_applicationcode + _business + "_EDIT", _business, null));
                return s;
            }
        }
    }
}