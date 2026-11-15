using System;

namespace IPG203_F25_Homework
{
    public class AirConditioner : Appliance
    {
        private double coolingCapacity;
        private bool isInverter;
        private RefrigerantType refrigerantType;
        public AirConditioner(string serialNumber, string brand, string model, DateTime receivedDate, DateTime deliveryDueDate, decimal baseRepairCost, double coolingCapacity, bool isInverter, RefrigerantType refrigerantType)
            : base(serialNumber, brand, model, receivedDate, deliveryDueDate, baseRepairCost)
        { CoolingCapacity = coolingCapacity; IsInverter = isInverter; RefrigerantType = refrigerantType; }
        public double CoolingCapacity { get { return coolingCapacity; } set { if (!InputValidator.IsValidCapacity(value)) throw new ArgumentException("Cooling capacity must be positive."); coolingCapacity = value; } }
        public bool IsInverter { get { return isInverter; } set { isInverter = value; } }
        public RefrigerantType RefrigerantType
        {
            get { return refrigerantType; }
            set { if (!Enum.IsDefined(typeof(RefrigerantType), value)) throw new ArgumentException("Invalid refrigerant type."); refrigerantType = value; }
        }
        public override decimal CalculateRepairCost()
        {
            // Formula: base + $0.015 per BTU + inverter surcharge + refrigerant service surcharge.
            return BaseRepairCost + (decimal)coolingCapacity * 0.015m + (isInverter ? 90m : 0m) + (refrigerantType == IPG203_F25_Homework.RefrigerantType.Other ? 30m : 50m);
        }
        public override void DisplayDeviceInformation()
        {
            base.DisplayDeviceInformation();
            Console.WriteLine("Cooling: {0} BTU | Inverter: {1} | Refrigerant: {2}", coolingCapacity, isInverter, refrigerantType);
        }
    }
}
