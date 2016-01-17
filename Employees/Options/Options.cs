using System.Configuration;

namespace Employees.Options
{
    public class Options
    {
        /// <summary>
        /// Returns the Connection String with Database
        /// </summary>

        public static string ConnectionString
        {
            get
            {
                // Default Connection String
                string defaultConnectionString = "Data Source=(local);Initial Catalog=BaseOfEmployees;Integrated Security=True";

                ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings["BaseOfEmployees"];

                return settings != null ? settings.ConnectionString : defaultConnectionString;
            }
        }
    }
}