﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Travel.Context.Models.Travel;
using Travel.Data.Interfaces;
using Travel.Shared.ViewModels;

namespace Travel.Data.Repositories
{
    public class RoleRes: IRole
    {
        private readonly TravelContext _db;
        private Notification message;
        private Response res;
        public RoleRes(TravelContext db)
        {
            _db = db;
            message = new Notification();
            res = new Response();
        }
        public Response ViewAll()
        {
            try
            {

                var result = _db.Roles.Where(x => x.IsDelete == false).ToList();
                if (result.Count() > 0)
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
