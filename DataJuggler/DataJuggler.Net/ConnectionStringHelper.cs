﻿

#region using statements

using System.Text;
using System.Data.SqlClient;

#endregion

namespace DataJuggler.Net
{

    #region class ConnectionStringHelper
    /// <summary>
    /// This class [enter description here].
    /// </summary>
    public class ConnectionStringHelper
    {

        #region BuildConnectionString(string serverName, string databaseName)
        /// <summary>
        /// This method builds a connection string when Windows Authentication is used.
        /// </summary>
        /// <param name="ServerName"></param>
        /// <param name="DatabaseName"></param>
        /// <returns></returns>
        public static string BuildConnectionString(string serverName, string databaseName)
        {
            // Create an instance of the SqlConnectionStringBuilder
            SqlConnectionStringBuilder connectionStringBuilder = new System.Data.SqlClient.SqlConnectionStringBuilder();

            connectionStringBuilder.DataSource = serverName;
            connectionStringBuilder.InitialCatalog = databaseName;
            connectionStringBuilder.IntegratedSecurity = true;
            
            // Return Built Connection String
            return connectionStringBuilder.ConnectionString;
        }
        #endregion

        #region BuildConnectionString(string ServerName, string DatabaseName, string UserID, string Password)
        /// This method builds a connection string when SQLServer Authentication is used.
        /// A UserID and Password are required for this method.
        /// </summary>
        /// <param name="ServerName"></param>
        /// <param name="DatabaseName"></param>
        /// <param name="UserID"></param>
        /// <param name="Password"></param>
        /// <returns></returns>
        public static string BuildConnectionString(string serverName, string databaseName, string userID, string password)
        {
            // Create an instance of the SqlConnectionStringBuilder
            SqlConnectionStringBuilder connectionStringBuilder = new System.Data.SqlClient.SqlConnectionStringBuilder();

            // set the properties
            connectionStringBuilder.DataSource = serverName;
            connectionStringBuilder.InitialCatalog = databaseName;
            connectionStringBuilder.IntegratedSecurity = false;
            connectionStringBuilder.UserID = userID;
            connectionStringBuilder.Password = password;

            // Return Built Connection String
            return connectionStringBuilder.ConnectionString;
        }
        #endregion
        
    }
    #endregion

}
