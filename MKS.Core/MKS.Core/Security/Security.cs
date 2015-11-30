using MKS.Core.Business.Interfaces;

namespace MKS.Core.Security
{
    /// <summary>
    /// Classe de securite de base pour les business, elle encapsule les permissions en fonction du systeme, du nom du business et l'operation.
    /// </summary>
    /// <typeparam name="TBusiness"></typeparam>
    public class Security<TBusiness> : SecurityCRUDE
        where TBusiness : IBusinessOperations
    {
        public Security()
            : base(Globals.GetUserEnvironment.GetCurrentSystemCode(), typeof(TBusiness).Name)
        { }
    }
    /// <summary>
    /// Classe de base pour definir la sécurité sur les fonctions du CRUDE
    /// </summary>
    /// <typeparam name="TBusiness">Object metier a traiter</typeparam>
    /// <typeparam name="TSecurityAdapter">Lien avec se systeme de securité</typeparam>
    public class Security<TBusiness, TSecurityAdapter> : SecurityCRUDE, ISecurityAdapter
        where TBusiness : IBusinessOperations
        where TSecurityAdapter : ISecurityAdapter, new()
    {
        public Security()
            : base(Globals.GetUserEnvironment.GetCurrentSystemCode(), typeof(TBusiness).Name)
        { }

        public IApplicationAuthorization GetApplicationAuthorization(string userCode, string applicationCode)
        {
            TSecurityAdapter s = new TSecurityAdapter();
            return s.GetApplicationAuthorization(userCode, applicationCode);
        }
    }
}