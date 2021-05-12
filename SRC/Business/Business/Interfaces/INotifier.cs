using Business.Notifications;
using System.Collections.Generic;

namespace Business.Interfaces
{
    public interface INotifier
    {
        bool HasNotification();
        List<Notification> FindAlls();
        void Handle(Notification notification);

    }
}
