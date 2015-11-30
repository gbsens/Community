using System;
using System.ComponentModel;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;

namespace MKS.Core
{
    /// <summary>
    ///     Classe de base d'une règle de validation
    /// </summary>
    /// <remarks>
    ///     S'utilise dans une classe RuleToItemList
    /// </remarks>
    [KnownType(typeof (RuleContain))]
    [KnownType(typeof (RuleStringRequired))]
    [KnownType(typeof (RuleStringMaxLength))]
    [KnownType(typeof (RuleStringMinLength))]
    [KnownType(typeof (RuleIntegerMaxValue))]
    [KnownType(typeof (RuleIntegerMinValue))]
    [KnownType(typeof (RuleDateFormat))]
    [KnownType(typeof (RulePostalCode))]
    [KnownType(typeof (RuleStrongPassword))]
    [KnownType(typeof (RuleAlphanumericCharacters))]
    [KnownType(typeof (RuleFloatingNumber))]
    [KnownType(typeof (RulePostalCodeCAN))]
    [KnownType(typeof (RulePostalCodeUS))]
    [KnownType(typeof (RulePhoneNumber))]
    [KnownType(typeof (RulePhoneNumberWithExtention))]
    [KnownType(typeof (RuleEmailAddress))]
    [KnownType(typeof (RuleRegularExpression))]
    [KnownType(typeof (RuleDateGreaterOrEqualThan))]
    [KnownType(typeof (RuleDateGreaterThan))]
    [KnownType(typeof (RuleDateLessOrEqualThan))]
    [KnownType(typeof (RuleDateLessThan))]
    [KnownType(typeof (RuleDateRequired))]
    [KnownType(typeof (RuleObjectRequired))]
    [KnownType(typeof (RuleCustom))]
    [KnownType(typeof (RuleLuhn))]
    [KnownType(typeof (RuleListNotEmpty))]
    [KnownType(typeof (RuleObjectContainsOneValidProperty))]
    [KnownType(typeof (RuleStringExactLength))]
    [KnownType(typeof (RuleDecimalMaxValue))]
    [KnownType(typeof (RuleDecimalMinValue))]
    [KnownType(typeof (RuleDoubleMaxValue))]
    [KnownType(typeof (RuleDoubleMinValue))]
    [KnownType(typeof (RuleSeverity))]
    [KnownType(typeof (RuleType))]
    [DataContract]
    public abstract class Rule
    {
        #region RuleSeverity enum

        /// <summary>
        ///     Spécifie la sévérité de l'erreur de validation.
        /// </summary>
        [DataContract]
        public enum RuleSeverity
        {
            /// <summary>
            ///     Indique une erreur sérieuse
            ///     d'intégrité de la règle pouvant
            ///     rendre l'objet invalide dans le cas d'une règles d'intégritée.
            ///     Dans le cas d'un processus d'affaire,
            ///     celui-ci est automatiquement arrêté et une exception est généré.
            /// </summary>
            /// <remarks>
            ///     Attention aux différences entre une règle d'affaire et règle d'intégritée
            /// </remarks>
            [EnumMember] Error,

            /// <summary>
            ///     Indique une erreur d'importance
            ///     moyenne qui doit être rapportée
            ///     à l'utilisateur, mais qui ne
            ///     devrait pas rendre un objet
            ///     invalide.
            ///     Dans le cas d'une validation d'affaire, le processus est arrêté mais aucune exception n'est lancé.
            /// </summary>
            /// <remarks>
            ///     Attention aux différences entre une règle d'affaire et règle d'intégritée
            /// </remarks>
            [EnumMember] Warning,

            /// <summary>
            ///     Indique un résultat de règle
            ///     d'importance minime mais qui doit
            ///     néanmoins être affichée à l'utilisateur.
            ///     Dans le cas d'un processus d'affaire, le processus n'est pas arrêté et aucune exception n'est généré.
            ///     Par contre l'ensemle des règles qui n'ont pas passé sont cumulés dans les ProcessResulTResult.
            /// </summary>
            /// <remarks>
            ///     Attention aux différences entre une règle d'affaire et règle d'intégritée
            /// </remarks>
            [EnumMember] Information
        }

