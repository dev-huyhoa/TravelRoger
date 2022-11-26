using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
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
            banner = new Banner();
            message = new Notification();
            res = new Response();
        }

        public Response UploadBanner(string name,IFormCollection frmdata, ICollection<IFormFile> files)
        {
            try
            {
                if (files.Count > 0)
                {
                    var Id = Guid.NewGuid();
                    banner.NameBanner = name;
                    banner.IdBanner = Id;
                    banner.IsActive = true;
                    banner.IsDelete = false;
                    banner.Total = files.Count;
                    _db.Banners.Add(banner);
                    _db.SaveChanges();
                    int err = 0;
                    foreach (var file in files)
                    {
                        var image = Ultility.WriteFile(file, "Banners", Id.ToString(), ref _message);
                        if (_message != null)
                        {
                            err++;
                            message.Messenge = _message.Messenge + " (" + err + ")";
                        }
                        else
                        {
                            return Ultility.Responses("Thêm thành công", Enums.TypeCRUD.Success.ToString());
                        }
                    }
                }
                return Ultility.Responses(" ", Enums.TypeCRUD.Success.ToString());
            }
            catch (Exception)
            {
                return Ultility.Responses("Lỗi !", Enums.TypeCRUD.Success.ToString());
            }
        }

        public Response GetBanner()
        {
            try
            {
                var result = (from x in _db.Banners.AsNoTracking() where x.IsDelete == false select x).ToList();
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


        public Response DeleteBanner(Guid idBanner)
        {
            try
            {
               Banner banner = (from x in _db.Banners where x.IdBanner == idBanner
                select x).SingleOrDefault();
                if (banner != null)
                {
                    banner.IsDelete = true;
                    _db.SaveChanges();
                    return Ultility.Responses("Xóa thành công !", Enums.TypeCRUD.Success.ToString());
                }
                else
                {
                    return Ultility.Responses("Không tìm thấy !", Enums.TypeCRUD.Success.ToString());
                }

            }
            catch (Exception)
            {
                return Ultility.Responses("Có lỗi xảy ra !", Enums.TypeCRUD.Success.ToString());
            }
        }
    }
}
