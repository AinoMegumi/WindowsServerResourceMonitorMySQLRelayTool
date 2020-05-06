using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;

namespace WindowsServerResourceMonitorMySQLRelayTool
{
    class Program
    {
        public static void Main(string[] args)
        {
            Directory.SetCurrentDirectory(Directory.GetParent(Assembly.GetExecutingAssembly().Location).FullName);
            RelayConfig relay = new RelayConfig();
            ServiceControlManager sc = new ServiceControlManager(relay.GetServerServiceName());
            RequestManager request = new RequestManager();
            SQLDataAddManager exec = new SQLDataAddManager(relay.GetSQLExecuteInformation(), relay.GetTableStructInfoFile(), relay.GetSQLMaxDataNum());
            for (System.ServiceProcess.ServiceControllerStatus status = sc.GetServiceStatus();
                status != System.ServiceProcess.ServiceControllerStatus.StopPending;
                status = sc.GetServiceStatus())
            {
                if (status != System.ServiceProcess.ServiceControllerStatus.Running) continue;

            }
        }
    }
}