        #endregion

        #region RuleType enum

        /// <summary>
        ///     Liste des règles disponibles pour validation.
        /// </summary>
        /// <remarks>
        ///     Chaque constante correspond à une règle de la classe CommonRules
        /// </remarks>
        [DataContract]
        public enum RuleType
        {
            /// <summary>
            ///     Validation le contenu en fonction d'un contenu attendu
            /// </summary>
            [Description("Validation du contenu en fonction d'un contenu non attendu")]
            [EnumMember]
            DoesNotContainString,
            /// <summary>
            ///     Validation le contenu en fonction d'un contenu attendu
            /// </summary>
            [Description("Validation du contenu en fonction d'un contenu attendu")]
            [EnumMember]
            ContainString,
            /// <summary>
            ///     Validation d'une maximum de valeur decimale
            /// </summary>
            [EnumMember] DecimalMaxNumber,

            /// <summary>
            ///     Validation d'une minimum de valeur decimale
            /// </summary>
            [EnumMember] DecimalMinNumber,

            /// <summary>
            ///     Validation d'une maximum de valeur float
            /// </summary>
            [EnumMember] FloatMaxNumber,

            /// <summary>
            ///     Validation d'une minimum de valeur float
            /// </summary>
            [EnumMember] FloatMinNumber,

            /// <summary>
            ///     Cette règle n'est pas gérée par le Systeme. Elle doit l'être par une classe utilisateur
            /// </summary>
            [EnumMember] DateCustomRules,

            /// <summary>
            ///     Validation d'une chaîne de caractères non vide
            /// </summary>
            [Description("Validation d'une chaîne de caractères non vide")] [EnumMember] StringRequired,

            /// <summary>
            ///     Validation que l'objet n'est pas vide
            /// </summary>
            [Description("Validation que l'objet n'est pas vide")] [EnumMember] ObjectRequired,

            /// <summary>
            ///     Validation d'une chaîne de caractères sur sa longueur maximum
            /// </summary>
            [Description("Validation d'une chaîne de caractères sur sa longueur maximum")] [EnumMember] StringMaxLength,

            /// <summary>
            ///     Validation d'une chaîne de caractères sur sa longueur minimale
            /// </summary>
            [Description("Validation d'une chaîne de caractères sur sa longueur minimum")] [EnumMember] StringMinLength,

            /// <summary>
            ///     Validation d'un nombre entier sur sa valeur maximale
            /// </summary>
            [Description("Validation d'un nombre entier sur sa valeur maximale")] [EnumMember] IntegerMaxValue,

            /// <summary>
            ///     Validation d'un nombre entier sur sa valeur minimale
            /// </summary>
            [Description("Validation d'un nombre entier sur sa valeur minimale")] [EnumMember] IntergerMinValue,

            /// <summary>
            ///     Validation d'une chaîne de caractère au format date
            /// </summary>
            [Description("Validation d'une chaîne de caractère au format date")] [EnumMember] DateFormat,

            /// <summary>
            ///     Validation d'une chaîne de caractère sous forme de code postal Canadien ou US
            /// </summary>
            [Description("Validation d'une chaîne de caractère sous forme de code postal Canadien ou US")] [EnumMember] PostalCode,

            /// <summary>
            ///     Validation d'un numéro de téléphone du Canada
            /// </summary>
            [Description("Validation d'un numéro de téléphone du Canada")] [EnumMember] PhoneNumber,

            /// <summary>
            ///     Validation d'un numéro de téléphone du Canada avec extension
            /// </summary>
            [Description("Validation d'un numéro de téléphone du Canada avec extension")] [EnumMember] RulePhoneNumberWithExtention,

            /// <summary>
            ///     Validation d'une adresse de courriel
            /// </summary>
            [Description("Validation d'une adresse de courriel")] [EnumMember] EmailAddress,

