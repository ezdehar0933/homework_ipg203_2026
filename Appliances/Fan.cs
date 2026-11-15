using System;

namespace IPG203_F25_Homework
{
    public class Fan : Appliance
    {
        private FanType fanType;
        private int speedLevels;
        private bool isRemoteControlled;
        public Fan(string serialNumber, string brand, string model, DateTime receivedDate, DateTime deliveryDueDate, decimal baseRepairCost, FanType fanType, int speedLevels, bool isRemoteControlled)
            : base(serialNumber, brand, model, receivedDate, deliveryDueDate, baseRepairCost)
        { FanType = fanType; SpeedLevels = speedLevels; IsRemoteControlled = isRemoteControlled; }
        public FanType FanType
        {
            get { return fanType; }
            set { if (!Enum.IsDefined(typeof(FanType), value)) throw new ArgumentException("Invalid fan type."); fanType = value; }
        }
        public int SpeedLevels { get { return speedLevels; } set { if (value < 1 || value > 20) throw new ArgumentException("Speed levels must be from 1 to 20."); speedLevels = value; } }
        public bool IsRemoteControlled { get { return isRemoteControlled; } set { isRemoteControlled = value; } }
        public override decimal CalculateRepairCost()
        {
            // Formula: base + $8 per speed level + type-specific surcharge + remote-control surcharge.
            return BaseRepairCost + speedLevels * 8m + (decimal)(int)fanType * 15m + (isRemoteControlled ? 20m : 0m);
        }
        public override void DisplayDeviceInformation()
        {
            base.DisplayDeviceInformation();
            Console.WriteLine("Type: {0} | Speeds: {1} | Remote: {2}", fanType, speedLevels, isRemoteControlled);
        }
    }
}
