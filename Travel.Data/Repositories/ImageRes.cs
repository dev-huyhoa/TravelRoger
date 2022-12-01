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
    public class ImageRes : IImage
    {
        private readonly TravelContext _db;
        private Notification message;
        private Response res;
        private readonly ILog _log;


        public ImageRes(TravelContext db, ILog log)
        {
            _db = db;
            _log = log;
            message = new Notification();
            res = new Response();
        }

        public Response GetImageByIdTour(string idTour)
        {
            try
            {
                var image = (from x in _db.Images
                                where x.IdService == idTour
                                select x).ToList();
               
                if (image != null)
                {
                    res = Ultility.Responses("", Enums.TypeCRUD.Success.ToString(), image);
                }
                return res;
            }
            catch (Exception e)
            {
                return Ultility.Responses("Có lỗi xảy ra !", Enums.TypeCRUD.Error.ToString(), description: e.Message);
            }
        }
    }
}
