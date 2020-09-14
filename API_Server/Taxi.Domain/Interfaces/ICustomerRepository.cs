using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Taxi.Domain.Entities;

namespace Taxi.Domain.Interfaces
{
    public interface ICustomerRepository
    {
        //customer profile
        Customer GetById(int UserId);
        //booking
        IEnumerable<Order> GetOrdersOfUser(int userId);
        Order GetCurrentOrderOfCustomer(int customerId);
        Order GetOrderById(int orderId);

        IEnumerable<Schedule> SearchSchedule(string pickupLocation, string returnLocation, int Reservations);
        Task<Order> CreateOrder(Order order);
        //void UpdateOrder(Order order);
        void DeleteOrder(int id);
        void CancelOrder(int id);

        //rate
        void Rating(Order orderParams);

        //report driver
        IEnumerable<Report> GetReportsOfUser(int userId);
        Report GetReportById(int reportId);
        Task<Report> CreateReport(Report report);

        //helper method
        int GetCustomerId(int userId);
        int GetUserIdOfDriver(int driverId);
        float GetRateAverageOfDriver(int driverId);
        Driver GetDriverbyId(int driverId);
    }
}
