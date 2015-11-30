using MKS.Core.Business.Interfaces;
using MKS.Core.Model;
using MKS.Core.Model.Error;
using System;
using System.Collections.Generic;


namespace MKS.Core.Business
{
    /// <summary>
    /// Cette classe exécute les processus d'affaires séquencement aux règles définis.
    /// </summary>
    /// <typeparam name="TBusinessObject"></typeparam>
    public abstract class BusinessProcess<TBusinessObject> : IBusinessProcess where TBusinessObject : IBusinessObject
    {
        /// <summary>
        /// Contient la liste des règles à exécuter par le Framework. 
        /// La liste des règles permet déclancher le 'DoBusinessProcess' pour l'exécution de chacune de règle.
        /// </summary>
        /// <returns>Retourne l'état du processus après son exécution</returns>
        public abstract List<RuleBusiness> GetProcessRules();
        
        /// <summary>
        /// Contient l'ensemble de processus en fonction des règles défini dans le 'GetProcessRules'
        /// </summary>
        /// <param name="rule">Liste des règles à exécuter</param>
        /// <param name="TBusinessObject">Objet d'affaire d'information</param>
        /// <returns></returns>
        public abstract Process DoBusinessProcess(RuleBusiness rule, TBusinessObject businessObject);
        

        public virtual void Dispose()
        {
        }

        /// <summary>
        /// Contient l'ensemble de processus en fonction des règles défini dans le 'GetProcessRules'
        /// </summary>
        /// <param name="rule">Liste des règles à exécuter</param>
        /// <param name="IBusinessObject">Objet d'affaire d'information</param>
        /// <returns></returns>
        public Process DoBusinessProcess(RuleBusiness rule, ref IBusinessObject businessObject)
        {
            TBusinessObject businessObjectAdd = (TBusinessObject)businessObject;
            return DoBusinessProcess(rule, businessObjectAdd);
        }
    }

    public abstract class BusinessProcessAdd<TObject> : BusinessProcess<BusinessObjectAdd<TObject>>
    {
    }

    public abstract class BusinessProcessUpdate<TObject> : BusinessProcess<BusinessObjectUpdate<TObject>>
    {
    }

    public abstract class BusinessProcessDelete<TObject> : BusinessProcess<BusinessObjectDelete<TObject>>
    {
    }

    public abstract class BusinessProcessDelete<TObject, TKey> : BusinessProcess<BusinessObjectDelete<TObject, TKey>>
        where TKey : IKey
    {
    }

    public abstract class BusinessProcessDelete<TObject, TResult, TSearch> : BusinessProcess<BusinessObjectDelete<TObject, TResult, TSearch>>
        where TSearch : ISearch
    {
    }

    public abstract class BusinessProcessSelect<TObject> : BusinessProcess<BusinessObjectSelect<TObject>>
    {
    }

    public abstract class BusinessProcessSelect<TObject, TKey> : BusinessProcess<BusinessObjectSelect<TObject, TKey>>
        where TKey : IKey
    {
    }

    public abstract class BusinessProcessSelect<TObject, TResult, TSearch> : BusinessProcess<BusinessObjectSelect<TObject, TResult, TSearch>>
        where TSearch : ISearch
    {
    }

    public abstract class BusinessProcessExecute<TObject> : BusinessProcess<BusinessObjectExecute<TObject>>
    {
    }

    /// <summary>
    /// Permet de trapper les erreurs d'affaires et les erreurs non géré.
    /// </summary>
    public abstract class BusinessProcessError : BusinessProcess<BusinessObjectError>
    {
        public override sealed List<RuleBusiness> GetProcessRules()
        {
            return new List<RuleBusiness> { new RuleBusiness("ERRORLOG", "LOG ERROR EXTERNAL", Rule.RuleSeverity.Error) };
        }
        
        public override sealed Process DoBusinessProcess(RuleBusiness rule, BusinessObjectError businessObject)
        {
            if (businessObject.Exception is ExceptionProcess<ProcessResults>)
            {
                var faultException = businessObject.Exception as ExceptionProcess<ProcessResults>;
                BusinessProcessValidation(businessObject, faultException);
            }
            else
            {
                BusinessProcessException(businessObject, businessObject.Exception);
            }

            // IMPORTANT de ne pas changer la valeur de retour, car cela va faire une référence circulaire.
            return Process.Succeed;
        }

        

        /// <summary>
        ///   Méthode appelée lorsqu'une exception de type Exception est lancée.
        ///   Faire un override de cette méthode pour effectuer un traitement personnalisé.
        /// </summary>
        /// <param name="businessObject"> Objet de processus d'affaire </param>
        /// <param name="exception"> Erreur. </param>
        public virtual void BusinessProcessException(BusinessObjectError businessObject, Exception exception)
        {
        }

        /// <summary>
        ///   Méthode appelée lorsqu'une exception de type FaultException&lt;ProcessResults&gt; est lancée.
        ///   Soit par une validation d'intégrité ou par un processus d'affaire
        ///   Faire un override de cette méthode pour effectuer un traitement personnalisé.
        /// </summary>
        /// <param name="businessObject"> Objet de processus d'affaire </param>
        /// <param name="faultException"> Erreur. </param>
        public virtual void BusinessProcessValidation(BusinessObjectError businessObject, ExceptionProcess<ProcessResults> ExceptionProcess)
        {
        }
    }
}