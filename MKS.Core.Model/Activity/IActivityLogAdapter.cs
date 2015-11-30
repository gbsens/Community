namespace MKS.Core.Activity
{
    public interface IActivityAdapter
    {
        IActivity Activity { get; set; }

        /// <summary>
        ///     Après la logique d'affaire, le Business Factory lance la fonction pour demander une journalisation.
        /// </summary>
        void DoActivityLog(IActivity eventlog);
    }
}