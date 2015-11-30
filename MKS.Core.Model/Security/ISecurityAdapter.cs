namespace MKS.Core.Security
{
    public interface ISecurityAdapter
    {
        /// <summary>
        ///     Cette fonction est appele par le Framework lors de la verification de la securite
        /// </summary>
        /// <param name="userCode">Code de l'utilisateur</param>
        /// <param name="applicationCode">Code de l'application</param>
        /// <returns></returns>
        IApplicationAuthorization GetApplicationAuthorization(string userCode, string applicationCode);
    }
}