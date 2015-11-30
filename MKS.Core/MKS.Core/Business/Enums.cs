namespace MKS.Core.Business
{
    public enum DeleteInMapping
    {
        ReturnInt,
        ReturnDeletedItems
    }

    public enum BusinessStep
    {
        /// <summary>
        ///   Indique que l'appel est effectué avant le mapping
        /// </summary>
        BeforeDataMap,

        /// <summary>
        ///   Indique que l'appel est effectué après le mapping
        /// </summary>
        AfterDataMap
    }

    public enum FunctionName
    {
        Add,
        Update,
        Delete,
        DeleteKey,
        DeleteSearch,
        Select,
        SelectKey,
        SelectSearch,
        Edit,
        EditKey,
        EditSearch,
        Execute
    }

    /// <summary>
    ///   Enumération permettant d'informer le Framework sur l'action a suivre lors du retour d'un process métier.
    /// </summary>
    public enum Process
    {
        /// <summary>
        ///   Indique que le processus est erreur et renvoie une erreur. Provoque une exception
        ///   de type ExceptionProcess<ProcessResults> . Si une transaction est démarrée, il fait un rollback.
        /// </summary>
        FailedThrow,

        /// <summary>
        ///   Indique que le processus est en erreur, mais continue de faire les règles
        ///   suivantes. Si une transaction est démarrée et que c'est la dernière règle à
        ///   exécuter, il fait le commit.
        /// </summary>
        /// <remarks>
        ///   Les erreurs sont enregistrées au niveau du BusinessFactory. Il est donc possible de les consulter.
        /// </remarks>
        SuccessAddMessage,

        /// <summary>
        ///   Indique que le processus est en erreur et arrête l'exécution des règles. Ne
        ///   lance pas d'exception. Si une transaction est démarrée et que c'est la dernière
        ///   règle à exécuter, il fait un rollback.
        /// </summary>
        /// <remarks>
        ///   Les erreurs sont enregistrées au niveau du BusinessFactory. Il est donc possible
        ///   de les consulter.
        /// </remarks>
        FailedStopRules,

        /// <summary>
        ///   Indique que le processus est réussi. Si une transaction est démarrée, il fait le
        ///   commit si c'est la dernière règle en exécution.
        /// </summary>
        Succeed
    }
}