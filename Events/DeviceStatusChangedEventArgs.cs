using System;

namespace IPG203_F25_Homework
{
    public class DeviceStatusChangedEventArgs : EventArgs
    {
        private readonly string serialNumber;
        private readonly ApplianceStatus previousStatus;
        private readonly ApplianceStatus newStatus;
        private readonly DateTime changeDateTime;

        public DeviceStatusChangedEventArgs(string serialNumber, ApplianceStatus previousStatus, ApplianceStatus newStatus, DateTime changeDateTime)
        {
            this.serialNumber = serialNumber;
            this.previousStatus = previousStatus;
            this.newStatus = newStatus;
            this.changeDateTime = changeDateTime;
        }

        public string SerialNumber { get { return serialNumber; } }
        public ApplianceStatus PreviousStatus { get { return previousStatus; } }
        public ApplianceStatus NewStatus { get { return newStatus; } }
        public DateTime ChangeDateTime { get { return changeDateTime; } }
    }
}
