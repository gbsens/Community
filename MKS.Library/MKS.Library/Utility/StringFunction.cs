using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace MKS.Library.Utility
{
    /// <summary>
    /// Classe possédant des fonctions utilitaires sur les string
    /// </summary>
    public static class StringFunction
    {
        /// <summary>
        /// Cette fonction normalise les caractères en enlevant les accents, les cédilles et tout ce qui peut se retrouver sur une lettre.
        /// Exemple : (àÂëéèêçáñ) va devenir (aAeeeecan)
        /// </summary>
        /// <param name="s">Le string à modifier</param>
        /// <returns>Le string modifié</returns>
        public static string RemoveAccent(string s)
        {
            if (!String.IsNullOrEmpty(s))
            {
                String normalizedString = s.Normalize(NormalizationForm.FormD);
                StringBuilder stringBuilder = new StringBuilder();

                for (int i = 0; i < normalizedString.Length; i++)
                {
                    Char c = normalizedString[i];
                    if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                        stringBuilder.Append(c);
                }

                return stringBuilder.ToString();
            }
            else
                return s;
        }
    }
}
