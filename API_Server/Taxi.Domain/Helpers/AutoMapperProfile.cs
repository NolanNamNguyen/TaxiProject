using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Taxi.Domain.Entities;
using Taxi.API;
using Taxi.API.Models;
using Taxi.Domain.Models.Drivers.Vehicles;
using Taxi.Domain.Models.Drivers.Schedule;
using Taxi.Domain.Models.Drivers;
using Taxi.Domain.Models.Customers;
using Taxi.Domain.Models.Customers.orders;
using Taxi.Domain.Models.Customers.Reports;
using Taxi.Domain.Models.Forum;
using Taxi.Domain.Models.Notify;

namespace Taxi.Domain.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserModel>();
            CreateMap<RegisterModel, User>();
            CreateMap<UpdateModel, User>();

            //driver
            CreateMap<Vehicle, VehicleModel>()
                .ForMember(dest => dest.DriverName, opt => opt.MapFrom(src => src.Driver.User.Name))
                .ForMember(dest => dest.DriverPhone, opt => opt.MapFrom(src => src.Driver.User.Phone));

            CreateMap<VehicleRegisterModel, Vehicle>();
            CreateMap<VehicleUpdateModel, Vehicle>();

            CreateMap<Schedule, ScheduleModel>()
                .ForMember(dest => dest.DriverName, opt => opt.MapFrom(src => src.Driver.User.Name))
                .ForMember(dest => dest.DriverPhone, opt => opt.MapFrom(src => src.Driver.User.Phone))
                .ForMember(dest => dest.VehicleName, opt => opt.MapFrom(src => src.Driver.Vehicle.VehicleName))
                .ForMember(dest => dest.Seater, opt => opt.MapFrom(src => src.Driver.Vehicle.Seater))
                .ForMember(dest => dest.DriverImage, opt => opt.MapFrom(src => src.Driver.User.ImagePath));
            CreateMap<RegisterScheduleModel, Schedule>();
            //driver profile
            CreateMap<Driver, DriverModel>()
                .ForMember(dst => dst.UserName, opt => opt.MapFrom(src => src.User.UserName))
                .ForMember(dst => dst.Name, opt => opt.MapFrom(src => src.User.Name))
                .ForMember(dst => dst.Phone, opt => opt.MapFrom(src => src.User.Phone))
                .ForMember(dst => dst.Email, opt => opt.MapFrom(src => src.User.Email))
                .ForMember(dst => dst.Address, opt => opt.MapFrom(src => src.User.Address))
                .ForMember(dst => dst.License, opt => opt.MapFrom(src => src.Vehicle.License))
                .ForMember(dst => dst.VehicleName, opt => opt.MapFrom(src => src.Vehicle.VehicleName))
                .ForMember(dst => dst.ImagePath, opt => opt.MapFrom(src => src.User.ImagePath));
            CreateMap<Order, DriverReviewModel>()
                .ForMember(dst => dst.CustomerName, opt => opt.MapFrom(src => src.Customer.User.Name))
                .ForMember(dst => dst.CustomerImage, opt => opt.MapFrom(src => src.Customer.User.ImagePath));

            //customer profile
            CreateMap<Customer, CustomerModel>()
                .ForMember(dst => dst.UserName, opt => opt.MapFrom(src => src.User.UserName))
                .ForMember(dst => dst.Name, opt => opt.MapFrom(src => src.User.Name))
                .ForMember(dst => dst.Phone, opt => opt.MapFrom(src => src.User.Phone))
                .ForMember(dst => dst.Email, opt => opt.MapFrom(src => src.User.Email))
                .ForMember(dst => dst.Address, opt => opt.MapFrom(src => src.User.Address))
                .ForMember(dst => dst.ImagePath, opt => opt.MapFrom(src => src.User.ImagePath));

            //orders
            CreateMap<Order, OrderModel>()
                .ForMember(dst => dst.DriverName, opt => opt.MapFrom(src => src.Driver.User.Name))
                .ForMember(dst => dst.DriverImg, opt => opt.MapFrom(src => src.Driver.User.ImagePath))
                .ForMember(dst => dst.CustomerName, opt => opt.MapFrom(src => src.Customer.User.Name))
                .ForMember(dst => dst.CustomerImg, opt => opt.MapFrom(src => src.Customer.User.ImagePath))
                .ForMember(dst => dst.CustomerPhone, opt => opt.MapFrom(src => src.Customer.User.Phone));
            CreateMap<AddOrderModel, Order>();
            CreateMap<RateModel, Order>();

            //report
            CreateMap<Report, ReportModel>()
                .ForMember(dst => dst.DriverName, opt => opt.MapFrom(src => src.Driver.User.Name))
                .ForMember(dst => dst.CustomerName, opt => opt.MapFrom(src => src.Customer.User.Name));
            CreateMap<CreateReportModel, Report>();

            //forum
            CreateMap<Review, ReviewModel>()
                .ForMember(dst => dst.Likes, opt => opt.MapFrom(src => src.Likes.Count))
                .ForMember(dst => dst.Cmts, opt => opt.MapFrom(src => src.Comments.Count));
            CreateMap<AddReviewModel, Review>();

            CreateMap<AddLikeModel, Like>();

            CreateMap<AddCommentModel, Comment>();
            CreateMap<Comment, CommentModel>()
                .ForMember(dst => dst.UserName, opt => opt.MapFrom(src => src.User.UserName));

            //notify
            CreateMap<Notify, NotifyModel>();
        }
    }
}
