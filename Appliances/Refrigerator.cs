using System;

namespace IPG203_F25_Homework
{
    public class Refrigerator : Appliance
    {
        private double capacity;
        private bool hasFreezer;
        private CompressorCondition compressorCondition;

        public Refrigerator(string serialNumber, string brand, string model, DateTime receivedDate, DateTime deliveryDueDate, decimal baseRepairCost, double capacity, bool hasFreezer, CompressorCondition compressorCondition)
            : base(serialNumber, brand, model, receivedDate, deliveryDueDate, baseRepairCost)
        {
            Capacity = capacity; HasFreezer = hasFreezer; CompressorCondition = compressorCondition;
        }
        public double Capacity { get { return capacity; } set { if (!InputValidator.IsValidCapacity(value)) throw new ArgumentException("Capacity must be positive."); capacity = value; } }
        public bool HasFreezer { get { return hasFreezer; } set { hasFreezer = value; } }
        public CompressorCondition CompressorCondition
        {
            get { return compressorCondition; }
            set { if (!Enum.IsDefined(typeof(CompressorCondition), value)) throw new ArgumentException("Invalid compressor condition."); compressorCondition = value; }
        }
        public override decimal CalculateRepairCost()
        {
            // Formula: base + $0.50 per liter + freezer surcharge + condition surcharge.
            decimal condition = new decimal((int)compressorCondition) * 55m;
            return BaseRepairCost + (decimal)capacity * 0.50m + (hasFreezer ? 35m : 0m) + condition;
        }
        public override void DisplayDeviceInformation()
        {
            base.DisplayDeviceInformation();
            Console.WriteLine("Capacity: {0} L | Freezer: {1} | Compressor: {2}", capacity, hasFreezer, compressorCondition);
        }
    }
}
