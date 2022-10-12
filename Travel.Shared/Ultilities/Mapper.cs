﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Context.Models;
using Travel.Shared.ViewModels.Travel;
using Travel.Shared.ViewModels.Travel.TourVM;
using Travel.Shared.Ultilities;
namespace Travel.Shared.Ultilities
{
    public static class Mapper
    {
        private static IMapper _mapper;
        public static void RegisterMappings()
        {
            var mapperConfiguration = new MapperConfiguration(cfg =>
            {

                //create
                cfg.CreateMap<CreateTourViewModel, Tour>()
                .ForMember(dto => dto.IdTour, opt => opt.MapFrom(src => src.IdTour))
                .ForMember(dto => dto.NameTour, opt => opt.MapFrom(src => src.NameTour))
                .ForMember(dto => dto.Thumbsnail, opt => opt.MapFrom(src => src.Thumbsnail))
                .ForMember(dto => dto.FromPlace, opt => opt.MapFrom(src => src.FromPlace))
                .ForMember(dto => dto.ToPlace, opt => opt.MapFrom(src => src.ToPlace))
                .ForMember(dto => dto.Rating, opt => opt.MapFrom(src => 10))
                .ForMember(dto => dto.ApproveStatus, opt => opt.MapFrom(src => Enums.ApproveStatus.Waiting))
                .ForMember(dto => dto.Status, opt => opt.MapFrom(src =>
                 Enums.TourStatus.Normal))
                .ForMember(dto => dto.CreateDate, opt => opt.MapFrom(src =>
                Ultility.ConvertDatetimeToUnixTimeStampMiliSecond(DateTime.Now)))
                .ForMember(dto => dto.IsDelete, opt => opt.MapFrom(src => false))
                .ForMember(dto => dto.IsActive, opt => opt.MapFrom(src => false))
                ;

                //create
                cfg.CreateMap<CreateTourViewModel, TourDetail>()
                .ForMember(dto => dto.IdTourDetail, opt => opt.MapFrom(src => $"{Ultility.GenerateId(src.NameTour)}-Details"))
                .ForMember(dto => dto.TourId, opt => opt.MapFrom(src => src.IdTour))
                .ForMember(dto => dto.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dto => dto.DisplayPrice, opt => opt.MapFrom(src => 0))
                .ForMember(dto => dto.DisplayPromotionPrice, opt => opt.MapFrom(src => 0))
                .ForMember(dto => dto.FinalPrice, opt => opt.MapFrom(src => 0))
                .ForMember(dto => dto.IsPromotion, opt => opt.MapFrom(src => false))
                .ForMember(dto => dto.PriceAdult, opt => opt.MapFrom(src => 0))
                .ForMember(dto => dto.PriceAdultPromotion, opt => opt.MapFrom(src => 0))
                .ForMember(dto => dto.PriceBaby, opt => opt.MapFrom(src => 0))
                .ForMember(dto => dto.PriceBabyPromotion, opt => opt.MapFrom(src => 0))
                .ForMember(dto => dto.PriceChild, opt => opt.MapFrom(src => 0))
                .ForMember(dto => dto.PriceChildPromotion, opt => opt.MapFrom(src => 0))
                .ForMember(dto => dto.Profit, opt => opt.MapFrom(src => 0))
                .ForMember(dto => dto.TotalCostTour, opt => opt.MapFrom(src => 0))
                .ForMember(dto => dto.Vat, opt => opt.MapFrom(src => src.VAT));


                // Create
                cfg.CreateMap<CreateUpdateEmployeeViewModel, Employee>()
                          .ForMember(dto => dto.IdEmployee, opt => opt.MapFrom(src => src.IdEmployee))
                          .ForMember(dto => dto.NameEmployee, opt => opt.MapFrom(src => src.NameEmployee))
                          .ForMember(dto => dto.Email, opt => opt.MapFrom(src => src.Email))
                          .ForMember(dto => dto.Birthday, opt => opt.MapFrom(src => src.Birthday))
                          .ForMember(dto => dto.Image, opt => opt.MapFrom(src => src.Image))
                          .ForMember(dto => dto.Phone, opt => opt.MapFrom(src => src.Phone))
                          .ForMember(dto => dto.RoleId, opt => opt.MapFrom(src => src.RoleId));

                cfg.CreateMap<CreateUpdateRoleViewModel, Role>()
                       .ForMember(dto => dto.IdRole, opt => opt.MapFrom(src => src.IdRole))
                       .ForMember(dto => dto.NameRole, opt => opt.MapFrom(src => src.RoleName))
                       .ForMember(dto => dto.Description, opt => opt.MapFrom(src => string.IsNullOrEmpty(src.Description) ? "" : src.Description));

                cfg.CreateMap<Role, RoleViewModel>()
                          .ForMember(dto => dto.IdRole, opt => opt.MapFrom(src => src.IdRole))
                          .ForMember(dto => dto.NameRole, opt => opt.MapFrom(src => src.NameRole))
                          .ForMember(dto => dto.Description, opt => opt.MapFrom(src => string.IsNullOrEmpty(src.Description) ? "" : src.Description));
                cfg.CreateMap<Employee, EmployeeViewModel>()
                         .ForMember(dto => dto.IdEmployee, opt => opt.MapFrom(src => src.IdEmployee))
                         .ForMember(dto => dto.NameEmployee, opt => opt.MapFrom(src => src.NameEmployee))
                         .ForMember(dto => dto.Birthday, opt => opt.MapFrom(src => src.Birthday))
                         .ForMember(dto => dto.CreateDate, opt => opt.MapFrom(src => src.CreateDate))
                         .ForMember(dto => dto.Email, opt => opt.MapFrom(src => src.Email))
                         .ForMember(dto => dto.Image, opt => opt.MapFrom(src => src.Image))
                         .ForMember(dto => dto.IsActive, opt => opt.MapFrom(src => src.IsActive))
                         .ForMember(dto => dto.IsDelete, opt => opt.MapFrom(src => src.IsDelete))
                         .ForMember(dto => dto.ModifyBy, opt => opt.MapFrom(src => src.ModifyBy))
                         .ForMember(dto => dto.ModifyDate, opt => opt.MapFrom(src => src.ModifyDate))
                         .ForMember(dto => dto.Phone, opt => opt.MapFrom(src => src.Phone))
                         .ForMember(dto => dto.RoleDescription, opt => opt.MapFrom(src => string.IsNullOrEmpty(src.Role.Description) ? "" : src.Role.Description))
                         .ForMember(dto => dto.IdRole, opt => opt.MapFrom(src => src.RoleId))
                         .ForMember(dto => dto.RoleName, opt => opt.MapFrom(src => src.Role.NameRole));
                
            });
            _mapper = mapperConfiguration.CreateMapper();
        }
        public static RoleViewModel MapRole(Role data)
        {
            return _mapper.Map<Role, RoleViewModel>(data);
        }
        public static List<RoleViewModel> MapRole(List<Role> data)
        {
            return _mapper.Map<List<Role>, List<RoleViewModel>>(data);
        }
        public static EmployeeViewModel MapEmployee(Employee data)
        {
            return _mapper.Map<Employee, EmployeeViewModel>(data);
        }
        public static List<EmployeeViewModel> MapEmployee(List<Employee> data)
        {
            return _mapper.Map<List<Employee>, List<EmployeeViewModel>>(data);
        }
        public static Role MapCreateRole(CreateUpdateRoleViewModel data)
        {
            return _mapper.Map<CreateUpdateRoleViewModel,Role>(data);
        }
        public static Employee MapCreateEmployee(CreateUpdateEmployeeViewModel data)
        {
            return _mapper.Map<CreateUpdateEmployeeViewModel, Employee>(data);
        }
        public static Tour MapCreateTour(CreateTourViewModel data)
        {
            return _mapper.Map<CreateTourViewModel, Tour>(data);
        }
        public static TourDetail MapCreateTourDetails(CreateTourViewModel data)
        {
            return _mapper.Map<CreateTourViewModel, TourDetail>(data);
        }
    }
}
