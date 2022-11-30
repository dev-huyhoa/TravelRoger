using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Context.Models.Travel;
using Travel.Data.Interfaces;
using Travel.Shared.Ultilities;

namespace Travel.Data.Repositories
{
    public class LogRepository :ILog
    {
        private readonly TravelContext _db;
        public LogRepository(TravelContext db)
        {
            _db = db;
        }
        public bool AddLog(string content, string type,string emailCreator, string classContent)
        {
            Logs log = new Logs();
            log.Content = content;
            log.Type = type;
            log.CreationDate = Ultility.ConvertDatetimeToUnixTimeStampMiliSecond(DateTime.Now);
            log.EmailCreator = emailCreator;
            log.Id = Guid.NewGuid();
            log.ClassContent = classContent;
             _db.Logs.Add(log);
            return  _db.SaveChanges() > 0;
        }

    }
}
