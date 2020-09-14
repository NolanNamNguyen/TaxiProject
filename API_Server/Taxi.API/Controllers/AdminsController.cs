using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Taxi.Domain.Entities;
using Taxi.Domain.Helpers;
using Taxi.Domain.Interfaces;
using Taxi.Domain.Models.Customers;
using Taxi.Domain.Models.Customers.orders;
using Taxi.Domain.Models.Customers.Reports;
using Taxi.Domain.Models.Drivers;

namespace Taxi.API.Controllers
{
    [Authorize(Roles = Role.Admin)]
    [Route("api/[controller]")]
    [ApiController]
    public class AdminsController : ControllerBase
    {
        private IAdminRepository _adminRepository;
        private IMapper _mapper;

        public AdminsController(
            IAdminRepository adminRepository,
            IMapper mapper)
        {
            _adminRepository = adminRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Get list drivers
        /// </summary>
        /// <returns></returns>
        [HttpGet("drivers")]
        public IActionResult GetAllDrivers()
        {
            var drivers = _adminRepository.GetAllDrivers();
            var model = _mapper.Map<IList<DriverModel>>(drivers);
            return Ok(model);
        }

        /// <summary>
        /// Get driver by driverId
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("drivers/{id}")]
        public IActionResult GetDriver(int id)
        {
            var driver = _adminRepository.GetDriver(id);
            var model = _mapper.Map<DriverModel>(driver);

            if (model == null)
                return NotFound();
            return Ok(model);
        }

        /// <summary>
        /// Delete driver by driverId
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        [HttpDelete("drivers/{id}")]
        public IActionResult DeleteDriver(int id)
        {
            try
            {
                _adminRepository.DeleteDriver(id);
                return Ok();
            }
            catch (AppException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        //customer

        /// <summary>
        /// Get list customers
        /// </summary>
        /// <returns></returns>
        [HttpGet("customers")]
        public IActionResult GetAllCustomers()
        {
            var customers = _adminRepository.GetAllCustomers();
            var model = _mapper.Map<IList<CustomerModel>>(customers);
            return Ok(model);
        }

        /// <summary>
        /// Get customer by customerId
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("customers/{id}")]
        public IActionResult GetCustomer(int id)
        {
            var customer = _adminRepository.GetCusomter(id);
            var model = _mapper.Map<CustomerModel>(customer);
            if (model == null)
                return NotFound();
            return Ok(model);
        }

        /// <summary>
        /// Delete customer by customerId
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("customers/{id}")]
        public IActionResult DeleteCustomer(int id)
        {
            try
            {
                _adminRepository.DeleteCustomer(id);
                return Ok();
            }
            catch (AppException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        //order

        /// <summary>
        /// Get list orders
        /// </summary>
        /// <returns></returns>
        [HttpGet("orders")]
        public IActionResult GetAllOrders()
        {
            var orders = _adminRepository.GetAllOrders();
            var model = _mapper.Map<IList<OrderModel>>(orders);
            return Ok(model);
        }

        /// <summary>
        /// Get order by orderId
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("orders/{id}")]
        public IActionResult GetOrder(int id)
        {
            var order = _adminRepository.GetOrder(id);
            var model = _mapper.Map<OrderModel>(order);
            if (model == null)
                return NotFound();
            return Ok(model);
        }

        //system reports

        /// <summary>
        /// Get the number of drivers
        /// </summary>
        /// <returns></returns>
        [HttpGet("numberofdrivers")]
        public IActionResult NumOfDrivers()
        {
            int num = _adminRepository.NumberOfDrivers();
            return Ok(new
            {
                NumberOfDrivers = num
            });
        }

        /// <summary>
        /// get number of customers
        /// </summary>
        /// <returns></returns>
        [HttpGet("numberofcustomers")]
        public IActionResult NumOfCustomers()
        {
            int num = _adminRepository.NumberOfCustomers();
            return Ok(new
            {
                NumberOfCustomers = num
            });
        }

        /// <summary>
        /// get number of orders
        /// </summary>
        /// <returns></returns>
        [HttpGet("numberoforders")]
        public IActionResult NumOfOrders()
        {
            int num = _adminRepository.NumberOfOrders();
            return Ok(new
            {
                NumberOfOrders = num
            });
        }

        //reports driver

        /// <summary>
        /// Get list reports
        /// </summary>
        /// <returns></returns>
        [HttpGet("reports")]
        public IActionResult GetAllReports()
        {
            var reports = _adminRepository.GetAllReports();
            var model = _mapper.Map<IList<ReportModel>>(reports);
            return Ok(model);
        }

        /// <summary>
        /// Get report by reportId
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("reports/{id}")]
        public IActionResult GetReportById(int id)
        {
            var report = _adminRepository.GetReport(id);
            var model = _mapper.Map<ReportModel>(report);
            if (model == null)
                return NotFound();
            return Ok(model);
        }
    }
}
