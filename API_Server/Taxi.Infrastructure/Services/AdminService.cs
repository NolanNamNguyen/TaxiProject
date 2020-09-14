using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Taxi.Domain.Entities;
using Taxi.Domain.Interfaces;
using Taxi.Infrastructure.Data;

namespace Taxi.Infrastructure.Services
{
    public class AdminService : IAdminRepository
    {
        private TaxiContext _context;
        public AdminService(TaxiContext context)
        {
            _context = context;
        }
        //driver
        public IEnumerable<Driver> GetAllDrivers()
        {
            return _context.Drivers;
        }

        public Driver GetDriver(int id)
        {
            return _context.Drivers.Find(id);
        }

        public void DeleteDriver(int id)
        {
            var driver = _context.Drivers.Find(id);
            if (driver != null)
            {
                //delete related data
                var schedule = driver.Schedule;
                if (schedule != null)
                    _context.Schedules.Remove(schedule);
                var vehicle = driver.Vehicle;
                if (vehicle != null)
                    _context.Vehicles.Remove(vehicle);

                var userId = driver.UserId;
                _context.Drivers.Remove(driver);
                var user = _context.Users.Find(userId);
                _context.Users.Remove(user);

                _context.SaveChanges();
            }
        }

        //customer
        public IEnumerable<Customer> GetAllCustomers()
        {
            return _context.Customers;
        }

        public Customer GetCusomter(int id)
        {
            return _context.Customers.Find(id);
        }
        public void DeleteCustomer(int id)
        {
            var customer = _context.Customers.Find(id);
            if (customer != null)
            {
                var userId = customer.UserId;
                var user = _context.Users.Find(userId);

                _context.Customers.Remove(customer);
                _context.Users.Remove(user);

                _context.SaveChanges();
            }
        }

        //order
        public IEnumerable<Order> GetAllOrders()
        {
            return _context.Orders.OrderByDescending(e => e.Created);
        }

        public Order GetOrder(int id)
        {
            return _context.Orders.Find(id);
        }

        //system report
        public int NumberOfDrivers()
        {
            return _context.Drivers.Count();
        }

        public int NumberOfCustomers()
        {
            return _context.Customers.Count();
        }

        public int NumberOfOrders()
        {
            return _context.Orders.Count();
        }

        //reports driver
        public IEnumerable<Report> GetAllReports()
        {
            return _context.Reports.OrderByDescending(e => e.Created);
        }
        public Report GetReport(int id)
        {
            return _context.Reports.Find(id);
        }

    }
}
