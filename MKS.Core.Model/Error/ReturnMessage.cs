using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace MKS.Core
{
    /// <summary>
    ///     Classe qui permet d'encapsuler les informations sur une exception de validation
    ///     d'intégrité, d'affaire ou de sécurité.
    /// </summary>
    /// <remarks>
    ///     Cette classe est principalement utilisée pour transporter la description des
    ///     exceptions à l'aide de la classe ProcessResults. Cette dernière contient une
    ///     liste de ReturnMessage contenant les exceptions de validation et de sécurité.
    ///     Les exceptions .NET ne seront jamais incluses dans cette classe.
    /// </remarks>
    [DataContract]
    public class ReturnMessage
    {
        [DataMember(Name = "_codeMessage")] private string _codeMessage = string.Empty;

        [DataMember(Name = "_description")] private string _description;

        [DataMember(Name = "_isCodeMessageFromFrameWork")] private bool _isCodeMessageFromFrameWork;

        [DataMember(Name = "_parametersList")] private List<string> _parametersList = new List<string>();

        [DataMember(Name = "_severity")] private Severity _severity;

        /// <summary>
        ///     Type de l'erreur
        /// </summary>
        [DataMember(Name = "_typeErreur")] private TypeError _typeErreur;

        /// <summary>
        ///     Nom de l'objet.
        /// </summary>
        [DataMember(Name = "ObjectName")] public string ObjectName;

        /// <summary>
        ///     Nom de la propriété.
        /// </summary>
        [DataMember(Name = "ObjectPropertyName")] public string ObjectPropertyName;

        [DataMember(Name = "Rule")] public Rule RuleReference;

        /// <summary>
        ///     Constructeur de l'objet ReturnMessage
        /// </summary>
        public ReturnMessage()
        {
            _isCodeMessageFromFrameWork = false;
        }

        /// <summary>
        ///     Constructeur de l'objet ReturnMessage
        /// </summary>
        /// <param name="typeErreur"> Type de l'erreur </param>
        /// <param name="severity"> Sévérité de l'erreur </param>
        /// <param name="description"> Informations de l'erreur </param>
        public ReturnMessage(TypeError typeErreur, Severity severity, string description)
        {
            _typeErreur = typeErreur;
            _description = description;
            _severity = severity;
            _codeMessage = string.Empty;
            _isCodeMessageFromFrameWork = false;
        }

        /// <summary>
        ///     Constructeur de l'objet ReturnMessage
        /// </summary>
        /// <param name="typeErreur"> Type de l'erreur </param>
        /// <param name="severity"> Sévérité de l'erreur </param>
        /// <param name="codeMessage"> Code de message de l'erreur </param>
        /// <param name="description"> Informations de l'erreur </param>
        /// <param name="isCodeMessageFromFrameWork"> Booléen indiquant si l'erreur provient du framework </param>
        public ReturnMessage(TypeError typeErreur, Severity severity, string codeMessage,
            string description, bool isCodeMessageFromFrameWork)
        {
            _typeErreur = typeErreur;
            _description = description;
            _severity = severity;
            _codeMessage = codeMessage;
            _description = description;
            _isCodeMessageFromFrameWork = isCodeMessageFromFrameWork;
        }

        /// <summary>
        ///     Constructeur de l'objet ReturnMessage
        /// </summary>
        /// <param name="typeErreur"> Type de l'erreur </param>
        /// <param name="severity"> Sévérité de l'erreur </param>
        /// <param name="codeMessage"> Code de message de l'erreur </param>
        /// <param name="description"> Informations de l'erreur </param>
        /// <param name="isCodeMessageFromFrameWork"> Booléen indiquant si l'erreur provient du framework </param>
        /// <param name="parameters"> Liste de paramètres qui se rattache à l'erreur </param>
        public ReturnMessage(TypeError typeErreur, Severity severity, string codeMessage,
            string description, bool isCodeMessageFromFrameWork, params string[] parameters)
        {
            _typeErreur = typeErreur;
            _description = description;
            _severity = severity;
            _codeMessage = codeMessage;
            _description = description;
            _isCodeMessageFromFrameWork = isCodeMessageFromFrameWork;
            foreach (var t in parameters)
            {
                _parametersList.Add(t);
            }
        }

        /// <summary>
        ///     Constructeur de l'objet ReturnMessage
        /// </summary>
        /// <param name="typeErreur"> Type de l'erreur </param>
        /// <param name="severity"> Sévérité de l'erreur </param>
        /// <param name="codeMessage"> Code de message de l'erreur </param>
        /// <param name="description"> Informations de l'erreur </param>
        /// <param name="isCodeMessageFromFrameWork"> Booléen indiquant si l'erreur provient du framework </param>
        /// <param name="parametersList"> Liste de paramètres qui se rattache à l'erreur </param>
        public ReturnMessage(TypeError typeErreur, Severity severity, string codeMessage, string description,
            bool isCodeMessageFromFrameWork, IEnumerable<string> parametersList)
        {
            _typeErreur = typeErreur;
            _description = description;
            _severity = severity;
            _codeMessage = codeMessage;
            _description = description;
            _isCodeMessageFromFrameWork = isCodeMessageFromFrameWork;
            foreach (var x in parametersList)
            {
                _parametersList.Add(x);
            }
        }

        [DataMember(Name = "_viewPropertyName")]
        public string ViewPropertyName { get; set; }

        /// <summary>
        ///     Obtient ou définit le type de règle.
        /// </summary>
        [DataMember(Name = "RuleType")]
        public Rule.RuleType RuleType { get; set; }

        /// <summary>
        ///     Liste de paramètres reliés au message
        /// </summary>
        public List<string> ParametersList
        {
            get { return _parametersList; }
            set { _parametersList = value; }
        }

        /// <summary>
        ///     Code du message.  Ce code peut se retrouver dans le fichier ressource du site afin d'offrir un message
        ///     à l'utilisateur.
        /// </summary>
        public string CodeMessage
        {
            get { return (_codeMessage); }
            set { _codeMessage = value; }
        }

        /// <summary>
        ///     Sévérité de l'erreur
        /// </summary>
        public Severity Severity
        {
            get { return _severity; }
            set { _severity = value; }
        }

        /// <summary>
        ///     Valeur booléenne indiquant si le message provient du framework
        /// </summary>
        public bool IsCodeMessageFromFrameWork
        {
            get { return _isCodeMessageFromFrameWork; }
        }

        /// <summary>
        ///     Retourne la description de l'exception.
        /// </summary>
        /// <returns> Chaîne de caractères contenant le texte d'une exception. </returns>
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        /// <summary>
        ///     Retourne le type de l'exception.
        /// </summary>
        /// <returns> Le type de l'exception dans l'enum TypeError </returns>
        public TypeError TypeErrorMessage
        {
            get { return _typeErreur; }
        }

        /// <summary>
        ///     Converti en un string l'information du ReturnMessage.
        /// </summary>
        /// <returns> String contenant l'information du ReturnMessage. </returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendFormat("{0}:{1}:{2}:", _codeMessage, _severity, _typeErreur);
            foreach (var x in _parametersList)
            {
                sb.AppendFormat("[{0}]", x);
            }
            sb.AppendFormat(":{0}", _description);
            return sb.ToString();
        }
    }
}