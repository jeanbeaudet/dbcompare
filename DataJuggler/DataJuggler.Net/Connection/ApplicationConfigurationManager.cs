

#region using statements

using System;
using System.Configuration;
using System.Reflection;
using System.Xml;
using System.IO;

#endregion

namespace DataJuggler.Net.Connection
{

    #region class ApplicationConfigurationManager
    /// <summary>
    /// This class is used to read and store values 
    /// from the app.config file.
    /// </summary>
    public class ApplicationConfigurationManager
    {

        #region Private Variables
        private ApplicationLogin applicationLogin;
        private ConnectionInfo connectionInfo;
        private XmlDocument xmlDoc;
        private Exception lastError;
        private string validatationErrors;
        private string encryptionKey;
        #endregion

        #region Constructors

            #region Default Constructor
            /// <summary>
            /// Creates a new instance of a RADApplicationConfirmation object.
            /// This object represents the values from the app.config file.
            /// The values will always be publicly unencrypted, but when you 
            /// save this file, the values are stored with encryption for security.
            /// </summary>
            public ApplicationConfigurationManager()
            {
                // Startup 
                Init();
            }
            #endregion

            #region Parameterized Constructor(string encryptionKey)
            /// <summary>
            /// Creates a new instance of a RADApplicationConfirmation object.
            /// This object represents the values from the app.config file.
            /// The values will always be publicly unencrypted, but when you 
            /// save this file, the values are stored with encryption for security.
            /// </summary>
            public ApplicationConfigurationManager(string encryptionKey)
            {
                // set the encryption key
                this.EncryptionKey = encryptionKey;

                // Startup 
                Init();
            }
            #endregion

        #endregion

        #region Methods

            #region Init()
            /// <summary>
            /// This method performs any startup functionality 
            /// required for this object.
            /// </summary>
            private void Init()
            {
                // Set the Validation Errors to an empty string
                this.ValidatationErrors = "";

                // Load the configuration document
                this.LoadConfigDocument();
            }
            #endregion

            #region AppConfigFileName()
            /// <summary>
            /// This method returns the path to the 
            /// </summary>
            /// <returns></returns>
            private string AppConfigFileName()
            {
                // get fileName
                string fileName = AppDomain.CurrentDomain.BaseDirectory + "System\\App.config";

                // return value
                return fileName;
            }
            #endregion

            #region LoadConfigDocument()
            /// <summary>
            /// This method loads the app.config doc
            /// </summary>
            /// <returns></returns>
            public XmlDocument LoadConfigDocument()
            {
                // create the doc
                XmlDocument doc = null;

                try
                {
                    // create the file
                    doc = new XmlDocument();

                    // get the config file path
                    string configFilePath = GetConfigFilePath();

                    // do we have a ocnfig file path
                    bool hasConfigFilePath = (!String.IsNullOrEmpty(configFilePath));

                    // if the file was found
                    if (hasConfigFilePath)
                    {
                        // if the config file path exists
                        if (File.Exists(configFilePath))
                        {
                            // load the doc
                            doc.Load(configFilePath);

                            // set the xml document
                            this.XmlDoc = doc;
                        }
                        else
                        {
                            // raise the error
                            throw new FileNotFoundException("No configuration file found.");
                        }
                    }
                    else
                    {
                        // raise the error
                        throw new FileNotFoundException("No configuration file found.");
                    }
                }
                catch (System.IO.FileNotFoundException e)
                {
                    throw new Exception("No configuration file found.", e);
                }

                // return the doc
                return doc;
            }
            #endregion

