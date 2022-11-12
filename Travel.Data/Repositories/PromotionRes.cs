﻿using Microsoft.EntityFrameworkCore;
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
using Travel.Shared.ViewModels.Travel;
using Travel.Shared.ViewModels.Travel.PromotionVM;
using static Travel.Shared.Ultilities.Enums;

namespace Travel.Data.Repositories
{
    public class PromotionRes : IPromotions
    {
        private readonly TravelContext _db;
        private Notification message;
        private Employee GetCurrentUser(Guid IdUserModify)
        {
            return (from x in _db.Employees.AsNoTracking()
                    where x.IdEmployee == IdUserModify
                    select x).FirstOrDefault();
        }
        public PromotionRes(TravelContext db)
        {
            _db = db;
            message = new Notification();
        }
        private void CreateDatabase<T>(T input)
        {
            _db.Entry(input).State = EntityState.Added;
        }
        private void DeleteDatabaseNotSave(Promotion input)
        {
            _db.Entry(input).State = EntityState.Deleted;
        }
        private void UpdateDatabase(Promotion input)
        {
            _db.Entry(input).State = EntityState.Modified;
            _db.SaveChanges();
        }
        private void DeleteDatabase(Promotion input)
        {
            _db.Entry(input).State = EntityState.Deleted;
            _db.SaveChanges();
        }
        private void CreateDatabase(Promotion input)
        {
            _db.Entry(input).State = EntityState.Added;
            _db.SaveChanges();
        }
        public string CheckBeforSave(JObject frmData, ref Notification _message, TypeService type, bool isUpdate = false)
        {
            try
            {
                var idPromotion = PrCommon.GetString("idPromotion", frmData) ?? "0";

                 if (String.IsNullOrEmpty(idPromotion))
                {
                }
                var value = PrCommon.GetString("value", frmData);
                if (String.IsNullOrEmpty(value))
                {
                }
                var toDate = PrCommon.GetString("toDate", frmData);
                if (String.IsNullOrEmpty(toDate))
                {
                }
                var fromDate = PrCommon.GetString("fromDate", frmData);
                if (String.IsNullOrEmpty(fromDate))
                {
                }

                var typeAction = PrCommon.GetString("typeAction", frmData);
                if (String.IsNullOrEmpty(typeAction))
                {
                }
                var idUserModify = PrCommon.GetString("idUserModify", frmData);
                if (String.IsNullOrEmpty(idUserModify))
                {
                }

                if (isUpdate)
                {
                    UpdatePromotionViewModel uPromotionObj = new UpdatePromotionViewModel();
                    uPromotionObj.IdPromotion = int.Parse(idPromotion);
                    uPromotionObj.Value = int.Parse(value);
                    uPromotionObj.ToDate = long.Parse(toDate);
                    uPromotionObj.FromDate = long.Parse(fromDate);
                    uPromotionObj.IdUserModify = Guid.Parse(idUserModify);
                    uPromotionObj.TypeAction = "update";
                    return JsonSerializer.Serialize(uPromotionObj);
                }
                else
                {
                    CreatePromotionViewModel PromotionObj = new CreatePromotionViewModel();
                    PromotionObj.Value = int.Parse(value);
                    PromotionObj.ToDate = long.Parse(toDate);
                    PromotionObj.FromDate = long.Parse(fromDate);
                    PromotionObj.IdUserModify = Guid.Parse(idUserModify);
                    return JsonSerializer.Serialize(PromotionObj);
                }

            }
            catch (Exception e)
            {
                _message = Ultility.Responses("Có lỗi xảy ra !", Enums.TypeCRUD.Error.ToString(), description: e.Message).Notification;
                return string.Empty;
            }
        }

        public Response GetsPromotion(bool isDelete)
        {
            try
            {
                var list = (from x in _db.Promotions.AsNoTracking()
                            where
                            x.IsDelete == isDelete &&
                            x.IsTempdata == false &&
                            x.Approve == Convert.ToInt16(Enums.ApproveStatus.Approved)

                            select x).ToList();
                var result = Mapper.MapPromotion(list);
                return Ultility.Responses("", Enums.TypeCRUD.Success.ToString(), result);
            }
            catch (Exception e)
            {
                return Ultility.Responses("Có lỗi xảy ra !", Enums.TypeCRUD.Error.ToString(), description: e.Message);
            }
        }

