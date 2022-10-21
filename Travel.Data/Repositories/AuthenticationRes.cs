﻿using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Travel.Context.Models;
using Travel.Context.Models.Travel;
using Travel.Data.Interfaces;
using Travel.Shared.Ultilities;
using Travel.Shared.ViewModels;

namespace Travel.Data.Responsives
{
    public class AuthenticationRes : IAuthentication
    {
        private readonly TravelContext _db;
        private Response res;
        public AuthenticationRes(TravelContext db)
        {
            _db = db;
            res = new Response();
        }
        public Employee EmpLogin(string email)
        {
            try
            {
                //var result = _db.Employees.Where(x => x.IsDelete == false &&
                //                                      x.Email == email).FirstOrDefault();
                var result = (from x in _db.Employees where x.IsDelete == false && 
                                                            x.Email == email select x).FirstOrDefault();
                return result;
 
            }
            catch (Exception)
            {
                return null;
            }
        }

        public Employee EmpLogin(string email, string password)
        {
            try
            {
                //var result = _db.Employees.Where(x => x.IsDelete == false &&
                //                                      x.Password  == password &&
                //                                      x.Email == email).FirstOrDefault();
                var result = (from x in _db.Employees
                              where x.IsDelete == false &&
                                    x.Email == email &&
                                    x.Password == password
                              select x).FirstOrDefault();

                return result;
            }
            catch (Exception)
            {
                return null;
            }
        }


        public bool EmpAddToken(string token, Guid idEmp)
        {
            try
            {
                _db.Employees.Find(idEmp).AccessToken = token;
                _db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool EmpActive(string email)
        {
            try
            {
                //var result = _db.Employees.Where(x => x.IsDelete == false &&
                //                                      x.IsActive == true &&
                //                                      x.Email == email).FirstOrDefault();
                var result = (from x in _db.Employees
                              where x.IsDelete == false &&
                                    x.IsActive == true &&
                                    x.Email == email
                              select x).FirstOrDefault();
                return (result != null) ? true : false;


            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool EmpIsNew(string email)
        {
            try
            {
                //var result = _db.Users.Where(x => x.IsDelete == false &&
                //                                      x.IsNew == false &&
                //                                      x.UserEmail == email).FirstOrDefault();
                //return (result != null) ? true : false;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool EmpDeleteToken(string Id)
        {
            try
            {
                try
                {
                    //var user = _db.Users.Where(x => x.UserId == userId).FirstOrDefault();
                    //user.UserToken = null;
                    //user.UserStatus = false;
                    //_db.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public Response CusDeleteToken(Guid idCus)
        {
            try
            {
                var cus = (from x in _db.Customers
                           where x.IsDelete == false &&
                                 x.IdCustomer == idCus
                           select x).FirstOrDefault();
                cus.AccessToken = null;
                cus.GoogleToken = null;
                cus.FbToken = null;
                _db.SaveChanges();
                res.Notification.DateTime = DateTime.Now;
                res.Notification.Messenge = "Đăng xuất thành công !";
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

        public Customer CusLogin(string email)
        {
            try
            {
                //var result = _db.Employees.Where(x => x.IsDelete == false &&
                //                                      x.Email == email).FirstOrDefault();
                var result = (from x in _db.Customers
                              where x.IsDelete == false &&
                                    x.Email == email
                              select x).FirstOrDefault();
                return result;

            }
            catch (Exception)
            {
                return null;
            }
        }

        public Customer CusLogin(string email, string password)
        {
            try
            {
                var result = (from x in _db.Customers
                              where x.IsDelete == false &&
                                    x.Email == email &&
                                    x.Password == password
                              select x).FirstOrDefault();

                return result;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool CusAddTokenGoogle(string token, Guid idCus)
        {
            try
            {
                _db.Customers.Find(idCus).GoogleToken = token;
                _db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


        public bool CusAddToken(string token, Guid idCus)
        {
            try
            {
                _db.Customers.Find(idCus).AccessToken = token;
                _db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool CreateAccountGoogle(Customer cus)
        {
            try
            {
                cus.CreateDate = Ultility.ConvertDatetimeToUnixTimeStampMiliSecond(DateTime.Now);
                _db.Customers.Add(cus);
                _db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        //public bool CusActive(string email)
        //{
        //    try
        //    {
        //        //var result = _db.Employees.Where(x => x.IsDelete == false &&
        //        //                                      x.IsActive == true &&
        //        //                                      x.Email == email).FirstOrDefault();
        //        var result = (from x in _db.Customers
        //                      where x.IsDelete == false &&
        //                            x.IsActive == true &&
        //                            x.Email == email
        //                      select x).FirstOrDefault();
        //        return (result != null) ? true : false;


        //    }
        //    catch (Exception)
        //    {
        //        return false;
        //    }
        //}

        public string Encryption(string password)
        {
            MD5CryptoServiceProvider MD5 = new MD5CryptoServiceProvider();
            byte[] Encrypt;
            UTF8Encoding Encode = new UTF8Encoding();
            Encrypt = MD5.ComputeHash(Encode.GetBytes(password));
            StringBuilder Encryptdata = new StringBuilder();
            for (int i = 0; i < Encrypt.Length; i++)
            {
                Encryptdata.Append(Encrypt[i].ToString());
            }
            return Encryptdata.ToString();
        }
    }
}
