using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MKS.Library.Utility
{
    /// <summary>
    /// Classe possédant des fonctions sur les dates
    /// </summary>
    public static class DateFunction
    {
        /// <summary>
        /// Cette fonction permet de savoir si une date passée en paramètre est entre deux autres dates passées en paramètre
        /// </summary>
        /// <param name="p_dateToCheck">La date à vérifier</param>
        /// <param name="p_startDate">La date de début</param>
        /// <param name="p_endDate">La date de fin</param>
        /// <returns>True si la date est dans la période, false sinon.</returns>
        public static bool IsDateBetweenDateRange(DateTime p_dateToCheck, DateTime p_startDate, DateTime p_endDate)
        {
            if (p_dateToCheck <= p_endDate && p_dateToCheck >= p_startDate)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Cette fonction permet de savoir si deux périodes de temps passées en paramètre entrent en conflit.
        /// Elle permet également de savoir si des périodes de temps vont entrer en conflit puisqu'on peut mettre
        /// null comme paramètre aux dates de fin.
        /// </summary>
        /// <param name="p_firstStartDate">La date de début de la première période</param>
        /// <param name="p_firstEndDate">La date de fin de la première période</param>
        /// <param name="p_secondStartDate">La date de début de la deuxième période</param>
        /// <param name="p_secondEndDate">La date de fin de la deuxième période</param>
        /// <returns>True s'il y a un conflit entre les deux périodes, false sinon</returns>
        public static bool IsDateRangeInConflictWithAnotherDateRange(Nullable<DateTime> p_firstStartDate, Nullable<DateTime> p_firstEndDate, Nullable<DateTime> p_secondStartDate, Nullable<DateTime> p_secondEndDate)
        {
            //Si toutes les dates ont une valeur
            if (p_firstStartDate.HasValue && p_firstEndDate.HasValue && p_secondStartDate.HasValue && p_secondEndDate.HasValue)
                return CheckConflictNoNull(p_firstStartDate.Value, p_firstEndDate.Value, p_secondStartDate.Value, p_secondEndDate.Value);

            //Si seulement la deuxième date de fin est null
            else if (p_firstStartDate.HasValue && p_firstEndDate.HasValue && p_secondStartDate.HasValue)
                return CheckConflictSecondEndDateNull(p_firstStartDate.Value, p_firstEndDate.Value, p_secondStartDate.Value);

            //Si seulement la première date de fin est null
            else if (p_firstStartDate.HasValue && p_secondStartDate.HasValue && p_secondEndDate.HasValue)
                return CheckConflictFirstEndDateNull(p_firstStartDate.Value, p_secondStartDate.Value, p_secondEndDate.Value);

            //Si la première date de début n'a pas de valeur
            else if (!p_firstStartDate.HasValue && p_firstEndDate.HasValue && p_secondStartDate.HasValue)
                return CheckConflictFirstStartDateNull(p_firstEndDate.Value, p_secondStartDate.Value);

            //Si la deuxième date de début n'a pas de valeur
            else if (!p_secondStartDate.HasValue && p_firstStartDate.HasValue && p_secondEndDate.HasValue)
                return CheckConflictSecondStartDateNull(p_firstStartDate.Value, p_secondEndDate.Value);

            //Voici les autres situations causant un conflit : Les dates de début sans valeur, les dates de fin sans valeur et toutes les dates sans valeur
            else
                return true;
        }

        /// <summary>
        /// Vérifie le conflit entre deux périodes de temps définies (pas de null)
        /// </summary>
        /// <param name="p_firstStartDate">La date de début de la première période</param>
        /// <param name="p_firstEndDate">La date de fin de la première période</param>
        /// <param name="p_secondStartDate">La date de début de la deuxième période</param>
        /// <param name="p_secondEndDate">La date de fin de la deuxième période</param>
        /// <returns>True s'il y a un conflit entre les deux périodes, false sinon</returns>
        private static bool CheckConflictNoNull(DateTime p_firstStartDate, DateTime p_firstEndDate, DateTime p_secondStartDate, DateTime p_secondEndDate)
        {
            //La date de début de la première période est dans la deuxième période
            if (p_firstStartDate >= p_secondStartDate && p_firstStartDate <= p_secondEndDate)
                return true;

            //La date de fin de la première période est dans la deuxième période
            if (p_firstEndDate >= p_secondStartDate && p_firstEndDate <= p_secondEndDate)
                return true;

            //La deuxième période est englobée dans la première période
            if (p_firstStartDate <= p_secondStartDate && p_firstEndDate >= p_secondEndDate)
                return true;

            //La première période est englobée dans la deuxième période
            if (p_firstStartDate >= p_secondStartDate && p_firstEndDate <= p_secondEndDate)
                return true;

            return false;
        }

        /// <summary>
        /// Vérifie le conflit entre deux périodes dont la première est indéfinie
        /// </summary>
        /// <param name="p_firstStartDate">La date de début de la première période</param>
        /// <param name="p_secondStartDate">La date de début de la deuxième période</param>
        /// <param name="p_secondEndDate">La date de fin de la deuxième période</param>
        /// <returns>True s'il y a un conflit entre les deux périodes, false sinon</returns>
        private static bool CheckConflictFirstEndDateNull(DateTime p_firstStartDate, DateTime p_secondStartDate, DateTime p_secondEndDate)
        {
            //La date de début de la première période est avant la deuxième période
            if (p_firstStartDate <= p_secondStartDate)
                return true;

            //La date de début de la première période est dans la deuxième période
            if (p_firstStartDate >= p_secondStartDate && p_firstStartDate <= p_secondEndDate)
                return true;

            return false;
        }

        /// <summary>
        /// Vérifie le conflit entre deux périodes dont la deuxième est indéfinie
        /// </summary>
        /// <param name="p_firstStartDate">La date de début de la première période</param>
        /// <param name="p_firstEndDate">La date de fin de la première période</param>
        /// <param name="p_secondStartDate">La date de début de la deuxième période</param>
        /// <returns>True s'il y a un conflit entre les deux périodes, false sinon</returns>
        private static bool CheckConflictSecondEndDateNull(DateTime p_firstStartDate, DateTime p_firstEndDate, DateTime p_secondStartDate)
        {
            //La date de début de la deuxième période est avant la première période
            if (p_secondStartDate <= p_firstStartDate)
                return true;

            //La date de début de la deuxième période est dans la première période
            if (p_secondStartDate >= p_firstStartDate && p_secondStartDate <= p_firstEndDate)
                return true;

            return false;
        }

        /// <summary>
        /// Vérifie le conflit entre deux périodes dont la première n'a pas de date de début
        /// </summary>
        /// <param name="p_firstEndDate">La date de fin de la première période</param>
        /// <param name="p_secondStartDate">La date de début de la deuxième période</param>
        /// <returns>True s'il y a un conflit entre les deux périodes, false sinon</returns>
        private static bool CheckConflictFirstStartDateNull(DateTime p_firstEndDate, DateTime p_secondStartDate)
        {
            //La date de début de la deuxième période est dans la première période
            if (p_secondStartDate <= p_firstEndDate)
                return true;

            return false;
        }

        /// <summary>
        /// Vérifie le conflit entre deux périodes dont la deuxième n'a pas de date de début
        /// </summary>
        /// <param name="p_firstStartDate">La date de début de la première période</param>
        /// <param name="p_secondEndDate">La date de fin de la deuxième période</param>
        /// <returns>True s'il y a un conflit entre les deux périodes, false sinon</returns>
        private static bool CheckConflictSecondStartDateNull(DateTime p_firstStartDate, DateTime p_secondEndDate)
        {
            //La date de début de la première période est dans la deuxième période
            if (p_firstStartDate <= p_secondEndDate)
                return true;

            return false;
        }

        /// <summary>
        /// Permet de savoir si la date du jour fait partie d'une période de temps déterminée et est considérée comme active
        /// </summary>
        /// <param name="p_startDate">Date de début</param>
        /// <param name="p_endDate">Date de fin</param>
        /// <returns>True si la date du jour fait partie de la période, false sinon</returns>
        public static bool IsDateActive(Nullable<DateTime> p_startDate, Nullable<DateTime> p_endDate)
        {
            return IsDateActive(p_startDate, p_endDate, DateTime.Today, false);
        }

        /// <summary>
        /// Permet de savoir si une date fait partie d'une période de temps déterminée et est considérée comme active
        /// </summary>
        /// <param name="p_startDate">Date de début</param>
        /// <param name="p_endDate">Date de fin</param>
        /// <param name="p_dateTocheck">La date à vérifier</param>
        /// <returns>True si la date du jour fait partie de la période, false sinon</returns>
        public static bool IsDateActive(Nullable<DateTime> p_startDate, Nullable<DateTime> p_endDate, DateTime p_dateTocheck)
        {
            return IsDateActive(p_startDate, p_endDate, p_dateTocheck, false);
        }

        /// <summary>
        /// Permet de savoir si la date du jour fait partie d'une période de temps déterminée et est considérée comme active
        /// </summary>
        /// <param name="p_startDate">Date de début</param>
        /// <param name="p_endDate">Date de fin</param>
        /// <param name="p_includeEndDate">Si on doit inclure la date de fin</param>
        /// <returns>True si la date du jour fait partie de la période, false sinon</returns>
        public static bool IsDateActive(Nullable<DateTime> p_startDate, Nullable<DateTime> p_endDate, bool p_includeEndDate)
        {
            return IsDateActive(p_startDate, p_endDate, DateTime.Today, p_includeEndDate);
        }

        /// <summary>
        /// Permet de savoir si une date fait partie d'une période de temps déterminée et est considérée comme active
        /// </summary>
        /// <param name="p_startDate">Date de début</param>
        /// <param name="p_endDate">Date de fin</param>
        /// <param name="p_dateTocheck">La date à vérifier</param>
        /// <param name="p_includeEndDate">Si on doit inclure la date de fin</param>
        /// <returns>True si la date du jour fait partie de la période, false sinon</returns>
        public static bool IsDateActive(Nullable<DateTime> p_startDate, Nullable<DateTime> p_endDate, DateTime p_dateTocheck, bool p_includeEndDate)
        {
            if (p_includeEndDate && p_endDate.HasValue)
                p_endDate = p_endDate.Value.AddDays(1);

            if (p_startDate.HasValue && p_endDate.HasValue)
                return NoDateNull(p_startDate.Value, p_endDate.Value, p_dateTocheck);
            else if (p_startDate.HasValue)
                return EndDateNull(p_startDate.Value, p_dateTocheck);
            else if (p_endDate.HasValue)
                return StartDateNull(p_endDate.Value, p_dateTocheck);
            else
                return true;
        }

        /// <summary>
        /// Gère le fait qu'aucune date n'est à null
        /// </summary>
        /// <param name="p_startDate">Date de début</param>
        /// <param name="p_endDate">Date de fin</param>
        /// <param name="p_dateTocheck">Date à vérifier</param>
        /// <returns>True si la date du jour fait partie de la période, false sinon</returns>
        private static bool NoDateNull(DateTime p_startDate, DateTime p_endDate, DateTime p_dateTocheck)
        {
            if (p_dateTocheck >= p_startDate && p_dateTocheck < p_endDate)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Gère le fait que la date de début est à null
        /// </summary>
        /// <param name="p_endDate">Date de fin</param>
        /// <param name="p_dateTocheck">Date à vérifier</param>
        /// <returns>True si la date du jour fait partie de la période, false sinon</returns>
        private static bool StartDateNull(DateTime p_endDate, DateTime p_dateTocheck)
        {
            if (p_dateTocheck < p_endDate)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Gère le fait que la date de fin est à null
        /// </summary>
        /// <param name="p_startDate">Date de début</param>
        /// <param name="p_dateTocheck">Date à vérifier</param>
        /// <returns>True si la date du jour fait partie de la période, false sinon</returns>
        private static bool EndDateNull(DateTime p_startDate, DateTime p_dateTocheck)
        {
            if (p_dateTocheck >= p_startDate)
                return true;
            else
                return false;
        }
    }
}
