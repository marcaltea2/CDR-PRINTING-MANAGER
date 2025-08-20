using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cdr_reset.Common.Helper._configuration
{
    public class Configuration
    {
        #region Application

        #region Application Code

        /// <summary>
        /// Gets the application id.
        /// </summary>
        public static string ApplicationCode
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["AppCode"];
            }
        }

        #endregion

        #region TemporaryFolder

        /// <summary>
        /// Gets the location of temporary path.
        /// </summary>
        public static string TemporaryFolder
        {
            get
            {
                string path = System.Configuration.ConfigurationManager.AppSettings["TempFolder"];
                if (path.Length > 0)
                    if (path.Substring(path.Length - 1) != "\\") path = path + "\\";
                return path;
            }
        }

        #endregion

        #endregion

        #region Connection

        #region CommandTimeout

        /// <summary>
        /// Gets the wait time before terminating the attempt to execute a command
        /// and generating an error.
        /// </summary>
        public static int CommandTimeout
        {
            get
            {
                return Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["CommandTimeout"]);
            }
        }

        #endregion

        #region ConnectionStringName

        /// <summary>
        /// Gets the name of the current connection for the application.
        /// </summary>
        public static string ConnectionStringName
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["ConnectionStringName"];
            }
        }

        #endregion

        #region ConnectionString

        /// <summary>
        /// Gets the connection used to open a database
        /// </summary>
        public static string ConnectionString
        {
            get
            {
                return System.Configuration.ConfigurationManager.ConnectionStrings[ConnectionStringName].ConnectionString;
            }
        }

        #endregion

        #endregion
    }
}


