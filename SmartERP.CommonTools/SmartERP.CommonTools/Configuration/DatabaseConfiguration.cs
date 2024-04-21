namespace SmartERP.CommonTools.Configuration
{
    using System.Text.RegularExpressions;
    using Microsoft.Extensions.Configuration;

    public class DatabaseConfiguration
    {
        private const string XmlPath = @"/config/DatabaseConfiguration.xml";

        private const string DebugXmlPath = @"C:\Services\IS3\Config\DatabaseConfiguration.xml";

        #region Singleton

        private static object objLock = new object();

        private static DatabaseConfiguration instance;

        private DatabaseConfiguration()
        {
        }

        public static DatabaseConfiguration Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (objLock)
                    {
                        if (instance == null)
                        {
                            instance = ReadXmlConfig();
                        }
                    }
                }

                return instance;
            }
        }

        public void RefreshXmlData()
        {
            lock (objLock)
            {
                instance = ReadXmlConfig();
            }
        }

        #endregion Singleton

        #region Properties

        public string DatabaseServer { get; set; } = "10.3.1.12";

        public string DatabaseName { get; set; } = "PROFIREAL_PL_Testing10";

        public int AccessTokenValidityTimeInMinutes { get; set; }

        public int RefreshTokenValidityTimeInMinutes { get; set; }

        #endregion Properties

        #region Methods

        public string GetConnectionString(IConfiguration configuration)
        {
            IConfigurationSection connectionStringSection = configuration.GetSection("ConnectionStrings");
            IConfigurationSection propertiesSection = configuration.GetSection("Properties");

            string user = connectionStringSection["User"];
            string password = connectionStringSection["Password"];
            string applicationName = propertiesSection["Application"];
            applicationName = string.IsNullOrWhiteSpace(applicationName) ? "AuthenticationService" : applicationName;

            user = ConnectionStringDataEncoder.DecryptHash(user);
            password = ConnectionStringDataEncoder.DecryptHash(password);

            string connectionString =
                $"Data Source={DatabaseServer}; Integrated Security=False; Initial Catalog={DatabaseName}; MultipleActiveResultSets=True; " +
                $"User Id = {user}; Password = {password}; connection timeout =15;Application Name={applicationName}; Encrypt=False;" +
                $"TrustServerCertificate=True; MultiSubnetFailover=True; Pooling=False;";

            return connectionString;
        }

        public bool IsBase64String(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                return false;
            }

            text = text.Trim();
            return text.Length != 0
                   && text.Length % 4 == 0
                   && Regex.IsMatch(text, @"^[a-zA-Z0-9\+/]*={0,3}$", RegexOptions.None);
        }

        private static DatabaseConfiguration? ReadXmlConfig()
        {
            DatabaseConfiguration settings;
            System.Xml.Serialization.XmlSerializer xmlSerializer = new System.Xml.Serialization.XmlSerializer(typeof(DatabaseConfiguration));

            if (File.Exists(XmlPath))
            {
                using (Stream stream = new FileStream(XmlPath, FileMode.Open))
                {
                    settings = (DatabaseConfiguration)xmlSerializer.Deserialize(stream);
                }
            }
            else
            {
                using (Stream stream = new FileStream(DebugXmlPath, FileMode.Open))
                {
                    settings = (DatabaseConfiguration)xmlSerializer.Deserialize(stream);
                }
            }

            return settings;
        }

        #endregion Methods
    }
}