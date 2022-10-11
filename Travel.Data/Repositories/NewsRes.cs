using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Context.Models;
using Travel.Context.Models.Travel;
using Travel.Data.Interfaces;
using Travel.Shared.Ultilities;
using Travel.Shared.ViewModels;

namespace Travel.Data.Repositories
{
    public class NewsRes: INews
    {
        private readonly TravelContext _db;
        private  Banner banner;
        private Notification message, _message;
        private Response res;
        public NewsRes(TravelContext db)
        {
            _db = db;
            banner =  new Banner();
            message = new Notification();
            res = new Response();
        }

        public Notification UploadBanner(IFormCollection frmdata, ICollection<IFormFile> files, string name)
        {
            try
            {
                string nameBanner = frmdata["nameBanner"];
                if (files.Count > 0)
                {
                    //JObject frmData = JObject.Parse(frmdata["data"]);
                    var Id = Guid.NewGuid();
                    var isActive = true;
                    var isDelete = false;
                    banner.NameBanner = nameBanner;
                    banner.IdBanner = Id;
                    banner.IsActive = isActive;
                    banner.IsDelete = isDelete;
                    _db.Banners.Add(banner);
                    _db.SaveChanges();
                    int i = 0;
                    foreach (var file in files)
                    {
                        var image = Ultility.WriteFile(file, "Banners", Id, ref _message);
                        if (_message != null)
                        {
                            i++;
                            message.Messenge = _message.Messenge + " (" + i + ")";
                        }
                        else
                        {
                            _db.Images.Add(image);
                            _db.SaveChanges();
                            message.Messenge = "Upload Banners thành công !";
                            message.DateTime = DateTime.Now;
                            message.Type = "Success";
                        }
                    }
                }

                return message;
            }
            catch (Exception)
            {
                return message;
            }
        }

        public Response GetBanner()
        {
            try
            {
                //var banner = (from x in _db.Banners where x.IsActive == true select x).FirstOrDefault();
                //var lsImageBanner = (from x in _db.Images where x.IdService == banner.IdBanner select x).ToList();
                //var result = _db.Banners.ToList();
                var result = (from x in _db.Banners where x.IsDelete == false select x).ToList();
                if (result.Count > 0)
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
        // code ở đây

    }
}
