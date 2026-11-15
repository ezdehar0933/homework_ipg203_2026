using System;

namespace IPG203_F25_Homework
{
    public class VacuumCleaner : Appliance
    {
        private int suctionPower;
        private VacuumType vacuumType;
        private bool isBagless;
        public VacuumCleaner(string serialNumber, string brand, string model, DateTime receivedDate, DateTime deliveryDueDate, decimal baseRepairCost, int suctionPower, VacuumType vacuumType, bool isBagless)
            : base(serialNumber, brand, model, receivedDate, deliveryDueDate, baseRepairCost)
        { SuctionPower = suctionPower; VacuumType = vacuumType; IsBagless = isBagless; }
        public int SuctionPower { get { return suctionPower; } set { if (!InputValidator.IsValidPower(value)) throw new ArgumentException("Suction power must be positive."); suctionPower = value; } }
        public VacuumType VacuumType
        {
            get { return vacuumType; }
            set { if (!Enum.IsDefined(typeof(VacuumType), value)) throw new ArgumentException("Invalid vacuum type."); vacuumType = value; }
        }
        public bool IsBagless { get { return isBagless; } set { isBagless = value; } }
        public override decimal CalculateRepairCost()
        {
            // Formula: base + $0.06 per watt of suction + type surcharge + bagless filter surcharge.
            return BaseRepairCost + suctionPower * 0.06m + (decimal)(int)vacuumType * 25m + (isBagless ? 30m : 0m);
        }
        public override void DisplayDeviceInformation()
        {
            base.DisplayDeviceInformation();
            Console.WriteLine("Suction: {0} W | Type: {1} | Bagless: {2}", suctionPower, vacuumType, isBagless);
        }
    }
}
