using MKS.Core.Model;
using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Globalization;
using System.Security.Principal;
using System.Text;

namespace MKS.Core
{
    public static class Globals
    {
        public static UserEnvironment GetUserEnvironment 
        { 
            get; 
            set; 
        }
        
    }
    /// <summary>
    /// Classe globale qui permet de donner acces a des informations de base sur l'identification des systemes et utilisateurs.
    /// </summary>
    public class UserEnvironment : IUserEnvironment
    {
        private IUserEnvironment _userenvironnement;
        /// <summary>
        /// Permet de passer un objet de UserEnvironment pour être pris en compte par le framework.
        /// </summary>
        /// <param name="userenvironnement">Objet de userenvironnement pré initialisé</param>
        /// <remarks>Lors d'une communication WCF et REST il faut passer un objet d'environnement car les contextes de lecture des identités ne sont pas les mêmes</remarks>
        public void Initialise(IUserEnvironment userenvironnement)
        {
            _userenvironnement=userenvironnement;
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("GetClientCulture:{0} , ", _userenvironnement.GetClientCulture());
            sb.AppendFormat("GetClientImpersonatedUserCode:{0} , ", _userenvironnement.GetClientImpersonatedUserCode());
            sb.AppendFormat("GetClientMachineName:{0} , ", _userenvironnement.GetClientMachineName());
            sb.AppendFormat("GetClientSystemCode:{0} , ", _userenvironnement.GetClientSystemCode());
            sb.AppendFormat("GetClientUICulture:{0} , ", _userenvironnement.GetClientUICulture());
            sb.AppendFormat("GetClientUserCode:{0} , ", _userenvironnement.GetClientUserCode());
            sb.AppendFormat("GetClientUserCodeWithoutDomain:{0} , ", _userenvironnement.GetClientUserCodeWithoutDomain());
            sb.AppendFormat("GetClientUserDomainName:{0} , ", _userenvironnement.GetClientUserDomainName());
            sb.AppendFormat("GetClientUserSessionID:{0} , ", _userenvironnement.GetClientUserSessionID());
            sb.AppendFormat("GetContractName:{0} , ", _userenvironnement.GetContractName());
            sb.AppendFormat("GetCulture:{0} , ", _userenvironnement.GetCulture());
            sb.AppendFormat("GetCurrentCulture:{0} , ", _userenvironnement.GetCurrentCulture());
            sb.AppendFormat("GetCurrentImpersonatedUserCode:{0} , ", _userenvironnement.GetCurrentImpersonatedUserCode());
            sb.AppendFormat("GetCurrentMachineName:{0} , ", _userenvironnement.GetCurrentMachineName());
            sb.AppendFormat("GetCurrentSystemCode:{0} , ", _userenvironnement.GetCurrentSystemCode());
            sb.AppendFormat("GetCurrentUICulture:{0} , ", _userenvironnement.GetCurrentUICulture());
            sb.AppendFormat("GetCurrentUserCode:{0} , ", _userenvironnement.GetCurrentUserCode());
            sb.AppendFormat("GetCurrentUserCodeWithoutDomain:{0} , ", _userenvironnement.GetCurrentUserCodeWithoutDomain());
            sb.AppendFormat("GetCurrentUserDomainName:{0} , ", _userenvironnement.GetCurrentUserDomainName());
            sb.AppendFormat("GetCurrentUserSessionID:{0} , ", _userenvironnement.GetCurrentUserSessionID());
            sb.AppendFormat("GetImpersonatedUserCode:{0} , ", _userenvironnement.GetImpersonatedUserCode());
            sb.AppendFormat("GetMachineName:{0} , ", _userenvironnement.GetMachineName());            
            sb.AppendFormat("GetSystemCode:{0} , ", _userenvironnement.GetSystemCode());
            sb.AppendFormat("GetUICulture:{0} , ", _userenvironnement.GetUICulture());
            sb.AppendFormat("GetUserCode:{0} , ", _userenvironnement.GetUserCode());
            sb.AppendFormat("GetUserCodeWithoutDomain:{0} , ", _userenvironnement.GetUserCodeWithoutDomain());
            sb.AppendFormat("GetUserDomainName:{0} , ", _userenvironnement.GetUserDomainName());
            sb.AppendFormat("GetUserSessionID:{0} , ", _userenvironnement.GetUserSessionID());
            sb.AppendFormat("IsDebugModeActivated:{0}", _userenvironnement.IsDebugModeActivated());
            return sb.ToString();


        }
        /// <summary>
        /// Code du systeme. La base du framework lit ces informations lors de traitements.
        /// </summary>
        /// <returns>Si l'le code passé à l'initialisation est null, le code est lu à partir de la balise 
        /// du fichier de configuration de l'application "CodeSysteme"
        /// </returns>
        public string GetSystemCode()
        {
            var sysCode= _userenvironnement.GetSystemCode();
            if (sysCode == null)
                sysCode = ConfigurationManager.AppSettings["CodeSysteme"];
            return sysCode;
        }

