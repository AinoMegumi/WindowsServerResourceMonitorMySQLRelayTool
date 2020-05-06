using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace WindowsServerResourceMonitorMySQLRelayTool
{
    namespace TableStructInformationImpl
    {
        namespace Structures
        {
            class Processor
            {
                [JsonProperty("usage")]
                public string ProcessorUsage;
                [JsonProperty("process")]
                public string ProcessNum;
                public override string ToString()
                {
                    return $"{ProcessorUsage}, {ProcessNum}";
                }
                public string GetTableCreateCommandImpl()
                {
                    return $"{ProcessorUsage} float, {ProcessNum} int";
                }
            }

            class Memory
            {
                [JsonProperty("totalphysical")]
                public string TotalPhysical;
                [JsonProperty("totalcommit")]
                public string TotalCommit;
                [JsonProperty("availphysical")]
                public string AvailPhys;
                [JsonProperty("availcommit")]
                public string AvailCommit;
                [JsonProperty("usedphysical")]
                public string UsedPhys;
                [JsonProperty("usedcommit")]
                public string UsedCommit;
                [JsonProperty("usedperphysical")]
                public string UsedPerPhys;
                [JsonProperty("usedpercommit")]
                public string UsedPerCommit;
                public override string ToString()
                {
                    return $"{TotalPhysical}, {TotalCommit}, {AvailPhys}, {AvailCommit}, {UsedPhys}, {UsedCommit}, {UsedPerPhys}, {UsedPerCommit}";
                }
                public string GetTableCreateCommandImpl()
                {
                    return $"{TotalPhysical} float, {TotalCommit} float, {AvailPhys} float, {AvailCommit} float, {UsedPhys} float, {UsedCommit} float, {UsedPerPhys} float, {UsedPerCommit} float";
                }
            }
            class Disk
            {
                [JsonProperty("total")]
                public string DiskTotal;
                [JsonProperty("free")]
                public string DiskFree;
                [JsonProperty("used")]
                public string DiskUsed;
                [JsonProperty("freeper")]
                public string DiskFreePer;
                [JsonProperty("usedper")]
                public string DiskUsedPer;
                [JsonProperty("read")]
                public string DiskRead;
                [JsonProperty("write")]
                public string DiskWrite;
                public override string ToString()
                {
                    return $"{DiskTotal}, {DiskFree}, {DiskUsed}, {DiskFreePer}, {DiskUsedPer}, {DiskRead}, {DiskWrite}";
                }
                public string GetTableCreateCommandImpl()
                {
                    return $"{DiskTotal} TEXT, {DiskFree} TEXT, {DiskUsed} TEXT, {DiskFreePer} float, {DiskUsedPer} float, {DiskRead} float, {DiskWrite} float";
                }

            }
            class Network
            {
                [JsonProperty("receive")]
                public string NetReceive;
                [JsonProperty("send")]
                public string NetSend;
                public override string ToString()
                {
                    return $"{NetReceive}, {NetSend}";
                }
                public string GetTableCreateCommandImpl()
                {
                    return $"{NetReceive} float, {NetSend} float";
                }
            }
        }
        class Processor
        {
            [JsonProperty("name")]
            public string TableName;
            [JsonProperty("struct")]
            public Structures.Processor TableStruct;
            public string CreateTableCommand()
            {
                return $"CREATE TABLE IF NOT EXISTS {TableName}({TableStruct.GetTableCreateCommandImpl()}";
            }
        }
        class Memory
        {
            [JsonProperty("name")]
            public string TableName;
            [JsonProperty("struct")]
            public Structures.Memory TableStruct;
            public string CreateTableCommand()
            {
                return $"CREATE TABLE IF NOT EXISTS {TableName}({TableStruct.GetTableCreateCommandImpl()}";
            }
        }
        class Disk
        {
            [JsonProperty("struct")]
            public Structures.Disk TableStruct;
            public string CreateTableCommand(string TableName)
            {
                return $"CREATE TABLE IF NOT EXISTS {TableName}({TableStruct.GetTableCreateCommandImpl()}";
            }
        }
        class Network
        {
            [JsonProperty("struct")]
            public Structures.Network TableStruct;
            public string CreateTableCommand(string TableName)
            {
                return $"CREATE TABLE IF NOT EXISTS {TableName}({TableStruct.GetTableCreateCommandImpl()}";
            }
        }
    }
    class TableStructInformation
    {
        [JsonProperty("processor")]
        public TableStructInformationImpl.Processor Processor;
        [JsonProperty("memory")]
        public TableStructInformationImpl.Memory Memory;
        [JsonProperty("disk")]
        public TableStructInformationImpl.Disk Disk;
        [JsonProperty("network")]
        public TableStructInformationImpl.Network Network;
    }
}
