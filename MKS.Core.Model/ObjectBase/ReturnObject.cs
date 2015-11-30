using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace MKS.Core
{
    /// <summary>
    ///     Classe de base encapsulant l'ensemble des exceptions qui sont survenues lors des validations d'affaires ou d'intégrités
    ///     incluant les status d'exécution de sévérité Informative ou Warning.
    ///     Cette classe permet d'encapsuler un objet reçu en paramètre du constructeur.
    /// </summary>
    /// <remarks>Cette classe doit être impémenté par les objets de retour d'un processus d'affaire</remarks>
    [DataContract]
    public abstract class ReturnObject : IReturnObject
    {
        private ProcessResults _ProcessResultList = new ProcessResults();

        /// <summary>
        ///     Liste des exceptions que le Business ajoute automatiquement.
        /// </summary>
        [DataMember(Name = "ProcessResults")]
        public ProcessResults ProcessResults
        {
            get { return (_ProcessResultList); }

            set { _ProcessResultList = value; }
        }

        /// <summary>
        ///     Liste de message que l'objet pourrait retourner à l'interface.
        /// </summary>
        [DataMember(Name = "Messages")]
        public Dictionary<string, string> Messages { get; set; }
    }
}