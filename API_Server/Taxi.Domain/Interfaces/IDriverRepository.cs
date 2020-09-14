using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Taxi.Domain.Entities;

namespace Taxi.Domain.Interfaces
{
    public interface IDriverRepository
    {
        //driver profile
        IEnumerable<Driver> DGetAll();
        Driver DGetById(int UserId);
        IEnumerable<Order> DGetAllReviewsOfDriver(int driverId);

        //vehicle
        IEnumerable<Vehicle> VGetAll();
        Vehicle VGetById(int id);
        Vehicle VCreate(Vehicle vehicle);
        void VUpdate(Vehicle vehicle);
        void VDelete(int id);

        //schedule
        IEnumerable<Schedule> SGetAll();
        Schedule SGetById(int id);
        Schedule SCreate(Schedule schedule);
        void SUpdate(Schedule schedule);
        void SDelete(int id);
        //orders
        IEnumerable<Order> GetOrdersOfUser(int userId);
        Task CompleteOrder(int orderId);
        //heppers method
        int FindDriverIdViaUserId(int UserId);
        int GetUserIdofCustomerId(int orderId);

    }
}
