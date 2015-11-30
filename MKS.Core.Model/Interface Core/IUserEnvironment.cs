using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Specialized;

namespace MKS.Core.Model
{
    public interface IUserEnvironment
    {
        #region SystemCode
        /// <summary>
        ///   Permet d'obtenir le code du système
        /// </summary>
        /// <returns> Code du système </returns>
        string GetSystemCode();
        /// <summary>
        ///   Permet d'obtenir le code du système
        /// </summary>
        /// <param name="allowEmptyCode"> Lorsque ce paramètre est à true, la méthode retourne un string vide si aucun code de système n'est spécifié.
        /// Lorsque le paramètre est à false, une exception est lancée si aucun code n'est trouvé. </param>
        /// <returns> Code du système </returns>
        string GetSystemCode(bool allowEmptyCode);
        /// <summary>
        ///   Permet d'obtenir le code de système de l'appelant.
        /// </summary>
        /// <returns> Retourne le code du système de l'appelant. </returns>
        string GetClientSystemCode();
        /// <summary>
        ///   Permet d'obtenir le code du système
        /// </summary>
        /// <param name="allowEmptyCode"> Lorsque ce paramètre est à true, la méthode retourne un string vide si aucun code de système n'est spécifié.
        /// Lorsque le paramètre est à false, une exception est lancée si aucun code n'est trouvé. </param>
        /// <returns> Code du système </returns>
        string GetCurrentSystemCode(bool allowEmptyCode);
        /// <summary>
        ///   Permet d'obtenir le code du système
        /// </summary>
        /// <returns> Code du système </returns>
        string GetCurrentSystemCode();
        #endregion SystemCode
        #region UserCode

        /// <summary>
        ///   Permet d'obtenir le code de l'utilisateur
        /// </summary>
        /// <returns> Le code de l'utilisateur </returns>
        string GetUserCode();

        /// <summary>
        ///   Permet d'obtenir le code de l'utilisateur
        /// </summary>
        /// <returns> Le code de l'utilisateur </returns>
        string GetClientUserCode();

        /// <summary>
        ///   Permet d'obtenir le code de l'utilisateur
        /// </summary>
        /// <returns> Le code de l'utilisateur </returns>
        string GetCurrentUserCode();

        #endregion UserCode
        #region UserCodeWithoutDomain

        /// <summary>
        ///   Permet d'obtenir le code de l'utilisateur sans le nom de domaine en préfixe
        /// </summary>
        /// <returns> Le code de l'utilisateur sans préfixe </returns>
        string GetUserCodeWithoutDomain();

        /// <summary>
        ///   Permet d'obtenir le code de l'utilisateur sans le nom de domaine en préfixe
        /// </summary>
        /// <returns> Le code de l'utilisateur sans préfixe </returns>
        string GetClientUserCodeWithoutDomain();
        /// <summary>
        ///   Permet d'obtenir le code de l'utilisateur sans le nom de domaine en préfixe
        /// </summary>
        /// <returns> Le code de l'utilisateur sans préfixe </returns>
        string GetCurrentUserCodeWithoutDomain();

        #endregion UserCodeWithoutDomain
        #region UserDomainName

        /// <summary>
        ///   Permet d'obtenir le nom du domaine
        /// </summary>
        /// <returns> Nom du domaine </returns>
        string GetUserDomainName();

        /// <summary>
        ///   Permet d'obtenir le nom du domaine
        /// </summary>
        /// <returns> Nom du domaine </returns>
        string GetClientUserDomainName();

        /// <summary>
        ///   Permet d'obtenir le nom du domaine
        /// </summary>
        /// <returns> Nom du domaine </returns>
        string GetCurrentUserDomainName();
        #endregion UserDomainName
        #region ImpersonatedUserCode

        /// <summary>
        ///   Permet d'obtenir le code de l'usager impersonifié
        /// </summary>
        /// <returns> Le nom de l'usager impersonifié </returns>
        string GetImpersonatedUserCode();
        

        /// <summary>
        ///   Permet d'obtenir le code de l'usager impersonifié
        /// </summary>
        /// <returns> Le nom de l'usager impersonifié </returns>
        string GetClientImpersonatedUserCode();

