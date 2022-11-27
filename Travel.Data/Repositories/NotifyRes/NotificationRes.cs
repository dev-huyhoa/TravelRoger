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

namespace Travel.Data.Repositories.NotifyRes
{
    public class NotificationRes : INotification
    {
        private readonly TravelContext _db;
        private readonly NotificationContext _notifyContext;
        public NotificationRes(NotificationContext notifyContext, TravelContext db)
        {
            _db = db;
            _notifyContext = notifyContext;
        }

        public async Task<Response> Get(Guid idEmployee)
        {
            try
            {
                var list = (from x in _notifyContext.Notifications
                            where x.EmployeeId == idEmployee
                            select x);
                var res = Ultility.Responses("", Enums.TypeCRUD.Success.ToString(), list.ToList());

                var usSeen = await (from x in list
                              where x.IsSeen == false
                              select x).ToListAsync();
                res.TotalResult = usSeen.Count;
                return res;
            }
            catch(Exception e)
            {
                return Ultility.Responses("Có lỗi xảy ra!", Enums.TypeCRUD.Error.ToString(), description:e.Message);
            }
        }

        public async Task<Response> UpdateIsSeen(Guid idNotification)
        {
            try
            {
                var  notification = await (from x in _notifyContext.Notifications
                            where   x.IsSeen == false &&
                                    x.IdNotification == idNotification 
                                    
                            select x).FirstOrDefaultAsync();

                if(notification != null)
                {
                    notification.IsSeen = true;
                    _notifyContext.SaveChanges();
                }
                return Ultility.Responses("", Enums.TypeCRUD.Success.ToString()); 
            }
            catch (Exception e)
            {
                return Ultility.Responses("Có lỗi xảy ra!", Enums.TypeCRUD.Error.ToString(), description: e.Message);
            }
        }
    }
}
