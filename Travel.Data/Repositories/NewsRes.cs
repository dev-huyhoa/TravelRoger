using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;
using PrUtility;
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
using Travel.Shared.ViewModels.Travel;

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

        public Notification UploadBanner(IFormCollection frmdata, ICollection<IFormFile> files)
        {
            try
            {
                if (files.Count > 0)
                {
                    JObject frmData = JObject.Parse(frmdata["data"]);
                    var Id = Guid.NewGuid();
                    banner.NameBanner = PrCommon.GetString("name", frmData);
                    banner.IdBanner = Id;
                    banner.IsActive = true;
                    banner.IsDelete = false;
                    banner.Total = files.Count;
                    _db.Banners.Add(banner);
                    _db.SaveChanges();
                    int err = 0;
                    foreach (var file in files)
                    {
                        var image = Ultility.WriteFile(file, "Banners", Id, ref _message);
                        if (_message != null)
                        {
                            err++;
                            message.Messenge = _message.Messenge + " (" + err + ")";
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

        //public Response DeleteBanner(DeleteBannerViewModel input)
        //{
        //    try
        //    {
        //        Banner banner = new Banner();
               
        //        var check = _db.Banners.Find(banner.IdBanner);
        //        if (check != null)
        //        {
        //            _db.Banners.Update(banner);
        //            _db.SaveChanges();
        //            res.Notification.DateTime = DateTime.Now;
        //            res.Notification.Messenge = "Sửa thành công !";
        //            res.Notification.Type = "Success";
        //        }
        //        else
        //        {
        //            res.Notification.DateTime = DateTime.Now;
        //            res.Notification.Messenge = "Không tìm thấy !";
        //            res.Notification.Type = "Warning";
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        res.Notification.DateTime = DateTime.Now;
        //        res.Notification.Description = e.Message;
        //        res.Notification.Messenge = "Có lỗi xảy ra !";
        //        res.Notification.Type = "Error";
        //    }
        //    return res;
        //}

    }
}
