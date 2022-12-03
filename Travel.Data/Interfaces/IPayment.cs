using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Shared.ViewModels;
using Travel.Shared.ViewModels.Travel;

namespace Travel.Data.Interfaces
{
    public interface IPayment
    {
        string CheckBeforSave(JObject frmData, ref Notification _message, bool isUpdate = false);
        Response Gets(int pageIndex, int pageSize);
        Response Create(CreatePaymentViewModel input, string emailUser);
        byte[] CreateByteQR(string qrCodeText);
        Task<Response> SendOTP(string email, string bytes);  
        Response AddImg(string qrCodeText, string idService);
        //Response Update(CreateUpdatePaymentViewModel input);
    }
}
