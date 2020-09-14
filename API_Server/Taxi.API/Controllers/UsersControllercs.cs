using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Taxi.API.Models;
using Taxi.Domain.Entities;
using Taxi.Domain.Helpers;
using Taxi.Domain.Interfaces;
using Taxi.Domain.Models.Users;

namespace Taxi.API.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUserRepository _userService;
        private IMapper _mapper;
        private readonly AppSettings _appSettings;

        public UsersController(
            IUserRepository userService,
            IMapper mapper,
            IOptions<AppSettings> appSettings)
        {
            _userService = userService;
            _mapper = mapper;
            _appSettings = appSettings.Value;
        }
        /// <summary>
        /// Login
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET /authenticate
        ///     {
        ///         "UserName":"customer1",
        ///         "Password":"customer123"
        ///     }
        /// </remarks>
        /// <param name="model"></param>
        /// <returns>Info of user and token</returns>
        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] AuthenticateModel model)
        {
            var user = _userService.Authenticate(model.UserName, model.Password);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });
            if (!user.IsVerified)
                return BadRequest(new { message = "Email has not been verified" });

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.UserId.ToString()),
                    new Claim(ClaimTypes.Role, user.Role)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            if (user.Admin != null)
                return Ok(new
                {
                    UserId = user.UserId,
                    UserName = user.UserName,
                    Name = user.Name,
                    Phone = user.Phone,
                    Email = user.Email,
                    Address = user.Address,
                    Role = user.Role,
                    AdminId = user.Admin.AdminId,
                    Created = user.Created,
                    Updated = user.Updated,
                    ImagePath = user.ImagePath,
                    IsVerified = user.IsVerified,
                    Token = tokenString
                });
            if (user.Driver != null)
            {
                bool IsRegVehicle = false;
                if (user.Driver.Vehicle != null)
                    IsRegVehicle = true;
                bool IsRegSchedule = false;
                if (user.Driver.Schedule != null)
                    IsRegSchedule = true;
                return Ok(new
                {
                    UserId = user.UserId,
                    UserName = user.UserName,
                    Name = user.Name,
                    Phone = user.Phone,
                    Email = user.Email,
                    Address = user.Address,
                    Role = user.Role,
                    DriverId = user.Driver.DriverId,
                    Created = user.Created,
                    Updated = user.Updated,
                    ImagePath = user.ImagePath,
                    IsVerified = user.IsVerified,
                    IsRegVehicle = IsRegVehicle,
                    IsRegSchedule = IsRegSchedule,
                    Token = tokenString
                });
            }

            return Ok(new
            {
                UserId = user.UserId,
                UserName = user.UserName,
                Name = user.Name,
                Phone = user.Phone,
                Email = user.Email,
                Address = user.Address,
                Role = user.Role,
                CustomerId = user.Customer.CustomerId,
                Created = user.Created,
                Updated = user.Updated,
                ImagePath = user.ImagePath,
                IsVerified = user.IsVerified,
                Token = tokenString
            });
        }

        /// <summary>
        /// Register new account
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterModel model)
        {
            string filePath = "https://localhost:44363/wwwroot/avatars\\defaultAvatar.png"; //default avatar

            //map model to entity
            var user = _mapper.Map<User>(model);
            user.ImagePath = filePath;

            try
            {
                //create user
                _userService.Create(user, model.Password, Request.Headers["origin"]);
                return Ok(new { message = "Registration successful, please check your email for verification instructions" });
            }
            catch (AppException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Verify email after register account
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("verify-email")]
        public IActionResult VerifyEmail(VerifyEmailRequestModel model)
        {
            _userService.VerifyEmail(model.Token);
            return Ok(new { message = "Verification successful, you can now login" });
        }

        /// <summary>
        /// Send email to reset password
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("forgot-password")]
        public IActionResult ForgotPassword(ForgotPasswordRequestModel model)
        {
            _userService.ForgotPassword(model, Request.Headers["origin"]);
            return Ok(new { meesage = "Please check your email for password reset instructions" });
        }

        /// <summary>
        /// Reset Password
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("reset-password")]
        public IActionResult ResetPassword(ResetPasswordRequestModel model)
        {
            _userService.ResetPassword(model);
            return Ok(new { message = "Password reset successful, you can now login" });
        }

        /// <summary>
        /// Get list Users
        /// </summary>
        /// <returns></returns>
        //admin only
        [Authorize(Roles = Role.Admin)]
        [HttpGet]
        public IActionResult GetAll()
        {
            var users = _userService.GetAll();
            var model = _mapper.Map<IList<UserModel>>(users);
            return Ok(model);
        }

        /// <summary>
        /// Get user by userId
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            //only allow admins to access other user records
            var currentUserId = int.Parse(User.Identity.Name);
            if (id != currentUserId && !User.IsInRole(Role.Admin))
                return Forbid();
            var user = _userService.GetById(id);

            if (user == null) return NotFound();
            var model = _mapper.Map<UserModel>(user);
            return Ok(model);
        }

        /// <summary>
        /// Upload avatar, edit infomation of user
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromForm] UpdateModel model)
        {
            //only allow admins to access other user records

            var user = _mapper.Map<User>(model);
            user.UserId = id;
            var currentUserId = int.Parse(User.Identity.Name);
            if (id != currentUserId && !User.IsInRole(Role.Admin))
                return Forbid();

            //update avatar if provied
            var image = model.Image;
            //saving image on server
            if (image != null)
            {
                if (image.Length > 0)
                {
                    var supportedTypes = new[] { ".jpg", ".png", ".bmp", ".gif", ".jpeg", "webp" };
                    var imageExt = Path.GetExtension(image.FileName).ToLower();
                    if (!supportedTypes.Contains(imageExt))
                        throw new AppException("File Extension Is InValid - Only Upload jpg/png/bmp/gif/jpeg/webp File");

                    Directory.CreateDirectory("wwwroot/avatars");
                    string fileName = user.UserId + "_avatar" + imageExt;
                    string filePath = Path.Combine("wwwroot/avatars", fileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        image.CopyTo(fileStream);
                    }
                    user.ImagePath = "https://localhost:44363/" + filePath;
                }
            }

            try
            {
                //update user
                _userService.Update(user, model.Password);
                return Ok();
            }
            catch (AppException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Delete user
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        //admin only
        [Authorize(Roles = Role.Admin)]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _userService.Delete(id);
            return Ok();
        }
    }
}
