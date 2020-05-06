using System.Collections.Generic;
using Newtonsoft.Json;

namespace WindowsServerResourceMonitorMySQLRelayTool
{
    namespace ResourceInfo
    {
        namespace SubClass
        {
            class Memory
            {
                [JsonProperty("total")]
                public float Max;
                [JsonProperty("available")]
                public float Available;
                [JsonProperty("used")]
                public float Used;
                [JsonProperty("usedper")]
                public float UsedPer;
            }

            class DiskCapacityInfo
            {
                [JsonProperty("capacity")]
                public float DiskCapacity;
                [JsonProperty("per")]
                public float DiskCapacityPer;
                [JsonProperty("unit")]
                public string DiskCapacityUnit;
            }
            
            class DiskTotalInfo
            {
                [JsonProperty("capacity")]
                public float DiskCapacity;
                [JsonProperty("unit")]
                public string DiskCapacityUnit;
            }
        }
        class Processor
        {
            [JsonProperty("name")]
            public string ProcessorName;
            [JsonProperty("process")]
            public int ProcessNum;
            [JsonProperty("usage")]
            public float ProcessorUsage;
        }

        class Memory
        {
            [JsonProperty("physical")]
            public SubClass.Memory PhysicalMemory;
            [JsonProperty("commit")]
            public SubClass.Memory CommitMemory;
        }

        class Disk
        {
            [JsonProperty("drive")]
            public string DiskDrive;
            [JsonProperty("total")]
            public SubClass.DiskTotalInfo DiskTotal;
            [JsonProperty("used")]
            public SubClass.DiskCapacityInfo DiskUsed;
            [JsonProperty("free")]
            public SubClass.DiskCapacityInfo DiskFreeSpace;
            [JsonProperty("read")]
            public float DiskRead;
            [JsonProperty("write")]
            public float DiskWrite;
        }

        class Network
        {
            [JsonProperty("device")]
            public string DeviceName;
            [JsonProperty("receive")]
            public float NetReceived;
            [JsonProperty("send")]
            public float NetSent;
        }

        class Service
        {
            [JsonProperty("display")]
            public string ServiceDisplayName;
            [JsonProperty("name")]
            public string ServiceName;
            [JsonProperty("status")]
            public string ServiceStatus;
            [JsonProperty("type")]
            public string ServiceType;
        }
    }
    class ResponseManager
    {
        [JsonProperty("cpu")]
        public ResourceInfo.Processor Processor;
        [JsonProperty("memory")]
        public ResourceInfo.Memory Memory;
        [JsonProperty("disk")]
        public List<ResourceInfo.Disk> Disk;
        [JsonProperty("network")]
        public List<ResourceInfo.Network> Network;
        [JsonProperty("service")]
        public List<ResourceInfo.Service> Service;
        public static ResponseManager ParseResponseData(string ResponseBody)
        {
            return JsonConvert.DeserializeObject<ResponseManager>(ResponseBody);
        }
    }
}