        public Response GetsWaitingPromotion(Guid idUser)
        {
            try
            {
                var userLogin = (from x in _db.Employees.AsNoTracking()
                                 where x.IdEmployee == idUser
                                 select x).FirstOrDefault();
                var listWaiting = new List<Promotion>();
                if (userLogin.RoleId == (int)Enums.TitleRole.Admin)
                {
                    listWaiting = (from x in _db.Promotions.AsNoTracking()
                                   where x.Approve == Convert.ToInt16(ApproveStatus.Waiting) select x).ToList();
                }
                else
                {
                    listWaiting = (from x in _db.Promotions.AsNoTracking()
                                   where x.IdUserModify == idUser
                                   && x.Approve == Convert.ToInt16(ApproveStatus.Waiting)
                                   select x).ToList();
                }

                var result = Mapper.MapPromotion(listWaiting);

                return Ultility.Responses("", Enums.TypeCRUD.Success.ToString(), result);
            }
            catch (Exception e)
            {
                return Ultility.Responses("Có lỗi xảy ra !", Enums.TypeCRUD.Error.ToString(), description: e.Message);
            }
        } 
         public Response CreatePromotion(CreatePromotionViewModel input)
        {
            Promotion promotion
                        = Mapper.MapCreatePromotion(input);
            var user = GetCurrentUser(input.IdUserModify);
            input.ModifyBy = user.NameEmployee;
            promotion.TypeAction = "insert";
            promotion.ModifyDate = Ultility.ConvertDatetimeToUnixTimeStampMiliSecond(DateTime.Now);
            CreateDatabase(promotion);
            return Ultility.Responses("Thêm thành công !", Enums.TypeCRUD.Success.ToString());
        }

        public Response DeletePromotion(int id, Guid idUser)
        {
            try
            {
                var promotion = (from x in _db.Promotions.AsNoTracking()
                                 where x.IdPromotion == id
                             select x).FirstOrDefault();

                var userLogin = (from x in _db.Employees.AsNoTracking()
                                 where x.IdEmployee == idUser
                                 select x).FirstOrDefault();
                if (promotion.Approve == (int)ApproveStatus.Approved)
                {
                    promotion.ModifyBy = userLogin.NameEmployee;
                    promotion.TypeAction = "delete";
                    promotion.IdUserModify = userLogin.IdEmployee;
                    promotion.ModifyDate = Ultility.ConvertDatetimeToUnixTimeStampMiliSecond(DateTime.Now);
                    promotion.Approve = (int)ApproveStatus.Waiting;
                    promotion.IsDelete = true;
                    UpdateDatabase(promotion);
                    return Ultility.Responses("Đã gửi yêu cầu xóa !", Enums.TypeCRUD.Success.ToString());
                }
                else
                {
                    if (promotion.IdUserModify == idUser)
                    {
                        if (promotion.TypeAction == "insert")
                        {
                            DeleteDatabase(promotion);

                            return Ultility.Responses("Đã xóa!", Enums.TypeCRUD.Success.ToString());
                        }
                        else if (promotion.TypeAction == "update")
                        {
                            var idPromotionTemp = promotion.IdAction;
                            // old hotel
                            var promotionTemp = (from x in _db.Promotions.AsNoTracking()
                                                 where x.IdPromotion == int.Parse(idPromotionTemp)
                                             select x).FirstOrDefault();
                            promotion.Approve = (int)ApproveStatus.Approved;
                            promotion.IdAction = null;
                            promotion.TypeAction = null;
                            #region restore old data

                            promotion.Approve = (int)ApproveStatus.Approved;
                            promotion.Value = promotionTemp.Value;

                            promotion.ToDate = promotionTemp.ToDate;
                            promotion.FromDate = promotionTemp.FromDate;
                            #endregion

                            DeleteDatabase(promotionTemp);
                            return Ultility.Responses("Đã hủy yêu cầu chỉnh sửa !", Enums.TypeCRUD.Success.ToString());
                        }
                        else if (promotion.TypeAction == "restore")
                        {
                            promotion.IdAction = null;
                            promotion.TypeAction = null;
                            promotion.IsDelete = true;
                            promotion.Approve = (int)ApproveStatus.Approved;

                                UpdateDatabase(promotion);

                            return Ultility.Responses("Đã hủy yêu cầu khôi phục!", Enums.TypeCRUD.Success.ToString());

                        }
                        else // delete
                        {
                            promotion.IdAction = null;
                            promotion.TypeAction = null;
                            promotion.IsDelete = false;
                            promotion.Approve = (int)ApproveStatus.Approved;
                            UpdateDatabase(promotion);
                            return Ultility.Responses("Đã hủy yêu cầu xóa !", Enums.TypeCRUD.Success.ToString());
                        }
                    }
                    else
                    {
                        return Ultility.Responses("Bạn không thể thực thi hành động này !", Enums.TypeCRUD.Info.ToString());
                    }
                }
            }
            catch (Exception e)
            {
                return Ultility.Responses("Có lỗi xảy ra !", Enums.TypeCRUD.Error.ToString(), description: e.Message);
            }
        }

