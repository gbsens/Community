using MKS.Core.Resources;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace MKS.Core
{
    /// <summary>
    ///   Cette classe contient l'ensemble des règles les plus communes.
    /// </summary>
    /// <remarks>
    ///   Le nom des propriétés et champs sont case sensitive. Les règles communes étant
    ///   constamment mises à jour, s'assurer d'avoir la dernière version du framework
    /// </remarks>
    public static class CommonRules
    {
        #region AlphaNumerique

        private static readonly Regex _regexAlphanumericCharacters =
            new Regex(RuleAlphanumericCharacters.RegexValidation);

        internal static bool AlphanumericCharacters(object target, string propertyName, bool allowNull)
        {
            var value = (string)Utilities.CallByName(target, propertyName, CallType.Get);

            if (allowNull && string.IsNullOrEmpty(value))
                return true;
            return AlphanumericCharacters(value);
        }

        /// <summary>
        ///   Valide qu'une chaine de caractères AlphanumericCharacters.
        /// </summary>
        /// <param name="value"> Chaine contenant AlphanumericCharacters à valider. </param>
        /// <returns> Si la condition est respectée ou non </returns>
        public static bool AlphanumericCharacters(string value)
        {
            return value != null && _regexAlphanumericCharacters.IsMatch(value);
        }

        #endregion AlphaNumerique

        #region StrongPassword

        private static readonly Regex _regexStrongPassword = new Regex(RuleStrongPassword.RegexValidation);

        internal static bool StrongPassword(object target, string propertyName, bool allowNull)
        {
            var value = (string)Utilities.CallByName(target, propertyName, CallType.Get);

            if (allowNull && string.IsNullOrEmpty(value))
                return true;
            return StrongPassword(value);
        }

        /// <summary>
        ///   Valide qu'une chaine de caractères AlphanumericCharacters.
        /// </summary>
        /// <param name="value"> Chaine contenant AlphanumericCharacters à valider. </param>
        /// <returns> Si la condition est respectée ou non </returns>
        public static bool StrongPassword(string value)
        {
            return value != null && _regexStrongPassword.IsMatch(value);
        }

        #endregion StrongPassword

        #region FloatingNumber

        private static readonly Regex _regexFloatingNumber = new Regex(RuleFloatingNumber.RegexValidation);

        internal static bool FloatingNumber(object target, string propertyName, bool allowNull)
        {
            var value = (string)Utilities.CallByName(target, propertyName, CallType.Get);

            if (allowNull && string.IsNullOrEmpty(value))
                return true;
            return FloatingNumber(value);
        }

        /// <summary>
        ///   Valide qu'une chaine de caractères AlphanumericCharacters.
        /// </summary>
        /// <param name="value"> Chaine contenant AlphanumericCharacters à valider. </param>
        /// <returns> Si la condition est respectée ou non </returns>
        public static bool FloatingNumber(string value)
        {
            return value != null && _regexFloatingNumber.IsMatch(value);
        }

        #endregion FloatingNumber

        #region ObjectRequired

        /// <summary>
        ///   Vérifie qu'une propriété ou une méthode d'un objet n'est pas nulle.
        /// </summary>
        /// <remarks>
        ///   Pour utiliser cette règle, il faut créer une règle de validation
        ///   <para> <c>IValidation</c> </para>
        ///   <para> personalisée qui implémente cette règle commune. </para>
        /// </remarks>
        /// <param name="target"> Objet à valider </param>
        /// <param name="PropertyName"> Nom de la propriété ou de la méthode de l'objet à valider </param>
        /// <returns> Retourne Vrai si la valeur retournée par la méthode de l'objet est non nulle ou non vide, retourne Faux dans le cas contraire </returns>
        internal static bool ObjectRequired(object target, string propertyName)
        {
            object value = Utilities.CallByName(target, propertyName, CallType.Get);
            return ObjectRequired(value);
        }

        /// <summary>
        ///   Vérifie qu'un objet n'est pas null
        /// </summary>
        /// <param name="value"> L'objet à vérifier </param>
        /// <returns> Si l'objet est null ou non </returns>
        public static bool ObjectRequired(object value)
        {
            if (value == null)
            {
                return false;
            }
            return true;
        }

        #endregion ObjectRequired

        #region StringRequired

        /// <summary>
        ///   Vérifie qu'une propriété ou une méthode d'un objet n'est pas vide ou nulle.
        /// </summary>
        /// <remarks>
        ///   Pour utiliser cette règle, il faut créer une règle de validation
        ///   <para> <c>IValidation</c> </para>
        ///   <para> personalisée qui implémente cette règle commune. </para>
        /// </remarks>
        /// <param name="target"> Objet à valider </param>
        /// <param name="PropertyName"> Nom de la propriété ou de la méthode de l'objet à valider </param>
        /// <returns> Retourne Vrai si la valeur retournée par la méthode de l'objet est non nulle ou non vide, retourne Faux dans le cas contraire </returns>
        /// <example>
        ///   <code>public void main()
        ///     {
        ///     anObject myObject = new anObject;
        ///     myObject.text = string.null;
        ///     myObject.Name = &quot;my ObjectType&quot;;
        ///     //returns false
        ///     bool FalseBool = StringRequired(myObject, &quot;text&quot;);
        ///     //returns true
        ///     bool TrueBool  = StringRequired(myObject, &quot;Name&quot;);
        ///     }</code>
        /// </example>
        internal static bool StringRequired(object target, string propertyName)
        {
            Object value = Utilities.CallByName(target, propertyName, CallType.Get);
            if (value == null) return false;
            return StringRequired(value.ToString());
        }

        /// <summary>
        ///   Vérifie qu'une chaine de caractères n'est pas vide ou nulle.
        /// </summary>
        /// <param name="value"> La valeur à vérifier </param>
        /// <returns> Si le string est vide ou non </returns>
        public static bool StringRequired(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return false;
            }
            return true;
        }

        #endregion StringRequired

        #region StringMaxLength

        /// <summary>
        ///   Vérifie que le nombre de caractères contenus dans une propriété ou une méthode
        ///   d'un objet ne dépasse pas la valeur indiquée dans MaxLength.
        /// </summary>
        /// <remarks>
        ///   Pour utiliser cette règle, il faut créer une règle de validation
        ///   <para> <c>IValidation</c> </para>
        ///   <para> personalisée qui implémente cette règle commune. </para>
        /// </remarks>
        /// <param name="target"> Objet à valider </param>
        /// <param name="PropertyName"> nom de la propriété ou champs de l'objet sur lequel la règle doit être appliquée </param>
        /// <param name="maxLength"> Nombre de caractères qui ne doit pas être dépassé </param>
        /// <param name="allowNull"> Permet de passer par dessus la validation si on alloue null </param>
        /// <returns> Retourne Vrai si le nombre de caractères est inférieur ou égal à maxLengh et Faux dans le cas contraire </returns>
        /// <example>
        ///   <code>public void main()
        ///     {
        ///     anObject myObject = new anObject;
        ///     myObject.text = &quot;Hello World!&quot;;
        ///     //returns false
        ///     bool FalseBool = StringMaxLength(myObject, &quot;text&quot;, 4);
        ///     //returns true
        ///     bool TrueBool  = StringMaxLength(myObject, &quot;text&quot;, 12);
        ///     }</code>
        /// </example>
        internal static bool StringMaxLength(object target, string propertyName, int maxLength, bool allowNull)
        {
            var value = (string)Utilities.CallByName(target, propertyName, CallType.Get);
            if (allowNull && string.IsNullOrEmpty(value))
                return true;
            return StringMaxLength(value, maxLength);
        }

        /// <summary>
        ///   Vérifie que la chaine de caractères est d'une longueur inférieur ou égale à la valeur indiquée dans MaxLength
        /// </summary>
        /// <param name="value"> La valeur à vérifier </param>
        /// <param name="maxLength"> La longueur maximale </param>
        /// <returns> Si la condition est respectée ou non </returns>
        public static bool StringMaxLength(string value, int maxLength)
        {
            return (value == null || value.Length <= maxLength);
        }

        #endregion StringMaxLength

        #region StringMinLength

        /// <summary>
        ///   Vérifie que le nombre de caractères contenus dans une propriété ou une méthode
        ///   d'un objet est supérieur ou égal à la valeur indiquée dans minLength
        /// </summary>
        /// <remarks>
        ///   Pour utiliser cette règle, il faut créer une règle de validation
        ///   <para> <c>IValidation</c> </para>
        ///   <para> personalisée qui implémente cette règle commune. </para>
        /// </remarks>
        /// <param name="target"> Objet à valider </param>
        /// <param name="PropertyName"> nom de la propriété ou champs de l'objet sur lequel la règle doit être appliquée </param>
        /// <param name="allowNull"> Permet de passer par dessus la validation si on alloue null </param>
        /// <param name="minLength"> Nombre de caractères minimum accepté </param>
        /// <returns> Retourne Vrai si le nombre de caractères est supérieur ou égal à minLengh, Faux dans le cas contraire </returns>
        /// <example>
        ///   <code>public void main()
        ///     {
        ///     anObject myObject = new anObject;
        ///     myObject.text = &quot;Hello World!&quot;;
        ///     //returns false
        ///     bool FalseBool = StringMinLength(myObject, &quot;text&quot;, 14);
        ///     //returns true
        ///     bool TrueBool  = StringMinLength(myObject, &quot;text&quot;, 6);
        ///     }</code>
        /// </example>
        internal static bool StringMinLength(object target, string propertyName, int minLength, bool allowNull)
        {
            var value = (string)Utilities.CallByName(target, propertyName, CallType.Get);
            if (allowNull && string.IsNullOrEmpty(value))
                return true;
            return StringMinLength(value, minLength);
        }

        /// <summary>
        ///   Vérifie que la chaine de caractères est d'une longueur supérieure ou égale à la valeur indiquée dans minLength
        /// </summary>
        /// <param name="value"> La valeur à vérifier </param>
        /// <param name="minLength"> La longueur minimale </param>
        /// <returns> Si la condition est respectée ou non </returns>
        public static bool StringMinLength(string value, int minLength)
        {
            return (value != null && value.Length >= minLength);
        }

        #endregion StringMinLength

        #region IntegerMaxValue

        /// <summary>
        ///   Vérifie que la valeur (integer) d'une propriété ou d'une méthode d'un objet est
        ///   inférieure ou égale à la valeur indiquée dans maxLength
        /// </summary>
        /// <remarks>
        ///   Pour utiliser cette règle, il faut créer une règle de validation
        ///   <para> <c>IValidation</c> </para>
        ///   <para> personalisée qui implémente cette règle commune. </para>
        /// </remarks>
        /// <param name="target"> Objet à valider </param>
        /// <param name="PropertyName"> nom de la propriété ou champs de l'objet sur lequel la règle doit être appliquée </param>
        /// <param name="maxLength"> Valeur maximale acceptée </param>
        /// <param name="allowNull"> Permet de passer par dessus la validation si on alloue null </param>
        /// <returns> Retourne Vrai si la valeur de la méthode ou de la propriété est inférieure ou égale à la valeur de maxLengh, Faux dans le cas contraire </returns>
        /// <example>
        ///   <code>public void main()
        ///     {
        ///     anObject myObject = new anObject;
        ///     myObject.temperature = 135&quot;;
        ///     //returns false
        ///     bool FalseBool = IntegerMaxValue(myObject, &quot;temperature&quot;, 100);
        ///     //returns true
        ///     bool TrueBool  = IntegerMaxValue(myObject, &quot;temperature&quot;, 150);
        ///     }</code>
        /// </example>
        internal static bool IntegerMaxValue(object target, string propertyName, int maxLength, bool allowNull)
        {
            int value;
            Object valueTemp = (Utilities.CallByName(target, propertyName, CallType.Get));
            if (allowNull && valueTemp == null)
                return true;
            if (allowNull == false && valueTemp == null)
                return false;
            if (int.TryParse(valueTemp.ToString(), out value))
                return IntegerMaxValue(value, maxLength);
            return false;
        }

        /// <summary>
        ///   Vérifie que la valeur (integer) est inférieure ou égale à la valeur indiquée dans maxLength
        /// </summary>
        /// <param name="value"> Valeur à vérifier. </param>
        /// <param name="maxLength"> Valeur maximale de l'entier. </param>
        /// <returns> Si la condition est respectée ou non </returns>
        public static bool IntegerMaxValue(int value, int maxLength)
        {
            if (value > maxLength)
            {
                return false;
            }
            return true;
        }

        #endregion IntegerMaxValue

        #region IntegerMinValue

        /// <summary>
        ///   Vérifie que la valeur (integer) d'une propriété ou d'une méthode d'un objet est
        ///   superieure ou égale à la valeur indiquée dans minLength
        /// </summary>
        /// <remarks>
        ///   Pour utiliser cette règle, il faut créer une règle de validation
        ///   <para> <c>IValidation</c> </para>
        ///   <para> personalisée qui implémente cette règle commune. </para>
        /// </remarks>
        /// <param name="target"> Objet à valider </param>
        /// <param name="propertyName"> nom de la propriété ou champs de l'objet sur lequel la règle doit être appliquée </param>
        /// <param name="minLength"> Valeur minimale que peut contenir la propriété ou méthode testée </param>
        /// <param name="allowNull"> Permet de passer par dessus la validation si on alloue null </param>
        /// <returns> Retourne Vrai si la valeur de la méthode ou de la propriété est superieure ou égale à la valeur de minLength, Faux dans le cas contraire </returns>
        /// <example>
        ///   <code>public void main()
        ///     {
        ///     anObject myObject = new anObject;
        ///     myObject.temperature = 135&quot;;
        ///     //returns false
        ///     bool FalseBool = IntegerMinValue(myObject, &quot;temperature&quot;, 150);
        ///     //returns true
        ///     bool TrueBool  = IntegerMinValue(myObject, &quot;temperature&quot;, 100);
        ///     }</code>
        /// </example>
        internal static bool IntegerMinValue(object target, string propertyName, int minLength, bool allowNull)
        {
            int value;
            Object valueTemp = (Utilities.CallByName(target, propertyName, CallType.Get));
            if (allowNull && valueTemp == null)
                return true;
            if (allowNull == false && valueTemp == null)
                return false;
            if (int.TryParse(valueTemp.ToString(), out value))
                return IntegerMinValue(value, minLength);
            return false;
        }

        /// <summary>
        ///   Vérifie que la valeur (integer) est supérieure ou égale à la valeur indiquée dans minLength
        /// </summary>
        /// <param name="value"> Valeur à vérifier. </param>
        /// <param name="minLength"> Valeur minimale de l'entier. </param>
        /// <returns> Si la condition est respectée ou non </returns>
        public static bool IntegerMinValue(int value, int minLength)
        {
            if (value < minLength)
            {
                return false;
            }
            return true;
        }

        #endregion IntegerMinValue

        #region DateFormat

        /// <summary>
        ///   Vérifie que le texte contenu dans une propriété ou une méthode d'un objet
        ///   correspond au format date indiqué.
        /// </summary>
        /// <param name="target"> Objet à valider </param>
        /// <param name="PropertyName"> nom de la propriété ou champs de l'objet sur lequel la règle doit être appliquée. </param>
        /// <param name="format"> Format de date qui doit être reconnu </param>
        /// <param name="allowNull"> Permet de passer par dessus la validation si on alloue null </param>
        /// <returns> Retourne Vrai si le texte de la méthode est une date au format indiqué dans le paramètre <i>format</i> , retourne Faux autrement </returns>
        /// <example>
        ///   <code>public void main()
        ///     {
        ///     anObject myObject = new anObject;
        ///     myObject.text = &quot;22/10/2009&quot;;
        ///     // returns False
        ///     bool FalseBool = DateFormat(myObject, &quot;text&quot;, &quot;mm/dd/yy&quot;);
        ///     // returns True
        ///     bool FalseBool = DateFormat(myObject, &quot;text&quot;, &quot;dd/mm/yyyy&quot;);
        ///     }</code>
        /// </example>
        public static bool DateFormat(object target, string propertyName, String format, bool allowNull)
        {
            object value = Utilities.CallByName(target, propertyName, CallType.Get);

            if (value != null)
            {
                return DateFormat(value.ToString(), format);
            }
            return allowNull;
        }

        /// <summary>
        ///   Vérifie que la chaine de caractères représente une date au format spécifié dans format
        /// </summary>
        /// <param name="value"> Chaine à vérifier. </param>
        /// <param name="format"> Format de date à respecter. </param>
        /// <returns> Si la condition est respectée ou non </returns>
        public static bool DateFormat(string value, String format)
        {
            try
            {
                var dtfi = new DateTimeFormatInfo { ShortDatePattern = format };

                DateTime dt = DateTime.ParseExact(value, format, new CultureInfo("en-US"));
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        #endregion DateFormat

        #region RegularExpression

        /// <summary>
        ///   Vérifie que le contenu d'une méthode ou d'une propriété d'un objet correspond à
        ///   une expression régulière (RegEx)
        /// </summary>
        /// <param name="target"> Objet à valider </param>
        /// <param name="PropertyName"> Propriété ou méthode de l'objet dont le contenu doit être vérifié </param>
        /// <param name="regularexpression"> Expression régulière à vérifier </param>
        /// <param name="allowNull"> Permet de passer par dessus la validation si on alloue null </param>
        /// <returns> Retourne Vrai si le contenu de la méthode ou de la propriété correspond à l'expression régulière </returns>
        /// <example>
        ///   <code>public static void main()
        ///     {
        ///     anObject myObject = new anObject;
        ///     myObject.tel  = &quot;1-514-555-6699&quot;;
        ///     myObject.fax = &quot;+425 336 789 556&quot;;</code>
        ///   <para> </para>
        ///   <code>//Returns False
        ///     bool FalseBool = RegularExpression(myObject, &quot;fax&quot;,&quot;1-\d{3}-\d{3}-\d{4}&quot;);
        ///     //Returns True
        ///     bool TrueBool = RegularExpression(myObject, &quot;tel&quot;,&quot;1-\d{3}-\d{3}-\d{4}&quot;);
        ///     }</code>
        ///   <para> </para>
        /// </example>
        /// <seealso cref="System.Text.RegularExpressions.Regex">Regular
        ///   Expressions</seealso>
        internal static bool RegularExpression(object target, string propertyName, String regularexpression,
                                               bool allowNull)
        {
            var value = (string)Utilities.CallByName(target, propertyName, CallType.Get);

            if (allowNull && string.IsNullOrEmpty(value))
                return true;
            return RegularExpression(value, regularexpression);
        }

        /// <summary>
        ///   Valide qu'une chaine de caractères représente une expression régulière.
        /// </summary>
        /// <param name="value"> Chaine devant respecter l'expression régulière. </param>
        /// <param name="regularexpression"> Expression régulière. </param>
        /// <returns> Si la condition est respectée ou non </returns>
        public static bool RegularExpression(string value, String regularexpression)
        {
            var objNotNaturalPattern = new Regex(regularexpression);
            try
            {
                if (!objNotNaturalPattern.IsMatch(value))
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        #endregion RegularExpression

        #region CodePostal

        private static readonly Regex _regexCodePostal = new Regex(RulePostalCode.RegexValidation);
        private static readonly Regex _regexCodePostalUS = new Regex(RulePostalCodeUS.RegexValidation);
        private static readonly Regex _regexCodePostalCAN = new Regex(RulePostalCodeCAN.RegexValidation);

        /// <summary>
        ///   Vérifie que le contenu d'une méthode ou d'une propriété d'un objet est au format
        ///   d'un code postal.
        /// </summary>
        /// <remarks>
        ///   Un code postal est sous la forme A1A 1A1
        /// </remarks>
        /// <param name="target"> Objet à valider </param>
        /// <param name="PropertyName"> Nom de la propriété ou de la méthode de l'objet à valider. </param>
        /// <param name="allowNull"> Permet de passer par dessus la validation si on alloue null </param>
        /// <returns> Retourne Vrai si la méthode ou la propriété testée correspond à un code postal, retourne Faux autrement. </returns>
        /// <example>
        ///   <code>public static void main()
        ///     {
        ///     anObject myObject = new anObject;
        ///     myObject.CP1 = &quot;G6S 1J2&quot;;
        ///     myObject.CP2= &quot;HH7 803&quot;;</code>
        ///   <para> </para>
        ///   <code>//Returns True
        ///     bool TrueBool = CodePostal(myObject, &quot;CP1&quot;);
        ///     //Returns False
        ///     TrueBool = CodePostal(myObject, &quot;CP2&quot;);</code>
        /// </example>
        public static bool CodePostal(object target, string propertyName, bool allowNull)
        {
            var value = (string)Utilities.CallByName(target, propertyName, CallType.Get);

            if (allowNull && string.IsNullOrEmpty(value))
                return true;
            return CodePostal(value);
        }

        /// <summary>
        ///   Valide qu'une chaine de caractères représente un code postal.
        /// </summary>
        /// <param name="value"> Chaine contenant le code postal à valider. </param>
        /// <returns> Si la condition est respectée ou non </returns>
        public static bool CodePostal(string value)
        {
            return value != null && _regexCodePostal.IsMatch(value);
        }

        /// <summary>
        ///   Valide qu'une chaine de caractères représente un code postal.
        /// </summary>
        /// <param name="value"> Chaine contenant le code postal à valider. </param>
        /// <returns> Si la condition est respectée ou non </returns>
        public static bool CodePostalUS(string value)
        {
            return value != null && _regexCodePostalUS.IsMatch(value);
        }

        /// <summary>
        ///   Valide qu'une chaine de caractères représente un code postal.
        /// </summary>
        /// <param name="value"> Chaine contenant le code postal à valider. </param>
        /// <returns> Si la condition est respectée ou non </returns>
        public static bool CodePostalCAN(string value)
        {
            return value != null && _regexCodePostalCAN.IsMatch(value);
        }

        #endregion CodePostal

        #region PhoneNumber

        private static readonly Regex _regexPhoneNumber = new Regex(RulePhoneNumber.RegexValidation);

        private static readonly Regex _regexPhoneNumberExtention =
            new Regex(RulePhoneNumberWithExtention.RegexValidation);

        /// <summary>
        ///   Valide qu'un numéro de téléphone à le format : 123 456-7890
        /// </summary>
        /// <param name="target"> Objet à valider </param>
        /// <param name="PropertyName"> Nom de la propriété ou de la méthode de l'objet à valider. </param>
        /// <param name="allowNull"> Permet de passer par dessus la validation si on alloue null </param>
        /// <returns> retourne vrai si le numéro de téléphone est valide </returns>
        internal static bool PhoneNumber(object target, string propertyName, bool allowNull)
        {
            var value = (string)Utilities.CallByName(target, propertyName, CallType.Get);
            if (allowNull && string.IsNullOrEmpty(value))
                return true;
            return PhoneNumber(value);
        }

        /// <summary>
        ///   Valide qu'une chaine de caractères représente un numéro de téléphone.
        /// </summary>
        /// <param name="value"> Chaine contenant le numéro de téléphone à valider. </param>
        /// <returns> Si la condition est respectée ou non </returns>
        public static bool PhoneNumber(string value)
        {
            try
            {
                if (!_regexPhoneNumber.IsMatch(value))
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        ///   Valide qu'un numéro de téléphone à le format : 123 456-7890
        /// </summary>
        /// <param name="target"> Objet à valider </param>
        /// <param name="PropertyName"> Nom de la propriété ou de la méthode de l'objet à valider. </param>
        /// <param name="allowNull"> Permet de passer par dessus la validation si on alloue null </param>
        /// <returns> retourne vrai si le numéro de téléphone est valide </returns>
        internal static bool PhoneNumberWithExtention(object target, string propertyName, bool allowNull)
        {
            var value = (string)Utilities.CallByName(target, propertyName, CallType.Get);
            if (allowNull && string.IsNullOrEmpty(value))
                return true;
            return PhoneNumberWithExtention(value);
        }

        /// <summary>
        ///   Valide qu'une chaine de caractères représente un numéro de téléphone avec extension.
        /// </summary>
        /// <param name="value"> Chaine contenant le numéro de téléphone à valider. </param>
        /// <returns> Si la condition est respectée ou non </returns>
        public static bool PhoneNumberWithExtention(string value)
        {
            try
            {
                if (!_regexPhoneNumberExtention.IsMatch(value))
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        #endregion PhoneNumber

        #region emailAddress

        private static readonly Regex _regexEmailValidation = new Regex(RuleEmailAddress.RegexValidation);

        /// <summary>
        ///   Valide qu'une adresse de courriel est valide
        /// </summary>
        /// <param name="target"> Objet à valider </param>
        /// <param name="PropertyName"> Nom de la propriété ou de la méthode de l'objet à valider. </param>
        /// <param name="allowNull"> Permet de passer par dessus la validation si on alloue null </param>
        /// <returns> retourne vrai si l'adresse de courriel est valide </returns>
        internal static bool EmailAddress(object target, string propertyName, bool allowNull)
        {
            var value = (string)Utilities.CallByName(target, propertyName, CallType.Get);
            if (allowNull && string.IsNullOrEmpty(value))
                return true;
            return EmailAddress(value);
        }

        /// <summary>
        ///   Valide qu'une chaine de caractères représente une adresse de courriel.
        /// </summary>
        /// <param name="value"> Chaine contenant le courriel à valider. </param>
        /// <returns> retourne vrai si la chaine représente une adresse courriel </returns>
        public static bool EmailAddress(string value)
        {
            try
            {
                if (!_regexEmailValidation.IsMatch(value))
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        #endregion emailAddress

        #region DateGreaterThan

        /// <summary>
        ///   Vérifie que la date contenue dans une méthode ou une propriété d'un objet est
        ///   supérieure à une date donnée.
        /// </summary>
        /// <param name="target"> Objet à valider </param>
        /// <param name="PropertyName"> Nom de la propriété ou de la méthode de l'objet dans laquelle se trouve la date à comparer </param>
        /// <param name="dateToCompare"> Date par rapport à laquelle le contenu de la méthode ou de la propriété doit être comparée. </param>
        /// <param name="allowNull"> Permet de passer par dessus la validation si on alloue null </param>
        /// <returns> Retourne Vrai si la date contenue dans la méthode est strictement supérieure à la date de comparaison, retourne False autrement. </returns>
        /// <example>
        ///   <code>public static void main()
        ///     {
        ///     anObject myObject = new anObject;
        ///     myObject.Date1 = new Date(2009, 1, 1);
        ///     myObject.Date2 = new Date(2009, 5, 1);
        ///     myObject.Date3 = new Date(2009, 10, 22);</code>
        ///   <para> </para>
        ///   <code>//Returns False
        ///     bool FalseBool = DateGreaterThan(myObject, &quot;Date1&quot;, new Date(2009, 5, 1));
        ///     FalseBool = DateGreaterThan(myObject, &quot;Date2&quot;, new Date(2009, 5, 1));</code>
        ///   <para> </para>
        ///   <code>//Returns True
        ///     bool TrueBool = DateGreaterThan(myObject, &quot;Date3&quot;, new Date(2009, 5, 1));
        ///     }</code>
        /// </example>
        public static bool DateGreaterThan(object target, string propertyName, DateTime dateToCompare,
                                             bool allowNull)
        {
            var value = (DateTime?)Utilities.CallByName(target, propertyName, CallType.Get);
            if (allowNull && value == null)
                return true;
            if (allowNull == false && value == null)
                return false;
            return DateGreaterThan(value, dateToCompare);
        }

        /// <summary>
        ///   Vérifie que la date est plus grande que la date à laquelle on la compare.
        /// </summary>
        /// <param name="value"> Date à vérifier. </param>
        /// <param name="dateToCompare"> Date de comparaison. </param>
        /// <returns> vrai si la date est plus grande que "dateToCompare". </returns>
        public static bool DateGreaterThan(DateTime? value, DateTime dateToCompare)
        {
            return (value > dateToCompare);
        }

        #endregion DateGreaterThan

        #region DateGreaterOrEqualThan

        ///<summary>
        ///  Vérifie que la date contenue dans une méthode ou une propriété d'un objet est
        ///  supérieure ou égal à une date donnée.
        ///</summary>
        ///<param name="target"> Objet à valider </param>
        ///<param name="PropertyName"> Nom de la propriété ou de la méthode de l'objet dans laquelle se trouve la date à comparer </param>
        ///<param name="dateToCompare"> Date par rapport à laquelle le contenu de la méthode ou de la propriété doit être comparée. </param>
        ///<param name="allowNull"> Permet de passer par dessus la validation si on alloue null </param>
        ///<returns> Retourne Vrai si la date contenue dans la méthode est supérieure ou égale à la date de comparaison, retourne False autrement. </returns>
        ///<example>
        ///  <code>public static void main()
        ///    {
        ///    anObject myObject = new anObject;
        ///    myObject.Date1 = new Date(2009, 1, 1);
        ///    myObject.Date2 = new Date(2009, 5, 1);
        ///    myObject.Date3 = new Date(2009, 10, 22);
        ///
        ///    //Returns False
        ///    bool FalseBool = DateGreaterOrEqualThan(myObject, &quot;Date1&quot;, new Date(2009, 5, 1));
        ///
        ///    //Returns True
        ///    bool TrueBool = DateGreaterOrEqualThan(myObject, &quot;Date2&quot;, new Date(2009, 5, 1));
        ///    TrueBool = DateGreaterOrEqualThan(myObject, &quot;Date3&quot;, new Date(2009, 5, 1));
        ///    }</code>
        ///</example>
        public static bool DateGreaterOrEqualThan(object target, string propertyName, DateTime dateToCompare,
                                                    bool allowNull)
        {
            var value = (DateTime?)Utilities.CallByName(target, propertyName, CallType.Get);
            if (allowNull && value == null)
                return true;
            if (allowNull == false && value == null)
                return false;
            return DateGreaterOrEqualThan(value, dateToCompare);
        }

        /// <summary>
        ///   Vérifie que la date est plus grande ou égale à la date à laquelle on la compare.
        /// </summary>
        /// <param name="value"> Date à vérifier. </param>
        /// <param name="dateToCompare"> Date de comparaison. </param>
        /// <returns> vrai si la date est plus grande ou égale à "dateToCompare". </returns>
        public static bool DateGreaterOrEqualThan(DateTime? value, DateTime dateToCompare)
        {
            return (value >= dateToCompare);
        }

        #endregion DateGreaterOrEqualThan

        #region DateLessThan

        ///<summary>
        ///  Vérifie que la date contenue dans une méthode ou une propriété d'un objet est
        ///  inférieure à une date donnée.
        ///</summary>
        ///<param name="target"> Objet à valider </param>
        ///<param name="PropertyName"> Nom de la propriété ou de la méthode de l'objet dans laquelle se trouve la date à comparer </param>
        ///<param name="dateToCompare"> Date par rapport à laquelle le contenu de la méthode ou de la propriété doit être comparée. </param>
        ///<param name="allowNull"> Permet de passer par dessus la validation si on alloue null </param>
        ///<returns> Retourne Vrai si la date contenue dans la méthode est strictement inférieure à la date de comparaison, retourne False autrement. </returns>
        ///<example>
        ///  <code>public static void main()
        ///    {
        ///    anObject myObject = new anObject;
        ///    myObject.Date1 = new Date(2009, 1, 1);
        ///    myObject.Date2 = new Date(2009, 5, 1);
        ///    myObject.Date3 = new Date(2009, 10, 22);
        ///
        ///    //Returns False
        ///    bool FalseBool = DateLessThan(myObject, &quot;Date2&quot;, new Date(2009, 5, 1));
        ///    FalseBool = DateLessThan(myObject, &quot;Date3&quot;, new Date(2009, 5, 1));</code>
        ///  <para> </para>
        ///  <code>//Returns True
        ///    bool TrueBool = DateLessThan(myObject, &quot;Date1&quot;, new Date(2009, 5, 1));
        ///    }</code>
        ///</example>
        public static bool DateLessThan(object target, string propertyName, DateTime dateToCompare, bool allowNull)
        {
            var value = (DateTime?)Utilities.CallByName(target, propertyName, CallType.Get);
            if (allowNull && value == null)
                return true;
            if (allowNull == false && value == null)
                return false;
            return DateLessThan(value, dateToCompare);
        }

        /// <summary>
        ///   Vérifie que la date est plus petite que la date à laquelle on la compare.
        /// </summary>
        /// <param name="value"> Date à vérifier. </param>
        /// <param name="dateToCompare"> Date de comparaison. </param>
        /// <returns> vrai si la date est plus petite que "dateToCompare". </returns>
        public static bool DateLessThan(DateTime? value, DateTime dateToCompare)
        {
            return (value < dateToCompare);
        }

        #endregion DateLessThan

        #region DateLessOrEqualThan

        ///<summary>
        ///  Vérifie que la date contenue dans une méthode ou une propriété d'un objet est
        ///  inférieure ou égale à une date donnée.
        ///</summary>
        ///<param name="target"> Objet à valider </param>
        ///<param name="PropertyName"> Nom de la propriété ou de la méthode de l'objet dans laquelle se trouve la date à comparer </param>
        ///<param name="dateToCompare"> Date par rapport à laquelle le contenu de la méthode ou de la propriété doit être comparée. </param>
        ///<param name="allowNull"> Permet de passer par dessus la validation si on alloue null </param>
        ///<returns> Retourne Vrai si la date contenue dans la méthode est inférieure ou égale à la date de comparaison, retourne False autrement. </returns>
        ///<example>
        ///  <code>public static void main()
        ///    {
        ///    anObject myObject = new anObject;
        ///    myObject.Date1 = new Date(2009, 1, 1);
        ///    myObject.Date2 = new Date(2009, 5, 1);
        ///    myObject.Date3 = new Date(2009, 10, 22);
        ///
        ///    //Returns True
        ///    bool TrueBool = DateLessOrEqualThan(myObject, &quot;Date1&quot;, new Date(2009, 5, 1));
        ///    TrueBool = DateLessOrEqualThan(myObject, &quot;Date2&quot;, new Date(2009, 5, 1));</code>
        ///  <para> </para>
        ///  <code>//Returns False
        ///    bool FalseBool = DateLessOrEqualThan(myObject, &quot;Date3&quot;, new Date(2009, 5, 1));
        ///    }</code>
        ///</example>
        public static bool DateLessOrEqualThan(object target, string propertyName, DateTime dateToCompare,
                                                 bool allowNull)
        {
            var value = (DateTime?)Utilities.CallByName(target, propertyName, CallType.Get);
            if (allowNull && value == null)
                return true;
            if (allowNull == false && value == null)
                return false;
            return DateLessOrEqualThan(value, dateToCompare);
        }

        /// <summary>
        ///   Vérifie que la date est plus petite ou égale à la date à laquelle on la compare.
        /// </summary>
        /// <param name="value"> Date à vérifier. </param>
        /// <param name="dateToCompare"> Date de comparaison. </param>
        /// <returns> vrai si la date est plus petite ou égale à "dateToCompare". </returns>
        public static bool DateLessOrEqualThan(DateTime? value, DateTime dateToCompare)
        {
            return (value <= dateToCompare);
        }

        #endregion DateLessOrEqualThan

        #region DateRequired

        /// <summary>
        ///   Vérifie que l'objet de type DateTime n'est pas null et qu'il contient une date
        /// </summary>
        /// <param name="target"> Objet à valider </param>
        /// <param name="PropertyName"> Nom de la propriété ou de la méthode de l'objet dans laquelle se trouve la date à comparer </param>
        /// <param name="allowNull"> Permet de passer par dessus la validation si on alloue null </param>
        /// <returns> Booléen indiquant si l'objet DateTime est non-null et non-vide </returns>
        public static bool DateRequired(object target, string propertyName, bool allowNull)
        {
            var datevide = new DateTime();
            var value = (DateTime?)Utilities.CallByName(target, propertyName, CallType.Get);
            if (allowNull && value == null)
                return true;
            if (allowNull == false && value == null)
                return false;
            if (value == datevide)
                return false;
            return true;
        }

        #endregion DateRequired

        #region Luhn

        /// <summary>
        ///   Vérifier le chiffre preuve dans une chaîne selon la formule Luhn
        ///   La chaîne doit comprendre le chiffre preuve
        /// </summary>
        /// <param name="target"> Objet à valider </param>
        /// <param name="PropertyName"> nom de la propriété à valider </param>
        /// <param name="allowNull"> Permet de passer par dessus la validation si on alloue null </param>
        /// <returns> Booléen indiquant s'il s'agit d'une chaîne de caractère ce terminant par un nombre de validation valide selon la formule de Luhn </returns>
        public static bool Luhn(object target, string propertyName, bool allowNull)
        {
            var value = (string)Utilities.CallByName(target, propertyName, CallType.Get);
            if (allowNull && string.IsNullOrEmpty(value))
                return true;
            return Core.Luhn.Validate(value);
        }

        #endregion Luhn

        #region ListNotEmpty

        /// <summary>
        ///   Vérifie qu'une liste n'est pas vide
        /// </summary>
        /// <param name="target"> Objet à valider </param>
        /// <param name="PropertyName"> nom de la propriété à valider </param>
        /// <param name="allowNull"> Permet de passer par dessus la validation si on alloue null </param>
        /// <returns> Booléen indiquant s'il s'agit d'une chaîne de caractère ce terminant par un nombre de validation valide selon la formule de Luhn </returns>
        public static bool ListNotEmpty(object target, string propertyName, bool allowNull)
        {
            Object value = Utilities.CallByName(target, propertyName, CallType.Get);
            Boolean Succes;

            if (allowNull && value == null) //si on permet les null et que la valeur est null
            {
                Succes = true;
            }
            else if (value == null) //Si la valeur est null la règle n'est pas valide
            {
                Succes = false;
            }
            else
            {
                IEnumerable<Type> listGeneric = value.GetType().GetInterfaces().Where(x => x.Name == "IList");
                if (listGeneric.Any())
                {
                    Succes = ((IList)value).Count > 0;
                }
                else //Si ce n'est pas un type list --> return false
                {
                    Succes = false;
                }
            }
            return Succes;
        }

        #endregion ListNotEmpty

        #region ObjectContainsOneValidProperty

        /// <summary>
        ///   Cette méthode permet de vérifier qu'un objet a au moins une de ses propriétés qui n'est pas null ou à sa valeur par défaut.
        ///   Exemple : Un objet ne possède pas que des string à null ou étant vides ou ses propriétés numériques ne sont pas à 0.
        /// </summary>
        /// <param name="target"> L'objet à valider </param>
        /// <param name="listPropertyName"> La liste des noms des propriétés à valider </param>
        /// <param name="allowNull"> Si l'objet à valider peut être à null </param>
        /// <returns> True si la liste contient un élément de valide, false sinon </returns>
        public static bool ObjectContainsOneValidProperty(object target, List<string> listPropertyName, bool allowNull)
        {
            //Si l'objet peut être à null et qu'il l'est
            if (allowNull && target == null)
                return true;

            foreach (string propertyName in listPropertyName)
            {
                PropertyInfo propertyInfo = target.GetType().GetProperty(propertyName);

                //Si la propriété n'existe pas
                if (propertyInfo == null)
                    throw new MissingMemberException(string.Format(CoreResources.EX0027, propertyName));

                object value = propertyInfo.GetValue(target, null);

                //Si la valeur est déjà à null... pourquoi faire autre chose?
                if (value != null)
                {
                    Type typePropertyInfo = value.GetType();

                    //On fait des vérifications pour chaque type
                    //IsNullOrEmpty pour un string
                    if (typePropertyInfo == typeof(String))
                    {
                        if (!string.IsNullOrEmpty((string)propertyInfo.GetValue(target, null)))
                            return true;
                    }
                    else if (typePropertyInfo == typeof(Int16) || //Plus grand que 0 pour les types int
                             typePropertyInfo == typeof(Int32) ||
                             typePropertyInfo == typeof(Int64) ||
                             typePropertyInfo == typeof(UInt16) ||
                             typePropertyInfo == typeof(UInt32) ||
                             typePropertyInfo == typeof(UInt64))
                    {
                        if (Convert.ToInt32(propertyInfo.GetValue(target, null)) > 0)
                            return true;
                    }
                    else if (typePropertyInfo == typeof(Decimal) || //Plus grand que 0 pour les types à virgule
                             typePropertyInfo == typeof(Double) ||
                             typePropertyInfo == typeof(float))
                    {
                        if (Convert.ToDecimal(propertyInfo.GetValue(target, null)) > 0)
                            return true;
                    }
                    else if (typePropertyInfo == typeof(SByte) || //Plus grand que 0 pour les entiers sur 8 bits
                             typePropertyInfo == typeof(Byte) ||
                             typePropertyInfo == typeof(Char))
                    {
                        if (Convert.ToChar(propertyInfo.GetValue(target, null)) > 0)
                            return true;
                    }
                    else
                        return true;
                }
            }

            return false;
        }

        #endregion ObjectContainsOneValidProperty

        #region StringExactLength

        /// <summary>
        ///   Vérifie que le nombre de caractères contenus dans une propriété ou une méthode
        ///   d'un objet correspond à la valeur indiquée dans l'ExactLength.
        /// </summary>
        /// <remarks>
        ///   Pour utiliser cette règle, il faut créer une règle de validation
        ///   <para> <c>IValidation</c> </para>
        ///   <para> personalisée qui implémente cette règle commune. </para>
        /// </remarks>
        /// <param name="target"> Objet à valider </param>
        /// <param name="PropertyName"> nom de la propriété ou champs de l'objet sur lequel la règle doit être appliquée </param>
        /// <param name="exactLength"> Nombre exact de caractères </param>
        /// <param name="allowNull"> Permet de passer par dessus la validation si on alloue null </param>
        /// <returns> Retourne Vrai si le nombre de caractères est égal à exactLength et Faux dans le cas contraire </returns>
        /// <example>
        ///   <code>public void main()
        ///     {
        ///     anObject myObject = new anObject;
        ///     myObject.text = &quot;Hello World!&quot;;
        ///     //returns false
        ///     bool FalseBool = StringExactLength(myObject, &quot;text&quot;, 4);
        ///     //returns true
        ///     bool TrueBool  = StringExactLength(myObject, &quot;text&quot;, 12);
        ///     }</code>
        /// </example>
        internal static bool StringExactLength(object target, string propertyName, int exactLength, bool allowNull)
        {
            var value = (string)Utilities.CallByName(target, propertyName, CallType.Get);
            if (allowNull && string.IsNullOrEmpty(value))
                return true;
            return StringExactLength(value, exactLength);
        }

        /// <summary>
        ///   Vérifie que la chaîne de caractères est d'une longueur égale à la valeur indiquée dans exactLength
        /// </summary>
        /// <param name="value"> La valeur à vérifier </param>
        /// <param name="exactLength"> La longueur exact </param>
        /// <returns> Si la condition est respectée ou non </returns>
        public static bool StringExactLength(string value, int exactLength)
        {
            return (value == null || value.Length == exactLength);
        }

        #endregion StringExactLength

        #region DecimalMaxValue

        /// <summary>
        ///   Vérifie que la valeur (decimal) d'une propriété ou d'une méthode d'un objet est
        ///   inférieure ou égale à la valeur indiquée dans maxLength
        /// </summary>
        /// <remarks>
        ///   Pour utiliser cette règle, il faut créer une règle de validation personalisée qui implémente cette règle commune.
        /// </remarks>
        /// <param name="target"> Objet à valider </param>
        /// <param name="PropertyName"> nom de la propriété ou champs de l'objet sur lequel la règle doit être appliquée </param>
        /// <param name="maxLength"> Valeur maximale acceptée </param>
        /// <param name="allowNull"> Permet de passer par dessus la validation si on alloue null </param>
        /// <returns> Retourne Vrai si la valeur de la méthode ou de la propriété est inférieure ou égale à la valeur de maxLengh, Faux dans le cas contraire </returns>
        internal static bool DecimalMaxValue(object target, string propertyName, decimal maxLength, bool allowNull)
        {
            decimal value;
            Object valueTemp = (Utilities.CallByName(target, propertyName, CallType.Get));
            if (allowNull && valueTemp == null)
                return true;
            if (allowNull == false && valueTemp == null)
                return false;
            if (decimal.TryParse(valueTemp.ToString(), out value))
                return DecimalMaxValue(value, maxLength);
            return false;
        }

        /// <summary>
        ///   Vérifie que la valeur (decimal) est inférieure ou égale à la valeur indiquée dans maxLength
        /// </summary>
        /// <param name="value"> Valeur à vérifier. </param>
        /// <param name="maxLength"> Valeur maximale de l'entier. </param>
        /// <returns> Si la condition est respectée ou non </returns>
        public static bool DecimalMaxValue(decimal value, decimal maxLength)
        {
            if (value > maxLength)
                return false;

            return true;
        }

        #endregion DecimalMaxValue

        #region DecimalMinValue

        /// <summary>
        ///   Vérifie que la valeur (Decimal) d'une propriété ou d'une méthode d'un objet est
        ///   superieure ou égale à la valeur indiquée dans minLength
        /// </summary>
        /// <remarks>
        ///   Pour utiliser cette règle, il faut créer une règle de validation personalisée qui implémente cette règle commune.
        /// </remarks>
        /// <param name="target"> Objet à valider </param>
        /// <param name="propertyName"> nom de la propriété ou champs de l'objet sur lequel la règle doit être appliquée </param>
        /// <param name="minLength"> Valeur minimale que peut contenir la propriété ou méthode testée </param>
        /// <param name="allowNull"> Permet de passer par dessus la validation si on alloue null </param>
        /// <returns> Retourne Vrai si la valeur de la méthode ou de la propriété est superieure ou égale à la valeur de minLength, Faux dans le cas contraire </returns>
        internal static bool DecimalMinValue(object target, string propertyName, decimal minLength, bool allowNull)
        {
            decimal value;
            Object valueTemp = (Utilities.CallByName(target, propertyName, CallType.Get));
            if (allowNull && valueTemp == null)
                return true;
            if (allowNull == false && valueTemp == null)
                return false;
            if (decimal.TryParse(valueTemp.ToString(), out value))
                return DecimalMinValue(value, minLength);
            return false;
        }

        /// <summary>
        ///   Vérifie que la valeur (Decimal) est supérieure ou égale à la valeur indiquée dans minLength
        /// </summary>
        /// <param name="value"> Valeur à vérifier. </param>
        /// <param name="minLength"> Valeur minimale de l'entier. </param>
        /// <returns> Si la condition est respectée ou non </returns>
        public static bool DecimalMinValue(decimal value, decimal minLength)
        {
            if (value < minLength)
                return false;

            return true;
        }

        #endregion DecimalMinValue

        #region DoubleMaxValue

        /// <summary>
        ///   Vérifie que la valeur (double) d'une propriété ou d'une méthode d'un objet est
        ///   inférieure ou égale à la valeur indiquée dans maxLength
        /// </summary>
        /// <remarks>
        ///   Pour utiliser cette règle, il faut créer une règle de validation personalisée qui implémente cette règle commune.
        /// </remarks>
        /// <param name="target"> Objet à valider </param>
        /// <param name="PropertyName"> nom de la propriété ou champs de l'objet sur lequel la règle doit être appliquée </param>
        /// <param name="maxLength"> Valeur maximale acceptée </param>
        /// <param name="allowNull"> Permet de passer par dessus la validation si on alloue null </param>
        /// <returns> Retourne Vrai si la valeur de la méthode ou de la propriété est inférieure ou égale à la valeur de maxLengh, Faux dans le cas contraire </returns>
        internal static bool DoubleMaxValue(object target, string propertyName, double maxLength, bool allowNull)
        {
            double value;
            Object valueTemp = (Utilities.CallByName(target, propertyName, CallType.Get));
            if (allowNull && valueTemp == null)
                return true;
            if (allowNull == false && valueTemp == null)
                return false;
            if (double.TryParse(valueTemp.ToString(), out value))
                return DoubleMaxValue(value, maxLength);
            return false;
        }

        /// <summary>
        ///   Vérifie que la valeur (Double) est inférieure ou égale à la valeur indiquée dans maxLength
        /// </summary>
        /// <param name="value"> Valeur à vérifier. </param>
        /// <param name="maxLength"> Valeur maximale de l'entier. </param>
        /// <returns> Si la condition est respectée ou non </returns>
        public static bool DoubleMaxValue(double value, double maxLength)
        {
            if (value > maxLength)
                return false;

            return true;
        }

        #endregion DoubleMaxValue

        #region DoubleMinValue

        /// <summary>
        ///   Vérifie que la valeur (double) d'une propriété ou d'une méthode d'un objet est
        ///   superieure ou égale à la valeur indiquée dans minLength
        /// </summary>
        /// <remarks>
        ///   Pour utiliser cette règle, il faut créer une règle de validation personalisée qui implémente cette règle commune.
        /// </remarks>
        /// <param name="target"> Objet à valider </param>
        /// <param name="propertyName"> nom de la propriété ou champs de l'objet sur lequel la règle doit être appliquée </param>
        /// <param name="minLength"> Valeur minimale que peut contenir la propriété ou méthode testée </param>
        /// <param name="allowNull"> Permet de passer par dessus la validation si on alloue null </param>
        /// <returns> Retourne Vrai si la valeur de la méthode ou de la propriété est superieure ou égale à la valeur de minLength, Faux dans le cas contraire </returns>
        internal static bool DoubleMinValue(object target, string propertyName, double minLength, bool allowNull)
        {
            double value;
            Object valueTemp = (Utilities.CallByName(target, propertyName, CallType.Get));
            if (allowNull && valueTemp == null)
                return true;
            if (allowNull == false && valueTemp == null)
                return false;
            if (double.TryParse(valueTemp.ToString(), out value))
                return DoubleMinValue(value, minLength);
            return false;
        }

        /// <summary>
        ///   Vérifie que la valeur (double) est supérieure ou égale à la valeur indiquée dans minLength
        /// </summary>
        /// <param name="value"> Valeur à vérifier. </param>
        /// <param name="minLength"> Valeur minimale de l'entier. </param>
        /// <returns> Si la condition est respectée ou non </returns>
        public static bool DoubleMinValue(double value, double minLength)
        {
            if (value < minLength)
                return false;

            return true;
        }

        #endregion DoubleMinValue
    }
}