using System;

namespace IPG203_F25_Homework
{
    internal class Program
    {
        private static RepairCenter repairCenter;

        private static void Main(string[] args)
        {
            repairCenter = new RepairCenter("Home Appliances Repair Center");
            LoadSampleData();
            bool running = true;
            while (running)
            {
                DisplayMenu();
                int choice = ReadInteger("Select an option: ", 1, 18);
                try
                {
                    switch (choice)
                    {
                        case 1: RegisterRefrigerator(); break;
                        case 2: RegisterWashingMachine(); break;
                        case 3: RegisterAirConditioner(); break;
                        case 4: RegisterMicrowave(); break;
                        case 5: RegisterFan(); break;
                        case 6: RegisterVacuumCleaner(); break;
                        case 7: RegisterTV(); break;
                        case 8: repairCenter.DisplayAllAppliances(); break;
                        case 9: SearchAppliance(); break;
                        case 10: UpdateStatus(); break;
                        case 11: DisplayByStatus(); break;
                        case 12: AddMaintenanceRecord(); break;
                        case 13: repairCenter.DisplayMaintenanceRecords(); break;
                        case 14: Console.WriteLine("Total estimated repair cost: {0:C}", repairCenter.CalculateTotalRepairCost()); break;
                        case 15: Console.WriteLine("Total registered devices: {0}", RepairCenter.TotalRegisteredDevices); break;
                        case 16: repairCenter.CheckOverdueDeliveries(); break;
                        case 17: repairCenter.DisplayRepairCenterStatistics(); break;
                        case 18: running = false; break;
                    }
                }
                catch (ArgumentException exception)
                {
                    Console.WriteLine("Error: " + exception.Message);
                }
                if (running)
                {
                    Console.WriteLine();
                }
            }
        }

        // Pre-loads demo data at startup so every feature can be tried right away
        // without typing anything. Dates are relative to today so the "delayed
        // deliveries" check always has overdue devices to report.
        // To start with an empty system, delete or comment out the LoadSampleData() call in Main.
        private static void LoadSampleData()
        {
            DateTime today = DateTime.Today;

            SeedAppliance(new Refrigerator("RF-1001", "Samsung", "RT38", today.AddDays(-10), today.AddDays(-3), 120m, 380, true, CompressorCondition.Fair), ApplianceStatus.UnderRepair);
            SeedAppliance(new WashingMachine("WM-2001", "LG", "F4V5", today.AddDays(-7), today.AddDays(5), 90m, 8, true, MotorCondition.Good), ApplianceStatus.UnderInspection);
            SeedAppliance(new AirConditioner("AC-3001", "Carrier", "42K-18", today.AddDays(-15), today.AddDays(-5), 150m, 18000, true, RefrigerantType.R410A), ApplianceStatus.WaitingForParts);
            SeedAppliance(new Microwave("MW-4001", "Panasonic", "NN-ST34", today.AddDays(-4), today.AddDays(2), 40m, 900, true, true), ApplianceStatus.Received);
            SeedAppliance(new Fan("FN-5001", "Toshiba", "TF-120", today.AddDays(-6), today.AddDays(1), 25m, FanType.StandingFan, 3, true), ApplianceStatus.Repaired);
            SeedAppliance(new VacuumCleaner("VC-6001", "Philips", "FC9174", today.AddDays(-12), today.AddDays(-2), 55m, 2000, VacuumType.Canister, true), ApplianceStatus.Delivered);
            SeedAppliance(new TV("TV-7001", "Sony", "KD-55X80J", today.AddDays(-3), today.AddDays(4), 200m, 55, ScreenTechnology.OLED, true, ScreenResolution.UHD4K), ApplianceStatus.UnderRepair);
            SeedAppliance(new TV("TV-7002", "Samsung", "QE43Q60", today.AddDays(-8), today.AddDays(-1), 110m, 43, ScreenTechnology.QLED, true, ScreenResolution.FullHD), ApplianceStatus.Received);

            repairCenter.AddMaintenanceRecord(new MaintenanceRecord("REC-001", "RF-1001", "Compressor inspection and gas refill", today.AddDays(-8), "Ahmad Khalil", 75m));
            repairCenter.AddMaintenanceRecord(new MaintenanceRecord("REC-002", "AC-3001", "Replaced faulty fan motor", today.AddDays(-6), "Sara Nasser", 130m));
            repairCenter.AddMaintenanceRecord(new MaintenanceRecord("REC-003", "FN-5001", "Replaced speed switch", today.AddDays(-4), "Omar Hasan", 20m));
            repairCenter.AddMaintenanceRecord(new MaintenanceRecord("REC-004", "TV-7001", "Panel backlight repair", today.AddDays(-2), "Sara Nasser", 160m));

            Console.WriteLine("[Demo] Loaded {0} sample appliances and sample maintenance records.", repairCenter.ApplianceCount);
            Console.WriteLine();
        }