        public Response UpdatePromotion(UpdatePromotionViewModel input)
        {
            try
            {
                var userLogin = (from x in _db.Employees.AsNoTracking()
                                 where x.IdEmployee == input.IdUserModify
                                 select x).FirstOrDefault();

                var promotion = (from x in _db.Promotions.AsNoTracking()
                                 where x.IdPromotion == input.IdPromotion
                             select x).FirstOrDefault();

                // clone new object
                var promotionOld = new Promotion();
                promotionOld = Ultility.DeepCopy<Promotion>(promotion);
                promotionOld.IdAction = promotionOld.IdPromotion.ToString();
                promotionOld.IsTempdata = true;

                CreateDatabase<Promotion>(promotion);
                #region setdata
                promotion.IdAction = promotionOld.IdPromotion.ToString();
                promotion.IdUserModify = input.IdUserModify;
                promotion.TypeAction = input.TypeAction;
                promotion.Approve = (int)ApproveStatus.Waiting;
                promotion.ModifyBy = userLogin.NameEmployee;
                promotion.ModifyDate = Ultility.ConvertDatetimeToUnixTimeStampMiliSecond(DateTime.Now);


                promotion.Value = input.Value;
                promotion.ToDate = input.ToDate;
                promotion.FromDate = input.FromDate;
                #endregion

                UpdateDatabase(promotion);
                return Ultility.Responses("Đã gửi yêu cầu sửa !", Enums.TypeCRUD.Success.ToString());

            }
            catch (Exception e)
            {
                return Ultility.Responses("Có lỗi xảy ra !", Enums.TypeCRUD.Error.ToString(), description: e.Message);
            }
        }

        public Response ApprovePromotion(int id)
        {
            try
            {
                var promotion = (from x in _db.Promotions.AsNoTracking()
                             where x.IdPromotion == id
                             && x.Approve == (int)ApproveStatus.Waiting
                             select x).FirstOrDefault();
                if (promotion != null)
                { 
                    if (promotion.TypeAction == "update")
                    {
                        var idPromotionTemp = promotion.IdAction;
                        promotion.Approve = (int)ApproveStatus.Approved;
                        promotion.IdAction = null;
                        promotion.TypeAction = null;
                        // delete tempdata
                        var promotionTemp = (from x in _db.Promotions.AsNoTracking()
                                         where x.IdPromotion == int.Parse(idPromotionTemp)
                                         select x).FirstOrDefault();
                        DeleteDatabaseNotSave(promotionTemp);
                    }
                    else if (promotion.TypeAction == "insert")
                    {
                        promotion.IdAction = null;
                        promotion.TypeAction = null;
                        promotion.Approve = (int)ApproveStatus.Approved;
                    }
                    else if (promotion.TypeAction == "restore")
                    {
                        promotion.IdAction = null;
                        promotion.TypeAction = null;
                        promotion.Approve = (int)ApproveStatus.Approved;
                        promotion.IsDelete = false;
                    }
                    else
                    {
                        promotion.IdAction = null;
                        promotion.TypeAction = null;
                        promotion.Approve = (int)ApproveStatus.Approved;
                        promotion.IsDelete = true;                                       
                    }
                    UpdateDatabase(promotion);
                    return Ultility.Responses("Duyệt thành công !", Enums.TypeCRUD.Success.ToString());
                }
                else
                {
                    return Ultility.Responses("Không tim thấy dữ liệu !", Enums.TypeCRUD.Warning.ToString());
                }

            }
            catch (Exception e)
            {
                return Ultility.Responses("Có lỗi xảy ra !", Enums.TypeCRUD.Error.ToString(), description: e.Message);
            }
        }

