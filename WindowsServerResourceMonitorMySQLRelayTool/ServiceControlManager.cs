using System.ServiceProcess;

namespace WindowsServerResourceMonitorMySQLRelayTool
{
    class ServiceControlManager
    {
        private ServiceController sc;
        public ServiceControlManager(string ServiceName)
        {
            sc = new ServiceController(ServiceName);
        }
        public ServiceControllerStatus GetServiceStatus()
        {
            return sc.Status;
        }
        public void Update()
        {
            sc.Refresh();
        }
    }
}