        // The status is set before the device is added to the repair center, so no
        // "STATUS NOTICE" messages are printed while the sample data is loading.
        private static void SeedAppliance(Appliance appliance, ApplianceStatus status)
        {
            if (status != ApplianceStatus.Received) appliance.UpdateStatus(status);
            repairCenter.AddAppliance(appliance);
        }

        private static void DisplayMenu()
        {
            Console.WriteLine(repairCenter.RepairCenterName);
            Console.WriteLine("1. Register New Refrigerator");
            Console.WriteLine("2. Register New Washing Machine");
            Console.WriteLine("3. Register New Air Conditioner");
            Console.WriteLine("4. Register New Microwave");
            Console.WriteLine("5. Register New Fan");
            Console.WriteLine("6. Register New Vacuum Cleaner");
            Console.WriteLine("7. Register New TV");
            Console.WriteLine("8. Display All Appliances");
            Console.WriteLine("9. Search Appliance by Serial Number");
            Console.WriteLine("10. Update Appliance Status");
            Console.WriteLine("11. Display Appliances by Status");
            Console.WriteLine("12. Add Maintenance Record");
            Console.WriteLine("13. Display Maintenance Records");
            Console.WriteLine("14. Calculate Total Estimated Repair Cost");
            Console.WriteLine("15. Display Total Registered Devices");
            Console.WriteLine("16. Check Delayed Deliveries");
            Console.WriteLine("17. Display Repair Center Statistics");
            Console.WriteLine("18. Exit");
        }

        private static ApplianceCommon ReadCommon()
        {
            ApplianceCommon common = new ApplianceCommon();
            common.SerialNumber = ReadText("Serial number: ", InputValidator.IsValidSerialNumber);
            common.Brand = ReadText("Brand: ", InputValidator.IsValidBrand);
            common.Model = ReadText("Model: ", InputValidator.IsValidModel);
            common.ReceivedDate = ReadDate("Received date (e.g. 2026-11-14): ");
            do
            {
                common.DeliveryDueDate = ReadDate("Delivery due date: ");
                if (common.DeliveryDueDate < common.ReceivedDate)
                {
                    Console.WriteLine("Delivery due date cannot be before received date.");
                }
            } while (common.DeliveryDueDate < common.ReceivedDate);
            common.BaseRepairCost = ReadDecimal("Base repair cost: ", true);
            return common;
        }

        private static void RegisterRefrigerator()
        {
            ApplianceCommon c = ReadCommon();
            double capacity = ReadDouble("Capacity in liters: ", 1);
            bool freezer = ReadBoolean("Has freezer (y/n): ");
            CompressorCondition condition = ReadEnum<CompressorCondition>("Compressor condition");
            Register(new Refrigerator(c.SerialNumber, c.Brand, c.Model, c.ReceivedDate, c.DeliveryDueDate, c.BaseRepairCost, capacity, freezer, condition));
        }

        private static void RegisterWashingMachine()
        {
            ApplianceCommon c = ReadCommon();
            double capacity = ReadDouble("Load capacity in kg: ", 1);
            bool front = ReadBoolean("Front-loading (y/n): ");
            MotorCondition condition = ReadEnum<MotorCondition>("Motor condition");
            Register(new WashingMachine(c.SerialNumber, c.Brand, c.Model, c.ReceivedDate, c.DeliveryDueDate, c.BaseRepairCost, capacity, front, condition));
        }

