using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace WindowsServerResourceMonitorMySQLRelayTool
{
    namespace RelayConfigImpl
    {
        public class MySQL
        {
            [JsonProperty("host")]
            public string HostName;
            [JsonProperty("database")]
            public string DatabaseName;
            [JsonProperty("user")]
            public string UserName;
            [JsonProperty("pass")]
            public string Password;
            [JsonProperty("structinfo")]
            public string StructInfoFilePath;
        }

        public class PathInformation
        {
            [JsonProperty("signin")]
            public string SignInPath;
            [JsonProperty("getresource")]
            public string GetResourcePath;
            [JsonProperty("signout")]
            public string SignOutPath;
        }

        public class Server
        {
            [JsonProperty("servicename")]
            public string ServiceName;
            [JsonProperty("host")]
            public string HostName;
            [JsonProperty("port")]
            public int Port;
            [JsonProperty("path")]
            public PathInformation ConnectPath;
            [JsonProperty("user")]
            public string UserName;
            [JsonProperty("pass")]
            public string Password;
        }

        public class RelayConfigInformation
        {
            [JsonProperty("mysql")]
            public MySQL SQLConfig;
            [JsonProperty("server")]
            public Server ResourceServer;
            [JsonProperty("processtime")]
            public int ProcessLoopTime;
            [JsonProperty("entrynum")]
            public int DataEntryMaxNum;
        }
    }
    public class RelayConfig
    {
        private readonly RelayConfigImpl.RelayConfigInformation config;
        public RelayConfig()
        {
            config = JsonConvert.DeserializeObject<RelayConfigImpl.RelayConfigInformation>(File.ReadAllText("relay.json"));
        }
        private string GetRootPath() { return "http://" + config.ResourceServer.HostName + ":" + config.ResourceServer.Port.ToString(); }
        public string GetServerServiceName() { return GetRootPath() + config.ResourceServer.ServiceName; }
        public string GetSigninUrl() { return GetRootPath() + config.ResourceServer.ConnectPath.SignInPath; }
        public string GetSignOutUrl() { return GetRootPath() + config.ResourceServer.ConnectPath.SignOutPath; }
        public string GetResourceUrl() { return GetRootPath() + config.ResourceServer.ConnectPath.GetResourcePath; }
        public string GetSQLExecuteInformation()
        {
            return "Server=" + config.SQLConfig.HostName
                + ";User ID=" + config.SQLConfig.UserName
                + ";Password=" + config.SQLConfig.Password 
                + ";Database=" + config.SQLConfig.DatabaseName;
        }
        public int GetSQLMaxDataNum()
        {
            return config.DataEntryMaxNum;
        }
        public string GetTableStructInfoFile()
        {
            return config.SQLConfig.StructInfoFilePath;
        }
    }
}
