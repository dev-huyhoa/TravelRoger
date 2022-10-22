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
    public interface IPromotions
    {
        string CheckBeforSave(JObject frmData, ref Notification _message, bool isUpdate);
        Response Gets();
        Response Create(CreatePromotionViewModel input);
        Response UpdatePromotion(int id,UpdatePromotionViewModel input);
        Response GetWaitingPromotion();// chưa ô, sài automap thì nó ko update đc, còn ko sài thì update đc, để tui thử cái àny,maf lucs dau thang kiet no lam theo cach kia bi gi ha| tui thay update dc r ma ta

    }
}