        public string GetSystemCode(bool allowEmptyCode)
        {            
            var sysCode = _userenvironnement.GetSystemCode(allowEmptyCode);
            if (sysCode == null)
                sysCode = ConfigurationManager.AppSettings["CodeSysteme"];
            return sysCode;
        }

        public string GetClientSystemCode()
        {            
            var sysCode = _userenvironnement.GetClientSystemCode();
            if (sysCode == null)
                sysCode = ConfigurationManager.AppSettings["CodeSysteme"];
            return sysCode;
        }

        public string GetCurrentSystemCode(bool allowEmptyCode)
        {
            
            var sysCode = _userenvironnement.GetCurrentSystemCode(allowEmptyCode);
            if (sysCode == null)
                sysCode = ConfigurationManager.AppSettings["CodeSysteme"];
            return sysCode;
        }

        public string GetCurrentSystemCode()
        {
            var sysCode = _userenvironnement.GetCurrentSystemCode();
            if (sysCode == null)
                sysCode= ConfigurationManager.AppSettings["CodeSysteme"];
            return sysCode;
        }

        public string GetUserCode()
        {
            var userCode = _userenvironnement.GetUserCode();
            if (userCode==null)
                userCode = Environment.UserName;
            return userCode;
        }

        public string GetClientUserCode()
        {
            var userCode= _userenvironnement.GetClientUserCode();
            if (userCode == null)
                userCode = Environment.UserName;
            return userCode;
        }

        public string GetCurrentUserCode()
        {
            
            var userCode = _userenvironnement.GetCurrentUserCode();
            if (userCode == null)
                userCode = Environment.UserName;
            return userCode;
        }

        /// <summary>
        ///   Permet d'obtenir le code de l'utilisateur sans le nom de domaine en préfixe
        /// </summary>
        /// <returns> Le code de l'utilisateur sans préfixe </returns>
        public string GetUserCodeWithoutDomain()
        {
            if (_userenvironnement.GetUserCodeWithoutDomain() != null)
                return GetClientUserCodeWithoutDomain();
            else
                return GetCurrentUserCodeWithoutDomain();
        }

        /// <summary>
        ///   Permet d'obtenir le code de l'utilisateur sans le nom de domaine en préfixe
        /// </summary>
        /// <returns> Le code de l'utilisateur sans préfixe </returns>
        public string GetClientUserCodeWithoutDomain()
        {
            return GetClientUserCode().Substring(GetClientUserCode().LastIndexOf("\\", StringComparison.Ordinal) + 1);
        }

        /// <summary>
        ///   Permet d'obtenir le code de l'utilisateur sans le nom de domaine en préfixe
        /// </summary>
        /// <returns> Le code de l'utilisateur sans préfixe </returns>
        public string GetCurrentUserCodeWithoutDomain()
        {
            return GetCurrentUserCode().Substring(GetCurrentUserCode().LastIndexOf("\\", StringComparison.Ordinal) + 1);
        }

        public string GetUserDomainName()
        {
            var dom =  _userenvironnement.GetUserDomainName();
            if (dom == null)
                dom = Environment.UserDomainName;
            return dom;
        }

