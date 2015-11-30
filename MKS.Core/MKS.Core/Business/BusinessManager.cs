using MKS.Core.Activity;
using MKS.Core.Model;
using MKS.Core.Security;

namespace MKS.Core.Business
{
    /// <summary>
    /// Classe qui permet de faire un traitement d'affaire incluant plusieurs business sur des objets
    /// </summary>
    /// <typeparam name="TProcessExecute">Classe de processus d'affaire</typeparam>
    /// <typeparam name="TMainObject">Objet d'affaire de traitement dans laquel ce retrouve les objets a traiter par le procesus</typeparam>
    public abstract class BusinessManager<TProcessExecute, TMainObject> : MKS.Core.Business.BusinessExecute<TMainObject>
        where TProcessExecute : BusinessProcessExecute<TMainObject>, new()
    {

        public BusinessManager()
        {
            
            SetProcessExecute<TProcessExecute>();
        }
    }

    /// <summary>
    /// Classe qui permet de faire un traitement d'affaire sur plusieurs business sur des objets
    /// </summary>
    /// <typeparam name="TProcessExecute">Classe de processus</typeparam>
    /// <typeparam name="TMainObject">Objet d'affaire dans laquel ce retrouve les objets a traiter par le procesus</typeparam>
    /// <typeparam name="TSecurity">Classe de sécurite d'acces</typeparam>
    public abstract class BusinessMain<TProcessExecute, TMainObject, TSecurity> : MKS.Core.Business.BusinessExecute<TMainObject>
        where TProcessExecute : BusinessProcessExecute<TMainObject>, new()
        where TSecurity : ISecurityPermission, MKS.Core.Security.ISecurityAdapter, new()
    {

        public BusinessMain()
        {
 
            SetProcessExecute<TProcessExecute>();
            SetSecurity<TSecurity>();
        }
    }

    /// <summary>
    /// Classe qui permet de faire un traitement d'affaire sur plusieurs business sur des objets
    /// </summary>
    /// <typeparam name="TProcessExecute">Classe de processus</typeparam>
    /// <typeparam name="TMainObject">Objet d'affaire dans laquel ce retrouve les objets a traiter par le procesus</typeparam>
    /// <typeparam name="TSecurity">Classe de sécurite d'acces</typeparam>
    /// <typeparam name="TActivityLog">Classe de journalisation</typeparam>
    public abstract class BusinessMain<TProcessExecute, TMainObject, TSecurity, TEventLog> : MKS.Core.Business.BusinessExecute<TMainObject>
        where TProcessExecute : BusinessProcessExecute<TMainObject>, new()
        where TSecurity : ISecurityPermission, MKS.Core.Security.ISecurityAdapter, new()
        where TEventLog : IActivityLogOperationsExecute<TMainObject>, IActivityAdapter, new()
    {
        public BusinessMain() { }
        public BusinessMain(IUserEnvironment userenvironnement)
        {
            Globals.GetUserEnvironment.Initialise(userenvironnement);
            SetProcessExecute<TProcessExecute>();
            SetSecurity<TSecurity>();
            SetActivityLog<TEventLog>();
        }
    }
}