        /// <summary>
        ///   Permet d'obtenir le code de l'usager impersonifié
        /// </summary>
        /// <returns> Le nom de l'usager impersonifié </returns>
        string GetCurrentImpersonatedUserCode();
        #endregion ImpersonatedUserCode

        #region MachineName

        /// <summary>
        ///   Permet d'obtenir le Machine Name
        /// </summary>
        /// <returns> Machine Name </returns>
        string GetMachineName();

        /// <summary>
        ///   Permet d'obtenir le Machine Name
        /// </summary>
        /// <returns> Machine Name </returns>
        string GetClientMachineName();
        /// <summary>
        ///   Permet d'obtenir le Machine Name
        /// </summary>
        /// <returns> Machine Name </returns>
        string GetCurrentMachineName();
        

        #endregion MachineName

        #region UserSessionID

        /// <summary>
        ///   Permet d'obtenir le Session ID
        /// </summary>
        /// <returns> Session ID </returns>
        string GetUserSessionID();
        /// <summary>
        ///   Permet d'obtenir le Session ID
        /// </summary>
        /// <remarks>
        ///   Si l'usager n'est pas dans un contexte Web ou encore n'est pas passé dans l'entête de l'appel Proxy, le SessionID sera généré à partir du
        ///   system.Diagnostics.Process.GetCurrentProcess().Id
        /// </remarks>
        /// <returns> Session ID </returns>
        string GetClientUserSessionID();
        /// <summary>
        ///   Permet d'obtenir le Session ID
        /// </summary>
        /// <returns> Session ID </returns>
        string GetCurrentUserSessionID();

        #endregion UserSessionID

        #region Culture

        /// <summary>
        ///   Donne le culture info de l'appelant
        /// </summary>
        /// <returns> le nom de la culture info </returns>
        string GetCulture();

        /// <summary>
        ///   Donne le culture info de l'appelant
        /// </summary>
        /// <returns> le nom de la culture info </returns>
        string GetClientCulture();
        /// <summary>
        ///   Donne le culture info de l'appelant
        /// </summary>
        /// <returns> le nom de la culture info </returns>
        string GetCurrentCulture();

        #endregion Culture

        #region UICulture

        /// <summary>
        ///   Donne le culture info du service en cours
        /// </summary>
        /// <returns> e nom de la culture info </returns>
        string GetUICulture();

        /// <summary>
        ///   Donne le culture info du service en cours
        /// </summary>
        /// <returns> e nom de la culture info </returns>
        string GetClientUICulture();
        /// <summary>
        ///   Donne le culture info du service en cours
        /// </summary>
        /// <returns> e nom de la culture info </returns>
        string GetCurrentUICulture();

        #endregion UICulture

        #region DebugMode

        /// <summary>
        /// Permet de savoir si le DebugMode a été activé. Ce mode, tel que son nom l'indique, est utilisé pour le déboggage
        /// des applications bâties avec le Framework 5 et plus. Il désactive le caching et écrit dans le journal des événements
        /// de Windows les urls (services) qui sont appelés.
        /// </summary>
        /// <returns>Si l'appelant (dans le cas d'un appel service) ou l'application elle-même a spécifié dans son fichier de configuration,
        /// dans la balise AppSettings la clé DebugMode=True, la fonction va retourner True. Dans le cas contraire ce sera False.</returns>
        bool IsDebugModeActivated();

        #endregion DebugMode

        #region ContractName

        string GetContractName();
        #endregion ContractName

        #region ImpersonateSystemCode

        /// <summary>
        ///   Permet d'obtenir le code de système impersonifié.
        /// </summary>
        /// <returns> Retourne le code de système impersonifié si on est dans un appel service, sinon c'est String.Empty </returns>
        string GetImpersonateSystemCode();

        #endregion ImpersonateSystemCode

        /// <summary>
        ///   Permet d'obtenir la valeur en fonction du header
        /// </summary>
        /// <returns> La valeur spécifiée par le header </returns>
        StringDictionary GetStringDictionaryHeader();


    }
}
