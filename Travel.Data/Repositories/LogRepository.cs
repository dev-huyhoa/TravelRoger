﻿using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using PrUtility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Context.Models.Travel;
using Travel.Data.Interfaces;
using Travel.Shared.Ultilities;
using Travel.Shared.ViewModels;

namespace Travel.Data.Repositories
{
    public class LogRepository : ILog
    {
        private readonly TravelContext _db;
        public LogRepository(TravelContext db)
        {
            _db = db;
        }
        public bool AddLog(string content, string type, string emailCreator, string classContent)
        {
            Logs log = new Logs();
            log.Content = content;
            log.Type = type;
            log.CreationDate = Ultility.ConvertDatetimeToUnixTimeStampMiliSecond(DateTime.Now);
            log.EmailCreator = emailCreator;
            log.Id = Guid.NewGuid();
            log.ClassContent = classContent;
            _db.Logs.Add(log);
            return _db.SaveChanges() > 0;
        }

        public Response GetsList(long fromDate, long toDate, int pageIndex, int pageSize)
        {
            try
            {
                var lsLog = (from x in _db.Logs.AsNoTracking()
                             where x.CreationDate >= fromDate
                             && x.CreationDate <= toDate
                             select x);
                int totalResult = lsLog.Count();
                var result = lsLog.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList();
                var res = Ultility.Responses("", Enums.TypeCRUD.Success.ToString(), result);
                res.TotalResult = totalResult;
                return res;
            }
            catch (Exception e)
            {
                return Ultility.Responses("Có lỗi xảy ra !", Enums.TypeCRUD.Error.ToString(), description: e.Message);
            }
        }

        public async Task<Response> SearchLogByType(JObject frmData)
        {
            try
            {
                var totalResult = 0;
                var pageSize = PrCommon.GetString("pageSize", frmData) == null ? 10 : Convert.ToInt16(PrCommon.GetString("pageSize", frmData));
                var pageIndex = PrCommon.GetString("pageIndex", frmData) == null ? 1 : Convert.ToInt16(PrCommon.GetString("pageIndex", frmData));
                var kwFromDate = PrCommon.GetString("fromDate", frmData);
                var kwToDate = PrCommon.GetString("toDate", frmData);
                var kwType = PrCommon.GetString("type", frmData);
                var lsLog = (from x in _db.Logs.AsNoTracking()
                             where x.Type == kwType
                             select x);
                totalResult = lsLog.Count();
                var result = await lsLog.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToListAsync();
                var res = Ultility.Responses("", Enums.TypeCRUD.Success.ToString(), result);
                res.TotalResult = totalResult;
                return res;
            }
            catch (Exception e)
            {
                return Ultility.Responses("Có lỗi xảy ra !", Enums.TypeCRUD.Error.ToString(), description: e.Message);
            }
        }
    }
}
