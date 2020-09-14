using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Taxi.Domain.Entities;
using Taxi.Domain.Interfaces;
using Taxi.Infrastructure.Data;

namespace Taxi.Infrastructure.Services
{
    public class NotifyService : INotifyRepository
    {
        private TaxiContext _context;
        public NotifyService(TaxiContext context)
        {
            _context = context;
        }
        public IEnumerable<Notify> GetNotifiesOfUser(int userId)
        {
            var notifies = _context.Notifies.Where(e => e.UserId == userId)
                .OrderByDescending(e => e.NotifyId);
            return notifies;
        }

        public void MarkedRead(int notifyId)
        {
            var notify = _context.Notifies.Find(notifyId);
            notify.IsRead = true;
            _context.Notifies.Update(notify);
            _context.SaveChanges();
        }
    }
}
