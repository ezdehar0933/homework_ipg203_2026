using System;

namespace IPG203_F25_Homework
{
    public class WashingMachine : Appliance
    {
        private double loadCapacity;
        private bool isFrontLoading;
        private MotorCondition motorCondition;
        public WashingMachine(string serialNumber, string brand, string model, DateTime receivedDate, DateTime deliveryDueDate, decimal baseRepairCost, double loadCapacity, bool isFrontLoading, MotorCondition motorCondition)
            : base(serialNumber, brand, model, receivedDate, deliveryDueDate, baseRepairCost)
        { LoadCapacity = loadCapacity; IsFrontLoading = isFrontLoading; MotorCondition = motorCondition; }
        public double LoadCapacity { get { return loadCapacity; } set { if (!InputValidator.IsValidCapacity(value)) throw new ArgumentException("Load capacity must be positive."); loadCapacity = value; } }
        public bool IsFrontLoading { get { return isFrontLoading; } set { isFrontLoading = value; } }
        public MotorCondition MotorCondition
        {
            get { return motorCondition; }
            set { if (!Enum.IsDefined(typeof(MotorCondition), value)) throw new ArgumentException("Invalid motor condition."); motorCondition = value; }
        }
        public override decimal CalculateRepairCost()
        {
            // Formula: base + $12 per kg + front-loader surcharge + motor condition surcharge.
            return BaseRepairCost + (decimal)loadCapacity * 12m + (isFrontLoading ? 45m : 0m) + (decimal)(int)motorCondition * 60m;
        }
        public override void DisplayDeviceInformation()
        {
            base.DisplayDeviceInformation();
            Console.WriteLine("Load: {0} kg | Front-loading: {1} | Motor: {2}", loadCapacity, isFrontLoading, motorCondition);
        }
    }
}
