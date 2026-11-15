using System;

namespace IPG203_F25_Homework
{
    public class Microwave : Appliance
    {
        private int powerWatts;
        private bool isDigital;
        private bool hasGrillFunction;
        public Microwave(string serialNumber, string brand, string model, DateTime receivedDate, DateTime deliveryDueDate, decimal baseRepairCost, int powerWatts, bool isDigital, bool hasGrillFunction)
            : base(serialNumber, brand, model, receivedDate, deliveryDueDate, baseRepairCost)
        { PowerWatts = powerWatts; IsDigital = isDigital; HasGrillFunction = hasGrillFunction; }
        public int PowerWatts { get { return powerWatts; } set { if (!InputValidator.IsValidPower(value)) throw new ArgumentException("Power must be positive."); powerWatts = value; } }
        public bool IsDigital { get { return isDigital; } set { isDigital = value; } }
        public bool HasGrillFunction { get { return hasGrillFunction; } set { hasGrillFunction = value; } }
        public override decimal CalculateRepairCost()
        {
            // Formula: base + $0.04 per watt + digital-control surcharge + grill surcharge.
            return BaseRepairCost + powerWatts * 0.04m + (isDigital ? 25m : 0m) + (hasGrillFunction ? 40m : 0m);
        }
        public override void DisplayDeviceInformation()
        {
            base.DisplayDeviceInformation();
            Console.WriteLine("Power: {0} W | Digital: {1} | Grill: {2}", powerWatts, isDigital, hasGrillFunction);
        }
    }
}
