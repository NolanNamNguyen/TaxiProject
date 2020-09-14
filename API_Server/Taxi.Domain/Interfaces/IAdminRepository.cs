using System;
using System.Collections.Generic;
using System.Text;
using Taxi.Domain.Entities;

namespace Taxi.Domain.Interfaces
{
    public interface IAdminRepository
    {
        //customer
        IEnumerable<Customer> GetAllCustomers();
        Customer GetCusomter(int id);
        void DeleteCustomer(int id);

        //driver
        IEnumerable<Driver> GetAllDrivers();
        Driver GetDriver(int id);
        void DeleteDriver(int id);

        //order
        IEnumerable<Order> GetAllOrders();
        Order GetOrder(int id);

        //reports driver
        IEnumerable<Report> GetAllReports();
        Report GetReport(int id);
        //system reports

        int NumberOfDrivers();
        int NumberOfCustomers();
        int NumberOfOrders();
    }
}
