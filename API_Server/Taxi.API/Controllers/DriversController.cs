using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Taxi.Domain.Entities;
using Taxi.Domain.Helpers;
using Taxi.Domain.Interfaces;
using Taxi.Domain.Models.Drivers.Vehicles;
using Taxi.Domain.Models.Drivers.Schedule;
using Taxi.Domain.Models.Drivers;
using System.IO;
using Taxi.Domain.Models.Customers.orders;
using Microsoft.AspNetCore.SignalR;
using Taxi.API.SignalRHub;

namespace Taxi.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class DriversController : ControllerBase
    {
        private IDriverRepository _driverService;
        private IMapper _mapper;
        private IHubContext<NotificationsHub, INotificationsHub> _signalrHub;
        public DriversController(
            IDriverRepository driverRepository,
            IMapper mapper,
            IHubContext<NotificationsHub, INotificationsHub> hub)
        {
            _driverService = driverRepository;
            _mapper = mapper;
            _signalrHub = hub;
        }
        //vehicle 

        /// <summary>
        /// Register vehicle
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Authorize(Roles = Role.Driver)]
        [HttpPost("vehicles/register")]
        public IActionResult VCreate([FromForm] VehicleRegisterModel model)
        {
            var currentUserId = int.Parse(User.Identity.Name);
            int currentDriverId = _driverService.FindDriverIdViaUserId(currentUserId);
            if (model.DriverId != currentDriverId)
                return Forbid();
            var vehicle = _mapper.Map<Vehicle>(model);
            //saving image, video on server
            var image = model.Image;
            if (image != null)
            {
                if (image.Length > 0)
                {
                    var supportedTypes = new[] { ".jpg", ".png", ".bmp", ".gif", ".jpeg", "webp" };
                    var imageExt = Path.GetExtension(image.FileName).ToLower();
                    if (!supportedTypes.Contains(imageExt))
                        throw new AppException("File Extension Is InValid - Only Upload jpg/png/bmp/gif/jpeg/webp File");

                    Directory.CreateDirectory("wwwroot/vehicles/images");
                    string fileName = vehicle.VehicleName + "_vehicleImage" + imageExt;
                    string filePath = Path.Combine("wwwroot/vehicles/images", fileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        image.CopyTo(fileStream);
                    }
                    vehicle.ImagePath = "https://localhost:44360/" + filePath;
                }
            }

            var video = model.Video;
            if (video != null)
            {
                var supportedTypes = new[] { ".mp4", ".wmv", ".webm", ".flv", ".avi" };
                var videoExt = Path.GetExtension(video.FileName).ToLower();
                if (!supportedTypes.Contains(videoExt))
                    throw new AppException("File Extension Is InValid - Only Upload mp4/wmv/webm/flv/avi File");

                Directory.CreateDirectory("wwwroot/vehicles/videos");
                string fileName = vehicle.VehicleName + "_vehicleVideo" + videoExt;
                string filePath = Path.Combine("wwwroot/vehicles/videos", fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    video.CopyTo(fileStream);
                }
                vehicle.VideoPath = "https://localhost:44360/" + filePath;
            }
            try
            {
                //register vehicle
                _driverService.VCreate(vehicle);
                return Ok(new { message = "Registration successful" });
            }
            catch (AppException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Delete vehicle
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("vehicles/{id}")]
        public IActionResult VDelete(int id)
        {
            if (User.IsInRole(Role.Customer))
                return Forbid();
            var currentUserId = int.Parse(User.Identity.Name);
            int currentDriverId;
            if (!User.IsInRole(Role.Admin))
            {
                currentDriverId = _driverService.FindDriverIdViaUserId(currentUserId);
                if (id != currentDriverId)
                    return Forbid();
            }
            try
            {
                _driverService.VDelete(id);
                return Ok();
            }
            catch (AppException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Get a list of vehicles
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = Role.Admin)]
        [HttpGet("vehicles")]
        public IActionResult VGetAll()
        {
            var vehicles = _driverService.VGetAll();
            var model = _mapper.Map<IList<VehicleModel>>(vehicles);
            return Ok(model);
        }

        /// <summary>
        /// Get a vehicle by driverId
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("vehicles/{id}")]
        public IActionResult VGetById(int id)
        {
            if (User.IsInRole(Role.Customer))
                return Forbid();
            var currentUserId = int.Parse(User.Identity.Name);
            int currentDriverId;
            if (!User.IsInRole(Role.Admin))
            {
                currentDriverId = _driverService.FindDriverIdViaUserId(currentUserId);
                if (id != currentDriverId)
                    return Forbid();
            }

            var vehicle = _driverService.VGetById(id);
            var model = _mapper.Map<VehicleModel>(vehicle);

            if (model == null) return NotFound();
            return Ok(model);
        }

        /// <summary>
        /// update vehicle info by driverId
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut("vehicles/{id}")]
        public IActionResult VUpdate(int id, [FromForm] VehicleUpdateModel model)
        {
            var vehicle = _mapper.Map<Vehicle>(model);
            vehicle.DriverId = id;
            if (User.IsInRole(Role.Customer))
                return Forbid();
            var currentUserId = int.Parse(User.Identity.Name);
            int currentDriverId;
            if (!User.IsInRole(Role.Admin))
            {
                currentDriverId = _driverService.FindDriverIdViaUserId(currentUserId);
                if (id != currentDriverId || currentDriverId != vehicle.DriverId)
                    return Forbid();
            }
            //update image, video 
            var image = model.Image;
            if (image != null)
            {
                if (image.Length > 0)
                {
                    var supportedTypes = new[] { ".jpg", ".png", ".bmp", ".gif", ".jpeg", "webp" };
                    var imageExt = Path.GetExtension(image.FileName).ToLower();
                    if (!supportedTypes.Contains(imageExt))
                        throw new AppException("File Extension Is InValid - Only Upload jpg/png/bmp/gif/jpeg/webp File");

                    Directory.CreateDirectory("wwwroot/vehicles/images");
                    string fileName = id + "_vehicleImage" + imageExt;
                    string filePath = Path.Combine("wwwroot/vehicles/images", fileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        image.CopyTo(fileStream);
                    }
                    vehicle.ImagePath = "https://localhost:44360/" + filePath;
                }
            }

            var video = model.Video;
            if (video != null)
            {
                var supportedTypes = new[] { ".mp4", ".wmv", ".webm", ".flv", ".avi" };
                var videoExt = Path.GetExtension(video.FileName).ToLower();
                if (!supportedTypes.Contains(videoExt))
                    throw new AppException("File Extension Is InValid - Only Upload mp4/wmv/webm/flv/avi File");

                Directory.CreateDirectory("wwwroot/vehicles/videos");
                string fileName = id + "_vehicleVideo" + videoExt;
                string filePath = Path.Combine("wwwroot/vehicles/videos", fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    video.CopyTo(fileStream);
                }
                vehicle.VideoPath = "https://localhost:44360/" + filePath;
            }

            try
            {
                _driverService.VUpdate(vehicle);
                return Ok();
            }
            catch (AppException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        //schedule

        /// <summary>
        /// Register schedule
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Authorize(Roles = Role.Driver)]
        [HttpPost("schedules/register")]
        public IActionResult SCreate([FromBody] RegisterScheduleModel model)
        {
            var currentUserId = int.Parse(User.Identity.Name);
            int currentDriverId = _driverService.FindDriverIdViaUserId(currentUserId);
            var schedule = _mapper.Map<Schedule>(model);
            schedule.DriverId = currentDriverId;

            try
            {
                //register vehicle
                _driverService.SCreate(schedule);
                return Ok(new { message = "Registration successful" });
            }
            catch (AppException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Delete schedule by driverId
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("schedules/{id}")]
        public IActionResult SDelete(int id)
        {
            if (User.IsInRole(Role.Customer))
                return Forbid();
            var currentUserId = int.Parse(User.Identity.Name);
            int currentDriverId;
            if (!User.IsInRole(Role.Admin))
            {
                currentDriverId = _driverService.FindDriverIdViaUserId(currentUserId);
                if (id != currentDriverId)
                    return Forbid();
            }
            try
            {
                _driverService.SDelete(id);
                //signalR
                var orders = _driverService.GetOrdersOfUser(currentUserId);
                string msg = "Chuyến đi của bạn đã bị hủy";
                foreach (var order in orders)
                {
                    var userId = order.Customer.UserId.ToString();
                    _signalrHub.Clients.Groups(userId).SendMessageToUser(msg);
                }
                return Ok();
            }
            catch (AppException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Get a list of schedules
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = Role.Admin)]
        [HttpGet("schedules")]
        public IActionResult SGetAll()
        {
            var schedules = _driverService.SGetAll();
            var model = _mapper.Map<IList<ScheduleModel>>(schedules);
            return Ok(model);
        }

        /// <summary>
        /// Get a schedule by driverId
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("schedules/{id}")]
        public IActionResult SGetById(int id)
        {
            if (User.IsInRole(Role.Customer))
                return Forbid();
            var currentUserId = int.Parse(User.Identity.Name);
            int currentDriverId;
            if (!User.IsInRole(Role.Admin))
            {
                currentDriverId = _driverService.FindDriverIdViaUserId(currentUserId);
                if (id != currentDriverId)
                    return Forbid();
            }

            var schedule = _driverService.SGetById(id);
            var model = _mapper.Map<ScheduleModel>(schedule);

            if (model == null) return NotFound();
            return Ok(model);
        }

        /// <summary>
        /// Update schedule info by driverId
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [Authorize(Roles = Role.Driver)]
        [HttpPut("schedules/{id}")]
        public IActionResult SUpdate(int id, [FromBody] RegisterScheduleModel model)
        {
            var currentUserId = int.Parse(User.Identity.Name);
            int currentDriverId = _driverService.FindDriverIdViaUserId(currentUserId);
            var schedule = _mapper.Map<Schedule>(model);
            schedule.DriverId = currentDriverId;
            try
            {
                _driverService.SUpdate(schedule);
                return Ok();
            }
            catch (AppException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        //driver profile

        /// <summary>
        /// get a list of drivers 
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = Role.Admin)]
        [HttpGet]
        public IActionResult DGetAll()
        {
            var drivers = _driverService.DGetAll();
            var model = _mapper.Map<IList<DriverModel>>(drivers);
            return Ok(model);
        }

        /// <summary>
        /// Get driver profile
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = Role.Driver)]
        [HttpGet("profile")]
        public IActionResult DriverProfile()
        {
            var currentUserId = int.Parse(User.Identity.Name);

            var driver = _driverService.DGetById(currentUserId);
            var model = _mapper.Map<DriverModel>(driver);
            float rateAve = 0;
            int _count = 0;
            foreach (var order in driver.Orders)
            {
                if (order.Rate > 0)
                {
                    rateAve += order.Rate;
                    _count++;
                }

            }
            rateAve /= _count;
            model.rateAverage = rateAve;

            if (model == null) return NotFound();
            return Ok(model);
        }

        /// <summary>
        /// Get driver's reviews
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [AllowAnonymous]
        //reviews/driverId
        [HttpGet("reviews/{id}")]
        public IActionResult GetReviewsOfDriver(int id)
        {
            var reviews = _driverService.DGetAllReviewsOfDriver(id);
            var models = _mapper.Map<IList<DriverReviewModel>>(reviews);
            if (models == null)
                return NotFound();

            float rateAverage = 0;
            foreach (var item in models)
                rateAverage += item.Rate;
            rateAverage = rateAverage / models.Count;

            return Ok(new
            {
                rateAverage,
                rates = models
            });
        }

        //orders

        /// <summary>
        /// get driver's orders list
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = Role.Driver)]
        [HttpGet("orders")]
        public IActionResult GetOrdersOfUser()
        {
            var currentUserId = int.Parse(User.Identity.Name);
            var orders = _driverService.GetOrdersOfUser(currentUserId);
            var model = _mapper.Map<IList<OrderModel>>(orders);
            if (model == null)
                return NotFound();
            return Ok(model);
        }

        /// <summary>
        /// mark the booking completed by orderId
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Roles = Role.Driver)]
        [HttpPut("orders/complete/{id}")]
        public async Task<IActionResult> CompleteOrder(int id)
        {
            try
            {
                await _driverService.CompleteOrder(id);
                //signalR
                var userId = _driverService.GetUserIdofCustomerId(id).ToString();
                string msg = "Chuyến đi hoàn thành. Bạn có thể đánh giá ngay bây giờ!";
                await _signalrHub.Clients.Groups(userId).SendMessageToUser(msg);

                return Ok();
            }
            catch (AppException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
