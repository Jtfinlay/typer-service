using System;

namespace TyperService.Dependencies.TableStorage
{
    /// <summary>
    /// Describes an object that contains the details needed to secure a storage connection.
    /// </summary>
    public class AzureTableSettings
    {
        /// <summary>
        /// Secret connection string to the store.
        /// </summary>
        public string ConnectionString { get; }
        
        /// <summary>
        /// Name of the target table.
        /// </summary>
        public string TableName { get; }

        /// <summary>
        /// Initialize a new instance of the <see cref="AzureTableSettings"/> class.
        /// </summary>
        /// <param name="connectionString">Secret connection string to the store.</param>
        /// <param name="tableName">Name of the target table.</param>
        public AzureTableSettings(string connectionString, string tableName)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new ArgumentNullException($"{nameof(connectionString)} is not a valid string");
            }

            if (string.IsNullOrWhiteSpace(tableName))
            {
                throw new ArgumentNullException($"{nameof(tableName)} is not a valid string");
            }

            (this.ConnectionString, this.TableName) = 
                (connectionString, tableName);
        }
    }
}
