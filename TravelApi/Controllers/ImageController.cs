using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Travel.Data.Interfaces;
using Travel.Shared.ViewModels;

namespace TravelApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IImage _imageRes;
        private Notification message;
        private Response res;

        public ImageController(IImage imageRes, ILog log)
        {
            _imageRes = imageRes;
            res = new Response();
        }

        [HttpGet]
        [Authorize]
        [Route("list-image-idTour")]
        public object GetImageByIdTour(string idTour)
        {
            res = _imageRes.GetImageByIdTour(idTour);
            return Ok(res);
        }
    }
}
