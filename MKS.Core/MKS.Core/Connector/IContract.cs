using System;

namespace MKS.Core.Connector
{
    public interface IContract
    {
        /// <summary>
        /// Name
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// ApplicationCode
        /// </summary>
        string ApplicationCode { get; set; }

        /// <summary>
        /// Url
        /// </summary>
        string Url { get; set; }

        /// <summary>
        /// Version
        /// </summary>
        Version Version { get; set; }
    }
}