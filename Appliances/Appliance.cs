using System;

namespace IPG203_F25_Homework
{
    public abstract class Appliance : IRepairableDevice
    {
        private readonly string serialNumber;
        private string brand;
        private string model;
        private DateTime receivedDate;
        private DateTime deliveryDueDate;
        private ApplianceStatus status;
        private decimal baseRepairCost;
        private bool delayNotificationSent;

        public event DeviceStatusChangedHandler StatusChanged;

        protected Appliance(string serialNumber, string brand, string model, DateTime receivedDate, DateTime deliveryDueDate, decimal baseRepairCost)
        {
            if (!InputValidator.IsValidSerialNumber(serialNumber)) throw new ArgumentException("Serial number is required.");
            this.serialNumber = serialNumber.Trim();
            Brand = brand;
            Model = model;
            ReceivedDate = receivedDate;
            DeliveryDueDate = deliveryDueDate;
            BaseRepairCost = baseRepairCost;
            status = ApplianceStatus.Received;
        }

        public string SerialNumber { get { return serialNumber; } }
        public string Brand
        {
            get { return brand; }
            set { if (!InputValidator.IsValidBrand(value)) throw new ArgumentException("Brand is required."); brand = value.Trim(); }
        }
        public string Model
        {
            get { return model; }
            set { if (!InputValidator.IsValidModel(value)) throw new ArgumentException("Model is required."); model = value.Trim(); }
        }
        public DateTime ReceivedDate
        {
            get { return receivedDate; }
            set
            {
                if (!InputValidator.IsValidDate(value.ToString())) throw new ArgumentException("Received date is invalid.");
                if (deliveryDueDate != DateTime.MinValue && value.Date > deliveryDueDate.Date) throw new ArgumentException("Received date cannot be after due date.");
                receivedDate = value.Date;
            }
        }
        public DateTime DeliveryDueDate
        {
            get { return deliveryDueDate; }
            set
            {
                if (!InputValidator.IsValidDate(value.ToString())) throw new ArgumentException("Delivery due date is invalid.");
                if (receivedDate != DateTime.MinValue && value.Date < receivedDate.Date) throw new ArgumentException("Due date cannot be before received date.");
                deliveryDueDate = value.Date;
            }
        }
        public ApplianceStatus Status { get { return status; } }
        public decimal BaseRepairCost
        {
            get { return baseRepairCost; }
            set { if (!InputValidator.IsValidCost(value) || value < 0) throw new ArgumentException("Base repair cost cannot be negative."); baseRepairCost = value; }
        }

        public abstract decimal CalculateRepairCost();

        public void StartMaintenance()
        {
            UpdateStatus(ApplianceStatus.UnderRepair);
        }

        public void CompleteRepair()
        {
            UpdateStatus(ApplianceStatus.Repaired);
        }

        public void UpdateStatus(ApplianceStatus newStatus)
        {
            if (!Enum.IsDefined(typeof(ApplianceStatus), newStatus)) throw new ArgumentException("Invalid appliance status.");
            if (status == newStatus) return;
            ApplianceStatus previous = status;
            status = newStatus;
            DeviceStatusChangedHandler handler = StatusChanged;
            if (handler != null) handler(this, new DeviceStatusChangedEventArgs(serialNumber, previous, status, DateTime.Now));
        }

        public virtual void DisplayDeviceInformation()
        {
            Console.WriteLine("{0} | {1} {2} | Serial: {3} | Status: {4} | Received: {5:d} | Due: {6:d} | Estimated: {7:C}",
                GetType().Name, brand, model, serialNumber, status, receivedDate, deliveryDueDate, CalculateRepairCost());
        }

        public bool CheckDeliveryDelay()
        {
            if (!delayNotificationSent && status != ApplianceStatus.Delivered && DateTime.Today > deliveryDueDate.Date)
            {
                delayNotificationSent = true;
                return true;
            }
            return false;
        }
    }
}
