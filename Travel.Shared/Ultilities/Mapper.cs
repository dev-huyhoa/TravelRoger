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
using Travel.Shared.ViewModels;
using Travel.Shared.ViewModels.Travel.CostTourVM;

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
                //view
                cfg.CreateMap<Tour, TourViewModel>()
                   .ForMember(dto => dto.Id, opt => opt.MapFrom(src => src.IdTour))
                   .ForMember(dto => dto.TourName, opt => opt.MapFrom(src => src.NameTour))
                   .ForMember(dto => dto.Rating, opt => opt.MapFrom(src => src.Rating))
                   .ForMember(dto => dto.TourName, opt => opt.MapFrom(src => src.NameTour))
                   .ForMember(dto => dto.Thumbsnail, opt => opt.MapFrom(src => src.Thumbsnail))
                   .ForMember(dto => dto.FromPlace, opt => opt.MapFrom(src => src.FromPlace))
                   .ForMember(dto => dto.ToPlace, opt => opt.MapFrom(src => src.ToPlace))
                   .ForMember(dto => dto.ApproveStatus, opt => opt.MapFrom(src => src.ApproveStatus))
                   .ForMember(dto => dto.Status, opt => opt.MapFrom(src => src.Status))
                    .ForMember(dto => dto.CreateDate, opt => opt.MapFrom(src => src.CreateDate))
                   .ForMember(dto => dto.IsActive, opt => opt.MapFrom(src => src.IsActive))
                   .ForMember(dto => dto.IsDelete, opt => opt.MapFrom(src => src.IsDelete))
                   .ForMember(dto => dto.ModifyBy, opt => opt.MapFrom(src => src.ModifyBy))
                   .ForMember(dto => dto.ModifyDate, opt => opt.MapFrom(src => src.ModifyDate));




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
                cfg.CreateMap<CreatePaymentViewModel, Payment>()
                           .ForMember(dto => dto.IdPayment, otp => otp.MapFrom(src => src.IdPayment))
                           .ForMember(dto => dto.NamePayment, otp => otp.MapFrom(src => src.NamePayment))
                           .ForMember(dto => dto.Type, otp => otp.MapFrom(src => src.Type));


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

                cfg.CreateMap<CreateCustomerViewModel, Customer>()
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

                cfg.CreateMap<CreateProvinceViewModel, Province>()
                      .ForMember(dto => dto.IdProvince, opt => opt.MapFrom(src => Guid.NewGuid()))
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

                cfg.CreateMap<CreateCarViewModel, Car>()
                       .ForMember(dto => dto.IdCar, opt => opt.MapFrom(src => src.IdCar))
                       .ForMember(dto => dto.LiscensePlate, opt => opt.MapFrom(src => src.LiscensePlate))
                       .ForMember(dto => dto.Status, opt => opt.MapFrom(src => src.Status))
                       .ForMember(dto => dto.NameDriver, opt => opt.MapFrom(src => src.NameDriver))
                       .ForMember(dto => dto.Phone, opt => opt.MapFrom(src => src.Phone));

                cfg.CreateMap<Car, CarViewModel>()
                       .ForMember(dto => dto.IdCar, opt => opt.MapFrom(src => src.IdCar))
                       .ForMember(dto => dto.LiscensePlate, opt => opt.MapFrom(src => src.LiscensePlate))
                       .ForMember(dto => dto.Status, opt => opt.MapFrom(src => src.Status))
                       .ForMember(dto => dto.NameDriver, opt => opt.MapFrom(src => src.NameDriver))
                       .ForMember(dto => dto.Phone, opt => opt.MapFrom(src => src.Phone));

                cfg.CreateMap<CreatePromotionViewModel, Promotion>()
                       .ForMember(dto => dto.IdPromotion, opt => opt.MapFrom(src => src.IdPromotion))
                       .ForMember(dto => dto.Value, opt => opt.MapFrom(src => src.Value))

                       .ForMember(dto => dto.ToDate, opt => opt.MapFrom(src => src.ToDate))
                       .ForMember(dto => dto.FromDate, opt => opt.MapFrom(src => src.FromDate));

                cfg.CreateMap<Promotion, PromotionViewModel>()
                       .ForMember(dto => dto.IdPromotion, opt => opt.MapFrom(src => src.IdPromotion))
                       .ForMember(dto => dto.Value, opt => opt.MapFrom(src => src.Value))

                       .ForMember(dto => dto.ToDate, opt => opt.MapFrom(src => src.ToDate))
                        .ForMember(dto => dto.FromDate, opt => opt.MapFrom(src => src.FromDate));


                cfg.CreateMap<CreateTimeLineViewModel, Timeline>()
                                  .ForMember(dto => dto.Description, otp => otp.MapFrom(src => src.Description))
                                  .ForMember(dto => dto.FromTime, otp => otp.MapFrom(src => src.FromTime))
                                   .ForMember(dto => dto.ToTime, opt => opt.MapFrom(src => src.ToTime))
                                   .ForMember(dto => dto.IdSchedule, opt => opt.MapFrom(src => src.IdSchedule));

                cfg.CreateMap<Timeline, TimeLineViewModel>()
                        .ForMember(dto => dto.IdTimeLine, otp => otp.MapFrom(src => src.IdTimeline))
                        .ForMember(dto => dto.Description, otp => otp.MapFrom(src => src.Description))
                        .ForMember(dto => dto.FromTime, otp => otp.MapFrom(src => src.FromTime))
                        .ForMember(dto => dto.ToTime, opt => opt.MapFrom(src => src.ToTime))
                        .ForMember(dto => dto.ModifyBy, opt => opt.MapFrom(src => src.ModifyBy))
                        .ForMember(dto => dto.ModifyDate, opt => opt.MapFrom(src => src.ModifyDate))
                        .ForMember(dto => dto.IdSchedule, opt => opt.MapFrom(src => src.IdSchedule));



















                // create 

                // create costtour
                cfg.CreateMap<CreateCostViewModel, CostTour>()
               .ForMember(dto => dto.IdCostTour, opt => opt.MapFrom(src => Guid.NewGuid()))
               .ForMember(dto => dto.TourDetailId, opt => opt.MapFrom(src => src.TourDetailId))
               .ForMember(dto => dto.Breakfast, opt => opt.MapFrom(src => src.Breakfast))
               .ForMember(dto => dto.Water, opt => opt.MapFrom(src => src.Water))
               .ForMember(dto => dto.FeeGas, opt => opt.MapFrom(src => src.FeeGas))
               .ForMember(dto => dto.Distance, opt => opt.MapFrom(src => src.Distance))
               .ForMember(dto => dto.SellCost, opt => opt.MapFrom(src => src.SellCost))
               .ForMember(dto => dto.Depreciation, opt => opt.MapFrom(src => src.Depreciation))
               .ForMember(dto => dto.OtherPrice, opt => opt.MapFrom(src => src.OtherPrice))
               .ForMember(dto => dto.Tolls, opt => opt.MapFrom(src => src.Tolls))
               .ForMember(dto => dto.CusExpected, opt => opt.MapFrom(src => src.CusExpected))
               .ForMember(dto => dto.InsuranceFee, opt => opt.MapFrom(src => src.InsuranceFee))
               .ForMember(dto => dto.IsHoliday, opt => opt.MapFrom(src => src.IsHoliday))
                    .ForMember(dto => dto.HotelId, opt => opt.MapFrom(src => src.HotelId))
               .ForMember(dto => dto.RestaurantId, opt => opt.MapFrom(src => src.RestaurantId))
               .ForMember(dto => dto.PlaceId, opt => opt.MapFrom(src => src.PlaceId))
               .ForMember(dto => dto.TotalCostTour, opt => opt.MapFrom(src =>
                       (src.Breakfast + src.Water + src.SellCost + src.OtherPrice + src.InsuranceFee) + (src.Depreciation + src.Tolls + (src.FeeGas * src.Distance))
               ))


               ;
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


        public static PaymentViewModel MapPayment(Payment data)
        {
            return _mapper.Map<Payment, PaymentViewModel>(data);
        }
        public static List<PaymentViewModel> MapPayment(List<Payment> data)
        {
            return _mapper.Map<List<Payment>, List<PaymentViewModel>>(data);
        }
        public static Payment MapCreatePayment(CreatePaymentViewModel data)
        {
            return _mapper.Map<CreatePaymentViewModel, Payment>(data);
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
            return _mapper.Map<CreateUpdateRoleViewModel, Role>(data);
        }
        public static Employee MapCreateEmployee(CreateUpdateEmployeeViewModel data)
        {
            return _mapper.Map<CreateUpdateEmployeeViewModel, Employee>(data);
        }
        public static Tour MapCreateTour(CreateTourViewModel data)
        {
            return _mapper.Map<CreateTourViewModel, Tour>(data);
        }
        public static List<TourViewModel> MapTour(List<Tour> data)
        {
            return _mapper.Map<List<Tour>, List<TourViewModel>>(data);
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
            return _mapper.Map<List<Province>, List<ProvinceViewModel>>(data);
        }
        public static Province MapCreateProvince(CreateProvinceViewModel data)
        {
            return _mapper.Map<CreateProvinceViewModel, Province>(data);
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

        public static Car MapCreateCar(CreateCarViewModel data)
        {
            return _mapper.Map<CreateCarViewModel, Car>(data);
        }
        public static List<CarViewModel> MapCar(List<Car> data)
        {
            return _mapper.Map<List<Car>, List<CarViewModel>>(data);
        }
        public static CarViewModel MapCar(Car data)
        {
            return _mapper.Map<Car, CarViewModel>(data);
        }

        public static Promotion MapCreatePromotion(CreatePromotionViewModel data)
        {
            return _mapper.Map<CreatePromotionViewModel, Promotion>(data);
        }
        public static List<PromotionViewModel> MapPromotion(List<Promotion> data)
        {
            return _mapper.Map<List<Promotion>, List<PromotionViewModel>>(data);
        }
        public static PromotionViewModel MapPromotion(Promotion data)
        {
            return _mapper.Map<Promotion, PromotionViewModel>(data);
        }
        public static List<TimeLineViewModel> MapTimeLine(List<Timeline> data)
        {
            return _mapper.Map<List<Timeline>, List<TimeLineViewModel>>(data);
        }
        public static TimeLineViewModel MapTimeLine(Timeline data)
        {
            return _mapper.Map<Timeline, TimeLineViewModel>(data);
        }
        public static Timeline MapCreateTimeline(CreateTimeLineViewModel data)
        {
            return _mapper.Map<CreateTimeLineViewModel, Timeline>(data);
        }







        // Create 
        public static CostTour MapCreateCost(CreateCostViewModel data)
        {
            return _mapper.Map<CreateCostViewModel, CostTour>(data);
        }
    }
}
