using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Context.Models.Notification;
using Travel.Context.Models.Travel;
using Travel.Data.Interfaces.INotify;
using Travel.Shared.Ultilities;
using Travel.Shared.ViewModels;
using Travel.Shared.ViewModels.Notify.MessengerVM;
using Travel.Shared.ViewModels.Travel.MessengerVM;

namespace Travel.Data.Repositories.NotifyRes
{
    public class MessengerRes : IMessenger
    {
        private readonly TravelContext _db;
        private readonly NotificationContext _dbNotify;
        private Notification message;
        private Response res;
        public MessengerRes(TravelContext db, NotificationContext dbNotify)
        {
            _db = db;
            message = new Notification();
            res = new Response();
            _dbNotify = dbNotify;
        }
        private async Task CreateDatabase(Messenger input)
        {
            _dbNotify.Entry(input).State = EntityState.Added;
            await _dbNotify.SaveChangesAsync();
        }
        private void SaveChange()
        {
            _db.SaveChanges();
        }
        public async Task<Response> SupportedReply(Messenger input)
        {
            try
            {
                input.IdMessenger = Guid.NewGuid();
                input.SendDate = Ultility.ConvertDatetimeToUnixTimeStampMiliSecond(DateTime.Now);
                await CreateDatabase(input);
                return Ultility.Responses("", Enums.TypeCRUD.Success.ToString());
            }
            catch (Exception e)
            {
                return Ultility.Responses("Có lỗi xảy ra !", Enums.TypeCRUD.Error.ToString(), e.Message);
            }
        }



        public async Task<Response> Create(Messenger input)
        {
            try
            {
                var isChated = await (from x in _dbNotify.Messengers.AsNoTracking()
                                      where x.SenderId == input.SenderId
                                      select x).FirstOrDefaultAsync();
                if (isChated != null)
                {
                    input.ReceiverId = isChated.ReceiverId;
                    input.IdMessenger = Guid.NewGuid();
                    input.IsSeen = false;
                    input.SendDate = Ultility.ConvertDatetimeToUnixTimeStampMiliSecond(DateTime.Now);
                    await CreateDatabase(input);
                    return Ultility.Responses("", Enums.TypeCRUD.Success.ToString());
                }
                else
                {
                    var mess = (from x in _db.Employees.AsNoTracking()
                                where x.IsOnline == true && x.RoleId == Convert.ToInt16(Enums.TitleRole.Supporter)
                                select x);
                    if (await mess.CountAsync() == 0)
                    {
                        mess = (from x in _db.Employees.AsNoTracking()
                                where x.IsOnline == false
                                && x.IsDelete == false
                                && x.IsActive == true
                                && x.RoleId == Convert.ToInt16(Enums.TitleRole.Supporter)
                                select x);
                    }
                    Random rnd = new Random();
                    int number = rnd.Next(0, await mess.CountAsync() - 1);
                    var listSp = await mess.ToListAsync();
                    input.ReceiverId = listSp[number].IdEmployee;
                    input.IdMessenger = Guid.NewGuid();
                    input.SendDate = Ultility.ConvertDatetimeToUnixTimeStampMiliSecond(DateTime.Now);
                    input.IsSeen = false;
                    await CreateDatabase(input);
                    return Ultility.Responses("", Enums.TypeCRUD.Success.ToString());
                }


            }
            catch (Exception e)
            {
                return Ultility.Responses("Có lỗi xảy ra !", Enums.TypeCRUD.Error.ToString(), e.Message);
            }
        }


        public async Task<Response> CustomerViewMessenger(Guid IdCustomer)
        {
            try
            {
                var messViewModel = new List<MessengerViewModel>();
                var messCustomerSend = await (from x in _dbNotify.Messengers.AsNoTracking()
                        where x.SenderId == IdCustomer
                        select x).ToListAsync();
                var messSupporterSend = await (from x in _dbNotify.Messengers.AsNoTracking()
                                               where x.ReceiverId == IdCustomer
                                               select x).ToListAsync();
                var listMess =  messCustomerSend.Concat(messSupporterSend).OrderBy(x => x.SendDate).ToList();
                return Ultility.Responses("", Enums.TypeCRUD.Success.ToString(), listMess);
            }
            catch (Exception e)
            {
                return Ultility.Responses("Có lỗi xảy ra !", Enums.TypeCRUD.Error.ToString(), e.Message);
            }
        }

        // admin viewm message
        public async Task<Response> SupporterViewMessenger(Guid IdSuporter)
        {
            try
            {
                var mess = new List<Messenger>();
                var messViewModel = new List<MessengerViewModel>();
                if (IdSuporter != Guid.Empty)
                {
                    var queryGuest = await (from x in _dbNotify.Messengers.AsNoTracking()
                                            where x.ReceiverId == IdSuporter
                                            select x).ToListAsync();
                    //var queryCurrentSupported = await (from x in _dbNotify.Messengers.AsNoTracking()
                    //                                   where x.SenderId == IdReceive
                    //                                   && x.ReceiverId == \)
                    //tất cả  tin nhắn mình đã gửi
                    var querySupporterSent = (from x in _dbNotify.Messengers.AsNoTracking()
                                              where x.SenderId == IdSuporter
                                              select x);
                    var grouping = queryGuest.GroupBy(x => x.SenderId);
                    foreach (var group in grouping)
                    {
                        var messengerItem = new MessengerViewModel();
                        messengerItem.IdCustomer = group.Key;
                       
                        // tin nhắn khách hàng gửi
                        var lsCustomerMessenger = group.ToList();
                        // tin nhắn mình trả lời
                        var lsReplyMessenger =await querySupporterSent.Where(x => x.ReceiverId == messengerItem.IdCustomer).ToListAsync();
                       
                        // tổng danh sách câu hỏi và trả lời tương ứng với từng khách hàng
                        var listMess = lsCustomerMessenger.Concat(lsReplyMessenger).OrderBy(x => x.SendDate).ToList();

                        messengerItem.NameCustomer = (from x in listMess
                                                      where x.SenderId == messengerItem.IdCustomer
                                                      select x.SenderName).FirstOrDefault();
                        messengerItem.Messengers = listMess;
                        messViewModel.Add(messengerItem);
                    }
                    return Ultility.Responses("", Enums.TypeCRUD.Success.ToString(), messViewModel);
                }
                else
                {
                    mess = (from x in _dbNotify.Messengers.AsNoTracking()
                            where x.SenderId == IdSuporter
                            select x).ToList();
                }
                return Ultility.Responses("", Enums.TypeCRUD.Success.ToString());
            }
            catch (Exception e)
            {
                return Ultility.Responses("Có lỗi xảy ra !", Enums.TypeCRUD.Error.ToString(), e.Message);
            }
        }

        public async Task<Response> CheckSeenMessenger(Guid IdMessenger)
        {
            try
            {
                var result = await (from x in _dbNotify.Messengers.AsNoTracking()
                              where x.IdMessenger == IdMessenger
                              select x).FirstOrDefaultAsync();
                if (result != null)
                {
                    result.IsSeen = true;
                    SaveChange();
                }
                return Ultility.Responses("", Enums.TypeCRUD.Success.ToString());
            }
            catch (Exception e)
            {
                return Ultility.Responses("Có lỗi xảy ra !", Enums.TypeCRUD.Error.ToString(), e.Message);
            }
        }
    }
}
