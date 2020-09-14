using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections;
using System.Threading.Tasks;
using Taxi.Domain.Models.Customers.orders;
using Taxi.Domain.Models.Customers.Reports;

namespace Taxi.API.SignalRHub
{
    public interface INotificationsHub
    {
        //send message to customer's client when driver complete the customer's order
        Task SendMessageToUser(string msg);

        //Send report info to admins when user report driver
        Task SendReportToAdmins(ReportModel reportInfo);

        //Send notification (info booking) to driver when customer booking.
        Task SendBookingInfoToDriver(OrderModel order);

        //online list
        Task OnlineList(int numbers, ICollection users);
    }
    public class NotificationsHub : Hub<INotificationsHub>
    {
        private static Hashtable _users = new Hashtable();
        //override
        public override async Task OnConnectedAsync()
        {
            var userId = Context.GetHttpContext().Request.Query["userid"];
            var role = Context.GetHttpContext().Request.Query["role"];
            var name = Context.GetHttpContext().Request.Query["username"];
            string _userId = userId;
            string _role = role;
            if (_role == "admin")
                await Groups.AddToGroupAsync(Context.ConnectionId, "Group Admins");
            await Groups.AddToGroupAsync(Context.ConnectionId, _userId);

            //online list
            if (!_users.ContainsValue(name))
                _users.Add(Context.ConnectionId, name);
            await Clients.Group("Group Admins").OnlineList(_users.Count, _users.Values);

            await base.OnConnectedAsync();
        }
        public override async Task OnDisconnectedAsync(Exception exception)
        {
            //online list
            _users.Remove(Context.ConnectionId);
            await Clients.Group("Group Admins").OnlineList(_users.Count, _users.Values);
            await base.OnDisconnectedAsync(exception);
        }
    }
}
