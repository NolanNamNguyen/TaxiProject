using Org.BouncyCastle.Math.EC.Rfc7748;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Taxi.Domain.Entities;
using Taxi.Domain.Helpers;
using Taxi.Domain.Interfaces;
using Taxi.Domain.Models.Users;
using Taxi.Infrastructure.Data;

namespace Taxi.Infrastructure.Services
{
    public class UserService : IUserRepository
    {
        private TaxiContext _context;
        private readonly IEmailService _emailService;
        public UserService(TaxiContext context, IEmailService emailService)
        {
            _context = context;
            _emailService = emailService;
        }
        public User Authenticate(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return null;
            var user = _context.Users.SingleOrDefault(x => x.UserName == username);
            //check if username exists
            if (user == null)
                return null;
            //check if password is correct
            if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                return null;
            return user;


        }

        public User Create(User user, string password, string origin)
        {
            //validation
            if (string.IsNullOrWhiteSpace(password))
                throw new AppException("Password is required");
            if (_context.Users.Any(x => x.UserName == user.UserName))
                throw new AppException("Username \"" + user.UserName + "\" is already taken");
            if (_context.Users.Any(x => x.Email == user.Email))
                throw new AppException("Email \"" + user.Email + "\" is already taken");

            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            if (string.IsNullOrWhiteSpace(user.Role)) user.Role = Role.Customer;

            user.Created = DateTime.Now;
            user.VerificationToken = randomTokenString();

            _context.Users.Add(user);
            _context.SaveChanges();

            sendVerificationEmail(user, origin);

            return user;
        }

        public void VerifyEmail(string token)
        {
            var user = _context.Users.SingleOrDefault(x => x.VerificationToken == token);
            if (user == null) throw new AppException("Verification failed");

            user.IsVerified = true;
            user.VerificationToken = null;

            if (user.Role == Role.Admin)
            {
                Admin admin = new Admin();
                admin.UserId = user.UserId;
                _context.Admins.Add(admin);
            }
            if (user.Role == Role.Driver)
            {
                Driver driver = new Driver();
                driver.UserId = user.UserId;
                _context.Drivers.Add(driver);
            }
            if (user.Role == Role.Customer)
            {
                Customer customer = new Customer();
                customer.UserId = user.UserId;
                _context.Customers.Add(customer);
            }

            _context.Users.Update(user);
            _context.SaveChanges();

            sendRegistrationCompletedEmail(user);
        }
        public void ForgotPassword(ForgotPasswordRequestModel model, string origin)
        {
            var user = _context.Users.SingleOrDefault(x => x.Email == model.Email);

            if (user == null) return;

            //create reset token that expires after 1 hours
            user.ResetToken = randomTokenString();
            user.ResetTokenExpires = DateTime.Now.AddHours(1);

            _context.Users.Update(user);
            _context.SaveChanges();

            sendPasswordResetEmail(user, origin);

        }
        public void ResetPassword(ResetPasswordRequestModel model)
        {
            var user = _context.Users.SingleOrDefault(x =>
                x.ResetToken == model.Token &&
                x.ResetTokenExpires > DateTime.Now);

            if (user == null)
                throw new AppException("Invalid token");

            //update password
            if (!string.IsNullOrWhiteSpace(model.Password))
            {
                byte[] passwordHash, passwordSalt;
                CreatePasswordHash(model.Password, out passwordHash, out passwordSalt);

                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;
            }
            user.Updated = DateTime.Now;
            user.ResetToken = null;
            _context.Users.Update(user);
            _context.SaveChanges();
        }


        public void Delete(int id)
        {
            var user = _context.Users.Find(id);

            if (user.Role == Role.Admin)
            {
                var admin = _context.Admins.FirstOrDefault(x => x.UserId == id);
                _context.Admins.Remove(admin);
            }
            if (user.Role == Role.Driver)
            {
                var driver = _context.Drivers.FirstOrDefault(x => x.UserId == id);
                _context.Drivers.Remove(driver);
            }
            if (user.Role == Role.Customer)
            {
                var customer = _context.Customers.FirstOrDefault(x => x.UserId == id);
                _context.Customers.Remove(customer);
            }

            if (user != null)
            {

                _context.Users.Remove(user);
                _context.SaveChanges();
            }
        }

