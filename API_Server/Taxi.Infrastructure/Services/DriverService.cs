using Microsoft.EntityFrameworkCore.Internal;
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
    public class DriverService : IDriverRepository
    {
        private TaxiContext _context;
        public DriverService(TaxiContext context)
        {
            _context = context;
        }

        //vehicle 
        public Vehicle VCreate(Vehicle vehicle)
        {
            if (_context.Vehicles.Any(x => x.License == vehicle.License))
                throw new AppException("Licence " + "'" + vehicle.License + "'" + "is already taken");
            vehicle.Created = DateTime.Now;

            _context.Vehicles.Add(vehicle);
            _context.SaveChanges();

            return vehicle;
        }
        //use DriverId to querry
        public void VDelete(int id)
        {
            var vehicle = _context.Vehicles.FirstOrDefault(x => x.DriverId == id);
            if (vehicle != null)
            {
                _context.Vehicles.Remove(vehicle);
                _context.SaveChanges();
            }
        }

        public IEnumerable<Vehicle> VGetAll()
        {
            return _context.Vehicles;
        }

        public Vehicle VGetById(int id)
        {
            return _context.Vehicles.FirstOrDefault(x => x.DriverId == id);
        }

        public void VUpdate(Vehicle vehicleParam)
        {
            var vehicle = _context.Vehicles.FirstOrDefault(x => x.DriverId == vehicleParam.DriverId);
            if (vehicle == null)
                throw new AppException("Vehicle Not Found");
            //update vehicle info if provided
            if (!string.IsNullOrWhiteSpace(vehicleParam.VehicleName))
                vehicle.VehicleName = vehicleParam.VehicleName;
            if (!string.IsNullOrWhiteSpace(vehicleParam.License) &&
                !_context.Vehicles.Any(x => x.License == vehicleParam.License))
                vehicle.License = vehicleParam.License;
            if (vehicleParam.Seater > 0)
                vehicle.Seater = vehicleParam.Seater;

            if (!string.IsNullOrWhiteSpace(vehicleParam.ImagePath))
                vehicle.ImagePath = vehicleParam.ImagePath;
            if (!string.IsNullOrWhiteSpace(vehicleParam.VideoPath))
                vehicle.VideoPath = vehicleParam.VideoPath;

            vehicle.Updated = DateTime.Now;

            _context.Vehicles.Update(vehicle);
            _context.SaveChanges();
        }
        //schedule
        public Schedule SCreate(Schedule schedule)
        {
            if (_context.Vehicles.SingleOrDefault(e => e.DriverId == schedule.DriverId) == null)
                throw new AppException("You must register vehicle");
            if (!ProvinceList.Province.ContainsKey(schedule.StartProvince)
                || !ProvinceList.Province.ContainsKey(schedule.DestinationProvince))
                throw new AppException("Start or Destination province not provided service");
            int start = (int)ProvinceList.Province[schedule.StartProvince];
            int end = (int)ProvinceList.Province[schedule.DestinationProvince];
            int distance = Math.Abs(start - end) + 1;
            if (distance > 4)
                throw new AppException("It is not possible to choose a route longer than 4 provinces");
            if (schedule.StartTime < DateTime.Now)
                throw new AppException("StartTime error");
            if (schedule.AvailablableSlot > _context.Vehicles.SingleOrDefault(e => e.DriverId == schedule.DriverId).Seater)
                throw new AppException("AvailablableSlot > Vehicle's Seater");
            if (schedule.AvailablableSlot == 0)
                schedule.AvailablableSlot = _context.Vehicles.SingleOrDefault(e => e.DriverId == schedule.DriverId).Seater;
            if (schedule.StartTime < DateTime.Now)
                throw new AppException("Error!Because Start Time < Now");
            schedule.Created = DateTime.Now;

            _context.Schedules.Add(schedule);
            _context.SaveChanges();

            return schedule;
        }

        public void SDelete(int id)
        {
            var schedule = _context.Schedules.SingleOrDefault(x => x.DriverId == id);
            if (schedule != null)
            {
                //send notify to customers had book this driver's schedule
                var orders = _context.Orders.Where(e => e.DriverId == schedule.DriverId)
                    .Where(e => e.Status == false)
                    .Where(e => e.IsCancel == false);
                foreach (var order in orders)
                {
                    order.IsCancel = true;
                    var notify = new Notify();
                    notify.UserId = order.Customer.UserId;
                    notify.Content = $"Driver: {order.Driver.User.Name} đã hủy chuyến đi của bạn";
                    _context.Notifies.Add(notify);
                }
                _context.Schedules.Remove(schedule);
                _context.SaveChanges();
            }
        }

        public IEnumerable<Schedule> SGetAll()
        {
            return _context.Schedules;
        }

        public Schedule SGetById(int id)
        {
            return _context.Schedules.SingleOrDefault(x => x.DriverId == id);
        }

        public void SUpdate(Schedule scheduleParams)
        {
            var schedule = _context.Schedules.SingleOrDefault(x => x.DriverId == scheduleParams.DriverId);
            if (schedule == null)
                throw new AppException("Schedule Not Found");
            //update schedule info if provided
            if (scheduleParams.StartTime >= DateTime.Now)
                schedule.StartTime = scheduleParams.StartTime;
            else throw new AppException("StartTime error");

            if (!ProvinceList.Province.ContainsKey(scheduleParams.StartProvince)
                || !ProvinceList.Province.ContainsKey(scheduleParams.DestinationProvince))
                throw new AppException("Start or Destination province not provided service");
            int start = (int)ProvinceList.Province[scheduleParams.StartProvince];
            int end = (int)ProvinceList.Province[scheduleParams.DestinationProvince];
            int distance = Math.Abs(start - end) + 1;
            if (distance > 4)
                throw new AppException("It is not possible to choose a route longer than 4 provinces");

            if (schedule.AvailablableSlot > _context.Vehicles.SingleOrDefault(e => e.DriverId == schedule.DriverId).Seater)
                throw new AppException("AvailablableSlot > Vehicle's Seater");
            if (scheduleParams.AvailablableSlot != 0)
                schedule.AvailablableSlot = scheduleParams.AvailablableSlot;

            schedule.StartProvince = scheduleParams.StartProvince;
            schedule.DestinationProvince = scheduleParams.DestinationProvince;
            if (!string.IsNullOrWhiteSpace(scheduleParams.StartAddress))
                schedule.StartAddress = scheduleParams.StartAddress;
            if (!string.IsNullOrWhiteSpace(scheduleParams.DestinationAddress))
                schedule.DestinationAddress = scheduleParams.DestinationAddress;
            schedule.Updated = DateTime.Now;

            _context.Update(schedule);
            _context.SaveChanges();
        }
        //driver profile
        public IEnumerable<Driver> DGetAll()
        {
            return _context.Drivers;
        }
        public Driver DGetById(int UserId)
        {
            return _context.Drivers.SingleOrDefault(e => e.UserId == UserId);
        }

        public IEnumerable<Order> DGetAllReviewsOfDriver(int driverId)
        {
            return _context.Orders.Where(e => e.DriverId == driverId)
                .Where(e => e.Rate > 0);
        }

        //orders
        //get all orders of driver where status = false, and IsCancel = false
        public IEnumerable<Order> GetOrdersOfUser(int userId)
        {
            var orders = _context.Orders.Where(e => e.Driver.UserId == userId)
                .Where(e => e.Status == false)
                .Where(e => e.IsCancel == false);
            return orders;
        }

        public async Task CompleteOrder(int orderId)
        {
            var order = await _context.Orders.FindAsync(orderId);
            if (order.IsCancel == true)
                throw new AppException("Error! Order have been cancel.");
            if (order.Status == false)
            {
                order.Status = true;
                order.Driver.Schedule.AvailablableSlot += order.Reservations;
                _context.Orders.Update(order);
                await _context.SaveChangesAsync();
            }
        }
        //helpers method

        //find drvierId via UserId
        public int FindDriverIdViaUserId(int UserId)
        {
            var driver = _context.Drivers.Single(x => x.UserId == UserId);
            return driver.DriverId;
        }
        public int GetUserIdofCustomerId(int orderId)
        {
            return _context.Orders.Find(orderId).Customer.UserId;
        }
    }
}