        private static void RegisterAirConditioner()
        {
            ApplianceCommon c = ReadCommon();
            double capacity = ReadDouble("Cooling capacity in BTU: ", 1);
            bool inverter = ReadBoolean("Inverter (y/n): ");
            RefrigerantType refrigerant = ReadEnum<RefrigerantType>("Refrigerant type");
            Register(new AirConditioner(c.SerialNumber, c.Brand, c.Model, c.ReceivedDate, c.DeliveryDueDate, c.BaseRepairCost, capacity, inverter, refrigerant));
        }

        private static void RegisterMicrowave()
        {
            ApplianceCommon c = ReadCommon();
            int power = ReadInteger("Power in watts: ", 1, 100000);
            bool digital = ReadBoolean("Digital controls (y/n): ");
            bool grill = ReadBoolean("Has grill (y/n): ");
            Register(new Microwave(c.SerialNumber, c.Brand, c.Model, c.ReceivedDate, c.DeliveryDueDate, c.BaseRepairCost, power, digital, grill));
        }

        private static void RegisterFan()
        {
            ApplianceCommon c = ReadCommon();
            FanType type = ReadEnum<FanType>("Fan type");
            int speeds = ReadInteger("Number of speed levels: ", 1, 20);
            bool remote = ReadBoolean("Remote controlled (y/n): ");
            Register(new Fan(c.SerialNumber, c.Brand, c.Model, c.ReceivedDate, c.DeliveryDueDate, c.BaseRepairCost, type, speeds, remote));
        }

        private static void RegisterVacuumCleaner()
        {
            ApplianceCommon c = ReadCommon();
            int suction = ReadInteger("Suction power in watts: ", 1, 100000);
            VacuumType type = ReadEnum<VacuumType>("Vacuum type");
            bool bagless = ReadBoolean("Bagless (y/n): ");
            Register(new VacuumCleaner(c.SerialNumber, c.Brand, c.Model, c.ReceivedDate, c.DeliveryDueDate, c.BaseRepairCost, suction, type, bagless));
        }

        private static void RegisterTV()
        {
            ApplianceCommon c = ReadCommon();
            double size = ReadDouble("Screen size in inches: ", 1);
            ScreenTechnology technology = ReadEnum<ScreenTechnology>("Screen technology");
            bool smart = ReadBoolean("Smart TV (y/n): ");
            ScreenResolution resolution = ReadEnum<ScreenResolution>("Resolution");
            Register(new TV(c.SerialNumber, c.Brand, c.Model, c.ReceivedDate, c.DeliveryDueDate, c.BaseRepairCost, size, technology, smart, resolution));
        }

        private static void Register(Appliance appliance)
        {
            if (repairCenter.AddAppliance(appliance))
            {
                Console.WriteLine("Appliance registered successfully. Estimated cost: {0:C}", appliance.CalculateRepairCost());
            }
            else
            {
                Console.WriteLine("An appliance with that serial number is already registered.");
            }
        }

        private static void SearchAppliance()
        {
            Appliance appliance = repairCenter.FindAppliance(ReadText("Serial number: ", InputValidator.IsValidSerialNumber));
            if (appliance == null) Console.WriteLine("Appliance not found.");
            else appliance.DisplayDeviceInformation();
        }

        private static void UpdateStatus()
        {
            string serial = ReadText("Serial number: ", InputValidator.IsValidSerialNumber);
            ApplianceStatus status = ReadEnum<ApplianceStatus>("New status");
            Console.WriteLine(repairCenter.UpdateApplianceStatus(serial, status) ? "Status updated." : "Appliance not found.");
        }

        private static void DisplayByStatus()
        {
            ApplianceStatus status = ReadEnum<ApplianceStatus>("Status to display");
            repairCenter.DisplayAppliancesByStatus(status);
        }