        public IEnumerable<User> GetAll()
        {
            return _context.Users;
        }

        public User GetById(int id)
        {
            return _context.Users.Find(id);
        }

        public void Update(User userParam, string password = null)
        {
            var user = _context.Users.Find(userParam.UserId);

            if (user == null)
                throw new AppException("User not found");
            if (!string.IsNullOrWhiteSpace(userParam.UserName) && userParam.UserName != user.UserName)
            {
                //update username if it has changed
                if (_context.Users.Any(x => x.UserName == userParam.UserName))
                    throw new AppException("Username " + userParam.UserName + " is already taken");
                user.UserName = userParam.UserName;
            }
            //update password if provied
            if (!string.IsNullOrWhiteSpace(password))
            {
                byte[] passwordHash, passwordSalt;
                CreatePasswordHash(password, out passwordHash, out passwordSalt);

                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;
            }
            //update avatar
            if (!string.IsNullOrWhiteSpace(userParam.ImagePath))
                user.ImagePath = userParam.ImagePath;
            //update user info if provided
            if (!string.IsNullOrWhiteSpace(userParam.Name))
                user.Name = userParam.Name;
            if (!string.IsNullOrWhiteSpace(userParam.Phone))
                user.Phone = userParam.Phone;
            if (!string.IsNullOrWhiteSpace(userParam.Address))
                user.Address = userParam.Address;

            user.Updated = DateTime.Now;

            _context.Users.Update(user);
            _context.SaveChanges();
        }
        //create password hash + salt
        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
        private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
            if (storedHash.Length != 64) throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");
            if (storedSalt.Length != 128) throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordHash");

            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }
            return true;
        }

        private string randomTokenString()
        {
            using var rngCryptoServiceProvider = new RNGCryptoServiceProvider();
            var randomBytes = new byte[40];
            rngCryptoServiceProvider.GetBytes(randomBytes);
            // convert random bytes to hex string
            return BitConverter.ToString(randomBytes).Replace("-", "");
        }
        private void sendVerificationEmail(User user, string origin)
        {
            string message;
            if (!string.IsNullOrEmpty(origin))
            {
                var verifyUrl = $"{origin}/users/verify-email?token={user.VerificationToken}";
                message = $@"<p>Please click the below link to verify your email address:</p>
                             <p><a href=""{verifyUrl}"">{verifyUrl}</a></p>";
            }
            else
            {
                message = $@"<p>Please use the below token to verify your email address with the <code>/users/verify-email</code> api route:</p>
                             <p><code>{user.VerificationToken}</code></p>";
            }

            _emailService.Send(
                to: user.Email,
                subject: "Umbrella Taxi - Verify Email",
                html: $@"<h4>Verify Email</h4>
                         <p>Thanks for registering!</p>
                         {message}"
            );
        }

        private void sendPasswordResetEmail(User user, string origin)
        {
            string message;
            if (!string.IsNullOrEmpty(origin))
            {
                var resetUrl = $"{origin}/users/reset-password?token={user.ResetToken}";
                message = $@"<p>Please click the below link to reset your password, the link will be valid for 1 hours:</p>
                             <p><a href=""{resetUrl}"">{resetUrl}</a></p>";
            }
            else
            {
                message = $@"<p>Please use the below token to reset your password with the <code>/users/reset-password</code> api route:</p>
                             <p><code>{user.ResetToken}</code></p>";
            }

            _emailService.Send(
                to: user.Email,
                subject: "Umbrella Taxi - Reset Password",
                html: $@"<h4>Reset Password Email</h4>
                         {message}"
                );
        }

        private void sendRegistrationCompletedEmail(User user)
        {
            string message;
            message = $@"<p>Dear {user.Name},</p>
                      <p>Thank you for joining Umbrella Taxi. We are excited to have you on board and look forward to helping you save time.</p>
                        <p>Your UserName: {user.UserName}</p>";
            _emailService.Send(
                to: user.Email,
                subject: "Welcome! Your Account is Ready",
                html: $@"{message}");
        }

    }
}