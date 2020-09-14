using System;
using System.Collections.Generic;
using System.Text;
using Taxi.Domain.Entities;

namespace Taxi.Domain.Interfaces
{
    public interface INotifyRepository
    {
        IEnumerable<Notify> GetNotifiesOfUser(int userId);
        void MarkedRead(int notifyId);

    }
}
