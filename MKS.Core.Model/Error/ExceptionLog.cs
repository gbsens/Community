
using System;
using System.Reflection;
using MKS.Core.Model.Ressources;


namespace MKS.Core.Model
{
    /// <summary>
    ///   Cette classe permet de créer une exception Applicative loguée dans le journal des évènements.
    ///   Les messages ne sont pas destinés aux utilisateurs, mais plutôt au programmeur.
    ///   Chacun des messages est structuré par classe d'objet
    ///   Dans une architecture orientée service cette exception ne devrait jamais etre affichée.
    /// </summary>
    public sealed class ExceptionLog : ApplicationException
    {
        #region " Attributs privés "

        private string _clientMachineName;
        private string _clientSystemCode;

        #endregion " Attributs privés "

        #region " Propriétées publiques "

        /// <summary>
        ///   Code système du client
        /// </summary>
        public string ClientSystemCode
        {
            get { return _clientSystemCode; }
        }

        /// <summary>
        ///   Nom de la machine du client
        /// </summary>
        public string ClientMachineName
        {
            get { return _clientMachineName; }
        }

        #endregion " Propriétées publiques "

        #region " Méthodes privées "

        /// <summary>
        ///   Initialise les variables privées
        /// </summary>
        private void Initialize(IUserEnvironment UserEnvironment)
        {
            try
            {
                _clientSystemCode = UserEnvironment.GetCurrentSystemCode();
            }
            catch
            {
                _clientSystemCode = CoreRessources.EX0008;
            }

            try
            {
                _clientMachineName = UserEnvironment.GetMachineName();
            }
            catch
            {
                _clientMachineName = CoreRessources.EX0010;
            }
        }

        #endregion " Méthodes privées "

        #region " Constructeurs publiques "

        /// <summary>
        ///   Crée une exception applicative qui sera publiée dans le journal des évènements MKS.
        /// </summary>
        /// <param name="p_message"> Message à Inscrire sur dans l'exception </param>
        public ExceptionLog(string p_message, IUserEnvironment UserEnvironment)
            : base(p_message)
        {
            Initialize(UserEnvironment);
            Source = Assembly.GetCallingAssembly().FullName;
        }

        /// <summary>
        ///   Crée une exception applicative qui sera publiée dans le journal des évènements MKS.
        /// </summary>
        /// <param name="p_exception"> Exception à partir de laquelle l'exception sera créé </param>
        public ExceptionLog(Exception p_exception, IUserEnvironment UserEnvironment)
            : base(p_exception.Message, p_exception)
        {
            Initialize(UserEnvironment);
            Source = string.IsNullOrEmpty(p_exception.Source)
                         ? Assembly.GetCallingAssembly().FullName
                         : p_exception.Source;
        }

        /// <summary>
        ///   Crée une exception applicative qui sera publiée dans le journal des évènements MKS.
        /// </summary>
        /// <param name="p_exception"> Exception à partir de laquelle l'exception sera créé </param>
        /// <param name="_message"> Message à Inscrire sur dans l'exception </param>
        public ExceptionLog(string _message, Exception p_exception,IUserEnvironment UserEnvironment)
            : base(_message, p_exception)
        {
            Initialize(UserEnvironment);
            Source = string.IsNullOrEmpty(p_exception.Source)
                         ? Assembly.GetCallingAssembly().FullName
                         : p_exception.Source;
        }

        #endregion " Constructeurs publiques "
    }
}