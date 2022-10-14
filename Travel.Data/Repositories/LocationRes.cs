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
using Travel.Shared.ViewModels.Travel.DistrictVM;
using Travel.Shared.ViewModels.Travel.WardVM;

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


        public string CheckBeforeSaveProvince(JObject frmData, ref Notification _message, bool isUpdate)
        {
            try
            {
                var idProvince = PrCommon.GetString("idProvince", frmData);
                if (String.IsNullOrEmpty(idProvince))
                {
                }
                var nameProvince = PrCommon.GetString("nameProvince", frmData);
                if (String.IsNullOrEmpty(nameProvince))
                {

                }

                if (isUpdate)
                {
                    UpdateProvinceViewModel objUpdate = new UpdateProvinceViewModel();
                    objUpdate.IdProvince = Guid.Parse(idProvince);
                    objUpdate.NameProvince = nameProvince;
                    return JsonSerializer.Serialize(objUpdate);
                }

                CreateProvinceViewModel objCreate = new CreateProvinceViewModel();
                objCreate.NameProvince = nameProvince;
                return JsonSerializer.Serialize(objCreate);
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

        public string CheckBeforeSaveDistrict(JObject frmData, ref Notification _message, bool isUpdate)
        {
            try
            {
                var idDistrict = PrCommon.GetString("idDistrict", frmData);
                if (String.IsNullOrEmpty(idDistrict))
                {
                }

                var nameDistrict = PrCommon.GetString("nameDistrict", frmData);
                if (String.IsNullOrEmpty(nameDistrict))
                {
                }

                var idProvince = PrCommon.GetString("idProvince", frmData);
                if (String.IsNullOrEmpty(idProvince))
                {
                }
                if (isUpdate)
                {
                    UpdateDistrictViewModel objUpdate = new UpdateDistrictViewModel();
                    objUpdate.IdDistrict = Guid.Parse(idDistrict);
                    objUpdate.NameDistrict = nameDistrict;
                    objUpdate.IdProvince = Guid.Parse(idProvince);
                    return JsonSerializer.Serialize(objUpdate);
                }

                CreateDistrictViewModel objCreate = new CreateDistrictViewModel();
                objCreate.NameDistrict = nameDistrict;
                objCreate.IdProvince = Guid.Parse(idProvince);
                return JsonSerializer.Serialize(objCreate);
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

        public string CheckBeforeSaveWard(JObject frmData, ref Notification _message, bool isUpdate)
        {
            try
            {
                var idWard = PrCommon.GetString("idWard", frmData);
                if (String.IsNullOrEmpty(idWard))
                {
                }

                var nameWard = PrCommon.GetString("nameWard", frmData);
                if (String.IsNullOrEmpty(nameWard))
                {
                }

                var idDistrict = PrCommon.GetString("idDistrict", frmData);
                if (String.IsNullOrEmpty(idDistrict))
                {
                }
                if (isUpdate)
                {
                    UpdateWardViewModel objUpdate = new UpdateWardViewModel();
                    objUpdate.IdWard = Guid.Parse(idWard);
                    objUpdate.NameWard = nameWard;
                    objUpdate.IdDistrict = Guid.Parse(idDistrict);
                    return JsonSerializer.Serialize(objUpdate);
                }

                CreateWardViewModel objCreate = new CreateWardViewModel();
                objCreate.NameWard = nameWard;
                objCreate.IdDistrict = Guid.Parse(idDistrict);
                return JsonSerializer.Serialize(objCreate);
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

        public Response GetsProvince()
        {
            try
            {
                var listProvince = (from x in _db.Provinces orderby x.NameProvince select x).ToList();

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
                var listDistrict = (from x in _db.Districts orderby x.Province.NameProvince, x.NameDistrict select x).ToList();
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
                var listWard = (from x in _db.Wards orderby x.District.NameDistrict, x.NameWard select x).ToList();
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
                                orderby x.NameProvince
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
                                    orderby x.Province.NameProvince, x.NameDistrict
                                    select x).ToList();
                }
                else
                {
                    listDistrict = (from x in _db.Districts
                                    where x.IdDistrict.ToString().ToLower().Contains(keywords.KwId) &&
                                           x.NameDistrict.ToLower().Contains(keywords.KwName)
                                    orderby x.Province.NameProvince, x.NameDistrict
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
                                orderby x.District.NameDistrict, x.NameWard
                                select x).ToList();
                }
                else
                {
                    listWard = (from x in _db.Wards
                                where x.IdWard.ToString().ToLower().Contains(keywords.KwId) &&
                                      x.NameWard.ToLower().Contains(keywords.KwName)
                                orderby x.District.NameDistrict, x.NameWard
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

        public Response CreateProvince(CreateProvinceViewModel input)
        {
            try
            {
                var province = Mapper.MapCreateProvince(input);
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

        public Response CreateDistrict(CreateDistrictViewModel input)
        {
            try
            {
                var district = Mapper.MapCreateDistrict(input);
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

        public Response CreateWard(CreateWardViewModel input)
        {
            try
            {
                var ward = Mapper.MapCreateWard(input);
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

        public Response UpdateProvince(UpdateProvinceViewModel input)
        {
            try
            {
                var province = Mapper.MapUpdateProvince(input);
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

        public Response UpdateDistrict(UpdateDistrictViewModel input)
        {
            try
            {
                var district = Mapper.MapUpdateDistrict(input);
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

        public Response UpdateWard(UpdateWardViewModel input)
        {
            try
            {
                var ward = Mapper.MapUpdateWard(input);
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
        public Response DeleteProvince(JObject frmData)
        {
            try
            {
                var idProvince = PrCommon.GetString("idProvince", frmData);
                var check = _db.Provinces.Find(Guid.Parse(idProvince));
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

        public Response DeleteDistrict(JObject frmData)
        {
            try
            {
                var idDistrict = PrCommon.GetString("idDistrict", frmData);
                var check = _db.Districts.Find(Guid.Parse(idDistrict));
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

        public Response DeleteWard(JObject frmData)
        {
            try
            {
                var idWard = PrCommon.GetString("idWard", frmData);
                var check = _db.Wards.Find(Guid.Parse(idWard));
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