        public string GetClientUserDomainName()
        {
            var dom= _userenvironnement.GetClientUserDomainName();
            if (dom == null)
                dom = Environment.UserDomainName;
            return dom;
        }

        public string GetCurrentUserDomainName()
        {
            var dom= _userenvironnement.GetCurrentUserDomainName();
            if (dom == null)
                dom = Environment.UserDomainName;
            return dom;
        }

        public string GetImpersonatedUserCode()
        {
            var iden= _userenvironnement.GetImpersonatedUserCode();
            if (iden == null)
                return GetCurrentImpersonatedUserCode();
            else
                return iden;
        }

        public string GetClientImpersonatedUserCode()
        {
            var iden= _userenvironnement.GetClientImpersonatedUserCode();
            if (iden == null)
                return GetCurrentImpersonatedUserCode();
            else
                return iden;
        }

        public string GetCurrentImpersonatedUserCode()
        {
            var ident= _userenvironnement.GetCurrentImpersonatedUserCode();
            if (ident == null)
            {
                WindowsIdentity windowsIdentity = WindowsIdentity.GetCurrent();
                return windowsIdentity != null ? windowsIdentity.Name : null;
            }
            else
            {
                return ident;
            }
        }

        public string GetMachineName()
        {
            var mach= _userenvironnement.GetMachineName();
            if (mach == null)
                mach = GetCurrentMachineName();
            return mach;
        }

        public string GetClientMachineName()
        {
            var mach= _userenvironnement.GetClientMachineName();
            if (mach == null)
                mach=GetCurrentMachineName();
            return mach;
        }

        public string GetCurrentMachineName()
        {
            var mach= _userenvironnement.GetCurrentMachineName();
            if(mach==null)
                mach = Environment.MachineName;
            return mach;
                

        }

        public string GetUserSessionID()
        {
            var user= _userenvironnement.GetUserSessionID();
            if (user == null)
                user = GetCurrentUserSessionID();
            return user;
        }

        public string GetClientUserSessionID()
        {
            var user= _userenvironnement.GetClientUserSessionID();
            if (user == null)
                user = GetCurrentUserSessionID();
            return user;
        }

        public string GetCurrentUserSessionID()
        {
            var user= _userenvironnement.GetCurrentUserSessionID();
            if(user==null)
                user=Environment.MachineName + "_" + System.Diagnostics.Process.GetCurrentProcess().Id.ToString(CultureInfo.InvariantCulture);
            return user;
        }

        public string GetCulture()
        {
            var cul= _userenvironnement.GetCulture();
            if (cul == null)
                cul = CultureInfo.CurrentCulture.Name;
            return cul;
        }

        public string GetClientCulture()
        {
            var cul= _userenvironnement.GetClientCulture();
            if (cul == null)
                cul = CultureInfo.CurrentCulture.Name;
            return cul;
        }

        public string GetCurrentCulture()
        {
            var cul= _userenvironnement.GetCurrentCulture();
            if (cul==null)
                cul = CultureInfo.CurrentCulture.Name;
            return cul;

        }

        public string GetUICulture()
        {
            var cului= _userenvironnement.GetUICulture();
            if (cului == null)
                cului = GetCurrentUICulture();
            return cului;
        }

        public string GetClientUICulture()
        {
            var cului= _userenvironnement.GetClientUICulture();
            if (cului == null)
                cului = GetCurrentUICulture();
            return cului;

        }

        public string GetCurrentUICulture()
        {
            var cului= _userenvironnement.GetCurrentUICulture();
            if (cului == null)
                cului = CultureInfo.CurrentUICulture.Name;
            return cului;
        }

        public bool IsDebugModeActivated()
        {
            var deb= _userenvironnement.IsDebugModeActivated();
            if(deb==null)
                deb = Utilities.IsDebugModeActivated();
            return deb;
        }

        public string GetContractName()
        {
            return _userenvironnement.GetContractName();
        }

        public string GetImpersonateSystemCode()
        {
            return _userenvironnement.GetImpersonateSystemCode();
        }
        public StringDictionary GetStringDictionaryHeader()
        {
            return _userenvironnement.GetStringDictionaryHeader();
        }


    }
}
