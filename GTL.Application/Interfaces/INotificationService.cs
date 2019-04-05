using GTL.Application.Notification;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GTL.Application.Interfaces
{
    public interface INotificationService
    {
        Task SendAsync(Message message);
    }
}
