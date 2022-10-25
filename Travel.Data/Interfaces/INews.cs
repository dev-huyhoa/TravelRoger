using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Context.Models;
using Travel.Shared.ViewModels;
using Travel.Shared.ViewModels.Travel;

namespace Travel.Data.Interfaces
{
    public interface INews
    {
        Notification UploadBanner(IFormCollection frmdata, ICollection<IFormFile> files);
        Response UploadNews(IFormCollection frmdata, IFormFile file);
        Response GetBanner();
        //Response DeleteBanner(DeleteBannerViewModel input);
        string CheckBeforeSave(IFormCollection frmdata, IFormFile file, ref Notification _message);
    }
}