            /// <summary>
            ///     Validation d'une chaîne de caractère à l'aide d'une expression régulière
            /// </summary>
            [Description("Validation d'une chaîne de caractère à l'aide d'une expression régulière")] [EnumMember] RegularExpression,

            /// <summary>
            ///     Validation d'une date devant être supérieure à celle passée en paramètre
            /// </summary>
            [Description("Validation d'une date devant être supérieure à celle passée en paramètre")] [EnumMember] DateGreaterThan,

            /// <summary>
            ///     Validation d'une date devant être supérieure ou égale à celle passée en paramètre
            /// </summary>
            [Description("Validation d'une date devant être supérieure uo égale à celle passée en paramètre")] [EnumMember] DateGreaterOrEqualThan,

            /// <summary>
            ///     Validation d'une date devant être inférieure à celle passée en paramètre
            /// </summary>
            [Description("Validation d'une date devant être inférieure à celle passée en paramètre")] [EnumMember] DateLessThan,

            /// <summary>
            ///     Validation d'une date devant être inférieure ou égale à celle passée en paramètre
            /// </summary>
            [Description("Validation d'une date devant être inférieure ou égale à celle passée en paramètre")] [EnumMember] DateLessOrEqualThan,

            /// <summary>
            ///     Validation d'une objet DateTime qui ne doit pas être null ou vide
            /// </summary>
            [Description("Validation d'une date doit-être présente")] [EnumMember] DateRequired,

            /// <summary>
            ///     Cette règle n'est pas gérée par le Systeme. Elle doit l'être par une classe utilisateur
            /// </summary>
            [EnumMember] CustomRules,

            /// <summary>
            ///     Cette règle n'est pas gérée par le Systeme. Elle doit l'être par une classe utilisateur
            /// </summary>
            [EnumMember] BusinessRules,

            /// <summary>
            ///     Validation d'une chaîne avec la formule de Luhn
            /// </summary>
            [EnumMember] Luhn,

            /// <summary>
            ///     Vérifie qu'une liste n'est pas vide
            /// </summary>
            [EnumMember] ListNotEmpty,

            /// <summary>
            ///     Vérifie qu'une liste n'est pas vide
            /// </summary>
            [EnumMember] ObjectContainsOneValidProperty,

            /// <summary>
            ///     Validation d'une chaîne de caractère sous forme de code postal US
            /// </summary>
            [Description("Validation d'une chaîne de caractère sous forme de code postal US")] [EnumMember] RulePostalCodeUS,

            /// <summary>
            ///     Validation d'une chaîne de caractère sous forme de code postal CAN
            /// </summary>
            [Description("Validation d'une chaîne de caractère sous forme de code postal CAN")] [EnumMember] RulePostalCodeCAN,

            /// <summary>
            ///     Vérifie un floating number
            /// </summary>
            [EnumMember] FloatingNumber,

            /// <summary>
            ///     Validates a strong password. It must be between 8 and 10 characters, contain at least one digit and one alphabetic
            ///     character, and must not contain special characters.
            /// </summary>
            [Description(
                "Validates a strong password. It must be between 8 and 10 characters, contain at least one digit and one alphabetic character, and must not contain special characters."
                )] [EnumMember] StrongPassword,

            /// <summary>
            ///     Validation d'une chaîne de caractères Alphanumeric characters
            /// </summary>
            [Description("Validation d'une chaîne de caractères Alphanumeric characters")] [EnumMember] AlphanumericCharacters,

            /// <summary>
            ///     Validation d'une chaîne de caractères sur sa longueur exact
            /// </summary>
            [Description("Validation d'une chaîne de caractères sur sa longueur exact")] [EnumMember] StringExactLength,

            /// <summary>
            ///     Validation d'un nombre décimal sur sa valeur minimale
            /// </summary>
            [Description("Validation d'un nombre décimal sur sa valeur minimale")] [EnumMember] DecimalMinValue,

