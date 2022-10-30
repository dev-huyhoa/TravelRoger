using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Shared.ViewModels;
using Travel.Shared.ViewModels.Travel.ContractVM;

namespace Travel.Data.Interfaces
{
   public  interface IService
    {
        string CheckBeforSave(JObject frmData, ref Notification _message, Shared.Ultilities.Enums.TypeService type, bool isUpdate = false);
        Response GetHotel();
        Response GetWaitingHotel(Guid idUser);
        Response CreateHotel(CreateHotelViewModel input);
        Response GetRestaurant();
        Response GetWaitingRestaurant(Guid idUser);

        Response CreateRestaurant(CreateRestaurantViewModel input);
        Response GetWaitingHPlace(Guid idUser);
        Response GetPlace();
        Response CreatePlace(CreatePlaceViewModel input);

        Response CreateContract(CreateContractViewModel input);


        Response UpdateHotel(UpdateHotelViewModel input);

        Response DeleteHotel(Guid id, Guid idUser);
        Response DeleteRestaurant(Guid id, Guid idUser);

        Response DeletePlace(Guid id, Guid idUser);

        Response ApproveHotel(Guid id);
        Response RefusedHotel(Guid id);

    }
}
