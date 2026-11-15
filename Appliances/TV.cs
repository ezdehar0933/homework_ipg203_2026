using System;

namespace IPG203_F25_Homework
{
    public class TV : Appliance
    {
        private double screenSize;
        private ScreenTechnology screenTechnology;
        private bool isSmartTV;
        private ScreenResolution resolution;
        public TV(string serialNumber, string brand, string model, DateTime receivedDate, DateTime deliveryDueDate, decimal baseRepairCost, double screenSize, ScreenTechnology screenTechnology, bool isSmartTV, ScreenResolution resolution)
            : base(serialNumber, brand, model, receivedDate, deliveryDueDate, baseRepairCost)
        { ScreenSize = screenSize; ScreenTechnology = screenTechnology; IsSmartTV = isSmartTV; Resolution = resolution; }
        public double ScreenSize { get { return screenSize; } set { if (!InputValidator.IsValidScreenSize(value)) throw new ArgumentException("Screen size must be positive."); screenSize = value; } }
        public ScreenTechnology ScreenTechnology
        {
            get { return screenTechnology; }
            set { if (!Enum.IsDefined(typeof(ScreenTechnology), value)) throw new ArgumentException("Invalid screen technology."); screenTechnology = value; }
        }
        public bool IsSmartTV { get { return isSmartTV; } set { isSmartTV = value; } }
        public ScreenResolution Resolution
        {
            get { return resolution; }
            set { if (!Enum.IsDefined(typeof(ScreenResolution), value)) throw new ArgumentException("Invalid screen resolution."); resolution = value; }
        }
        public override decimal CalculateRepairCost()
        {
            // Formula: base + $6 per inch + panel technology surcharge + smart and resolution surcharges.
            decimal panel = screenTechnology == IPG203_F25_Homework.ScreenTechnology.OLED ? 180m :
                (screenTechnology == IPG203_F25_Homework.ScreenTechnology.QLED ? 140m :
                (screenTechnology == IPG203_F25_Homework.ScreenTechnology.LED ? 70m : 40m));
            return BaseRepairCost + (decimal)screenSize * 6m + panel + (isSmartTV ? 45m : 0m) + (decimal)(int)resolution * 20m;
        }
        public override void DisplayDeviceInformation()
        {
            base.DisplayDeviceInformation();
            Console.WriteLine("Screen: {0} in | Technology: {1} | Smart: {2} | Resolution: {3}", screenSize, screenTechnology, isSmartTV, resolution);
        }
    }
}
