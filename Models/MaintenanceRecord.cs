using System;

namespace IPG203_F25_Homework
{
    public class MaintenanceRecord
    {
        private readonly string recordId;
        private readonly string serialNumber;
        private string description;
        private DateTime maintenanceDate;
        private string technicianName;
        private decimal cost;

        public MaintenanceRecord(string recordId, string serialNumber, string description, DateTime maintenanceDate, string technicianName, decimal cost)
        {
            if (string.IsNullOrWhiteSpace(recordId)) throw new ArgumentException("Record ID is required.");
            if (!InputValidator.IsValidSerialNumber(serialNumber)) throw new ArgumentException("Serial number is required.");
            this.recordId = recordId.Trim();
            this.serialNumber = serialNumber.Trim();
            Description = description;
            MaintenanceDate = maintenanceDate;
            TechnicianName = technicianName;
            Cost = cost;
        }
        public string RecordId { get { return recordId; } }
        public string SerialNumber { get { return serialNumber; } }
        public string Description
        {
            get { return description; }
            set { if (!InputValidator.IsValidModel(value)) throw new ArgumentException("Description is required."); description = value.Trim(); }
        }
        public DateTime MaintenanceDate
        {
            get { return maintenanceDate; }
            set { if (value == DateTime.MinValue) throw new ArgumentException("Maintenance date is required."); maintenanceDate = value; }
        }
        public string TechnicianName
        {
            get { return technicianName; }
            set { if (!InputValidator.IsValidBrand(value)) throw new ArgumentException("Technician name is required."); technicianName = value.Trim(); }
        }
        public decimal Cost
        {
            get { return cost; }
            set { if (!InputValidator.IsValidCost(value)) throw new ArgumentException("Cost must be non-negative."); cost = value; }
        }
        public void DisplayMaintenanceInformation()
        {
            Console.WriteLine("Record {0} | Appliance {1} | {2:d} | {3} | {4:C} | {5}", recordId, serialNumber, maintenanceDate, technicianName, cost, description);
        }
    }
}
