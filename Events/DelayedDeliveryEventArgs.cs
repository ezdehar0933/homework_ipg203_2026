using System;

namespace IPG203_F25_Homework
{
    public class DelayedDeliveryEventArgs : EventArgs
    {
        private readonly string serialNumber;
        private readonly DateTime deliveryDueDate;
        private readonly DateTime detectedDateTime;
        public DelayedDeliveryEventArgs(string serialNumber, DateTime deliveryDueDate, DateTime detectedDateTime)
        { this.serialNumber = serialNumber; this.deliveryDueDate = deliveryDueDate; this.detectedDateTime = detectedDateTime; }
        public string SerialNumber { get { return serialNumber; } }
        public DateTime DeliveryDueDate { get { return deliveryDueDate; } }
        public DateTime DetectedDateTime { get { return detectedDateTime; } }
    }
    public delegate void DelayedDeliveryHandler(object sender, DelayedDeliveryEventArgs e);
}
