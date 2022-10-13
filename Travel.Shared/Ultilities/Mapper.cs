using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Context.Models;
using Travel.Shared.ViewModels.Travel;
using Travel.Shared.ViewModels.Travel.TourVM;
using Travel.Shared.Ultilities;
using Travel.Shared.ViewModels.Travel.CustomerVM;

namespace Travel.Shared.Ultilities
{
    public static class Mapper
    {
        private static IMapper _mapper;
        public static void RegisterMappings()
        {
            var mapperConfiguration = new MapperConfiguration(cfg =>
            {
                // create schedule
                cfg.CreateMap<CreateScheduleViewModel, Schedule>()
               .ForMember(dto => dto.IdSchedule, opt => opt.MapFrom(src => src.IdSchedule))
               .ForMember(dto => dto.DepartureDate, opt => opt.MapFrom(src => src.DepartureDate))
               .ForMember(dto => dto.BeginDate, opt => opt.MapFrom(src => src.EndDate))
               .ForMember(dto => dto.TimePromotion, opt => opt.MapFrom(src => src.TimePromotion))
               .ForMember(dto => dto.FinalPrice, opt => opt.MapFrom(src => src.FinalPrice))
               .ForMember(dto => dto.QuantityAdult, opt => opt.MapFrom(src => 0))
               .ForMember(dto => dto.QuantityBaby, opt => opt.MapFrom(src => 0))
               .ForMember(dto => dto.QuantityChild, opt => opt.MapFrom(src => 0))
               .ForMember(dto => dto.MinCapacity, opt => opt.MapFrom(src => 30))
               .ForMember(dto => dto.MaxCapacity, opt => opt.MapFrom(src => 45))
               .ForMember(dto => dto.Status, opt => opt.MapFrom(src => Enums.StatusSchedule.Free))
               .ForMember(dto => dto.TourId, opt => opt.MapFrom(src => src.TourId))
               .ForMember(dto => dto.CarId, opt => opt.MapFrom(src => src.CarId))
               .ForMember(dto => dto.EmployeeId, opt => opt.MapFrom(src => src.EmployeeId))
               .ForMember(dto => dto.PromotionId, opt => opt.MapFrom(src => src.PromotionId))
               ;
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

                cfg.CreateMap<Customer, CustomerViewModel>()
                         .ForMember(dto => dto.IdCustomer, opt => opt.MapFrom(src => src.IdCustomer))
                         .ForMember(dto => dto.NameCustomer, otp => otp.MapFrom(src => src.NameCustomer))
                         .ForMember(dto => dto.Phone, otp => otp.MapFrom(src => src.Phone))
                         .ForMember(dto => dto.Email, otp => otp.MapFrom(src => src.Email))
                         .ForMember(dto => dto.Address, otp => otp.MapFrom(src => src.Address))
                         .ForMember(dto => dto.Password, otp => otp.MapFrom(src => src.Password))
                         .ForMember(dto => dto.Gender, otp => otp.MapFrom(src => src.Gender))
                         .ForMember(dto => dto.Birthday, otp => otp.MapFrom(src => src.Birthday))
                        .ForMember(dto => dto.CreateDate, opt => opt.MapFrom(src => src.CreateDate))
                         .ForMember(dto => dto.Point, otp => otp.MapFrom(src => src.Point));

                cfg.CreateMap<CreateCustomerViewModel, Customer >()
                        //.ForMember(dto => dto., opt => opt.MapFrom(src => src.IdTour))
                        .ForMember(dto => dto.NameCustomer, otp => otp.MapFrom(src => src.NameCustomer))
                        .ForMember(dto => dto.Phone, otp => otp.MapFrom(src => src.Phone))
                        .ForMember(dto => dto.Email, otp => otp.MapFrom(src => src.Email))
                        .ForMember(dto => dto.Gender, otp => otp.MapFrom(src => src.Gender))
                        .ForMember(dto => dto.Address, otp => otp.MapFrom(src => src.Address))
                        .ForMember(dto => dto.CreateDate, opt => opt.MapFrom(src =>
                Ultility.ConvertDatetimeToUnixTimeStampMiliSecond(DateTime.Now)))
                        .ForMember(dto => dto.Password, otp => otp.MapFrom(src => src.Password));


                cfg.CreateMap<Province, ProvinceViewModel>()
                       .ForMember(dto => dto.IdProvince, opt => opt.MapFrom(src => src.IdProvince))
                       .ForMember(dto => dto.NameProvince, opt => opt.MapFrom(src => src.NameProvince));

                cfg.CreateMap<CreateUpdateProvinceViewModel, Province>()
                      .ForMember(dto => dto.IdProvince, opt => opt.MapFrom(src => src.IdProvince != Guid.Empty ? src.IdProvince : Guid.NewGuid()))
                      .ForMember(dto => dto.NameProvince, opt => opt.MapFrom(src => src.NameProvince));

                cfg.CreateMap<UpdateProvinceViewModel, Province>()
                    .ForMember(dto => dto.IdProvince, opt => opt.MapFrom(src => src.IdProvince))
                    .ForMember(dto => dto.NameProvince, opt => opt.MapFrom(src => src.NameProvince));

                cfg.CreateMap<District, DistrictViewModel>()
                      .ForMember(dto => dto.IdDistrict, opt => opt.MapFrom(src => src.IdDistrict))
                      .ForMember(dto => dto.NameDistrict, opt => opt.MapFrom(src => src.NameDistrict))
                      .ForMember(dto => dto.IdProvince, opt => opt.MapFrom(src => src.ProvinceId))
                      .ForMember(dto => dto.ProvinceName, opt => opt.MapFrom(src => src.Province.NameProvince));

                cfg.CreateMap<Ward, WardViewModel>()
                     .ForMember(dto => dto.IdWard, opt => opt.MapFrom(src => src.IdWard))
                     .ForMember(dto => dto.NameWard, opt => opt.MapFrom(src => src.NameWard))
                     .ForMember(dto => dto.IdDistrict, opt => opt.MapFrom(src => src.DistrictId))
                     .ForMember(dto => dto.DistrictName, opt => opt.MapFrom(src => src.District.NameDistrict));
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
        public static Schedule MapCreateSchedule(CreateScheduleViewModel data)
        {
            return _mapper.Map<CreateScheduleViewModel, Schedule>(data);
        }
        public static TourDetail MapCreateTourDetails(CreateTourViewModel data)
        {
            return _mapper.Map<CreateTourViewModel, TourDetail>(data);
        }
        public static Customer MapCreateCustomer(CreateCustomerViewModel data)
        {
            return _mapper.Map<CreateCustomerViewModel, Customer>(data);
        }
        public static List<CustomerViewModel> MapCustomer(List<Customer> data)
        {
            return _mapper.Map<List<Customer>, List<CustomerViewModel>>(data);
        }
        public static Customer MapCustomer(CustomerViewModel data)
        {
            return _mapper.Map<CustomerViewModel, Customer>(data);
        }

        public static List<ProvinceViewModel> MapProvince(List<Province> data)
        {
            return _mapper.Map< List<Province>, List<ProvinceViewModel>>(data);
        }
        public static Province MapCreateProvince(CreateUpdateProvinceViewModel data)
        {
            return _mapper.Map<CreateUpdateProvinceViewModel, Province>(data);
        }
        public static Province MapUpdateProvince(UpdateProvinceViewModel data)
        {
            return _mapper.Map<UpdateProvinceViewModel, Province>(data);
        }
        public static List<DistrictViewModel> MapDistrict(List<District> data)
        {
            return _mapper.Map<List<District>, List<DistrictViewModel>>(data);
        }

        public static List<WardViewModel> MapWard(List<Ward> data)
        {
            return _mapper.Map<List<Ward>, List<WardViewModel>>(data);
        }
    }
}
