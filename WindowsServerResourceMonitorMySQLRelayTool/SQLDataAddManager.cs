using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using MySql.Data.MySqlClient;

namespace WindowsServerResourceMonitorMySQLRelayTool
{
    partial class SQLDataAddManager : MySQLExecution
    {
        private readonly int MaxDataCount;
        private readonly TableStructInformation table;
        public SQLDataAddManager(string SQLConnectInformation, string TableStructInfoFile, int MaxDataNum)
            : base(SQLConnectInformation)
        {
            MaxDataCount = MaxDataNum;
            table = JsonConvert.DeserializeObject<TableStructInformation>(File.ReadAllText(TableStructInfoFile));
        }
        private void InsertProcessorInformation(ResourceInfo.Processor ProcessorInfo)
        {
            string Command = $"INSERT INTO {table.Processor.TableName}({table.Processor.TableStruct}) VALUES({Digit(ProcessorInfo.ProcessNum)},{ProcessorInfo.ProcessNum})";
            InsertAndDelete(CreateCommand(Command), table.Processor.TableName);
        }
        private void InsertMemoryInformation(ResourceInfo.Memory MemoryInfo)
        {
            string Command = $"INSERT INTO {table.Memory.TableName}({table.Memory.TableStruct}) VALUES({CreateMemoryInfoInsertText(MemoryInfo)})";
            InsertAndDelete(CreateCommand(Command), table.Memory.TableName);
        }
        private void InsertDiskInformation(List<ResourceInfo.Disk> Disks)
        {
            foreach (ResourceInfo.Disk d in Disks) InsertDiskInformation(d);
        }
        private void InsertNetworkInformation(List<ResourceInfo.Network> Networks)
        {
            for (int i = 0; i < Networks.Count; i++) InsertNetworkInformation(Networks[i], $"Network_eth{i}");
        }
        public void Insert(ResponseManager res)
        {
            InsertProcessorInformation(res.Processor);
            InsertMemoryInformation(res.Memory);
            InsertDiskInformation(res.Disk);
            InsertNetworkInformation(res.Network);
        }
    }
}
