using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Taxi.Domain.Entities;
using Taxi.Domain.Helpers;
using Taxi.Domain.Interfaces;
using Taxi.Domain.Models.Drivers.Schedule;
using Taxi.Infrastructure.Data;

namespace Taxi.Infrastructure.Services
{
    public class CustomerService : ICustomerRepository
    {
        private const int VALUE = 100000;
        private TaxiContext _context;
        public CustomerService(TaxiContext context)
        {
            _context = context;
        }

        //profile
        public Customer GetById(int UserId)
        {
            return _context.Customers.SingleOrDefault(e => e.UserId == UserId);
        }

        //booking
        public IEnumerable<Order> GetOrdersOfUser(int userId)
        {
            var orders = _context.Orders.Where(e => e.Customer.UserId == userId)
                .Where(e => e.Status == true)
                .OrderByDescending(e => e.Created);
            return orders;
        }

        public Order GetCurrentOrderOfCustomer(int customerId)
        {
            var order = _context.Orders.Where(e => e.CustomerId == customerId)
                .Where(e => e.IsCancel == false)
                .SingleOrDefault(e => e.Status == false);
            return order;
        }

        public Order GetOrderById(int orderId)
        {
            return _context.Orders.Find(orderId);
        }

        public IEnumerable<Schedule> SearchSchedule(string pickupLocation, string returnLocation, int Reservations)
        {
            if (!ProvinceList.Province.ContainsKey(pickupLocation)
                || !ProvinceList.Province.ContainsKey(returnLocation))
                throw new AppException("Pickup or Return location not provided service");
            int _pickup = (int)ProvinceList.Province[pickupLocation];
            int _return = (int)ProvinceList.Province[returnLocation];
            int distance = Math.Abs(_pickup - _return) + 1;
            if (distance > 4)
                throw new AppException("It is not possible to choose a route longer than 4 provinces");
            if (_pickup == _return)
                throw new AppException("Pickup and return location cannot be the same");
            if (_pickup < _return)
            {
                var schedules = _context.Schedules.AsEnumerable().Where(e => _pickup >= (int)ProvinceList.Province[e.StartProvince])
                    .Where(e => _return <= (int)ProvinceList.Province[e.DestinationProvince])
                    .Where(e => e.StartTime >= DateTime.Now)
                    .Where(e => Reservations <= e.AvailablableSlot).ToList();
                if (schedules == null)
                    return null;
                return schedules.OrderBy(e => e.StartTime);
            }
            else
            {
                var schedules = _context.Schedules.AsEnumerable().Where(e => _pickup <= (int)ProvinceList.Province[e.StartProvince])
                    .Where(e => _return >= (int)ProvinceList.Province[e.DestinationProvince])
                    .Where(e => Reservations <= e.AvailablableSlot).ToList();
                if (schedules == null)
                    return null;
                return schedules.OrderBy(e => e.StartTime);
            }
        }

        public async Task<Order> CreateOrder(Order order)
        {
            //Verify that Customer have no order where order have not complete
            var _order = _context.Orders.Where(e => e.CustomerId == order.CustomerId)
                .Where(e => e.IsCancel == false)
                .SingleOrDefault(e => e.Status == false);
            if (_order != null)
                throw new AppException("Please complete or cancel your current booking first!!!");

            int _pickup = (int)ProvinceList.Province[order.PickupLocation];
            int _return = (int)ProvinceList.Province[order.ReturnLocation];
            int distance = Math.Abs(_pickup - _return);
            order.Price = distance * order.Reservations * VALUE;

            if (order.Reservations <= _context.Schedules.SingleOrDefault(e => e.DriverId == order.DriverId).AvailablableSlot)
                _context.Schedules.SingleOrDefault(e => e.DriverId == order.DriverId).AvailablableSlot -= order.Reservations;
            else
                throw new AppException("Error! Reservations > AvailableSlot");
            order.Created = DateTime.Now;
            //save notify
            var _notifi = new Notify();
            var _userId = _context.Drivers.Find(order.DriverId).UserId;
            var CustomerName = _context.Customers.Find(order.CustomerId).User.Name;
            _notifi.UserId = _userId;
            _notifi.Content = $"Có Order mới: Customer: {CustomerName}; Nơi đón: {order.PickupLocation}; Nơi đến: {order.ReturnLocation}; Số ghế: {order.Reservations}";
            _context.Notifies.Add(_notifi);
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();

            //send notification to driver

            return order;
        }

        /*public void UpdateOrder(Order order)
        {
            throw new AppException("chua code");
        }*/

        public void DeleteOrder(int id)
        {
            var order = _context.Orders.Find(id);
            if (order != null)
            {
                if (order.IsCancel == false || order.Status == false)
                    order.Driver.Schedule.AvailablableSlot += order.Reservations;
                _context.Orders.Remove(order);
                _context.SaveChanges();
            }
        }

        public void CancelOrder(int id)
        {
            var order = _context.Orders.Find(id);
            if (order.Status == true)
                throw new AppException("Error! Order have been done.");
            if (order.IsCancel == false)
            {
                order.IsCancel = true;
                order.Driver.Schedule.AvailablableSlot += order.Reservations;
                //save notify
                var _notifi = new Notify();
                var _userId = _context.Drivers.Find(order.DriverId).UserId;
                var CustomerName = _context.Customers.Find(order.CustomerId).User.Name;
                _notifi.UserId = _userId;
                _notifi.Content = $"Customer: {CustomerName} hủy chuyến; Thông tin Nơi đón: {order.PickupLocation}; Nơi đến: {order.ReturnLocation}; Số ghế: {order.Reservations}";
                _context.Notifies.Add(_notifi);
                _context.Orders.Update(order);
                _context.SaveChanges();
            }
            //send notification to driver
        }

        //Rate
        public void Rating(Order orderParams)
        {
            var order = _context.Orders.Find(orderParams.OrderId);
            if (order.CustomerId != orderParams.CustomerId)
                throw new AppException("Error! ForBid");
            if (order.Status == false)
                throw new AppException("Error! Completed order to rating");
            if (order.Rate > 0)
                throw new AppException("You have already rated. The rating cannot be edited");

            order.Rate = orderParams.Rate;
            if (!string.IsNullOrWhiteSpace(orderParams.RateContent))
                order.RateContent = orderParams.RateContent;

            _context.Update(order);
            _context.SaveChanges();
        }

        //report driver
        public IEnumerable<Report> GetReportsOfUser(int userId)
        {
            var reports = _context.Reports.Where(e => e.Customer.UserId == userId)
                .OrderBy(e => e.Created).OrderByDescending(e => e.Created);
            return reports;
        }

        public Report GetReportById(int reportId)
        {
            return _context.Reports.Find(reportId);
        }

        public async Task<Report> CreateReport(Report report)
        {
            report.Created = DateTime.Now;

            await _context.Reports.AddAsync(report);
            await _context.SaveChangesAsync();

            return report;
        }

        //helper method
        public int GetCustomerId(int userId)
        {
            return _context.Users.Find(userId).Customer.CustomerId;
        }
        public int GetUserIdOfDriver(int driverId)
        {
            return _context.Drivers.Find(driverId).UserId;
        }

        public Driver GetDriverbyId(int driverId)
        {
            return _context.Drivers.Find(driverId);
        }

        public float GetRateAverageOfDriver(int driverId)
        {
            float rate = 0;
            var orders = _context.Orders.Where(x => x.DriverId == driverId);
            foreach (var item in orders)
            {
                rate += item.Rate;
            }
            rate /= orders.Count(x => x.Rate != 0);
            return rate;
        }
    }
}
