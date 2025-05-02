namespace kargardoon.Services;

public enum NotificationLevel { Success, Error, Info, Warning }

public record NotificationMessage(string Message, NotificationLevel Level);

public class NotificationService
{
    public event Action<NotificationMessage>? OnNotify;

    public void Notify(string message, NotificationLevel level = NotificationLevel.Info)
        => OnNotify?.Invoke(new NotificationMessage(message, level));
}
