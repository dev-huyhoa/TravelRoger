
using Newtonsoft.Json.Linq;
using PrUtility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Travel.Context.Models;
using Travel.Context.Models.Travel;
using Travel.Data.Interfaces;
using Travel.Shared.Ultilities;
using Travel.Shared.ViewModels;
using Travel.Shared.ViewModels.Travel.VoucherVM;

namespace Travel.Data.Repositories
{
    public class VoucherRes : IVoucher
    {
        private readonly TravelContext _db;
        private Notification message;
        private Response res;
        public VoucherRes(TravelContext db)
        {
            _db = db;
            message = new Notification();
            res = new Response();
        }
        public string CheckBeforSave(JObject frmData, ref Notification _message, bool isUpdate)
        {
            try
            {            
                var code = PrCommon.GetString("code", frmData);
                if (String.IsNullOrEmpty(code))
                {
                }
                var description = PrCommon.GetString("description", frmData);
                if (String.IsNullOrEmpty(description))
                {

                }
                var value = PrCommon.GetString("value", frmData);
                if (String.IsNullOrEmpty(value))
                {
                }
                var startDate = PrCommon.GetString("startDate", frmData);
                if (String.IsNullOrEmpty(startDate))
                {
                }
                var endDate = PrCommon.GetString("endDate", frmData);
                if (String.IsNullOrEmpty(endDate))
                {
                }
               
                var point = PrCommon.GetString("point", frmData);
                if (String.IsNullOrEmpty(point))
                {
                }
                var customerId = PrCommon.GetString("customerId", frmData);
                if (String.IsNullOrEmpty(customerId))
                {
                }
                if (isUpdate)
                {
                    // map data
                    UpdateVoucherViewModel objUpdate = new UpdateVoucherViewModel();
                    objUpdate.Code = code;
                    objUpdate.Value = int.Parse(value);
                    objUpdate.StartDate = long.Parse(startDate);
                    objUpdate.EndDate = long.Parse(endDate);
                    
                    objUpdate.Point = int.Parse(point);

                    objUpdate.Description = description;
                    objUpdate.CustomerId = Guid.Parse(customerId);
                    // generate ID

                    return JsonSerializer.Serialize(objUpdate);
                }
                // map data
                CreateVoucherViewModel obj = new CreateVoucherViewModel();

                obj.Code = code;
                obj.Value = int.Parse(value);
                obj.StartDate = long.Parse(startDate);
                obj.EndDate = long.Parse(endDate);
               
                obj.Point = int.Parse(point);

                obj.Description = description;
                obj.CustomerId = Guid.Parse(customerId);
                return JsonSerializer.Serialize(obj);
            }
            catch (Exception e)
            {
                message.DateTime = DateTime.Now;
                message.Description = e.Message;
                message.Messenge = "Có lỗi xảy ra !";
                message.Type = "Error";
                _message = message;
                return null;
            }
        }

        public Response CreateVoucher(CreateVoucherViewModel input)
        {
            try
            {
                Voucher voucher = new Voucher();
                voucher = Mapper.MapCreateVoucher(input);
                _db.Vouchers.Add(voucher);
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

        public Response DeleteVoucher(int id)
        {
            try
            {
                var voucher = _db.Vouchers.Find(id);
                if (voucher != null)
                {
                    voucher.IsDelete = true;
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

        public Response GetsVoucher(bool isDelete)
        {
            try
            {
                var list = (from x in _db.Vouchers where x.IsDelete == isDelete select x).ToList();
                var result = Mapper.MapVoucher(list);
                if (list.Count() > 0)
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

        public Response RestoreVoucher(int id)
        {
            try
            {
                var voucher = _db.Vouchers.Find(id);
                if (voucher != null)
                {
                    voucher.IsDelete = false;
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

        public Response UpdateVoucher(UpdateVoucherViewModel input)
        {
            try
            {
                Voucher voucher = new Voucher();
                voucher = Mapper.MapUpdateVoucher(input);
                _db.Vouchers.Update(voucher);
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
    }
}
