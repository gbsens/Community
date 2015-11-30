using System.Configuration;
using System.Data;

namespace MKS.Core.Configuration
{
    /// <summary>
    /// Retourne les configurations du ficheir de configuration app.config, web.config par défaut du système
    /// </summary>
    public abstract class Configuration : IConfiguration
    {
        /// <summary>
        /// paramètres de connexion à la base de donnée. 
        /// Paramètre rechercher : [CodeSysteme] + DB
        /// Exemple le code système est SYSA donc SYSADB pour la connexion BD.
        /// </summary>  
        /// <remarks>FONCTION ENCORE EN DEVELOPPEMENT</remarks>
        /// <returns>Retourn null si pas de parametre dans le fichier de config</returns>
        public virtual IDbConnection GetConnection()
        {
            
            //return ConfigurationManager.AppSettings[ ConfigurationManager.AppSettings["CodeSysteme"] + "DB"];
            return null;
        }


    }
}