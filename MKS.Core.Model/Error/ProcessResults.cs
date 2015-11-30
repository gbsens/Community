using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace MKS.Core
{
    /// <summary>
    ///     La classe ProcessResults est utilisée pour encapsuler les exceptions de
    ///     validation d'intégrité et d'affaires. L'information du ProcessResults est
    ///     accessible directement dans la classe qui hérite du Business.
    ///     <para>
    ///         Cette classe est utile lorsque l'on veut gérer les différents types d'erreur lors de l'exécution d'un
    ///         Business.
    ///     </para>
    /// </summary>
    /// <remarks>
    ///     Cette classe est utilisée par le Business pour conserver les règles de
    ///     validation qui se sont avérées invalides lors de la validation. Ces règles sont
    ///     lancées en exception automatiquement lorsqu'elles ont une sévérité &quot;Error&quot;.
    ///     <para> Cette classe est utilisée comme générique pour l'envoi du FautlException. </para>
    /// </remarks>
    [DataContract]
    [KnownType(typeof (Severity))]
    [KnownType(typeof (ReturnMessage))]
    [KnownType(typeof (TypeError))]
    public class ProcessResults
    {
        [DataMember(Name = "_messageList")] private List<ReturnMessage> _messageList = new List<ReturnMessage>();

        /// <summary>
        ///     Constructeur
        /// </summary>
        public ProcessResults()
        {
        }

        /// <summary>
        ///     Constructeur de la classe. Ajoute automatiquement un message de type Exception et de sévérité "Erreur"
        ///     à la liste.
        /// </summary>
        /// <param name="codeMessage"> Code du message </param>
        /// <param name="isCodeMessageFromFrameWork"> Booléen indiquant si le message provient de la fondation </param>
        public ProcessResults(string codeMessage, bool isCodeMessageFromFrameWork)
        {
            _messageList.Add(new ReturnMessage(TypeError.Exception, Severity.Error, codeMessage, codeMessage,
                isCodeMessageFromFrameWork));
        }

        /// <summary>
        ///     Constructeur de la classe. Ajoute automatiquement un message du type souhaité et de sévérité "Erreur"
        ///     à la liste.
        /// </summary>
        /// <param name="typeErreur"> Type de l'erreur </param>
        /// <param name="codeMessage"> Code du message de l'erreur </param>
        /// <param name="description"> Informations de l'exception </param>
        /// <param name="isCodeMessageFromFrameWork"> Booléen indiquant si le message provient du framework </param>
        public ProcessResults(TypeError typeErreur, string codeMessage, string description,
            bool isCodeMessageFromFrameWork)
        {
            _messageList.Add(new ReturnMessage(typeErreur, Severity.Error, codeMessage, description,
                isCodeMessageFromFrameWork));
        }

        /// <summary>
        ///     Constructeur de la classe. Ajoute automatiquement un message du type souhaité et de sévérité souhaitée
        ///     à la liste.
        /// </summary>
        /// <param name="typeErreur"> Type de l'erreur </param>
        /// <param name="severity"> Sévérité de l'erreur </param>
        /// <param name="codeMessage"> Code du message de l'erreur </param>
        /// <param name="description"> Informations de l'erreur </param>
        /// <param name="isCodeMessageFromFrameWork"> Booléen indiquant si le message provient du framework </param>
        public ProcessResults(TypeError typeErreur, Severity severity, string codeMessage, string description,
            bool isCodeMessageFromFrameWork)
        {
            _messageList.Add(new ReturnMessage(typeErreur, severity, codeMessage, description,
                isCodeMessageFromFrameWork));
        }

        /// <summary>
        ///     Constructeur de la classe. Ajoute automatiquement un message du type souhaité et de sévérité souhaité
        ///     à la liste.
        /// </summary>
        /// <param name="typeErreur"> Type de l'erreur </param>
        /// <param name="severity"> Sévérité de l'erreur </param>
        /// <param name="codeMessage"> Code du message de l'erreur </param>
        /// <param name="description"> Informations de l'exception </param>
        /// <param name="isCodeMessageFromFrameWork"> Booléen indiquant si le message provient du framework </param>
        /// <param name="parametres"> Tableau (array) de paramètres reliés à l'exception </param>
        public ProcessResults(TypeError typeErreur, Severity severity, string codeMessage,
            string description, bool isCodeMessageFromFrameWork, params string[] parametres)
        {
            _messageList.Add(new ReturnMessage(typeErreur, severity, codeMessage, description,
                isCodeMessageFromFrameWork, parametres));
        }

        /// <summary>
        ///     Constructeur de la classe. Ajoute automatiquement un message du type souhaité et de sévérité souhaité
        ///     à la liste.
        /// </summary>
        /// <param name="typeErreur"> Type de l'erreur </param>
        /// <param name="severity"> Sévérité de l'erreur </param>
        /// <param name="codeMessage"> Code du message de l'erreur </param>
        /// <param name="description"> Informations de l'exception </param>
        /// <param name="isCodeMessageFromFrameWork"> Booléen indiquant si le message provient du framework </param>
        /// <param name="parametres"> Liste de paramètres reliés à l'exception </param>
        public ProcessResults(TypeError typeErreur, Severity severity, string codeMessage,
            string description, bool isCodeMessageFromFrameWork, IEnumerable<string> parametres)
        {
            _messageList.Add(new ReturnMessage(typeErreur, severity, codeMessage, description,
                isCodeMessageFromFrameWork, parametres));
        }

        /// <summary>
        ///     Retourne la liste des exceptions
        /// </summary>
        /// <returns> Liste d'exceptions au format ReturnMessage </returns>
        public List<ReturnMessage> MessagesList
        {
            get { return _messageList; }
        }

        /// <summary>
        ///     Indique si la liste des exceptions ou vide ou non
        /// </summary>
        /// <returns> True si la liste est vide, false dans le cas contraire </returns>
        public bool IsEmpty
        {
            get { return _messageList.Count <= 0; }
        }

        /// <summary>
        ///     Ajout d'un exception ou d'un message de retour
        /// </summary>
        /// <param name="message"> Message qui doit etre ajouté à l'exception </param>
        public void AddException(ReturnMessage message)
        {
            _messageList.Add(message);
        }

        /// <summary>
        ///     Ajout de la liste de messages d'une autre exception à la liste de celle-ci.
        /// </summary>
        /// <param name="processResults"> Exception pour laquelle on va ajouter tous ses messages </param>
        public void AddProcessResultsToList(ProcessResults processResults)
        {
            _messageList.AddRange(processResults.MessagesList);
        }

        /// <summary>
        ///     Converti en string l'information sur les exceptions qui est encapsulé dans cette classe
        /// </summary>
        /// <returns> String contenant l'information sur les exceptions qui est encapsulé dans cette classe </returns>
        public override string ToString()
        {
            var sb = new StringBuilder();

            foreach (var x in _messageList)
            {
                sb.Append(x);
            }
            return sb.ToString();
        }
    }
}