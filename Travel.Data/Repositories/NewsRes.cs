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
        public Response UploadNews(IFormCollection frmdata, IFormFile file)
        {
            try
            {
                if (file != null)
                {
                    News news = new News();
                    JObject frmData = JObject.Parse(frmdata["data"]);
                    var Id = Guid.NewGuid();
                    news.IdNews = Id;
                    news.CreateDate = Ultility.ConvertDatetimeToUnixTimeStampMiliSecond(DateTime.Now); 
                    news.Content = PrCommon.GetString("content", frmData);
                    
                    _db.News.Add(news);
                    _db.SaveChanges();
                    int err = 0;
                
                        var image = Ultility.WriteFile(file, "News", Id, ref _message);
                        if (_message != null)
                        {
                            message.Messenge = _message.Messenge;
                        }
                        else
                        {
                            _db.News.Add(news);
                            _db.SaveChanges();
                            message.Messenge = "Upload News thành công !";
                            message.DateTime = DateTime.Now;
                            message.Type = "Success";
                        }
                }

                res.Notification.DateTime = DateTime.Now;
                res.Notification.Messenge = "Có lỗi xảy ra !";
                res.Notification.Type = "Success";
                return res;
            }
            catch (Exception e)
            {
                res.Notification.DateTime = DateTime.Now;
                res.Notification.Messenge = e.Message;
                res.Notification.Type = "Error";
                return res;
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

        public string CheckBeforeSave(IFormCollection frmdata, IFormFile file, ref Notification _message)
        {
            try
            {
              
                JObject frmData = JObject.Parse(frmdata["data"]);
                if (frmData != null)
                {
                    var idNews = PrCommon.GetString("IdNews", frmData);
                    if (String.IsNullOrEmpty(idNews))
                    {
                        idNews = Guid.NewGuid().ToString();
                    }

                    var order = PrCommon.GetString("Order", frmData);
                    if (String.IsNullOrEmpty(order))
                    {
                    }
                    var image = PrCommon.GetString("Image", frmData);
                    if (String.IsNullOrEmpty(image))
                    {
                    }
                    var typeNews = PrCommon.GetString("TypeNews", frmData);
                    if (String.IsNullOrEmpty(typeNews))
                    {
                    }
                    var createDate = PrCommon.GetString("CreateDate", frmData);
                    if (String.IsNullOrEmpty(createDate))
                    {
                    }
                    var title = PrCommon.GetString("Title", frmData);
                    if (String.IsNullOrEmpty(title))
                    {
                    }
                    var sortContent = PrCommon.GetString("SortContent", frmData);
                    if (String.IsNullOrEmpty(sortContent))
                    {
                    }
                    var content = PrCommon.GetString("Content", frmData);
                    if (String.IsNullOrEmpty(content))
                    {
                    }
                    var isSubNews = PrCommon.GetString("IsSubNews", frmData);
                    if (String.IsNullOrEmpty(isSubNews))
                    {
                    }
                    var isMainNews = PrCommon.GetString("IsMainNews", frmData);
                    if (String.IsNullOrEmpty(isMainNews))
                    {
                    }
                    var isDelete = PrCommon.GetString("IsDelete", frmData);
                    if (String.IsNullOrEmpty(isDelete))
                    {
                    }
                    var isShow = PrCommon.GetString("IsShow", frmData);
                    if (String.IsNullOrEmpty(isShow))
                    {
                    }
                    if (file != null)
                    {
                        image = Ultility.WriteFile(file, "IdNews", Guid.Parse(idNews), ref _message).FilePath;
                        if (_message != null)
                        {
                            message = _message;
                        }
                    }
                    News news = new News();
                    news.IdNews = Guid.Parse(idNews);
                    news.Order = int.Parse(order);
                    news.Image = image;
                    news.TypeNews = typeNews;
                    news.CreateDate = long.Parse(createDate);
                    news.Title = title;
                    news.SortContent = sortContent;
                    news.Content = content;
                    news.IsSubNews = bool.Parse(isSubNews);
                    news.IsMainNews = bool.Parse(isMainNews);
                    news.IsDelete = bool.Parse(isDelete);
                    news.IsShow = bool.Parse(isShow);


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
