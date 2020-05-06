using MySql.Data.MySqlClient;

namespace WindowsServerResourceMonitorMySQLRelayTool
{
    partial class SQLDataAddManager : MySQLExecution
    {
        private int GetDataNum(string TargetTable)
        {
            int Count = -1;
            Open();
            using (MySqlCommand command = CreateCommand($"SELECT COUNT(*) FROM {TargetTable}"))
            {
                using (MySqlDataReader reader = ExecuteQuery(command))
                {
                    if (reader.Read()) Count = reader.GetInt32(0);
                }
            }
            Close();
            return Count;
        }
        private void InsertAndDelete(MySqlCommand InsertCommand, string TableName)
        {
            Execute(InsertCommand);
            int DataNum = GetDataNum(TableName);
            if (DataNum > MaxDataCount) DeleteData(TableName, DataNum - MaxDataCount);
        }
        private void DeleteData(string TargetTable, int DeleteNum)
        {
            Open();
            using (MySqlCommand command = CreateCommand($"DELETE FROM {TargetTable} LIMIT {DeleteNum}"))
            {
                Execute(command);
            }
            Close();
        }
        private static string Digit(double val)
        {
            string Num = ((int)(val * 100 + 0.5)).ToString();
            return $"{Num.Substring(0, Num.Length - 2)}.{Num.Substring(Num.Length - 3)}";
        }
        private string CreateMemoryInfoInsertText(ResourceInfo.Memory MemoryInfo)
        {
            return $"{Digit(MemoryInfo.PhysicalMemory.Max)},{Digit(MemoryInfo.CommitMemory.Max)},{Digit(MemoryInfo.PhysicalMemory.Available)},{Digit(MemoryInfo.CommitMemory.Available)}"
                + $"{Digit(MemoryInfo.PhysicalMemory.Used)},{Digit(MemoryInfo.CommitMemory.Used)},{Digit(MemoryInfo.PhysicalMemory.UsedPer)},{Digit(MemoryInfo.CommitMemory.UsedPer)}";
        }
        private void InsertDiskInformation(ResourceInfo.Disk DiskInfo)
        {
            string TableName = $"DiskInfo_{DiskInfo.DiskDrive.Substring(0, 1)}_Drive";
            string Command = $"INSERT INTO {TableName}({table.Disk.TableStruct}) "
                + $"VALUES('{Digit(DiskInfo.DiskTotal.DiskCapacity)}{DiskInfo.DiskTotal.DiskCapacityUnit}',"
                + $"'{Digit(DiskInfo.DiskFreeSpace.DiskCapacity)}{DiskInfo.DiskFreeSpace.DiskCapacityUnit}',"
                + $"'{Digit(DiskInfo.DiskUsed.DiskCapacity)}{DiskInfo.DiskUsed.DiskCapacityUnit}',"
                + $"{Digit(DiskInfo.DiskFreeSpace.DiskCapacityPer)},{Digit(DiskInfo.DiskUsed.DiskCapacityPer)}"
                + $"{Digit(DiskInfo.DiskRead)},{Digit(DiskInfo.DiskWrite)})";
            InsertAndDelete(CreateCommand(Command), TableName);
        }
        private void InsertNetworkInformation(ResourceInfo.Network NetInfo, string InsertTableName)
        {
            string Command = $"INSERT INTO {InsertTableName}({table.Network.TableStruct}) VALUES({NetInfo.NetReceived},{NetInfo.NetSent})";
            InsertAndDelete(CreateCommand(Command), InsertTableName);
        }
    }
}
