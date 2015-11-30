using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.ComponentModel;

namespace MKS.Library.Utility
{
    /// <summary>
    /// Classe possédant des fonctions sur les Enum
    /// </summary>
    public static class EnumFunction
    {
        /// <summary>
        /// Cette fonction permet d'obtenir le contenu de l'attribut Informations pour un membre d'enum
        /// </summary>
        /// <param name="value">Le membre possédant la description désirée</param>
        /// <returns>La description</returns>
        public static string GetEnumDescription(Enum value)
        {
            //Source : http://blog.spontaneouspublicity.com/post/2008/01/17/Associating-Strings-with-enums-in-C.aspx

            FieldInfo fi = value.GetType().GetField(value.ToString());

            DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute),false);

            if (attributes != null && attributes.Length > 0)
                return attributes[0].Description;
            else
                return value.ToString();
        }

        /// <summary>
        /// Cette fonction permet d'obtenir les descriptions de l'enum sous forme de liste de string
        /// </summary>
        /// <typeparam name="T">L'Enum à énumérer</typeparam>
        /// <returns>Les descriptions de l'enum</returns>
        public static List<string> GetEnumDescriptionList<T>()
        {
            List<string> lst = new List<string>();
            foreach (T type in EnumFunction.EnumToList<T>())
            {
                string description = string.Empty;
                FieldInfo fi = type.GetType().GetField(type.ToString());

                DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

                if (attributes != null && attributes.Length > 0)
                    description = attributes[0].Description;
                else
                    description = type.ToString();

                lst.Add(description);
            }
            return lst;
        }

        /// <summary>
        /// Retourne l'enum selon la valeur en string fournie
        /// </summary>
        /// <param name="value">La valeur</param>
        /// <returns>L'enum</returns>
        public static T GetEnumTypeFromDescription<T>(string value)
        {
            T enumValue = default(T);
            bool found = false;

            foreach (T type in EnumFunction.EnumToList<T>())
            {
                string description = string.Empty;
                FieldInfo fi = type.GetType().GetField(type.ToString());

                DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

                if (attributes != null && attributes.Length > 0)
                {
                    if (attributes[0].Description.Equals(value))
                    {
                        enumValue = type;
                        found = true;
                    }
                }

            }

            if (found)
                return enumValue;
            else
                throw new Exception(string.Format(MKS.Library.Ressources.ErrorMessages.DESIGN_ERROR_201, value, typeof(T).ToString()));
        }

        /// <summary>
        /// Cette fonction permet de transformer un Enum en IEnumerable pouvant être utilisé par exemple dans un Foreach
        /// </summary>
        /// <typeparam name="T">L'Enum à énumérer</typeparam>
        /// <returns>Un IEnumerable du type de l'Enum</returns>
        public static IEnumerable<T> EnumToList<T>()
        {
            //Source : http://blog.spontaneouspublicity.com/post/2008/01/17/Associating-Strings-with-enums-in-C.aspx

            Type enumType = CheckType<T>();

            Array enumValArray = Enum.GetValues(enumType);
            List<T> enumValList = new List<T>(enumValArray.Length);
            foreach (int val in enumValArray)
            {
                enumValList.Add((T)Enum.Parse(enumType, val.ToString()));
            }
            return enumValList;
        }

        /// <summary>
        /// Retourne la valeur de l'enum
        /// </summary>
        /// <param name="member">L'enum</param>
        /// <returns>La valeur</returns>
        public static int GetEnumValue(Enum member)
        {
            return Convert.ToInt32(member);
        }

        /// <summary>
        /// Retourne l'enum selon la valeur fournie
        /// </summary>
        /// <param name="value">La valeur</param>
        /// <returns>L'enum</returns>
        public static T GetEnumType<T>(int value)
        {
            Type enumType = CheckType<T>();

            if (Enum.IsDefined(enumType, value))
                return (T)Enum.ToObject(enumType, value);
            else
                return default(T);
        }

        /// <summary>
        /// Retourne l'enum selon la valeur en string fournie
        /// </summary>
        /// <param name="value">La valeur</param>
        /// <returns>L'enum</returns>
        public static T GetEnumType<T>(string value)
        {
            Type enumType = CheckType<T>();

            if (Enum.IsDefined(enumType, value))
                return (T)Enum.Parse(enumType, value);
            else
                return default(T);
        }

        /// <summary>
        /// Retourne le nom d'un enum selon sa valeur
        /// </summary>
        /// <param name="value">La valeur</param>
        /// <returns>Le nom</returns>
        public static string GetEnumString<T>(int value)
        {
            Type enumType = CheckType<T>();

            if (Enum.IsDefined(enumType, value))
                return Enum.ToObject(enumType, value).ToString();
            else
                return string.Empty;
        }

        /// <summary>
        /// Fonction permettant de s'assurer que le type généric reçu est bien un Enum
        /// </summary>
        /// <typeparam name="T">Type générique</typeparam>
        /// <returns>Le type de T</returns>
        private static Type CheckType<T>()
        {
            Type enumType = typeof(T);
            if (enumType.BaseType != typeof(Enum))
                throw new ArgumentException("T doit être de type System.Enum");

            return enumType;
        }
    }
}
