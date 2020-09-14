using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Taxi.API.SignalRHub;
using Taxi.Domain.Entities;
using Taxi.Domain.Helpers;
using Taxi.Domain.Interfaces;
using Taxi.Domain.Models.Customers;
using Taxi.Domain.Models.Customers.orders;
using Taxi.Domain.Models.Customers.Reports;
using Taxi.Domain.Models.Drivers.Schedule;

namespace Taxi.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private ICustomerRepository _customerService;
        private IMapper _mapper;
        private IHubContext<NotificationsHub, INotificationsHub> _signalrHub;
        public CustomersController(
            ICustomerRepository customerService,
            IMapper mapper,
            IHubContext<NotificationsHub, INotificationsHub> hub)
        {
            _mapper = mapper;
            _customerService = customerService;
            _signalrHub = hub;
        }

        //profile

        /// <summary>
        /// Get customer profile
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = Role.Customer)]
        [HttpGet("profile")]
        public IActionResult CustomerProfile()
        {
            var currentUserId = int.Parse(User.Identity.Name);

            var customer = _customerService.GetById(currentUserId);
            var model = _mapper.Map<CustomerModel>(customer);
            if (model == null)
                return NotFound();
            return Ok(model);
        }

        //booking

        /// <summary>
        /// Get list customer's orders(have been complete)
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = Role.Customer)]
        [HttpGet("orders")]
        public IActionResult GetOrdersOfUser()
        {
            var currentUserId = int.Parse(User.Identity.Name);
            var orders = _customerService.GetOrdersOfUser(currentUserId);
            var model = _mapper.Map<List<OrderModel>>(orders);
            if (model == null)
                return NotFound();
            return Ok(model);
        }
        /// <summary>
        /// Get current order of Customer by CustomerId
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Roles = Role.Customer)]
        [HttpGet("currentorder/{id}")]
        public IActionResult GetCurrentOrderOfUser(int id)
        {
            var currentUserId = int.Parse(User.Identity.Name);
            var order = _customerService.GetCurrentOrderOfCustomer(id);
            if (order == null)
                return NotFound();
            if (currentUserId != order.Customer.UserId)
                return Forbid();
            var model = _mapper.Map<OrderModel>(order);
            return Ok(model);
        }

        /// <summary>
        /// Get order by orderId
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Roles = Role.Customer)]
        [HttpGet("orders/{id}")]
        public IActionResult GetOrderById(int id)
        {
            var currentUserId = int.Parse(User.Identity.Name);
            var order = _customerService.GetOrderById(id);
            if (order == null)
                return NotFound();
            if (currentUserId != order.Customer.UserId && currentUserId != order.Driver.UserId)
                return Forbid();
            var driver = _customerService.GetDriverbyId(order.DriverId);
            var model = _mapper.Map<OrderModel>(order);
            model.DriverImg = driver.User.ImagePath;
            model.DriverName = driver.User.Name;
            model.DriverRate = _customerService.GetRateAverageOfDriver(order.DriverId);

            return Ok(model);
        }

        /// <summary>
        /// Find route to book
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Authorize(Roles = Role.Customer)]
        [HttpPost("schedules/search")]
        public IActionResult SearchSchedule([FromBody] SearchScheduleModel model)
        {
            var schedules = _customerService.SearchSchedule(model.PickupLocation, model.ReturnLocation, model.Reservations);
            if (schedules == null)
                return NotFound();

            var results = _mapper.Map<IList<ScheduleModel>>(schedules);
            foreach (var item in results)
            {
                item.RateAverage = _customerService.GetRateAverageOfDriver(item.DriverId);

            }

            return Ok(results);
        }

        /// <summary>
        /// Booking
        /// </summary>
        /// <remarks>
        ///     use the infomation have been search to booking
        /// </remarks>
        /// <param name="model"></param>
        /// <returns></returns>
        [Authorize(Roles = Role.Customer)]
        [HttpPost("booking")]
        public async Task<IActionResult> Booking([FromBody] AddOrderModel model)
        {
            var currentUserId = int.Parse(User.Identity.Name);
            var order = _mapper.Map<Order>(model);
            order.CustomerId = _customerService.GetCustomerId(currentUserId);
            try
            {
                var _order = await _customerService.CreateOrder(order);
                //SignalR
                var userId = _customerService.GetUserIdOfDriver(_order.DriverId).ToString();
                var _orderModel = _mapper.Map<OrderModel>(_order);
                _orderModel.CustomerName = _order.Customer.User.Name;
                await _signalrHub.Clients.Groups(userId).SendBookingInfoToDriver(_orderModel);

                return Ok(new { orderId = _order.OrderId });
            }
            catch (AppException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Delete order by orderId 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Roles = Role.Admin)]
        [HttpDelete("orders/{id}")]
        public IActionResult DeleteOrder(int id)
        {
            try
            {
                _customerService.DeleteOrder(id);
                return Ok();
            }
            catch (AppException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// cancel booking
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Roles = Role.Customer)]
        [HttpPut("orders/cancel/{id}")]
        public IActionResult CancelOrder(int id)
        {
            try
            {
                _customerService.CancelOrder(id);
                //SignalR
                var _order = _customerService.GetOrderById(id);
                var userId = _customerService.GetUserIdOfDriver(_order.DriverId).ToString();
                var _orderModel = _mapper.Map<OrderModel>(_order);
                _orderModel.CustomerName = _order.Customer.User.Name;
                _signalrHub.Clients.Groups(userId).SendBookingInfoToDriver(_orderModel);
                return Ok();
            }
            catch (AppException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        //rate

        /// <summary>
        /// booking reviews
        /// </summary>
        /// <remarks>
        ///     the booking must be completed to be rating. (status == true)
        /// </remarks>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [Authorize(Roles = Role.Customer)]
        [HttpPut("orders/rating/{id}")]
        public IActionResult Rating(int id, [FromBody] RateModel model)
        {
            var currentUserId = int.Parse(User.Identity.Name);
            var customerId = _customerService.GetCustomerId(currentUserId);
            var order = _mapper.Map<Order>(model);

            try
            {
                order.OrderId = id;
                order.CustomerId = customerId;
                _customerService.Rating(order);
                return Ok();
            }
            catch (AppException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        //report driver

        /// <summary>
        /// Get a list of my reports
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = Role.Customer)]
        [HttpGet("reports")]
        public IActionResult GetReportsOfUser()
        {
            var currentUserId = int.Parse(User.Identity.Name);
            var reports = _customerService.GetReportsOfUser(currentUserId);
            var model = _mapper.Map<IList<ReportModel>>(reports);
            if (model == null)
                return NotFound();
            return Ok(model);
        }

        /// <summary>
        /// get a report by reportId
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Roles = Role.Customer)]
        [HttpGet("reports/{id}")]
        public IActionResult GetReportById(int id)
        {
            var currentUserId = int.Parse(User.Identity.Name);
            var report = _customerService.GetReportById(id);
            if (report == null)
                return NotFound();
            if (currentUserId != report.Customer.UserId && currentUserId != report.Driver.UserId)
                return Forbid();
            var model = _mapper.Map<ReportModel>(report);

            return Ok(model);
        }

        /// <summary>
        /// Post report
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Authorize(Roles = Role.Customer)]
        [HttpPost("reports")]
        public async Task<IActionResult> Reporting([FromBody] CreateReportModel model)
        {
            var currentUserId = int.Parse(User.Identity.Name);
            var report = _mapper.Map<Report>(model);
            report.CustomerId = _customerService.GetCustomerId(currentUserId);
            try
            {
                var _report = await _customerService.CreateReport(report);
                //signalR
                var _reportModel = _mapper.Map<ReportModel>(_report);
                await _signalrHub.Clients.Group("Group Admins").SendReportToAdmins(_reportModel);
                return Ok(new { message = "Report have been sended" });
            }
            catch (AppException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
