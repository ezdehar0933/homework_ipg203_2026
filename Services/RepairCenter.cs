using System;
using System.Collections.Generic;

namespace IPG203_F25_Homework
{
    public class RepairCenter
    {
        private readonly List<Appliance> appliances;
        private readonly List<MaintenanceRecord> maintenanceRecords;
        private readonly HashSet<string> notifiedDelayedSerialNumbers;
        private string repairCenterName;
        private static int totalRegisteredDevices;

        public event DelayedDeliveryHandler DelayedDeliveryDetected;

        public RepairCenter(string repairCenterName)
        {
            RepairCenterName = repairCenterName;
            appliances = new List<Appliance>();
            maintenanceRecords = new List<MaintenanceRecord>();
            notifiedDelayedSerialNumbers = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            DelayedDeliveryDetected += OnDelayedDeliveryDetected;
        }
        public string RepairCenterName
        {
            get { return repairCenterName; }
            set { if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("Repair center name is required."); repairCenterName = value.Trim(); }
        }
        public static int TotalRegisteredDevices { get { return totalRegisteredDevices; } }
        public int ApplianceCount { get { return appliances.Count; } }

        public bool AddAppliance(Appliance appliance)
        {
            if (appliance == null) throw new ArgumentNullException("appliance");
            if (FindAppliance(appliance.SerialNumber) != null) return false;
            appliances.Add(appliance);
            appliance.StatusChanged += OnDeviceStatusChanged;
            totalRegisteredDevices++;
            return true;
        }

        public bool RemoveAppliance(string serialNumber)
        {
            Appliance appliance = FindAppliance(serialNumber);
            if (appliance == null) return false;
            appliance.StatusChanged -= OnDeviceStatusChanged;
            appliances.Remove(appliance);
            notifiedDelayedSerialNumbers.Remove(appliance.SerialNumber);
            totalRegisteredDevices--;
            return true;
        }

        public Appliance FindAppliance(string serialNumber)
        {
            int i;
            if (serialNumber == null) return null;
            for (i = 0; i < appliances.Count; i++)
                if (string.Equals(appliances[i].SerialNumber, serialNumber.Trim(), StringComparison.OrdinalIgnoreCase)) return appliances[i];
            return null;
        }

        public void DisplayAllAppliances()
        {
            if (appliances.Count == 0) { Console.WriteLine("No appliances are registered."); return; }
            int i;
            for (i = 0; i < appliances.Count; i++) appliances[i].DisplayDeviceInformation();
        }

        public void DisplayAppliancesByStatus(ApplianceStatus status)
        {
            int i;
            bool found = false;
            for (i = 0; i < appliances.Count; i++)
                if (appliances[i].Status == status) { appliances[i].DisplayDeviceInformation(); found = true; }
            if (!found) Console.WriteLine("No appliances have status {0}.", status);
        }

        public bool UpdateApplianceStatus(string serialNumber, ApplianceStatus status)
        {
            Appliance appliance = FindAppliance(serialNumber);
            if (appliance == null) return false;
            appliance.UpdateStatus(status);
            return true;
        }

        public bool AddMaintenanceRecord(MaintenanceRecord record)
        {
            if (record == null) throw new ArgumentNullException("record");
            if (FindAppliance(record.SerialNumber) == null) return false;
            int i;
            for (i = 0; i < maintenanceRecords.Count; i++)
                if (maintenanceRecords[i].RecordId == record.RecordId) return false;
            maintenanceRecords.Add(record);
            return true;
        }

        public void DisplayMaintenanceRecords()
        {
            if (maintenanceRecords.Count == 0) { Console.WriteLine("No maintenance records are available."); return; }
            int i;
            for (i = 0; i < maintenanceRecords.Count; i++) maintenanceRecords[i].DisplayMaintenanceInformation();
        }

        public decimal CalculateTotalRepairCost()
        {
            decimal total = 0;
            int i;
            for (i = 0; i < appliances.Count; i++) total += appliances[i].CalculateRepairCost();
            return total;
        }

        public int CountAppliancesByStatus(ApplianceStatus status)
        {
            int count = 0;
            int i;
            for (i = 0; i < appliances.Count; i++) if (appliances[i].Status == status) count++;
            return count;
        }

        public void CheckOverdueDeliveries()
        {
            int i;
            int notified = 0;
            for (i = 0; i < appliances.Count; i++)
            {
                if (appliances[i].CheckDeliveryDelay())
                {
                    notifiedDelayedSerialNumbers.Add(appliances[i].SerialNumber);
                    DelayedDeliveryHandler handler = DelayedDeliveryDetected;
                    if (handler != null) handler(this, new DelayedDeliveryEventArgs(appliances[i].SerialNumber, appliances[i].DeliveryDueDate, DateTime.Now));
                    notified++;
                }
            }
            if (notified == 0) Console.WriteLine("No newly delayed deliveries detected.");
        }

        public void DisplayRepairCenterStatistics()
        {
            Console.WriteLine("=== {0} Statistics ===", repairCenterName);
            Console.WriteLine("Registered appliances: {0}", appliances.Count);
            Console.WriteLine("Maintenance records: {0}", maintenanceRecords.Count);
            Console.WriteLine("Estimated repair total: {0:C}", CalculateTotalRepairCost());
            int i;
            Array statuses = Enum.GetValues(typeof(ApplianceStatus));
            for (i = 0; i < statuses.Length; i++)
            {
                ApplianceStatus status = (ApplianceStatus)statuses.GetValue(i);
                Console.WriteLine("{0}: {1}", status, CountAppliancesByStatus(status));
            }
        }

        private void OnDeviceStatusChanged(object sender, DeviceStatusChangedEventArgs e)
        {
            Console.WriteLine("STATUS NOTICE: Appliance {0} changed from {1} to {2} at {3}.",
                e.SerialNumber, e.PreviousStatus, e.NewStatus, e.ChangeDateTime);
        }

        private void OnDelayedDeliveryDetected(object sender, DelayedDeliveryEventArgs e)
        {
            Console.WriteLine("DELIVERY NOTICE: Appliance {0} was due on {1:d}; delay detected at {2}.",
                e.SerialNumber, e.DeliveryDueDate, e.DetectedDateTime);
        }
    }
}
