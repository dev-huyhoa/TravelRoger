using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using PrUtility;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using Travel.Context.Models;
using Travel.Data.Interfaces;
using Travel.Shared.ViewModels;

namespace TravelApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : Controller
    {
        private IConfiguration configuration;
        private IAuthentication authentication;
        private Response res;
        public AuthenticationController(IConfiguration _configuration, IAuthentication _authentication)
        {
            configuration = _configuration;
            authentication = _authentication;
            res = new Response();
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("login-employee")]
        public object EmpLogin([FromBody] JObject frmData)
        {
            try
            {
                string email = PrCommon.GetString("email", frmData);
                string password = PrCommon.GetString("password", frmData);
                var result = authentication.EmpLogin(email);
                if (result != null)
                {
                    string encryption = authentication.Encryption(password);
                    result = authentication.EmpLogin(email, encryption);
                    if (result != null)
                    {
                        var isNew = authentication.EmpIsNew(email);
                        if (isNew)
                        {
                            var active = authentication.EmpActive(email);
                            if (active)
                            {
                                var claim = new[]
                                {
                                    new Claim(JwtRegisteredClaimNames.Sub, configuration["Token:Subject"]),
                                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                                    new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                                    new Claim(JwtRegisteredClaimNames.Aud, configuration["Token:Audience"]),
                                    new Claim(ClaimTypes.Role, result.RoleId.ToString()),
                                    new Claim("EmployeeId", result.IdEmployee.ToString())
                                };

                                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Token:key"]));
                                var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                                var token = new JwtSecurityToken(configuration["Token:Issuer"],
                                    configuration["Token:Audience"], claim, expires: DateTime.UtcNow.AddMinutes(60),
                                    //configuration["Token:Audience"], claim, expires: DateTime.UtcNow.AddMinutes(525600),
                                    signingCredentials: signIn);

                                var tokenJWT = new JwtSecurityTokenHandler().WriteToken(token);

                                authentication.EmpAddToken(tokenJWT, result.IdEmployee);

                                Authentication auth = new Authentication();
                                auth.Token = tokenJWT;
                                auth.RoleId = result.RoleId;
                                auth.Id = result.IdEmployee;
                                auth.Name = result.NameEmployee;
                                auth.Image = result.Image;
                                auth.Email = result.Email;

                                res.Content = auth;
                                res.Notification.DateTime = DateTime.Now;
                                res.Notification.Description = null;
                                res.Notification.Messenge = "Đăng nhập thành công !";
                                res.Notification.Type = "Success";

                                return Ok(res);
                            }
                            else
                            {

                                res.Notification.DateTime = DateTime.Now;
                                res.Notification.Description = null;
                                res.Notification.Messenge = "Tài khoản của bạn chưa được kích hoạt !";
                                res.Notification.Type = "Error";
                                return Ok(res);
                            }
                        }
                        else
                        {
                            res.Notification.DateTime = DateTime.Now;
                            res.Notification.Description = null;
                            res.Notification.Messenge = "Tài khoản của bạn chưa xác nhận email !";
                            res.Notification.Type = "Error";
                            return Ok(res);
                        }

                    }
                    else
                    {
                        res.Notification.DateTime = DateTime.Now;
                        res.Notification.Description = null;
                        res.Notification.Messenge = "Sai mật khẩu !";
                        res.Notification.Type = "Error";
                        return Ok(res);
                    }

                }
                else
                {
                    res.Notification.DateTime = DateTime.Now;
                    res.Notification.Description = null;
                    res.Notification.Messenge = "Không tìm thấy email [" + email + "] trên hệ thống !";
                    res.Notification.Type = "Error";
                    return Ok(res);
                }
            }
            catch (Exception e)
            {
                res.Notification.DateTime = DateTime.Now;
                res.Notification.Description = e.Message;
                res.Notification.Messenge = "Đăng nhập thất bại !";
                res.Notification.Type = "Error";
                return Ok(res);
            }

        }

        [HttpPost]
        [AllowAnonymous]
        [Route("login-customer")]
        public object CusLogin([FromBody] JObject frmData)
        {
            try
            {
                string email = PrCommon.GetString("email", frmData);
                string password = PrCommon.GetString("password", frmData);
                string googleToken = PrCommon.GetString("googleToken", frmData);

                var result = authentication.CusLogin(email);

                if (result == null && !string.IsNullOrEmpty(googleToken))
                {
                    result = new Customer();
                    result.Email = PrCommon.GetString("email", frmData);
                    result.Password = googleToken;
                    result.NameCustomer = PrCommon.GetString("nameCustomer", frmData);
                    var status = authentication.CreateAccountGoogle(result);
                    if (!status)
                    {
                        res.Notification.DateTime = DateTime.Now;
                        res.Notification.Messenge = "Đăng nhập bằng google thất bại !";
                        res.Notification.Type = "Error";
                        return Ok(res);
                    }
                }

                if (result != null)
                {
                    if (string.IsNullOrEmpty(googleToken))
                    {
                        string encryption = authentication.Encryption(password);
                        result = authentication.CusLogin(email, encryption);
                    }
                    else
                    {
                        var status = authentication.CusAddTokenGoogle(googleToken, result.IdCustomer);
                        if (!status)
                        {
                            res.Notification.DateTime = DateTime.Now;
                            res.Notification.Messenge = "Đăng nhập bằng google thất bại !";
                            res.Notification.Type = "Error";
                            return Ok(res);
                        }
                    }

                    if (result != null)
                    {
                        var claim = new[]
                                {
                                    new Claim(JwtRegisteredClaimNames.Sub, configuration["Token:Subject"]),
                                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                                    new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                                    new Claim(JwtRegisteredClaimNames.Aud, configuration["Token:Audience"]),
                                    new Claim("CustomerId", result.IdCustomer.ToString())
                                };

                        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Token:key"]));
                        var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                        var token = new JwtSecurityToken(configuration["Token:Issuer"],
                            configuration["Token:Audience"], claim, expires: DateTime.UtcNow.AddMinutes(60),
                            //configuration["Token:Audience"], claim, expires: DateTime.UtcNow.AddMinutes(525600),
                            signingCredentials: signIn);

                        var tokenJWT = new JwtSecurityTokenHandler().WriteToken(token);

                        authentication.CusAddToken(tokenJWT, result.IdCustomer);

                        Authentication auth = new Authentication();
                        auth.Token = tokenJWT;
                        auth.Id = result.IdCustomer;
                        auth.Name = result.NameCustomer;
                        auth.Email = result.Email;

                        res.Content = auth;
                        res.Notification.DateTime = DateTime.Now;
                        res.Notification.Description = null;
                        res.Notification.Messenge = "Đăng nhập thành công !";
                        res.Notification.Type = "Success";

                        return Ok(res);
                    }
                    else
                    {
                        res.Notification.DateTime = DateTime.Now;
                        res.Notification.Description = null;
                        res.Notification.Messenge = "Sai mật khẩu !";
                        res.Notification.Type = "Error";
                        return Ok(res);
                    }

                }
                else
                {
                    res.Notification.DateTime = DateTime.Now;
                    res.Notification.Description = null;
                    res.Notification.Messenge = "Không tìm thấy email [" + email + "] trên hệ thống !";
                    res.Notification.Type = "Error";
                    return Ok(res);
                }
            }
            catch (Exception e)
            {
                res.Notification.DateTime = DateTime.Now;
                res.Notification.Description = e.Message;
                res.Notification.Messenge = "Đăng nhập thất bại !";
                res.Notification.Type = "Error";
                return Ok(res);
            }

        }

        [HttpGet]
        [AllowAnonymous]
        [Route("logout-customer")]
        public object CusLogout(Guid idCus)
        {
            res = authentication.CusDeleteToken(idCus);
            return Ok(res);
        }
    }
}
