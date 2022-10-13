using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Data.Interfaces;
using Travel.Shared.ViewModels;
using Travel.Shared.ViewModels.Travel.CustomerVM;
using Travel.Shared.Ultilities;
using PrUtility;
using Travel.Context.Models.Travel;
using Travel.Context.Models;

namespace Travel.Data.Repositories
{
    public class CustomerRes : ICustomer
    {
        private readonly TravelContext _db;
        private Notification message;
        private Response res;

        public CustomerRes(TravelContext db)
        {
            _db = db;
            message = new Notification();
            res = new Response();
        }
        public CreateCustomerViewModel CheckBeforeSave(JObject frmData, ref Notification _message)
        {
            CreateCustomerViewModel customer = new CreateCustomerViewModel();
            try
            {
                int error = 0;
                var name = PrCommon.GetString("nameCustomer", frmData);
                if (!String.IsNullOrEmpty(name))
                {
                    customer.NameCustomer = name.Trim();
                }
                else
                {
                    error++;
                }

                var point = PrCommon.GetString("point", frmData);
                if (!String.IsNullOrEmpty(point))
                {
                    customer.Phone = point.Trim();
                }
                else
                {
                    error++;
                }

                var phone = PrCommon.GetString("phone", frmData);
                if (!String.IsNullOrEmpty(phone))
                {
                    customer.Phone = phone.Trim();
                }
                else
                {
                    error++;
                }

                var email = PrCommon.GetString("email", frmData);
                if (!String.IsNullOrEmpty(email))
                {
                    customer.Email = email.Trim();
                }
                else
                {
                    error++;
                }


                var address = PrCommon.GetString("address", frmData);
                if (!String.IsNullOrEmpty(address))
                {
                    customer.Address = address.Trim();
                }
                else
                {
                    error++;
                }

                var birthday = PrCommon.GetString("birthday", frmData);
                if (!String.IsNullOrEmpty(birthday))
                {
                    customer.Birthday = long.Parse(birthday);
                }
                else
                {
                    error++;
                }

                var password = PrCommon.GetString("password", frmData);
                if (!String.IsNullOrEmpty(password))
                {
                    customer.Password = password.Trim();
                }
                else
                {
                    error++;
                }

                if (error > 0)
                {
                    message.DateTime = DateTime.Now;
                    message.Messenge = "Dữ liệu không hợp lệ !";
                    message.Type = "Error";

                    _message = message;
                }

                var gender = PrCommon.GetString("gender", frmData);
                if (!String.IsNullOrEmpty(gender))
                {
                    customer.Gender = Boolean.Parse(gender);
                }
                else
                {
                    error++;
                }

                if (error > 0)
                {
                    message.DateTime = DateTime.Now;
                    message.Messenge = "Dữ liệu không hợp lệ !";
                    message.Type = "Error";

                    _message = message;
                }
                return customer;

            }
            catch (Exception e)
            {

                message.DateTime = DateTime.Now;
                message.Description = e.Message;
                message.Messenge = "Có lỗi xảy ra !";
                message.Type = "Error";

                _message = message;
                 return customer;
            }
        }

        public Response Create(CreateCustomerViewModel input)
        {
            try
            {
                Customer customer = Mapper.MapCreateCustomer(input);
                customer.IdCustomer = Guid.NewGuid();
                customer.Point = 0;
                customer.IsDelete = false;
                _db.Customers.Add(customer);
                _db.SaveChanges();

                res.Notification.DateTime = DateTime.Now;
                res.Notification.Messenge = "Thêm thành công !";
                res.Notification.Type = "Success";
                return res;
            }
            catch (Exception e)
            {
                res.Notification.DateTime = DateTime.Now;
                res.Notification.Description = e.Message;
                res.Notification.Messenge = "Có lỗi xảy ra !";
                res.Notification.Type = "Error";
                return res;

            }
        }

        public Response Gets()
        {
            try
            {          
                var listCus = (from x in _db.Customers where x.IsDelete == false select x).ToList();
                var result = Mapper.MapCustomer(listCus);

                if (listCus.Count() > 0)
                {
                    res.Content = result;
                }
                else
                {
                    res.Notification.DateTime = DateTime.Now;
                    res.Notification.Messenge = "Không có dữ liệu trả về !";
                    res.Notification.Type = "Warning";
                }
                return res;
            }
            catch (Exception e)
            {
                res.Notification.DateTime = DateTime.Now;
                res.Notification.Description = e.Message;
                res.Notification.Messenge = "Có lỗi xảy ra !";
                res.Notification.Type = "Error";
                return res;
            }
        }
    }
}
