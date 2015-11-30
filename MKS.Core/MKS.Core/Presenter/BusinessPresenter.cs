using MKS.Core.Business.Interfaces;
using MKS.Core.Connector.Interfaces;

namespace MKS.Core.Presentation
{
    public abstract class BusinessPresenter<TView, TProcess> : IBusinessOperationsPresenter<TView> 
        where TView : IViewBase
        where TProcess : IViewProcess<TView>, new()
    {
        internal MKS.Core.Business.Business business;
        private readonly TView mView;
        protected IViewProcess<TView> ProcessInstance;

        public BusinessPresenter()
        {
            business = new MKS.Core.Business.Business();

            ProcessInstance = new TProcess();

            //Abonnement à l'événement de la vue
            mView.OnCommand += ExecuteCommand;

        }

        #region Set
        


        public void SetValidation<TValidation>() where TValidation : IValidation<TView>, new()
        {
            business.SetValidation(new TValidation());
        }


        #endregion Set


        #region Commande

        /// <summary>
        ///     Permet à l'interface d'envoyer des commandes pour être exécutées côté présenteur
        /// </summary>
        /// <param name="command"> Commande à exécuter </param>
        public void ExecuteCommand(string command)
        {
            ExecuteCommand(command, null);
        }

        /// <summary>
        ///     Permet à l'interface d'envoyer des commandes pour être exécutées côté présenteur
        /// </summary>
        /// <param name="command"> Commande à exécuter </param>
        /// <param name="args"> Liste de paramètres à la commande </param>
        public void ExecuteCommand(string command, CommandEventArgsCustom args)
        {
            
            ProcessInstance.OnCommand(command, args, mView, this);


        }

        public void ExecuteCommand<TinputParameter>(string command, TinputParameter args)
        {
            


            ProcessInstance.OnCommand(command, args, mView, this);
            

        }

        #endregion

    }
}