        public Response RefusedPromotion(int id)
        {
            try
            {
                var promotion = (from x in _db.Promotions.AsNoTracking()
                                  where x.IdPromotion == id
                                  && x.Approve == (int)ApproveStatus.Waiting
                                  select x).FirstOrDefault();
                if (promotion != null)
                {
                    if (promotion.TypeAction == "update")
                    {
                        var idPromotionTemp = promotion.IdAction;
                        // old hotel
                        var promotionTemp = (from x in _db.Promotions.AsNoTracking()
                                             where x.IdPromotion == int.Parse(idPromotionTemp)
                                              select x).FirstOrDefault();
                        promotion.Approve = (int)ApproveStatus.Approved;
                        promotion.IdAction = null;
                        promotion.TypeAction = null;
                        #region restore old data

                        promotion.Approve = (int)ApproveStatus.Approved;
                        promotion.Value = promotionTemp.Value;

                        promotion.ToDate = promotionTemp.ToDate;
                        promotion.FromDate = promotionTemp.FromDate;
                        #endregion
                        DeleteDatabaseNotSave(promotionTemp);
                       }
                    else if (promotion.TypeAction == "insert")
                    {
                        promotion.IdAction = null;
                        promotion.TypeAction = null;
                        promotion.Approve = (int)ApproveStatus.Refused;
                    }
                    else if (promotion.TypeAction == "restore")
                    {
                        promotion.IdAction = null;
                        promotion.TypeAction = null;
                        promotion.IsDelete = true;
                        promotion.Approve = (int)ApproveStatus.Approved;
                    }
                    else // delete
                    {
                        promotion.IdAction = null;
                        promotion.TypeAction = null;
                        promotion.IsDelete = false;
                        promotion.Approve = (int)ApproveStatus.Approved;
                    }
                    UpdateDatabase(promotion);
                    return Ultility.Responses("Từ chối thành công !", Enums.TypeCRUD.Success.ToString());
                }
                else
                {
                    return Ultility.Responses("Không tim thấy dữ liệu !", Enums.TypeCRUD.Warning.ToString());
                }
            }
            catch (Exception e)
            {
                return Ultility.Responses("Có lỗi xảy ra !", Enums.TypeCRUD.Error.ToString(), description: e.Message);
            }
        }

        public Response RestorePromotion(int id, Guid idUser)
        {
            try
            {
                var promotion = (from x in _db.Promotions.AsNoTracking()
                             where x.IdPromotion == id
                             select x).FirstOrDefault();

                var userLogin = (from x in _db.Employees.AsNoTracking()
                                 where x.IdEmployee == idUser
                                 select x).FirstOrDefault();
                if (promotion.Approve == (int)ApproveStatus.Approved)
                {
                    promotion.ModifyBy = userLogin.NameEmployee;
                    promotion.TypeAction = "restore";
                    promotion.IdUserModify = userLogin.IdEmployee;
                    promotion.ModifyDate = Ultility.ConvertDatetimeToUnixTimeStampMiliSecond(DateTime.Now);
                    promotion.Approve = (int)ApproveStatus.Waiting;
                    // bổ sung isdelete
                    promotion.IsDelete = false;
                }
                UpdateDatabase(promotion);
                return Ultility.Responses("Đã gửi yêu cầu khôi phục !", Enums.TypeCRUD.Success.ToString());

            }
            catch (Exception e)
            {
                return Ultility.Responses("Có lỗi xảy ra !", Enums.TypeCRUD.Error.ToString(), description: e.Message);
            }
        }
    }
}
