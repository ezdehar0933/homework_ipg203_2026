namespace IPG203_F25_Homework
{
    public interface IRepairableDevice
    {
        void StartMaintenance();
        void CompleteRepair();
        decimal CalculateRepairCost();
        void DisplayDeviceInformation();
    }
}
