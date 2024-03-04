namespace HermesSteaam.Notification.Worker.Services.Dtos
{
    public class Notification
    {
        public required string NotificationId { get; set; }
        public required string ClientId { get; set; }
        public required string Message { get; set; }
        public NotificationType NotificationType { get; set; }
    }

    public enum NotificationType
    {
        WARNING,
        INFORMATION
    }
}
