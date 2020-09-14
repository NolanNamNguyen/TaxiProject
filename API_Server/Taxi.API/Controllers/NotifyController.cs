using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Taxi.Domain.Entities;
using Taxi.Domain.Helpers;
using Taxi.Domain.Interfaces;
using Taxi.Domain.Models.Notify;

namespace Taxi.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class NotifyController : ControllerBase
    {
        private INotifyRepository _NotifyService;
        private IMapper _mapper;
        public NotifyController(INotifyRepository notifyRepository, IMapper mapper)
        {
            _NotifyService = notifyRepository;
            _mapper = mapper;
        }
        /// <summary>
        /// Get Notifies of User
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetNotifiesOfUser()
        {
            var userId = int.Parse(User.Identity.Name);
            var notifies = _NotifyService.GetNotifiesOfUser(userId);
            if (notifies == null)
                return NotFound();
            var model = _mapper.Map<IList<NotifyModel>>(notifies);
            return Ok(model);
        }
        /// <summary>
        /// Mark Read by notifyId
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public IActionResult MarkedRead(int id)
        {
            try
            {
                _NotifyService.MarkedRead(id);
                return Ok();
            }
            catch (AppException ex)
            {
                return BadRequest(new { message = ex.Message });
            }

        }
    }
}