            /// <summary>
            ///     Validation d'un nombre décimal sur sa valeur maximale
            /// </summary>
            [Description("Validation d'un nombre décimal sur sa valeur maximale")] [EnumMember] DecimalMaxValue,

            /// <summary>
            ///     Validation d'un nombre décimal sur sa valeur minimale
            /// </summary>
            [Description("Validation d'un nombre double sur sa valeur minimale")] [EnumMember] DoubleMinValue,

            /// <summary>
            ///     Validation d'un nombre décimal sur sa valeur maximale
            /// </summary>
            [Description("Validation d'un nombre double sur sa valeur maximale")] [EnumMember] DoubleMaxValue
        }

        #endregion

        /// <summary>
        ///     Constructeur d'une règle de validation
        /// </summary>
        /// <param name="codeMessage"> Identifiant de la règle ou code de règle </param>
        /// <param name="description"> Informations de la règle </param>
        /// <param name="severite"> Identification de la sévérité </param>
        /// <param name="ruleType"> Type de règle de validation. </param>
        /// <param name="parameter"> Paramètre utilisé pour la règle de validation. </param>
        /// <param name="objectType"> Type d'objet à valider. </param>
        /// <param name="validationObject"> Type de la classe de validation qui validera l'objet. </param>
        /// <param name="allowNull"> Est-ce que la validation passera si le champ est Null. </param>
        protected Rule(string codeMessage, string description, RuleSeverity severite, RuleType ruleType,
            object parameter, Type objectType, Type validationObject, bool allowNull)
        {
            CodeMessage = codeMessage;
            Description = description;
            Severity = severite;
            Type = ruleType;
            Parameter = parameter;
            AllowNull = allowNull;
            ValidationObject = validationObject;
            ObjectType = objectType;
        }

        /// <summary>
        ///     Nom de la règle
        /// </summary>
        [DataMember(Name = "CodeMessage")]
        public string CodeMessage { get; set; }

        /// <summary>
        ///     Informations de la règle
        /// </summary>
        [DataMember(Name = "Informations")]
        public string Description { get; set; }

        /// <summary>
        ///     Severité de la règle
        /// </summary>
        [DataMember(Name = "Severity")]
        public RuleSeverity Severity { get; set; }

        /// <summary>
        ///     Identification du type de validation
        /// </summary>
        [DataMember(Name = "Type")]
        public RuleType Type { get; set; }

        /// <summary>
        ///     Paramètre qui permet de passer une valeur à une validation
        /// </summary>
        [DataMember(Name = "Parameter")]
        public object Parameter { get; set; }

        /// <summary>
        ///     Est-ce que l'on permet à cette validation de réussir dans le cas ou l'objet demandé est Null.
        /// </summary>
        [DataMember(Name = "AllowNull")]
        public bool AllowNull { get; set; }

        /// <summary>
        ///     Type de l'objet de validation qui sera appliqué sur la règle.
        /// </summary>
        [DataMember(Name = "ValidationObject")]
        public Type ValidationObject { get; protected set; }

        /// <summary>
        ///     Type de l'objet sur lequel on applique la validation.
        /// </summary>
        [DataMember(Name = "ObjectType")]
        public Type ObjectType { get; protected set; }

        /// <summary>
        ///     Crée une copie de l'objet.
        /// </summary>
        /// <returns> Retourne une copie de l'objet. </returns>
        /// <remarks>
        ///     MemberwiseClone crée une copie de type "shallow".
        /// </remarks>
        public Rule CloneObject()
        {
            var obj = this;

            var inst = obj.GetType().GetMethod("MemberwiseClone",
                BindingFlags.Instance | BindingFlags.Public |
                BindingFlags.NonPublic);

            if (inst != null)
                return (Rule) inst.Invoke(obj, null);
            return null;
        }

        /// <summary>
        ///     Convertis en format string la règle
        /// </summary>
        /// <returns> Le string converti </returns>
        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.AppendFormat("{0}:", CodeMessage);

            sb.AppendFormat(":{0}", Description);
            return sb.ToString();
        }
    }
}