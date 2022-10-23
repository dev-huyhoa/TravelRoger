using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using PrUtility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Travel.Context.Models;
using Travel.Context.Models.Travel;
using Travel.Data.Interfaces;
using Travel.Shared.Ultilities;
using Travel.Shared.ViewModels;
using Travel.Shared.ViewModels.Travel;
using static Travel.Shared.Ultilities.Enums;

namespace Travel.Data.Repositories
{
    public class EmployeeRes : IEmployee
    {
        private readonly TravelContext _db;
        private Notification message;
        private Response res;
        public EmployeeRes(TravelContext db)
        {
            _db = db;
            message = new Notification();
            res = new Response();
        }
        // validate vd create
        public string CheckBeforeSave(IFormCollection frmdata, IFormFile file, ref Notification _message, bool isUpdate) // hàm đăng nhập  sử cho create update delete
        {
            try
            {
                JObject frmData = JObject.Parse(frmdata["data"]);
                if (frmData != null)
                {
                    var idEmployee = PrCommon.GetString("idEmployee", frmData);
                    if (String.IsNullOrEmpty(idEmployee))
                    {
                        idEmployee = Guid.NewGuid().ToString();
                    }

                    var nameEmployee = PrCommon.GetString("nameEmployee", frmData);
                    if (String.IsNullOrEmpty(nameEmployee))
                    {
                    }

                    var email = PrCommon.GetString("email", frmData);
                    if (!String.IsNullOrEmpty(email) && isUpdate == false)
                    {
                       var check = CheckEmailEmployee(email);
                        if (check.Notification.Type == "Error")
                        {
                            _message = check.Notification;
                            return string.Empty;
                        }
                    }


                    var phone = PrCommon.GetString("phone", frmData);
                    if (!String.IsNullOrEmpty(phone) && isUpdate == false)
                    {
                        var check = CheckPhoneEmployee(phone);
                        if (check.Notification.Type == "Error")
                        {
                            _message = check.Notification;
                            return string.Empty;
                        }
                    }

                    var roleId = PrCommon.GetString("roleId", frmData);
                    if (String.IsNullOrEmpty(roleId))
                    {
                    }

                   

                    var birthday = PrCommon.GetString("birthday", frmData);
                    if (String.IsNullOrEmpty(birthday))
                    {
                    }


                    var address = PrCommon.GetString("address", frmData);
                    if (String.IsNullOrEmpty(address))
                    {
                    }

                    var gender = PrCommon.GetString("gender", frmData);
                    if (String.IsNullOrEmpty(birthday))
                    {
                    }


                    var image = PrCommon.GetString("image", frmData);
                    if (String.IsNullOrEmpty(image))
                    {
                    }

                    var modifyBy = PrCommon.GetString("modifyBy", frmData);
                    if (String.IsNullOrEmpty(modifyBy))
                    {
                    }

                    var isActive = PrCommon.GetString("isActive", frmData);
                    if (String.IsNullOrEmpty(isActive))
                    {
                    }

                    if (file != null)
                    {
                        image = Ultility.WriteFile(file, "Employee", Guid.Parse(idEmployee), ref _message).FilePath;
                        if (_message != null)
                        {
                            message = _message;
                        }
                    }

                    if (isUpdate)
                    {
                        UpdateEmployeeViewModel objUpdate = new UpdateEmployeeViewModel();
                        objUpdate.IdEmployee = Guid.Parse(idEmployee);
                        objUpdate.NameEmployee = nameEmployee;
                        objUpdate.Phone = phone;
                        objUpdate.Email = email;
                        objUpdate.Address = address;
                        try
                        {
                            var date = DateTime.Parse(birthday);
                            objUpdate.Birthday = Ultility.ConvertDatetimeToUnixTimeStampMiliSecond(date);
                        }
                        catch (Exception)
                        {
                            objUpdate.Birthday = long.Parse(birthday);
                        }
                        objUpdate.RoleId = (TitleRole)int.Parse(roleId);
                        if (!String.IsNullOrEmpty(image))
                        {
                            objUpdate.Image = image;

                        }
                        objUpdate.IsActive = bool.Parse(isActive);
                        objUpdate.ModifyBy = modifyBy;
                        return JsonSerializer.Serialize(objUpdate);
                    }

                    CreateEmployeeViewModel objCreate = new CreateEmployeeViewModel();
                    objCreate.IdEmployee = Guid.Parse(idEmployee);
                    objCreate.NameEmployee = nameEmployee;
                    objCreate.Phone = phone;
                    objCreate.Email = email;
                    objCreate.Address = address;
                    try
                    {
                        var date = DateTime.Parse(birthday);
                        objCreate.Birthday = Ultility.ConvertDatetimeToUnixTimeStampMiliSecond(date);
                    }
                    catch (Exception)
                    {
                        objCreate.Birthday = long.Parse(birthday);
                    }
                    objCreate.RoleId = (TitleRole)int.Parse(roleId);
                    objCreate.Image = image;
                    objCreate.ModifyBy = modifyBy;
                    return JsonSerializer.Serialize(objCreate);

                   
                }
                return string.Empty;
            }
            catch (Exception e)
            {
                message.DateTime = DateTime.Now;
                message.Description = e.Message;
                message.Messenge = "Có lỗi xảy ra !";
                message.Type = "Error";

                _message = message;
                return string.Empty;
            }
        }
        public Response GetsEmployee(bool isDelete)
        {
            try
            {
                //var isDelete = false;
                //var check = PrCommon.GetString("isDelete", frmData);
                //if (!String.IsNullOrEmpty(check))
                //{
                //    isDelete = Boolean.Parse(check);
                //}
                #region đo tốc độ EF và linq
                //var stopWatch4 = Stopwatch.StartNew();
                //var result5 = _db.Employees.ToList();
                //var b4 = stopWatch4.Elapsed;

                //var stopWatch5 = Stopwatch.StartNew();
                //var result6 = (from x in _db.Employees select x).ToList();
                //var b5 = stopWatch5.Elapsed;
                #endregion

                var listEmp = (from x in _db.Employees 
                               where x.IsDelete == isDelete 
                               orderby x.RoleId
                               select new Employee 
                { 
                CreateDate = x.CreateDate,
                AccessToken = x.AccessToken,
                Address = x.Address,
                Birthday = x.Birthday,
                Email = x.Email,
                IsDelete = x.IsDelete,
                Gender = x.Gender,
                ModifyDate = x.ModifyDate,
                IdEmployee = x.IdEmployee,
                Image = x.Image,
                IsActive = x.IsActive,
                ModifyBy = x.ModifyBy,
                NameEmployee = x.NameEmployee,
                Password = x.Password,
                Phone = x.Phone,
                Role = (from r in _db.Roles where r.IdRole == x.RoleId select r).First(),
                RoleId = x.RoleId,
                }).ToList();

                var result = Mapper.MapEmployee(listEmp);

                if (listEmp.Count() > 0)
                {
                    res.Content = result;
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

        public Response CreateEmployee(CreateEmployeeViewModel input)
        {
            try
            {
                Employee employee = Mapper.MapCreateEmployee(input);
                employee.IsActive = true;
                employee.Password = "3244185981728979115075721453575112";

                _db.Employees.Add(employee);
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

        public Response UpdateEmployee(UpdateEmployeeViewModel input)
        {
            try
            {
                Employee employee = Mapper.MapCreateEmployee(input);
                _db.Employees.Update(employee);
                _db.SaveChanges();
                res.Notification.DateTime = DateTime.Now;
                res.Notification.Messenge = "Sửa thành công !";
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

        public Response SearchEmployee(JObject frmData)
        {
            try
            {
                Keywords keywords = new Keywords();

                var isDelete = PrCommon.GetString("isDelete", frmData);
                if (!String.IsNullOrEmpty(isDelete))
                {
                    keywords.IsDelete = Boolean.Parse(isDelete);
                }

                var kwId = PrCommon.GetString("idEmployee", frmData);
                if (!String.IsNullOrEmpty(kwId))
                {
                    keywords.KwId = kwId.Trim().ToLower();
                }
                else
                {
                    keywords.KwId = "";

                }

                var kwName = PrCommon.GetString("nameEmployee", frmData).Trim();
                if (!String.IsNullOrEmpty(kwName))
                {
                    keywords.KwName = kwName.Trim().ToLower();
                }
                else
                {
                    keywords.KwName = "";

                }

                var kwPhone = PrCommon.GetString("phone", frmData).Trim();
                if (!String.IsNullOrEmpty(kwPhone))
                {
                    keywords.KwPhone = kwPhone.Trim().ToLower();
                }
                else
                {
                    keywords.KwPhone = "";

                }

                var kwEmail = PrCommon.GetString("email", frmData).Trim();
                if (!String.IsNullOrEmpty(kwEmail))
                {
                    keywords.KwEmail = kwEmail.Trim().ToLower();
                }
                else
                {
                    keywords.KwEmail = "";

                }

                var kwIdRole = PrCommon.GetString("roleId", frmData);
                keywords.KwIdRole = PrCommon.getListInt(kwIdRole, ',', false);

                var kwIsActive = PrCommon.GetString("isActive", frmData);
                if (!String.IsNullOrEmpty(kwIsActive))
                {
                    keywords.KwIsActive = Boolean.Parse(kwIsActive);
                }


                //var listEmp = _db.Employees.FromSqlRaw("[SearchEmployees] {0}, {1}, {2}, {3}, {4}, {5}", kwId, kwName, kwEmail, kwPhone, kwRoleId, kwIsActive).ToList();
                var listEmp = new List<Employee>();
                if (keywords.KwIdRole.Count > 0)
                {
                    if (!string.IsNullOrEmpty(kwIsActive))
                    {
                        listEmp = (from x in _db.Employees
                                   where x.IsDelete == keywords.IsDelete &&
                                                   x.IdEmployee.ToString().ToLower().Contains(keywords.KwId) &&
                                                   x.NameEmployee.ToLower().Contains(keywords.KwName) &&
                                                   x.Email.ToLower().Contains(keywords.KwEmail) &&
                                                   x.Phone.ToLower().Contains(keywords.KwPhone) &&
                                                   x.IsActive == keywords.KwIsActive &&
                                                   keywords.KwIdRole.Contains(x.RoleId)
                                   orderby x.RoleId
                                   select new Employee
                                   {
                                       CreateDate = x.CreateDate,
                                       AccessToken = x.AccessToken,
                                       Address = x.Address,
                                       Birthday = x.Birthday,
                                       Email = x.Email,
                                       IsDelete = x.IsDelete,
                                       Gender = x.Gender,
                                       ModifyDate = x.ModifyDate,
                                       IdEmployee = x.IdEmployee,
                                       Image = x.Image,
                                       IsActive = x.IsActive,
                                       ModifyBy = x.ModifyBy,
                                       NameEmployee = x.NameEmployee,
                                       Password = x.Password,
                                       Phone = x.Phone,
                                       Role = (from r in _db.Roles where r.IdRole == x.RoleId select r).First(),
                                       RoleId = x.RoleId
                                   }).ToList();
                    }
                    else
                    {
                        listEmp = (from x in _db.Employees
                                  where x.IsDelete == keywords.IsDelete &&
                                                  x.IdEmployee.ToString().ToLower().Contains(keywords.KwId) &&
                                                  x.NameEmployee.ToLower().Contains(keywords.KwName) &&
                                                  x.Email.ToLower().Contains(keywords.KwEmail) &&
                                                  x.Phone.ToLower().Contains(keywords.KwPhone) &&
                                                  keywords.KwIdRole.Contains(x.RoleId)
                                   orderby x.RoleId
                                   select new Employee
                                   {
                                       CreateDate = x.CreateDate,
                                       AccessToken = x.AccessToken,
                                       Address = x.Address,
                                       Birthday = x.Birthday,
                                       Email = x.Email,
                                       IsDelete = x.IsDelete,
                                       Gender = x.Gender,
                                       ModifyDate = x.ModifyDate,
                                       IdEmployee = x.IdEmployee,
                                       Image = x.Image,
                                       IsActive = x.IsActive,
                                       ModifyBy = x.ModifyBy,
                                       NameEmployee = x.NameEmployee,
                                       Password = x.Password,
                                       Phone = x.Phone,
                                       Role = (from r in _db.Roles where r.IdRole == x.RoleId select r).First(),
                                       RoleId = x.RoleId
                                   }).ToList();
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(kwIsActive))
                    {
                        listEmp = (from x in _db.Employees
                                   where x.IsDelete == keywords.IsDelete &&
                                                   x.IdEmployee.ToString().ToLower().Contains(keywords.KwId) &&
                                                   x.NameEmployee.ToLower().Contains(keywords.KwName) &&
                                                   x.Email.ToLower().Contains(keywords.KwEmail) &&
                                                   x.Phone.ToLower().Contains(keywords.KwPhone) &&
                                                   x.IsActive == keywords.KwIsActive
                                   orderby x.RoleId
                                   select new Employee
                                   {
                                       CreateDate = x.CreateDate,
                                       AccessToken = x.AccessToken,
                                       Address = x.Address,
                                       Birthday = x.Birthday,
                                       Email = x.Email,
                                       IsDelete = x.IsDelete,
                                       Gender = x.Gender,
                                       ModifyDate = x.ModifyDate,
                                       IdEmployee = x.IdEmployee,
                                       Image = x.Image,
                                       IsActive = x.IsActive,
                                       ModifyBy = x.ModifyBy,
                                       NameEmployee = x.NameEmployee,
                                       Password = x.Password,
                                       Phone = x.Phone,
                                       Role = (from r in _db.Roles where r.IdRole == x.RoleId select r).First(),
                                       RoleId = x.RoleId
                                   }).ToList();
                    }
                    else
                    {
                        listEmp = (from x in _db.Employees
                                   where x.IsDelete == keywords.IsDelete &&
                                                   x.IdEmployee.ToString().ToLower().Contains(keywords.KwId) &&
                                                   x.NameEmployee.ToLower().Contains(keywords.KwName) &&
                                                   x.Email.ToLower().Contains(keywords.KwEmail) &&
                                                   x.Phone.ToLower().Contains(keywords.KwPhone)
                                   orderby x.RoleId
                                   select new Employee
                                   {
                                       CreateDate = x.CreateDate,
                                       AccessToken = x.AccessToken,
                                       Address = x.Address,
                                       Birthday = x.Birthday,
                                       Email = x.Email,
                                       IsDelete = x.IsDelete,
                                       Gender = x.Gender,
                                       ModifyDate = x.ModifyDate,
                                       IdEmployee = x.IdEmployee,
                                       Image = x.Image,
                                       IsActive = x.IsActive,
                                       ModifyBy = x.ModifyBy,
                                       NameEmployee = x.NameEmployee,
                                       Password = x.Password,
                                       Phone = x.Phone,
                                       Role = (from r in _db.Roles where r.IdRole == x.RoleId select r).First(),
                                       RoleId = x.RoleId
                                   }).ToList();
                    }
                }
                var result = Mapper.MapEmployee(listEmp);
                if (listEmp.Count() > 0)
                {
                    res.Content = result;
                }
                else
                {
                    res.Notification.DateTime = DateTime.Now;
                    res.Notification.Messenge = "Không tìm thấy dữ liệu !";
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

        public Response RestoreEmployee(Guid idEmployee)
        {
            try
            {
                var employee = _db.Employees.Find(idEmployee);
                if (employee != null)
                {
                    employee.IsDelete = false;
                    _db.SaveChanges();

                    res.Notification.DateTime = DateTime.Now;
                    res.Notification.Messenge = "Khôi phục thành công !";
                    res.Notification.Type = "Success";
                }
                else
                {
                    res.Notification.DateTime = DateTime.Now;
                    res.Notification.Messenge = "Không tìm thấy !";
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

        public Response DeleteEmployee(Guid idEmployee)
        {
            try
            {
                var employee = _db.Employees.Find(idEmployee);
                if (employee != null)
                {
                    employee.IsDelete = true;
                    _db.SaveChanges();

                    res.Notification.DateTime = DateTime.Now;
                    res.Notification.Messenge = "Xóa thành công !";
                    res.Notification.Type = "Success";
                }
                else
                {
                    res.Notification.DateTime = DateTime.Now;
                    res.Notification.Messenge = "Không tìm thấy !";
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

        public Response CheckEmailEmployee(string email)
        {
            try
            {
                var emp = (from x in _db.Employees where x.IsDelete == false && x.Email == email select x).Count();
                if (emp > 0)
                {
                    res.Notification.DateTime = DateTime.Now;
                    res.Notification.Messenge = "[" + email + "] này đã được đăng ký !";
                    res.Notification.Type = "Error";
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

        public Response CheckPhoneEmployee(string phone, string idEmployee = null)
        {
            try
            {
                if (idEmployee != null) // update
                {
                    Guid id = Guid.Parse(idEmployee);
                    string oldPhone = (from x in _db.Employees where x.IdEmployee == id select x).First().Phone;
                    if (phone != oldPhone) // có thay đổi  sdt
                    {
                        var obj = (from x in _db.Employees where x.Phone != oldPhone && x.Phone == phone select x).Count();
                        if (obj >0)
                        {
                            res.Notification.DateTime = DateTime.Now;
                            res.Notification.Messenge = "[" + phone + "] này đã được đăng ký !";
                            res.Notification.Type = "Error";
                            return res;
                        }
                    }
                }
                else // create
                {
                    var emp = (from x in _db.Employees where x.Phone == phone select x).Count();
                    if (emp > 0)
                    {
                        res.Notification.DateTime = DateTime.Now;
                        res.Notification.Messenge = "[" + phone + "] này đã được đăng ký !";
                        res.Notification.Type = "Error";
                        return res;
                    }
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

        public Response GetEmployee(Guid idEmployee)
        {
            try
            {
                var employee = (from x in _db.Employees 
                                where x.IdEmployee == idEmployee 
                                select x).First();
                if (employee != null)
                {
                    var result = Mapper.MapEmployee(employee);
                    res.Content = result;
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