        private static void AddMaintenanceRecord()
        {
            string serial = ReadText("Appliance serial number: ", InputValidator.IsValidSerialNumber);
            if (repairCenter.FindAppliance(serial) == null)
            {
                Console.WriteLine("No registered appliance has that serial number.");
                return;
            }
            string description = ReadText("Work description: ", InputValidator.IsValidModel);
            DateTime date = ReadDate("Maintenance date: ");
            string technician = ReadText("Technician name: ", InputValidator.IsValidBrand);
            decimal cost = ReadDecimal("Maintenance cost: ", true);
            MaintenanceRecord record = new MaintenanceRecord(Guid.NewGuid().ToString("N"), serial, description, date, technician, cost);
            Console.WriteLine(repairCenter.AddMaintenanceRecord(record) ? "Maintenance record added." : "Could not add maintenance record.");
        }

        private static string ReadText(string prompt, Func<string, bool> validator)
        {
            string value;
            do
            {
                Console.Write(prompt);
                value = Console.ReadLine();
                if (value == null) value = string.Empty;
                if (!validator(value)) Console.WriteLine("Please enter a valid, non-empty value.");
            } while (!validator(value));
            return value.Trim();
        }

        private static DateTime ReadDate(string prompt)
        {
            DateTime value;
            string text;
            do
            {
                Console.Write(prompt);
                text = Console.ReadLine();
                if (!InputValidator.IsValidDate(text) || !DateTime.TryParse(text, out value))
                {
                    Console.WriteLine("Enter a valid date.");
                }
                else return value.Date;
            } while (true);
        }

        private static decimal ReadDecimal(string prompt, bool nonNegative)
        {
            decimal value;
            string text;
            do
            {
                Console.Write(prompt);
                text = Console.ReadLine();
                if (decimal.TryParse(text, out value) && InputValidator.IsValidCost(value) && (!nonNegative || value >= 0)) return value;
                Console.WriteLine("Enter a valid non-negative amount.");
            } while (true);
        }

        private static double ReadDouble(string prompt, double minimum)
        {
            double value;
            string text;
            do
            {
                Console.Write(prompt);
                text = Console.ReadLine();
                if (double.TryParse(text, out value) && InputValidator.IsValidPositiveNumber(value) && value >= minimum) return value;
                Console.WriteLine("Enter a valid positive number.");
            } while (true);
        }

        private static int ReadInteger(string prompt, int minimum, int maximum)
        {
            int value;
            string text;
            do
            {
                Console.Write(prompt);
                text = Console.ReadLine();
                if (int.TryParse(text, out value) && value >= minimum && value <= maximum) return value;
                Console.WriteLine("Enter a whole number from {0} to {1}.", minimum, maximum);
            } while (true);
        }

        private static bool ReadBoolean(string prompt)
        {
            string value;
            do
            {
                Console.Write(prompt);
                value = Console.ReadLine();
                if (value != null && (value.Trim().Equals("y", StringComparison.OrdinalIgnoreCase) || value.Trim().Equals("yes", StringComparison.OrdinalIgnoreCase))) return true;
                if (value != null && (value.Trim().Equals("n", StringComparison.OrdinalIgnoreCase) || value.Trim().Equals("no", StringComparison.OrdinalIgnoreCase))) return false;
                Console.WriteLine("Enter y or n.");
            } while (true);
        }

        private static T ReadEnum<T>(string label) where T : struct
        {
            Array values = Enum.GetValues(typeof(T));
            int i;
            Console.WriteLine("{0}:", label);
            for (i = 0; i < values.Length; i++) Console.WriteLine("{0}. {1}", i + 1, values.GetValue(i));
            int choice = ReadInteger("Choose: ", 1, values.Length);
            return (T)values.GetValue(choice - 1);
        }

        private sealed class ApplianceCommon
        {
            private string serialNumber;
            private string brand;
            private string model;
            private DateTime receivedDate;
            private DateTime deliveryDueDate;
            private decimal baseRepairCost;

            public string SerialNumber { get { return serialNumber; } set { serialNumber = value; } }
            public string Brand { get { return brand; } set { brand = value; } }
            public string Model { get { return model; } set { model = value; } }
            public DateTime ReceivedDate { get { return receivedDate; } set { receivedDate = value; } }
            public DateTime DeliveryDueDate { get { return deliveryDueDate; } set { deliveryDueDate = value; } }
            public decimal BaseRepairCost { get { return baseRepairCost; } set { baseRepairCost = value; } }
        }
    }
}