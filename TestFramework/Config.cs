using MKS.Core.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestFramework
{
    public class Config:Configuration
    {
        public override System.Data.IDbConnection GetConnection()
        {

            string BDName = "ExempleMKS"; //Nom de la base de donnée
            string BDSource = @"STEPHANEALIEN\SQLEXPRESS"; // Nom du Serveur SQL. *** changer pour votre sql serveur ***

            string BSConn = @"Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=" + BDName + ";Data Source=" + BDSource;

            System.Data.SqlClient.SqlConnection sq = new System.Data.SqlClient.SqlConnection(BSConn);

            return sq;

        }
    }
}
