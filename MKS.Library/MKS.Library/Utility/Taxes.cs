using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace MKS.Library.Utility
{
    /// <summary>
    /// Conserve les informations sur un montant et les taxes
    /// </summary>
    [DataContract]
    public class MontantTaxes
    {
        /// <summary>
        /// Montant initial
        /// </summary>
        [DataMember, Obsolete("Utilisez la propriété MontantInitial")]
        public Decimal MontantInitiale
        {
            get
            {
                return MontantInitial;
            }
            set
            {
                MontantInitial = value;
            }
        }

        /// <summary>
        /// Montant initial
        /// </summary>
        [DataMember]
        public Decimal MontantInitial { get; set; }

        /// <summary>
        /// Montant de la TPS
        /// </summary>
        [DataMember]
        public Decimal TPS { get; set; }

        /// <summary>
        /// Montant de la TVQ
        /// </summary>
        [DataMember]
        public Decimal TVQ { get; set; }

        /// <summary>
        /// Montant total
        /// </summary>
        [DataMember]
        public Decimal MontantTotal { get; set; }

        /// <summary>
        /// Constructeur
        /// </summary>
        public MontantTaxes()
        {
        }
    }

    /// <summary>
    /// Traitements utilitaires
    /// </summary>
    public static class Utilities
    {
        static readonly DateTime NOUVELLE_DATE = new DateTime(2013, 1, 1);

        /// <summary>
        /// Calcule les taxes TPS et TVQ ainsi que le montant total incluant les taxes.
        /// </summary>
        /// <param name="montant">Montant initial</param>
        /// <param name="TPS">Decimal équivalent à la TPS. Pour une taxe de 5% ce paramètre doit être à 0.05</param>
        /// <param name="TVQ">Decimal équivalent à la TVQ. Pour une taxe de 8.5% ce paramètre doit être à 0.085</param>
        /// <param name="round">Permet de spécifier si on doit arrondir le nombre à 2 décimal après la virgule.
        /// True pour arrondir. False pour conserver tous les décimals</param>
        /// <param name="calculDate">DateTime équivalent à la date du calcul. Si à null ou non défini, la date du jour est utilisée</param>
        /// <returns>Objet MontantTaxes</returns>
        public static MontantTaxes CalculateTaxes(Decimal montant, Decimal TPS, Decimal TVQ, Boolean round, DateTime? calculDate = null)
        {
            if (calculDate == null)
                calculDate = DateTime.Now;

            MontantTaxes montantAvecTaxes = new MontantTaxes();

            montantAvecTaxes.MontantInitial = montant;
            montantAvecTaxes.TPS = montantAvecTaxes.MontantInitial * TPS;

            if (calculDate >= NOUVELLE_DATE)
                montantAvecTaxes.TVQ = montantAvecTaxes.MontantInitial * TVQ;
            else
                montantAvecTaxes.TVQ = (montantAvecTaxes.MontantInitial + montantAvecTaxes.TPS) * TVQ;

            montantAvecTaxes.MontantTotal = montantAvecTaxes.MontantInitial + montantAvecTaxes.TPS + montantAvecTaxes.TVQ;

            if (round)
            {
                montantAvecTaxes.TPS = Math.Round(montantAvecTaxes.TPS, 2, MidpointRounding.AwayFromZero);
                montantAvecTaxes.TVQ = Math.Round(montantAvecTaxes.TVQ, 2, MidpointRounding.AwayFromZero);
                montantAvecTaxes.MontantTotal = Math.Round(montantAvecTaxes.MontantTotal, 2, MidpointRounding.AwayFromZero);
            }

            return montantAvecTaxes;
        }

        /// <summary>
        /// Calcule les taxes TPS et TVQ ainsi que le montant total incluant les taxes. Les nombres sont arrondis à la deuxième décimale.
        /// </summary>
        /// <param name="montant">Montant initial</param>
        /// <param name="TPS">Decimal équivalent à la TPS. Pour une taxe de 5% ce paramètre doit être à 0.05</param>
        /// <param name="TVQ">Decimal équivalent à la TVQ. Pour une taxe de 8.5% ce paramètre doit être à 0.085</param>
        /// <param name="calculDate">DateTime équivalent à la date du calcul. Si à null ou non défini, la date du jour est utilisée</param>
        /// <returns>Objet MontantTaxes</returns>
        public static MontantTaxes CalculateTaxes(Decimal montant, Decimal TPS, Decimal TVQ, DateTime? calculDate = null)
        {
            if (calculDate == null)
                calculDate = DateTime.Now;

            return CalculateTaxes(montant, TPS, TVQ, true, calculDate);
        }
    }
}