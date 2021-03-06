using Business.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Business.Notifications
{
    public class Notifier : INotifier
    {
        private List<Notification> _notifications;

        public Notifier()
        {
            _notifications = new List<Notification>();
        }

        public void Handle(Notification notification)
        {
            _notifications.Add(notification);
        }
        public List<Notification> FindAlls()
        {
            return _notifications;
        }
        public bool HasNotification()
        {
            return _notifications.Any();
        }
    }
}