            #region GetConfigFilePath()
            /// <summary>
            /// this method returns the location of the app.config file.
            /// </summary>
            /// <returns></returns>
            private static string GetConfigFilePath()
            {
                // path to the app.config file.
                string configFilePath = Environment.CurrentDirectory;
                
                // if the curent direct does not end with a back slash
                if (!configFilePath.EndsWith(@"\"))
                {
                    // add a back slash
                    configFilePath += @"\";
                }

                // test only
                string frieldName = AppDomain.CurrentDomain.FriendlyName;

                // now add .config
                configFilePath += frieldName += ".config";

                // return value
                return configFilePath;
            }
            #endregion

            #region KeyExists(string strKey)
            /// <summary>
            /// Determines if a key exists within the App.config
            /// </summary>
            /// <param name="strKey"></param>
            /// <returns></returns>
            public bool KeyExists(string key)
            {
                // initial value
                bool keyExists = false;



                // Get appSettingsNode
                XmlNode appSettingsNode = xmlDoc.SelectSingleNode("configuration/appSettings");

                // verify appSettingsNode exists
                if (appSettingsNode != null)
                {
                    // Attempt to locate the requested setting.
                    foreach (XmlNode childNode in appSettingsNode)
                    {
                        // Get name of this key (in lowercase)
                        string name = childNode.Attributes["key"].Value.ToLower();

                        // if this is the correct key
                        if (name == key.ToLower())
                        {
                            // set keyExists to true
                            keyExists = true;

                            // break out of for loop
                            break;
                        }
                    }
                }

                // return value
                return keyExists;
            }
            #endregion

            #region Read()
            /// <summary>
            /// This method reads the app.config file and decrypts the values
            /// to load the properties for this object.
            /// </summary>
            /// <param name="readConnectionInfo"></param>
            /// <param name="readLoginInfo"></param>
            /// <returns></returns>
            public bool Read(bool readConnectionInfo, bool readLoginInfo)
            {
                // initial value
                bool readSuccess = false;

                // locals
                bool connectionRead = false;
                bool loginRead = false;

                try
                {  
                    // if read connection info
                    if (readConnectionInfo)
                    {
                        // Create the Connectioninfo
                        this.ConnectionInfo = new Connection.ConnectionInfo();

                        // Read each property
                        this.ConnectionInfo.ConnectionString = ReadAppSetting("ConnectionString");

                        // if the connection string is not set
                        if (!this.ConnectionInfo.HasConnectionString)
                        {
                            // read the other properies so the connection string can be built
                            this.ConnectionInfo.DatabaseName = ReadAppSetting("DatabaseName");
                            this.ConnectionInfo.DatabasePassword = ReadAppSetting("DatabasePassword");
                            this.ConnectionInfo.DatabaseServer = ReadAppSetting("DatabaseServer");
                            this.ConnectionInfo.DatabaseUserName = ReadAppSetting("DatabaseUserName");  

                            // set the vale for integratedsecurity
                            string integratedSecurity = ReadAppSetting("IntegratedSecurity");
                            bool useIntegratedSecurity = false;

                            // if the string exists
                            if (!String.IsNullOrEmpty(integratedSecurity))
                            {
                                // set the value for useIntegretedSecurity
                                useIntegratedSecurity = Convert.ToBoolean(integratedSecurity);
                            }
                            
                            // Set the value for IntegratedSecurity
                            this.ConnectionInfo.IntegratedSecurity = useIntegratedSecurity;
                        }

                        // the connection was read
                        connectionRead = this.ConnectionInfo.Validate();
                    }
                    else
                    {
                        // the connection was read
                        connectionRead = true;
                    }

                    // if the login should be read
                    if (readLoginInfo)
                    {
                        // Create the app login
                        this.ApplicationLogin = new ApplicationLogin();

                        // set the login read if the ApplicationLogin is valid
                        loginRead = this.ApplicationLogin.Validate();
                    }
                    else
                    {
                        // the login was considered red
                        loginRead = true;
                    }
                    
                    // set readSuccess to true
                    readSuccess = true;
                }
                catch (Exception error)
                {
                    // set the LastError
                    this.LastError = error;
                }

                // return value
                return readSuccess;
            }
            #endregion

            #region ReadAppSetting(string settingName)
            /// <summary>
            /// This method reads and decrypts the values
            /// in the app.config appSettings section.
            /// </summary>
            /// <param name="settingName"></param>
            /// <returns></returns>
            private string ReadAppSetting(string settingName)
            {
                // initial value
                string settingValue = null;

                try
                {  
                    // encrypted value
                    string encryptedValue = ConfigurationManager.AppSettings[settingName].ToString();

                    // if the EncryptionKey is set
                    if (this.HasEncryptionKey)
                    {
                        // now decrypt value with EncryptionKey
                        settingValue = CryptographyManager.DecryptString(encryptedValue, this.EncryptionKey);
                    }
                    else
                    {
                        // now decrypt value without EncyptionKey
                        settingValue = CryptographyManager.DecryptString(encryptedValue);
                    }
                }
                catch (Exception error)
                {
                    // set the last error
                    this.LastError = error;
                }


                // return value
                return settingValue;
            }
            #endregion

            #region UpdateKey(string key, string newValue, bool saveFile)
            /// <summary>
            /// 
            /// </summary>
            /// <param name="key"></param>
            /// <param name="newValue"></param>
            public void UpdateKey(string key, string newValue, bool saveFile)
            {
                // if the XmlDocument exists
                if (this.HasXmlDoc)
                {
                    // if the key exists
                    if (KeyExists(key))
                    {
                        // Select appSettingsNode
                        XmlNode appSettingsNode = this.XmlDoc.SelectSingleNode("configuration/appSettings");

                        // verify appSettingsNode exists
                        if (appSettingsNode != null)
                        {
                            // Attempt to locate the requested setting.
                            foreach (XmlNode childNode in appSettingsNode)
                            {
                                // if this is the correct key
                                if (childNode.Attributes["key"].Value.ToString().ToLower() == key.ToLower())
                                {
                                    // update value of this attribute
                                    childNode.Attributes["value"].Value = newValue;

                                    // break out of for loop
                                    break;
                                }
                            }
                        }

                        // save the file
                        if (saveFile)
                        {
                            // Save the app.config file
                            string currentConfigFile = GetConfigFilePath();
                            string exeConfigFile = currentConfigFile.Replace(".vshost", "");
                            int binIndex = currentConfigFile.IndexOf(@"\bin");
                            string appConfigFile = "unknown";
                           
                           // if the binIndex was found 
                           if (binIndex >= 10)
                           {
                                // set the apConfigFile
                                appConfigFile = currentConfigFile.Substring(0, binIndex) + @"\App.config";
                           }
                           

                            // verify fileName exists
                            if (File.Exists(currentConfigFile))
                            {
                                // Save the app.config file.
                                this.XmlDoc.Save(currentConfigFile);
                            }
                            
                            // if the exe file exists
                            if (File.Exists(exeConfigFile))
                            {
                                // Save the app.config file.
                                this.XmlDoc.Save(exeConfigFile);
                            }

                            //// if the appConfigFile file exists
                            if (File.Exists(appConfigFile))
                            {
                                // Save the app.config file.
                                this.XmlDoc.Save(appConfigFile);
                            }
                        }
                    }
                    else
                    {
                        // raise an error
                        throw new ArgumentNullException("Key", "<" + key + "> does not exist in the configuration. Update failed.");
                    }
                }
                else
                {
                    // raise an error
                    throw new ArgumentNullException("The XmlDocument does not exist.");
                }
            }
            #endregion

            #region UpdateKey(string key, string newValue)
            /// <summary>
            /// 
            /// </summary>
            /// <param name="key"></param>
            /// <param name="newValue"></param>
            public void UpdateKey(string key, string newValue)
            {
                // call the override so you do not break anything
                this.UpdateKey(key, newValue, true);
            }
            #endregion

            #region SaveConnection(ConnectionInfo connection)
            /// <summary>
            /// This method saves the current settings back to the app.config file.
            /// </summary>
            public bool SaveConnection(ConnectionInfo connection)
            {
                // initial value
                bool saved = false;

                // set the connection
                this.ConnectionInfo = connection;

                // locals
                string encryptedConnectionString = "";
                string encryptedDatabaseName = "";
                string encryptedDatabasePassword = "";
                string encryptedDatabaseServer = "";
                string encryptedDatabaseUserName = "";

                // if the connection exists
                if (connection != null)
                {
                    // if the connection is valid
                    if (connection.Validate())
                    {
                        // if the connection string is set
                        if (connection.HasConnectionString)
                        {
                            // if the EncryptionKey has been set
                            if (this.HasEncryptionKey)
                            {
                                // encrypted the connection string
                                encryptedConnectionString = CryptographyManager.EncryptString(connection.ConnectionString, this.EncryptionKey);
                            }
                            else
                            {
                                // encrypted the connection string
                                encryptedConnectionString = CryptographyManager.EncryptString(connection.ConnectionString);
                            }

                            // Update the connection string
                            this.UpdateKey("ConnectionString", encryptedConnectionString);
                        }
                        else
                        {
                            // if the EncryptionKey has been set
                            if (this.HasEncryptionKey)
                            {
                                // encrypt each string with a password
                                encryptedDatabaseName = CryptographyManager.EncryptString(this.ConnectionInfo.DatabaseName, this.EncryptionKey);
                                encryptedDatabasePassword = CryptographyManager.EncryptString(this.ConnectionInfo.DatabasePassword, this.EncryptionKey);
                                encryptedDatabaseServer = CryptographyManager.EncryptString(this.ConnectionInfo.DatabaseServer, this.EncryptionKey);
                                encryptedDatabaseUserName = CryptographyManager.EncryptString(this.ConnectionInfo.DatabaseUserName, this.EncryptionKey);
                            }
                            else
                            {
                                // encrypt each string without password
                                encryptedDatabaseName = CryptographyManager.EncryptString(this.ConnectionInfo.DatabaseName);
                                encryptedDatabasePassword = CryptographyManager.EncryptString(this.ConnectionInfo.DatabasePassword);
                                encryptedDatabaseServer = CryptographyManager.EncryptString(this.ConnectionInfo.DatabaseServer);
                                encryptedDatabaseUserName = CryptographyManager.EncryptString(this.ConnectionInfo.DatabaseUserName);
                            }

                            // now update each keys
                            this.UpdateKey("DatabaseName", encryptedDatabaseName, false);
                            this.UpdateKey("DatabasePassword", encryptedDatabasePassword, false);
                            this.UpdateKey("DatabaseServer", encryptedDatabaseServer, false);
                            this.UpdateKey("DatabaseUserName", encryptedDatabaseUserName, true);
                        }

                        // set saved to true
                        saved = true;
                    }
                    else
                    {
                        // add to the validation errors
                        this.ValidatationErrors += "The connection does not validate.";

                        // did not save
                        saved = false;
                    }
                }
                else
                {
                    // add to the validation errors
                    this.ValidatationErrors += "The connection does not exist.";

                    // did not save
                    saved = false;
                }

                // return value
                return saved;
            }
            #endregion

            #region SaveLogin(ApplicationLogin login)
            /// <summary>
            /// This method saves the current settings back to the app.config file.
            /// </summary>
            public bool SaveLogin(ApplicationLogin login)
            {
                // initial value
                bool saved = false;

                // if the login exists
                if (login != null)
                {
                    // first check if save password is true or not
                    if (!login.SavePassword)
                    {
                        // set password to null
                        login.Password = null;
                    }

                    // Encrypt Each Property
                    string encryptedPassword = "";
                    
                    // if the save password exists
                    if (login.HasPassword)
                    {
                        // Save Login Information
                        
                        // set the encrypted password
                        encryptedPassword = CryptographyManager.EncryptString(login.Password);

                        // Set the EncryptedPassword
                        this.UpdateKey("ApplicationPassword", encryptedPassword);
                    }

                    // encrypt the user name
                    string encryptedUserName = CryptographyManager.EncryptString(login.UserName);

                    // save the user name
                    this.UpdateKey("ApplicationUserName", encryptedUserName);
                }
                else
                {  
                    // add to the validation errors
                    this.ValidatationErrors += "The login does not exist.";
                 
                    // did not save
                    saved = false;
                }

                // return value
                return saved;
            }
            #endregion

            #region Validate()
            /// <summary>
            /// This method returns true if the values for DatabaseServer,
            /// DatabaseUserName & Databasepassword are present.
            /// </summary>
            /// <returns></returns>
            public bool Validate(bool saveConnection, bool saveLogin)
            {
                // initial value
                bool valid = false;

                // locals
                bool validConnection = false;
                bool validLogin = false;

                // if saveConnection
                if (saveConnection) 
                {  
                    // if the connection info exists
                    if (this.HasConnectionInfo)
                    {
                        // validate the connection info
                        validConnection = this.ConnectionInfo.Validate();
                    }
                    else
                    {
                        // Add to the validation errors
                        this.ValidatationErrors += "The connection info does not exist.";
                    }
                }

                // if saveLogin
                if (saveLogin)
                {
                    // if the Login exists
                    if (this.HasApplicationLogin)
                    {
                        // Validate the applicationLogin login
                        validLogin = this.ApplicationLogin.Validate();
                    }
                    else
                    {
                        // Add to the validation errors
                        this.ValidatationErrors += "The login does not exist.";
                    }
                }

                // return value
                return valid;
            }
            #endregion

        #endregion

        #region Properties
   
            #region ApplicationLogin
            /// <summary>
            /// This property gets or sets the value for 'ApplicationLogin'.
            /// </summary>
            public ApplicationLogin ApplicationLogin
            {
                get { return applicationLogin; }
                set { applicationLogin = value; }
            }
            #endregion
            
            #region ConnectionInfo
            /// <summary>
            /// This property gets or sets the value for 'ConnectionInfo'.
            /// </summary>
            public ConnectionInfo ConnectionInfo
            {
                get { return connectionInfo; }
                set { connectionInfo = value; }
            }
            #endregion
            
            #region EncryptionKey
            /// <summary>
            /// This property gets or sets the value for 'EncryptionKey'.
            /// </summary>
            public string EncryptionKey
            {
                get { return encryptionKey; }
                set { encryptionKey = value; }
            }
            #endregion
            
            #region HasApplicationLogin
            /// <summary>
            /// This property returns true if this object has an 'ApplicationLogin'.
            /// </summary>
            public bool HasApplicationLogin
            {
                get
                {
                    // initial value
                    bool hasApplicationLogin = (this.ApplicationLogin != null);
                    
                    // return value
                    return hasApplicationLogin;
                }
            }
            #endregion
            
            #region HasConnectionInfo
            /// <summary>
            /// This property returns true if this object has a 'ConnectionInfo'.
            /// </summary>
            public bool HasConnectionInfo
            {
                get
                {
                    // initial value
                    bool hasConnectionInfo = (this.ConnectionInfo != null);
                    
                    // return value
                    return hasConnectionInfo;
                }
            }
            #endregion
            
            #region HasEncryptionKey
            /// <summary>
            /// This property returns true if the 'EncryptionKey' exists.
            /// </summary>
            public bool HasEncryptionKey
            {
                get
                {
                    // initial value
                    bool hasEncryptionKey = (!String.IsNullOrEmpty(this.EncryptionKey));
                    
                    // return value
                    return hasEncryptionKey;
                }
            }
            #endregion
            
            #region HasXmlDoc
            /// <summary>
            /// This property returns true if this object has a 'XmlDoc'.
            /// </summary>
            public bool HasXmlDoc
            {
                get
                {
                    // initial value
                    bool hasXmlDoc = (this.XmlDoc != null);
                    
                    // return value
                    return hasXmlDoc;
                }
            }
            #endregion
            
            #region LastError
            /// <summary>
            /// This property gets or sets the value for 'LastError'.
            /// </summary>
            public Exception LastError
            {
                get { return lastError; }
                set { lastError = value; }
            }
            #endregion
            
            #region ValidatationErrors
            /// <summary>
            /// This property gets or sets the value for 'ValidatationErrors'.
            /// </summary>
            public string ValidatationErrors
            {
                get { return validatationErrors; }
                set { validatationErrors = value; }
            }
            #endregion
            
            #region XmlDoc
            /// <summary>
            /// This property gets or sets the value for 'XmlDoc'.
            /// </summary>
            public XmlDocument XmlDoc
            {
                get { return xmlDoc; }
                set { xmlDoc = value; }
            }
            #endregion
         
        #endregion

    }
    #endregion

}

