using System;
using System.Runtime.Serialization;
using System.Text;

namespace MKS.Core
{
    /// <summary>
    ///     Classe de résultat d'une validation
    /// </summary>
    [DataContract]
    [KnownType(typeof (Type))]
    [KnownType(typeof (Rule))]
    public class RuleResult
    {
        /// <summary>
        ///     Constructeur.
        /// </summary>
        public RuleResult()
        {
            RuleInformation = null;
            TypeItem = null;
            Property = "";
        }

        /// <summary>
        ///     Constructeur.
        /// </summary>
        /// <param name="rule"> Règle dont on créé le résultat </param>
        /// <param name="item"> Type de l'objet de règle </param>
        /// <param name="property"> Propriété le l'objet ciblée </param>
        public RuleResult(Rule rule, string item, string property)
        {
            RuleInformation = rule;
            TypeItem = item;
            Property = property;
        }

        /// <summary>
        ///     Constructeur.
        /// </summary>
        /// <param name="rule"> Règle dont on créé le résultat </param>
        /// <param name="item"> Type de l'objet de règle </param>
        /// <param name="property"> Propriété le l'objet ciblée </param>
        /// <param name="objectName"> Nom du type de l'objet ciblée </param>
        public RuleResult(Rule rule, string item, string property, string objectName)
        {
            RuleInformation = rule;
            TypeItem = item;
            Property = property;
            ObjectName = objectName;
            BindObjectName = objectName;
            BindPropertytName = property;
        }

        /// <summary>
        ///     Constructeur.
        /// </summary>
        /// <param name="rule"> Règle dont on créé le résultat </param>
        /// <param name="item"> Type de l'objet de règle </param>
        /// <param name="property"> Propriété le l'objet ciblée </param>
        /// <param name="objectName"> Nom du type de l'objet ciblée </param>
        /// <param name="bindObjectName"> Nom de l'objet ciblée associé par la règle </param>
        /// <param name="bindPropertyName"> Nom de la propriété associé par la règle </param>
        public RuleResult(Rule rule, string item, string property, string objectName, string bindObjectName,
            string bindPropertyName)
        {
            RuleInformation = rule;
            TypeItem = item;
            Property = property;
            ObjectName = objectName;
            BindObjectName = bindObjectName;
            BindPropertytName = bindPropertyName;
        }

        /// <summary>
        ///     Information sur la règle
        /// </summary>
        [DataMember(Name = "RuleInformation")]
        public Rule RuleInformation { get; set; }

        /// <summary>
        ///     Type d'item de la règle
        /// </summary>
        [DataMember(Name = "TypeItem")]
        public string TypeItem { get; set; }

        /// <summary>
        ///     Propriété ciblée par la règle
        /// </summary>
        [DataMember(Name = "Property")]
        public string Property { get; set; }

        /// <summary>
        ///     Nom de l'objet ciblé par la règle
        /// </summary>
        [DataMember(Name = "ObjectName")]
        public string ObjectName { get; set; }

        /// <summary>
        ///     Nom de l'objet ciblé associé par la règle
        /// </summary>
        [DataMember(Name = "BindObjectName")]
        public string BindObjectName { get; set; }

        /// <summary>
        ///     Nom de la propriété associé par la règle
        /// </summary>
        [DataMember(Name = "BindPropertytName")]
        public string BindPropertytName { get; set; }

        /// <summary>
        ///     Affiche un résumé d'information sur le résultat de règle
        /// </summary>
        /// <returns>
        ///     Retourne une chaîne de caractères contenant le type d'objet, la propriété utilisée, le nom de la règle et sa
        ///     description
        /// </returns>
        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.AppendFormat("{0}:[{1}]:{2}", RuleInformation.CodeMessage, Property, RuleInformation.Description);

            return sb.ToString();
        }
    }
}