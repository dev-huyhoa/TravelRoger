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

namespace Travel.Data.Repositories
{
    public class LocationRes : ILocation
    {
        private readonly TravelContext _db;
        private Response res;
        private Notification message;
        public LocationRes(TravelContext db)
        {
            _db = db;
            res = new Response();
            message = new Notification();
        }


        public Province CheckBeforeSaveProvince(JObject frmData, ref Notification _message)
        {
            Province province = new Province();
            try
            {
                var id = PrCommon.GetString("IdProvince", frmData);
                if (!String.IsNullOrEmpty(id))
                {
                    province.IdProvince = Guid.Parse(PrCommon.GetString("IdProvince", frmData));
                }

                province.NameProvince = PrCommon.GetString("nameProvince", frmData);
                return province;
            }
            catch (Exception e)
            {
                message.DateTime = DateTime.Now;
                message.Description = e.Message;
                message.Messenge = "Có lỗi xảy ra !";
                message.Type = "Error";

                _message = message;
                return province;
            }
        }

        public District CheckBeforeSaveDistrict(JObject frmData, ref Notification _message)
        {
            District district = new District();
            try
            {
                var id = PrCommon.GetString("Id", frmData);
                if (!String.IsNullOrEmpty(id))
                {
                    district.IdDistrict = Guid.Parse(PrCommon.GetString("IdDistrict", frmData));
                }

                var provinceId = PrCommon.GetString("ProvinceId", frmData);
                if (!String.IsNullOrEmpty(provinceId))
                {
                    district.ProvinceId = Guid.Parse(provinceId);
                }

                district.NameDistrict = PrCommon.GetString("Name", frmData);
                return district;
            }
            catch (Exception e)
            {
                message.DateTime = DateTime.Now;
                message.Description = e.Message;
                message.Messenge = "Có lỗi xảy ra !";
                message.Type = "Error";

                _message = message;
                return district;
            }
        }

        public Ward CheckBeforeSaveWard(JObject frmData, ref Notification _message)
        {
            Ward ward = new Ward();
            try
            {
                var id = PrCommon.GetString("Id", frmData);
                if (!String.IsNullOrEmpty(id))
                {
                    ward.IdWard = Guid.Parse(PrCommon.GetString("Id", frmData));
                }

                var districtId = PrCommon.GetString("DistrictId", frmData);
                if (!String.IsNullOrEmpty(districtId))
                {
                    ward.DistrictId = Guid.Parse(districtId);
                }

                ward.NameWard = PrCommon.GetString("NameWard", frmData);
                return ward;
            }
            catch (Exception e)
            {
                message.DateTime = DateTime.Now;
                message.Description = e.Message;
                message.Messenge = "Có lỗi xảy ra !";
                message.Type = "Error";

                _message = message;
                return ward;
            }
        }

