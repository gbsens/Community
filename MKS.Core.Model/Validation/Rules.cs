using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace MKS.Core
{
    /// <summary>
    ///     Validation d'une chaîne de caractères non vide.
    /// </summary>
    [DataContract]
    public class RuleStringRequired : Rule
    {
        /// <summary>
        ///     Validation d'une chaîne de caractères non vide.
        /// </summary>
        /// <param name="codeMessage"> Code du message pouvant se trouver dans un fichier ressource. </param>
        /// <param name="description"> Message de la règle de validation. </param>
        /// <param name="severite"> Spécifie la sévérité de l'erreur de validation. </param>
        public RuleStringRequired(string codeMessage, string description, RuleSeverity severite)
            : base(codeMessage, description, severite, RuleType.StringRequired, null, null, null, false)
        {
        }
    }

    /// <summary>
    ///     Validation d'une chaîne de caractères sur sa longueur maximum.
    /// </summary>
    [DataContract]
    public class RuleStringMaxLength : Rule
    {
        /// <summary>
        ///     Validation d'une chaîne de caractères sur sa longueur maximum.
        /// </summary>
        /// <param name="codeMessage"> Code du message pouvant se trouver dans un fichier ressource. </param>
        /// <param name="description"> Message de la règle de validation. </param>
        /// <param name="severite"> Spécifie la sévérité de l'erreur de validation. </param>
        /// <param name="maxLength"> Longueur maximale de la chaine. </param>
        /// <param name="allowNull"> Est-ce que la validation passera si le champ est Null. </param>
        public RuleStringMaxLength(string codeMessage, string description, RuleSeverity severite, int maxLength,
            bool allowNull)
            : base(codeMessage, description, severite, RuleType.StringMaxLength, maxLength, null, null, allowNull)
        {
        }
    }

    /// <summary>
    ///     Validation d'une chaîne de caractères sur sa longueur minimale.
    /// </summary>
    [DataContract]
    public class RuleStringMinLength : Rule
    {
        /// <summary>
        ///     Validation d'une chaîne de caractères sur sa longueur minimale.
        /// </summary>
        /// <param name="codeMessage"> Code du message pouvant se trouver dans un fichier ressource. </param>
        /// <param name="description"> Message de la règle de validation. </param>
        /// <param name="severite"> Spécifie la sévérité de l'erreur de validation. </param>
        /// <param name="minLength"> Taille minimale de la chaine. </param>
        /// <param name="allowNull"> Est-ce que la validation passera si le champ est Null. </param>
        public RuleStringMinLength(string codeMessage, string description, RuleSeverity severite, int minLength,
            bool allowNull)
            : base(codeMessage, description, severite, RuleType.StringMinLength, minLength, null, null, allowNull)
        {
        }
    }

    /// <summary>
    ///     Validation d'un nombre entier sur sa valeur maximale.
    /// </summary>
    [DataContract]
    public class RuleIntegerMaxValue : Rule
    {
        /// <summary>
        ///     Validation d'un nombre entier sur sa valeur maximale.
        /// </summary>
        /// <param name="codeMessage"> Code du message pouvant se trouver dans un fichier ressource. </param>
        /// <param name="description"> Message de la règle de validation. </param>
        /// <param name="severite"> Spécifie la sévérité de l'erreur de validation. </param>
        /// <param name="maxValue"> Valeur maximale de l'entier. </param>
        /// <param name="allowNull"> Est-ce que la validation passera si le champ est Null. </param>
        public RuleIntegerMaxValue(string codeMessage, string description, RuleSeverity severite, int maxValue,
            bool allowNull)
            : base(codeMessage, description, severite, RuleType.IntegerMaxValue, maxValue, null, null, allowNull)
        {
        }
    }

    /// <summary>
    ///     Validation d'un nombre entier sur sa valeur minimale.
    /// </summary>
    [DataContract]
    public class RuleIntegerMinValue : Rule
    {
        /// <summary>
        ///     Validation d'un nombre entier sur sa valeur minimale.
        /// </summary>
        /// <param name="codeMessage"> Code du message pouvant se trouver dans un fichier ressource. </param>
        /// <param name="description"> Message de la règle de validation. </param>
        /// <param name="severite"> Spécifie la sévérité de l'erreur de validation. </param>
        /// <param name="minValue"> Valeur minimale de l'entier. </param>
        /// <param name="allowNull"> Est-ce que la validation passera si le champ est Null. </param>
        public RuleIntegerMinValue(string codeMessage, string description, RuleSeverity severite, int minValue,
            bool allowNull)
            : base(codeMessage, description, severite, RuleType.IntergerMinValue, minValue, null, null, allowNull)
        {
        }
    }

    /// <summary>
    ///     Validation d'une chaîne de caractère au format date.
    /// </summary>
    [DataContract]
    public class RuleDateFormat : Rule
    {
        /// <summary>
        ///     Validation d'une chaîne de caractère au format date.
        /// </summary>
        /// <param name="codeMessage"> Code du message pouvant se trouver dans un fichier ressource. </param>
        /// <param name="description"> Message de la règle de validation. </param>
        /// <param name="severite"> Spécifie la sévérité de l'erreur de validation. </param>
        /// <param name="formatDate"> Format de la date à valider. </param>
        /// <param name="allowNull"> Est-ce que la validation passera si le champ est Null. </param>
        public RuleDateFormat(string codeMessage, string description, RuleSeverity severite, string formatDate,
            bool allowNull)
            : base(codeMessage, description, severite, RuleType.DateFormat, formatDate, null, null, allowNull)
        {
        }
    }

    /// <summary>
    ///     Validation d'une chaîne de caractère sous forme de code postal Canadien ou US.
    /// </summary>
    [DataContract]
    public class RulePostalCode : Rule
    {
        /// <summary>
        ///     Regex pour la validation des codes postaux.
        /// </summary>
        public const string RegexValidation =
            @"(^\d{5}(-\d{4})?$)|(^[ABCEGHJKLMNPRSTVXY]{1}\d{1}[A-Z]{1} *\d{1}[A-Z]{1}\d{1}$)";

        /// <summary>
        ///     Validation d'une chaîne de caractère sous forme de code postal Canadien ou US.
        /// </summary>
        /// <param name="codeMessage"> Code du message pouvant se trouver dans un fichier ressource. </param>
        /// <param name="description"> Message de la règle de validation. </param>
        /// <param name="severite"> Spécifie la sévérité de l'erreur de validation. </param>
        /// <param name="allowNull"> Est-ce que la validation passera si le champ est Null. </param>
        public RulePostalCode(string codeMessage, string description, RuleSeverity severite, bool allowNull)
            : base(codeMessage, description, severite, RuleType.PostalCode, RegexValidation, null, null, allowNull)
        {
        }
    }

    /// <summary>
    ///     Validation d'une chaîne de caractère sous forme de code postal Canadien ou US.
    /// </summary>
    [DataContract]
    public class RuleStrongPassword : Rule
    {
        /// <summary>
        ///     Regex pour la validation des codes postaux.
        /// </summary>
        public const string RegexValidation = @"(?!^[0-9]*$)(?!^[a-zA-Z]*$)^([a-zA-Z0-9]{8,10})$";

        /// <summary>
        ///     Validation d'une chaîne de caractère sous forme de code postal Canadien .
        /// </summary>
        /// <param name="codeMessage"> Code du message pouvant se trouver dans un fichier ressource. </param>
        /// <param name="description"> Message de la règle de validation. </param>
        /// <param name="severite"> Spécifie la sévérité de l'erreur de validation. </param>
        /// <param name="allowNull"> Est-ce que la validation passera si le champ est Null. </param>
        public RuleStrongPassword(string codeMessage, string description, RuleSeverity severite, bool allowNull)
            : base(codeMessage, description, severite, RuleType.StrongPassword, RegexValidation, null, null, allowNull
                )
        {
        }
    }

    /// <summary>
    ///     Validation d'une chaîne de caractère sous forme de code postal Canadien ou US.
    /// </summary>
    [DataContract]
    public class RuleAlphanumericCharacters : Rule
    {
        /// <summary>
        ///     Regex pour la validation des codes postaux.
        /// </summary>
        public const string RegexValidation = @"[A-Za-z0-9]";

        /// <summary>
        ///     Validation d'une chaîne de caractère sous forme de code postal Canadien .
        /// </summary>
        /// <param name="codeMessage"> Code du message pouvant se trouver dans un fichier ressource. </param>
        /// <param name="description"> Message de la règle de validation. </param>
        /// <param name="severite"> Spécifie la sévérité de l'erreur de validation. </param>
        /// <param name="allowNull"> Est-ce que la validation passera si le champ est Null. </param>
        public RuleAlphanumericCharacters(string codeMessage, string description, RuleSeverity severite,
            bool allowNull)
            : base(
                codeMessage, description, severite, RuleType.AlphanumericCharacters, RegexValidation, null, null,
                allowNull)
        {
        }
    }




    /// <summary>
    ///     Validation d'une chaîne de caractère sous forme de code postal Canadien ou US.
    /// </summary>
    [DataContract]
    public class RuleFloatingNumber : Rule
    {
        /// <summary>
        ///     Regex pour la validation des floating Number
        /// </summary>
        public const string RegexValidation = @"^[-+]?[0-9]*\.?[0-9]+$";

        /// <summary>
        ///     Validation d'une chaîne de caractère sous forme de code postal Canadien .
        /// </summary>
        /// <param name="codeMessage"> Code du message pouvant se trouver dans un fichier ressource. </param>
        /// <param name="description"> Message de la règle de validation. </param>
        /// <param name="severite"> Spécifie la sévérité de l'erreur de validation. </param>
        /// <param name="allowNull"> Est-ce que la validation passera si le champ est Null. </param>
        public RuleFloatingNumber(string codeMessage, string description, RuleSeverity severite, bool allowNull)
            : base(codeMessage, description, severite, RuleType.FloatingNumber, RegexValidation, null, null, allowNull
                )
        {
        }
    }

    /// <summary>
    ///     Validation d'une chaîne de caractère sous forme de code postal Canadien ou US.
    /// </summary>
    [DataContract]
    public class RulePostalCodeCAN : Rule
    {
        /// <summary>
        ///     Regex pour la validation des codes postaux.
        /// </summary>
        public const string RegexValidation = @"^[ABCEGHJKLMNPRSTVXY]{1}\d{1}[A-Z]{1} *\d{1}[A-Z]{1}\d{1}$";

        /// <summary>
        ///     Validation d'une chaîne de caractère sous forme de code postal Canadien .
        /// </summary>
        /// <param name="codeMessage"> Code du message pouvant se trouver dans un fichier ressource. </param>
        /// <param name="description"> Message de la règle de validation. </param>
        /// <param name="severite"> Spécifie la sévérité de l'erreur de validation. </param>
        /// <param name="allowNull"> Est-ce que la validation passera si le champ est Null. </param>
        public RulePostalCodeCAN(string codeMessage, string description, RuleSeverity severite, bool allowNull)
            : base(
                codeMessage, description, severite, RuleType.RulePostalCodeCAN, RegexValidation, null, null, allowNull
                )
        {
        }
    }

    /// <summary>
    ///     Validation d'une chaîne de caractère sous forme de code postal Canadien ou US.
    /// </summary>
    [DataContract]
    public class RulePostalCodeUS : Rule
    {
        /// <summary>
        ///     Regex pour la validation des codes postaux.
        /// </summary>
        public const string RegexValidation = @"^\d{5}(-\d{4})?$";

        /// <summary>
        ///     Validation d'une chaîne de caractère sous forme de code postal US.
        /// </summary>
        /// <param name="codeMessage"> Code du message pouvant se trouver dans un fichier ressource. </param>
        /// <param name="description"> Message de la règle de validation. </param>
        /// <param name="severite"> Spécifie la sévérité de l'erreur de validation. </param>
        /// <param name="allowNull"> Est-ce que la validation passera si le champ est Null. </param>
        public RulePostalCodeUS(string codeMessage, string description, RuleSeverity severite, bool allowNull)
            : base(
                codeMessage, description, severite, RuleType.RulePostalCodeUS, RegexValidation, null, null, allowNull)
        {
        }
    }

    /// <summary>
    ///     Validation d'un numéro de téléphone du Canada.
    /// </summary>
    [DataContract]
    public class RulePhoneNumber : Rule
    {
        public const string RegexValidation = @"^\(?([0-9]{3})\)?[ ]?([0-9]{3})[-]?([0-9]{4})$";

        /// <summary>
        ///     Validation d'un numéro de téléphone du Canada.
        /// </summary>
        /// <param name="codeMessage"> Code du message pouvant se trouver dans un fichier ressource. </param>
        /// <param name="description"> Message de la règle de validation. </param>
        /// <param name="severite"> Spécifie la sévérité de l'erreur de validation. </param>
        /// <param name="allowNull"> Est-ce que la validation passera si le champ est Null. </param>
        public RulePhoneNumber(string codeMessage, string description, RuleSeverity severite, bool allowNull)
            : base(codeMessage, description, severite, RuleType.PhoneNumber, RegexValidation, null, null, allowNull)
        {
        }
    }

    /// <summary>
    ///     Validation d'un numéro de téléphone avec extension
    /// </summary>
    /// <example>
    ///     1-234-567-8901
    ///     1-234-567-8901 x1234
    ///     1-234-567-8901 ext1234
    ///     1 (234) 567-8901
    ///     1.234.567.8901
    ///     1/234/567/8901
    ///     12345678901
    /// </example>
    [DataContract]
    public class RulePhoneNumberWithExtention : Rule
    {
        public const string RegexValidation =
            @"^(?:(?:\+?1\s*(?:[.-]\s*)?)?(?:\(\s*([2-9]1[02-9]|[2-9][02-8]1|[2-9][02-8][02-9])\s*\)|([2-9]1[02-9]|[2-9][02-8]1|[2-9][02-8][02-9]))\s*(?:[.-]\s*)?)?([2-9]1[02-9]|[2-9][02-9]1|[2-9][02-9]{2})\s*(?:[.-]\s*)?([0-9]{4})(?:\s*(?:#|x\.?|ext\.?|extension)\s*(\d+))?$";

        /// <summary>
        ///     Validation d'un numéro de téléphone du Canada.
        /// </summary>
        /// <param name="codeMessage"> Code du message pouvant se trouver dans un fichier ressource. </param>
        /// <param name="description"> Message de la règle de validation. </param>
        /// <param name="severite"> Spécifie la sévérité de l'erreur de validation. </param>
        /// <param name="allowNull"> Est-ce que la validation passera si le champ est Null. </param>
        public RulePhoneNumberWithExtention(string codeMessage, string description, RuleSeverity severite,
            bool allowNull)
            : base(
                codeMessage, description, severite, RuleType.RulePhoneNumberWithExtention, RegexValidation, null, null,
                allowNull)
        {
        }
    }

    /// <summary>
    ///     Validation d'une adresse de courriel.
    /// </summary>
    [DataContract]
    public class RuleEmailAddress : Rule
    {
        /// <summary>
        ///     Regex pour la validation des codes postaux.
        /// </summary>
        public const string RegexValidation =
            @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";

        /// <summary>
        ///     Validation d'une adresse de courriel.
        /// </summary>
        /// <param name="codeMessage"> Code du message pouvant se trouver dans un fichier ressource. </param>
        /// <param name="description"> Message de la règle de validation. </param>
        /// <param name="severite"> Spécifie la sévérité de l'erreur de validation. </param>
        /// <param name="allowNull"> Est-ce que la validation passera si le champ est Null. </param>
        public RuleEmailAddress(string codeMessage, string description, RuleSeverity severite, bool allowNull)
            : base(codeMessage, description, severite, RuleType.EmailAddress, RegexValidation, null, null, allowNull)
        {
        }
    }

    /// <summary>
    ///     Validation d'une chaîne de caractère à l'aide d'une expression régulière.
    /// </summary>
    [DataContract]
    public class RuleRegularExpression : Rule
    {
        /// <summary>
        ///     Validation d'une chaîne de caractère à l'aide d'une expression régulière.
        /// </summary>
        /// <param name="codeMessage"> Code du message pouvant se trouver dans un fichier ressource. </param>
        /// <param name="description"> Message de la règle de validation. </param>
        /// <param name="severite"> Spécifie la sévérité de l'erreur de validation. </param>
        /// <param name="regularExpression"> Expression régulière. </param>
        /// <param name="allowNull"> Est-ce que la validation passera si le champ est Null. </param>
        public RuleRegularExpression(string codeMessage, string description, RuleSeverity severite,
            string regularExpression, bool allowNull)
            : base(
                codeMessage, description, severite, RuleType.RegularExpression, regularExpression, null, null,
                allowNull)
        {
        }
    }

    /// <summary>
    ///     Validation d'une date devant être supérieure ou égale à celle passée en paramètre.
    /// </summary>
    [DataContract]
    public class RuleDateGreaterOrEqualThan : Rule
    {
        /// <summary>
        ///     Validation d'une date devant être supérieure ou égale à celle passée en paramètre.
        /// </summary>
        /// <param name="codeMessage"> Code du message pouvant se trouver dans un fichier ressource. </param>
        /// <param name="description"> Message de la règle de validation. </param>
        /// <param name="severite"> Spécifie la sévérité de l'erreur de validation. </param>
        /// <param name="comparisonDate"> Date avec laquelle on effectue la comparaison. </param>
        /// <param name="allowNull"> Est-ce que la validation passera si le champ est Null. </param>
        public RuleDateGreaterOrEqualThan(string codeMessage, string description, RuleSeverity severite,
            DateTime comparisonDate, bool allowNull)
            : base(
                codeMessage, description, severite, RuleType.DateGreaterOrEqualThan, comparisonDate, null, null,
                allowNull)
        {
        }
    }

    /// <summary>
    ///     Validation d'une date devant être supérieure à celle passée en paramètre.
    /// </summary>
    [DataContract]
    public class RuleDateGreaterThan : Rule
    {
        /// <summary>
        ///     Validation d'une date devant être supérieure à celle passée en paramètre.
        /// </summary>
        /// <param name="codeMessage"> Code du message pouvant se trouver dans un fichier ressource. </param>
        /// <param name="description"> Message de la règle de validation. </param>
        /// <param name="severite"> Spécifie la sévérité de l'erreur de validation. </param>
        /// <param name="comparisonDate"> Date avec laquelle on effectue la comparaison. </param>
        /// <param name="allowNull"> Est-ce que la validation passera si le champ est Null. </param>
        public RuleDateGreaterThan(string codeMessage, string description, RuleSeverity severite,
            DateTime comparisonDate, bool allowNull)
            : base(codeMessage, description, severite, RuleType.DateGreaterThan, comparisonDate, null, null, allowNull
                )
        {
        }
    }

    /// <summary>
    ///     Validation d'une date devant être inférieure ou égale à celle passée en paramètre.
    /// </summary>
    [DataContract]
    public class RuleDateLessOrEqualThan : Rule
    {
        /// <summary>
        ///     Validation d'une date devant être inférieure ou égale à celle passée en paramètre.
        /// </summary>
        /// <param name="codeMessage"> Code du message pouvant se trouver dans un fichier ressource. </param>
        /// <param name="description"> Message de la règle de validation. </param>
        /// <param name="severite"> Spécifie la sévérité de l'erreur de validation. </param>
        /// <param name="comparisonDate"> Date avec laquelle on effectue la comparaison. </param>
        /// <param name="allowNull"> Est-ce que la validation passera si le champ est Null. </param>
        public RuleDateLessOrEqualThan(string codeMessage, string description, RuleSeverity severite,
            DateTime comparisonDate, bool allowNull)
            : base(
                codeMessage, description, severite, RuleType.DateLessOrEqualThan, comparisonDate, null, null,
                allowNull)
        {
        }
    }

    /// <summary>
    ///     Validation d'une date devant être inférieure à celle passée en paramètre.
    /// </summary>
    [DataContract]
    public class RuleDateLessThan : Rule
    {
        /// <summary>
        ///     Validation d'une date devant être inférieure à celle passée en paramètre.
        /// </summary>
        /// <param name="codeMessage"> Code du message pouvant se trouver dans un fichier ressource. </param>
        /// <param name="description"> Message de la règle de validation. </param>
        /// <param name="severite"> Spécifie la sévérité de l'erreur de validation. </param>
        /// <param name="comparisonDate"> Date avec laquelle on effectue la comparaison. </param>
        /// <param name="allowNull"> Est-ce que la validation passera si le champ est Null. </param>
        public RuleDateLessThan(string codeMessage, string description, RuleSeverity severite, DateTime comparisonDate,
            bool allowNull)
            : base(codeMessage, description, severite, RuleType.DateLessThan, comparisonDate, null, null, allowNull)
        {
        }
    }

    /// <summary>
    ///     Validation d'une objet DateTime qui ne doit pas être null ou vide.
    /// </summary>
    [DataContract]
    public class RuleDateRequired : Rule
    {
        /// <summary>
        ///     Validation d'une objet DateTime qui ne doit pas être null ou vide.
        /// </summary>
        /// <param name="codeMessage"> Code du message pouvant se trouver dans un fichier ressource. </param>
        /// <param name="description"> Message de la règle de validation. </param>
        /// <param name="severite"> Spécifie la sévérité de l'erreur de validation. </param>
        public RuleDateRequired(string codeMessage, string description, RuleSeverity severite)
            : base(codeMessage, description, severite, RuleType.DateRequired, null, null, null, false)
        {
        }
    }

    /// <summary>
    ///     Validation que l'objet n'est pas vide.
    /// </summary>
    [DataContract]
    public class RuleObjectRequired : Rule
    {
        /// <summary>
        ///     Validation que l'objet n'est pas vide.
        /// </summary>
        /// <param name="codeMessage"> Code du message pouvant se trouver dans un fichier ressource. </param>
        /// <param name="description"> Message de la règle de validation. </param>
        /// <param name="severite"> Spécifie la sévérité de l'erreur de validation. </param>
        public RuleObjectRequired(string codeMessage, string description, RuleSeverity severite)
            : base(codeMessage, description, severite, RuleType.ObjectRequired, null, null, null, false)
        {
        }
    }

    /// <summary>
    ///     Cette règle n'est pas gérée par le Systeme. Elle doit l'être par une classe utilisateur implémentant
    ///     l'interface iCustomValidation.
    /// </summary>
    [DataContract]
    public class RuleCustom : Rule
    {
        /// <summary>
        ///     Cette règle n'est pas gérée par le Systeme. Elle doit l'être par une classe utilisateur implémentant
        ///     l'interface iCustomValidation.
        /// </summary>
        /// <param name="codeMessage"> Code du message pouvant se trouver dans un fichier ressource. </param>
        /// <param name="description"> Message de la règle de validation. </param>
        /// <param name="severite"> Spécifie la sévérité de l'erreur de validation. </param>
        public RuleCustom(string codeMessage, string description, RuleSeverity severite)
            : base(codeMessage, description, severite, RuleType.CustomRules, null, null, null, false)
        {
        }
    }


    /// <summary>
    /// Valide si la valeur est contenu dans la propriété
    /// </summary>
    [DataContract]
    public class RuleContain : Rule
    {
        /// <summary>
        ///     Cette règle n'est pas gérée par le Systeme. Elle doit l'être par une classe utilisateur implémentant
        ///     l'interface iCustomValidation.
        /// </summary>
        /// <param name="codeMessage"> Code du message pouvant se trouver dans un fichier ressource. </param>
        /// <param name="description"> Message de la règle de validation. </param>
        /// <param name="severite"> Spécifie la sévérité de l'erreur de validation. </param>
        public RuleContain(string codeMessage, string description, RuleSeverity severite, bool allowNull, string valuetocheck)
            : base(codeMessage, description, severite, RuleType.ContainString, valuetocheck, null, null, allowNull)
        {
        }
    }
    /// <summary>
    /// Valide si la valeur n'est pas contenu dans la propriété
    /// </summary>
    [DataContract]
    public class RuleDoesNotContain : Rule
    {
        /// <summary>
        ///     Cette règle n'est pas gérée par le Systeme. Elle doit l'être par une classe utilisateur implémentant
        ///     l'interface iCustomValidation.
        /// </summary>
        /// <param name="codeMessage"> Code du message pouvant se trouver dans un fichier ressource. </param>
        /// <param name="description"> Message de la règle de validation. </param>
        /// <param name="severite"> Spécifie la sévérité de l'erreur de validation. </param>
        public RuleDoesNotContain(string codeMessage, string description, RuleSeverity severite, bool allowNull,string valuetocheck)
            : base(codeMessage, description, severite, RuleType.DoesNotContainString, valuetocheck, null, null, allowNull)
        {
        }
    }

    /// <summary>
    ///     Validation d'une chaîne avec la formule de Luhn
    /// </summary>
    [DataContract]
    public class RuleLuhn : Rule
    {
        /// <summary>
        ///     Validation d'une chaîne avec la formule de Luhn
        /// </summary>
        /// <param name="codeMessage"> Code du message pouvant se trouver dans un fichier ressource. </param>
        /// <param name="description"> Message de la règle de validation. </param>
        /// <param name="severite"> Spécifie la sévérité de l'erreur de validation. </param>
        /// <param name="allowNull"> Est-ce que la validation passera si le champ est Null. </param>
        public RuleLuhn(string codeMessage, string description, RuleSeverity severite, bool allowNull)
            : base(codeMessage, description, severite, RuleType.Luhn, null, null, null, allowNull)
        {
        }
    }

    /// <summary>
    ///     Vérifie qu'une liste n'est pas vide
    /// </summary>
    [DataContract]
    public class RuleListNotEmpty : Rule
    {
        /// <summary>
        ///     Vérifie qu'une liste n'est pas vide
        /// </summary>
        /// <param name="codeMessage"> Code du message pouvant se trouver dans un fichier ressource. </param>
        /// <param name="description"> Message de la règle de validation. </param>
        /// <param name="severite"> Spécifie la sévérité de l'erreur de validation. </param>
        /// <param name="allowNull"> Est-ce que la validation passera si le champ est Null. </param>
        public RuleListNotEmpty(string codeMessage, string description, RuleSeverity severite, bool allowNull)
            : base(codeMessage, description, severite, RuleType.ListNotEmpty, null, null, null, allowNull)
        {
        }
    }

    /// <summary>
    ///     Vérifie qu'une liste n'est pas vide
    /// </summary>
    [DataContract]
    public class RuleObjectContainsOneValidProperty : Rule
    {
        /// <summary>
        ///     Cette méthode permet de vérifier qu'un objet a au moins une de ses propriétés qui n'est pas null ou à sa valeur par
        ///     défaut.
        ///     Exemple : Pour qu'un objet passe la vérification, il doit avoir au moins une string n'étant pas à null ou vide, une
        ///     propriété
        ///     numérique doit être plus grande que 0, une propriété Nullable ne doit pas être null ou encore une de ses propriétés
        ///     est d'un type
        ///     Non Nullable.
        /// </summary>
        /// <param name="codeMessage"> Code du message pouvant se trouver dans un fichier ressource. </param>
        /// <param name="description"> Message de la règle de validation. </param>
        /// <param name="severite"> Spécifie la sévérité de l'erreur de validation. </param>
        /// <param name="listProperty"> Liste des propriétés à vérifier. </param>
        /// <param name="allowNull"> Est-ce que la validation passera si le champ est Null. </param>
        public RuleObjectContainsOneValidProperty(string codeMessage, string description, RuleSeverity severite,
            List<string> listProperty, bool allowNull)
            : base(
                codeMessage, description, severite, RuleType.ObjectContainsOneValidProperty, listProperty, null, null,
                allowNull)
        {
        }
    }

    /// <summary>
    ///     Validation d'une chaîne de caractères sur sa longueur maximum.
    /// </summary>
    [DataContract]
    public class RuleStringExactLength : Rule
    {
        /// <summary>
        ///     Validation d'une chaîne de caractères sur sa longueur exact.
        /// </summary>
        /// <param name="codeMessage"> Code du message pouvant se trouver dans un fichier ressource. </param>
        /// <param name="description"> Message de la règle de validation. </param>
        /// <param name="severite"> Spécifie la sévérité de l'erreur de validation. </param>
        /// <param name="exactLength"> Longueur exact de la chaine. </param>
        /// <param name="allowNull"> Est-ce que la validation passera si le champ est Null. </param>
        public RuleStringExactLength(string codeMessage, string description, RuleSeverity severite, int exactLength,
            bool allowNull)
            : base(codeMessage, description, severite, RuleType.StringExactLength, exactLength, null, null, allowNull)
        {
        }
    }

    /// <summary>
    ///     Validation d'un nombre décimal sur sa valeur maximale.
    /// </summary>
    [DataContract]
    public class RuleDecimalMaxValue : Rule
    {
        /// <summary>
        ///     Validation d'un nombre décimal sur sa valeur maximale.
        /// </summary>
        /// <param name="codeMessage"> Code du message pouvant se trouver dans un fichier ressource. </param>
        /// <param name="description"> Message de la règle de validation. </param>
        /// <param name="severite"> Spécifie la sévérité de l'erreur de validation. </param>
        /// <param name="maxValue"> Valeur maximale de l'entier. </param>
        /// <param name="allowNull"> Est-ce que la validation passera si le champ est Null. </param>
        public RuleDecimalMaxValue(string codeMessage, string description, RuleSeverity severite, decimal maxValue,
            bool allowNull)
            : base(codeMessage, description, severite, RuleType.DecimalMaxValue, maxValue, null, null, allowNull)
        {
        }
    }

    /// <summary>
    ///     Validation d'un nombre décimal sur sa valeur minimale.
    /// </summary>
    [DataContract]
    public class RuleDecimalMinValue : Rule
    {
        /// <summary>
        ///     Validation d'un nombre décimal sur sa valeur minimale.
        /// </summary>
        /// <param name="codeMessage"> Code du message pouvant se trouver dans un fichier ressource. </param>
        /// <param name="description"> Message de la règle de validation. </param>
        /// <param name="severite"> Spécifie la sévérité de l'erreur de validation. </param>
        /// <param name="minValue"> Valeur minimale du décimal. </param>
        /// <param name="allowNull"> Est-ce que la validation passera si le champ est Null. </param>
        public RuleDecimalMinValue(string codeMessage, string description, RuleSeverity severite, decimal minValue,
            bool allowNull)
            : base(codeMessage, description, severite, RuleType.DecimalMinValue, minValue, null, null, allowNull)
        {
        }
    }

    /// <summary>
    ///     Validation d'un nombre double sur sa valeur maximale.
    /// </summary>
    [DataContract]
    public class RuleDoubleMaxValue : Rule
    {
        /// <summary>
        ///     Validation d'un nombre double sur sa valeur maximale.
        /// </summary>
        /// <param name="codeMessage"> Code du message pouvant se trouver dans un fichier ressource. </param>
        /// <param name="description"> Message de la règle de validation. </param>
        /// <param name="severite"> Spécifie la sévérité de l'erreur de validation. </param>
        /// <param name="maxValue"> Valeur maximale du double. </param>
        /// <param name="allowNull"> Est-ce que la validation passera si le champ est Null. </param>
        public RuleDoubleMaxValue(string codeMessage, string description, RuleSeverity severite, double maxValue,
            bool allowNull)
            : base(codeMessage, description, severite, RuleType.DoubleMaxValue, maxValue, null, null, allowNull)
        {
        }
    }

    /// <summary>
    ///     Validation d'un nombre double sur sa valeur minimale.
    /// </summary>
    [DataContract]
    public class RuleDoubleMinValue : Rule
    {
        /// <summary>
        ///     Validation d'un nombre double sur sa valeur minimale.
        /// </summary>
        /// <param name="codeMessage"> Code du message pouvant se trouver dans un fichier ressource. </param>
        /// <param name="description"> Message de la règle de validation. </param>
        /// <param name="severite"> Spécifie la sévérité de l'erreur de validation. </param>
        /// <param name="minValue"> Valeur minimale du double. </param>
        /// <param name="allowNull"> Est-ce que la validation passera si le champ est Null. </param>
        public RuleDoubleMinValue(string codeMessage, string description, RuleSeverity severite, double minValue,
            bool allowNull)
            : base(codeMessage, description, severite, RuleType.DoubleMinValue, minValue, null, null, allowNull)
        {
        }
    }
}