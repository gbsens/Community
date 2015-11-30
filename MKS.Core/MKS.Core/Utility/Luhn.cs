using System.Globalization;

namespace MKS.Core
{
    /// <summary>
    ///   Classe qui permet de traiter les chaîne de caractère avec la formule de Luhn
    /// </summary>
    public static class Luhn
    {
        /// <summary>
        ///   Ajoute le checksum de validation de la formule de Luhn à la chaîne reçu en paramètre.
        /// </summary>
        /// <param name="s"> Chaîne qui recevra le checksum de la formule de Luhn </param>
        /// <returns> Chaîne avec le checksum de la formule de Luhn </returns>
        public static string AddCheckSum(string s)
        {
            int somme = GetCountSum(s + "0");
            return s + ((10 - (somme % 10)) % 10);
        }

        /// <summary>
        ///   Vérifier le chiffre preuve dans une chaîne selon la formule Luhn
        ///   La chaîne doit comprendre le chiffre preuve
        /// </summary>
        /// <param name="s"> Chaîne à vérifier </param>
        /// <returns> Retourne true si la chaîne est valide selon la formule de Luhn </returns>
        public static bool Validate(string s)
        {
            //Permet de ne pas véridier des caractères non approuvé pour le Luhn
            if (s.Contains("_"))
            {
                return (false);
            }

            int somme = GetCountSum(s);
            return (somme % 10) == 0;
        }

        /// <summary>
        ///   Calculer la somme d'une chaîne avec la formule Luhn
        ///   Les chiffres sont convertis en numérique (A,B,C, ... -> 0,1,2, ...)
        ///   Les autres caractères sont ignorés
        ///   http://fr.wikipedia.org/wiki/Formule_de_Luhn
        /// </summary>
        /// <param name="s"> string à calculer </param>
        /// <returns> Nombre de Luhn </returns>
        private static int GetCountSum(string s)
        {
            bool @double = false;

            int somme = 0;

            for (int i = s.Length - 1; i >= 0; i += -1)
            {
                int temp;
                char c = s.Substring(i, 1).ToCharArray()[0];

                if (c >= '0' && c <= '9')
                {
                    temp = int.Parse(c.ToString(CultureInfo.InvariantCulture));

                    if (@double) temp *= 2;
                    if (temp >= 10) temp = temp - 9;

                    somme += temp;
                    @double = !@double;
                }
                else if (c >= 'A' && c < 'Z')
                {
                    temp = c - 'A';
                    temp = temp % 10;

                    if (@double) temp *= 2;
                    if (temp >= 10) temp = temp - 9;

                    somme += temp;
                    @double = !@double;
                }
            }

            return somme;
        }
    }
}