        public Response GetsProvince()
        {
            try
            {
                var listProvince = (from x in _db.Provinces select x).ToList();

                var result = Mapper.MapProvince(listProvince);
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

        public Response GetsDistrict()
        {
            try
            {
                var listDistrict = (from x in _db.Districts select x).ToList();
                var result = Mapper.MapDistrict(listDistrict);
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

        public Response GetsWard()
        {
            try
            {
                var listWard = (from x in _db.Wards select x).ToList();
                var result = Mapper.MapWard(listWard);
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

        public Response SearchProvince(JObject frmData)
        {
            try
            {
                Keywords keywords = new Keywords();

                var kwId = PrCommon.GetString("idProvince", frmData);
                if (!String.IsNullOrEmpty(kwId))
                {
                    keywords.KwId = kwId.Trim().ToLower();
                }
                else
                {
                    keywords.KwId = "";

                }

                var kwName = PrCommon.GetString("nameProvince", frmData).Trim();
                if (!String.IsNullOrEmpty(kwName))
                {
                    keywords.KwName = kwName.Trim().ToLower();
                }
                else
                {
                    keywords.KwName = "";

                }

                var listProvince = new List<Province>();
                listProvince = (from x in _db.Provinces
                           where x.IdProvince.ToString().ToLower().Contains(keywords.KwId) &&
                                  x.NameProvince.ToLower().Contains(keywords.KwName)
                           select x).ToList();
                var result = Mapper.MapProvince(listProvince);
                if (listProvince.Count() > 0)
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

        public Response SearchDistrict(JObject frmData)
        {
            try
            {
                Keywords keywords = new Keywords();

                var kwId = PrCommon.GetString("idDistrict", frmData);
                if (!String.IsNullOrEmpty(kwId))
                {
                    keywords.KwId = kwId.Trim().ToLower();
                }
                else
                {
                    keywords.KwId = "";

                }

                var kwName = PrCommon.GetString("nameDistrict", frmData).Trim();
                if (!String.IsNullOrEmpty(kwName))
                {
                    keywords.KwName = kwName.Trim().ToLower();
                }
                else
                {
                    keywords.KwName = "";

                }


                var kwIdProvince = PrCommon.GetString("idProvince", frmData);

                keywords.KwIdProvince = PrCommon.getListString(kwIdProvince, ',', false);

                var listDistrict = new List<District>();
                if (keywords.KwIdProvince.Count > 0)
                {
                    listDistrict = (from x in _db.Districts
                                    where x.IdDistrict.ToString().ToLower().Contains(keywords.KwId) &&
                                           x.NameDistrict.ToLower().Contains(keywords.KwName) &&
                                            keywords.KwIdProvince.Contains(x.ProvinceId.ToString())
                                    select x).ToList();
                }
                else
                {
                    listDistrict = (from x in _db.Districts
                                    where x.IdDistrict.ToString().ToLower().Contains(keywords.KwId) &&
                                           x.NameDistrict.ToLower().Contains(keywords.KwName)
                                    select x).ToList();
                }
                var result = Mapper.MapDistrict(listDistrict);
                if (listDistrict.Count() > 0)
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

        public Response SearchWard(JObject frmData)
        {
            try
            {
                Keywords keywords = new Keywords();

                var kwId = PrCommon.GetString("idWard", frmData);
                if (!String.IsNullOrEmpty(kwId))
                {
                    keywords.KwId = kwId.Trim().ToLower();
                }
                else
                {
                    keywords.KwId = "";

                }

                var kwName = PrCommon.GetString("nameWard", frmData).Trim();
                if (!String.IsNullOrEmpty(kwName))
                {
                    keywords.KwName = kwName.Trim().ToLower();
                }
                else
                {
                    keywords.KwName = "";

                }


                var kwIdDistrict = PrCommon.GetString("idDistrict", frmData);

                keywords.KwIdDistrict = PrCommon.getListString(kwIdDistrict, ',', false);

                var listWard = new List<Ward>();
                if (keywords.KwIdDistrict.Count > 0)
                {
                    listWard = (from x in _db.Wards
                                    where x.IdWard.ToString().ToLower().Contains(keywords.KwId) &&
                                           x.NameWard.ToLower().Contains(keywords.KwName) &&
                                            keywords.KwIdDistrict.Contains(x.DistrictId.ToString())
                                    select x).ToList();
                }
                else
                {
                    listWard = (from x in _db.Wards
                                where x.IdWard.ToString().ToLower().Contains(keywords.KwId) &&
                                           x.NameWard.ToLower().Contains(keywords.KwName)
                                    select x).ToList();
                }
                var result = Mapper.MapWard(listWard);
                if (listWard.Count() > 0)
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

        public Response CreateProvince(Province province)
        {
            try
            {
                _db.Provinces.Add(province);
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

        public Response CreateDistrict(District district)
        {
            try
            {
                district.IdDistrict = Guid.NewGuid();
                _db.Districts.Add(district);
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

        public Response CreateWard(Ward ward)
        {
            try
            {
                ward.IdWard = Guid.NewGuid();
                _db.Wards.Add(ward);
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

        public Response UpdateProvince(Province province)
        {
            try
            {
                _db.Provinces.Update(province);
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

        public Response UpdateDistrict(District district)
        {
            try
            {
                _db.Districts.Update(district);
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

        public Response UpdateWard(Ward ward)
        {
            try
            {
                _db.Wards.Update(ward);
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
        public Response DeleteProvince(Province province)
        {
            try
            {
                var check = _db.Provinces.Find(province.IdProvince);
                if (check != null)
                {
                    _db.Provinces.Remove(check);
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

        public Response DeleteDistrict(District district)
        {
            try
            {
                var check = _db.Districts.Find(district.IdDistrict);
                if (check != null)
                {
                    _db.Districts.Remove(check);
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

        public Response DeleteWard(Ward ward)
        {
            try
            {
                var check = _db.Wards.Find(ward.IdWard);
                if (check != null)
                {
                    _db.Wards.Remove(check);
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
    }
}
