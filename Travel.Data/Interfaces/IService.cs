using Newtonsoft.Json.Linq;
using System;
using Travel.Shared.ViewModels;
using Travel.Shared.ViewModels.Travel.ContractVM;

namespace Travel.Data.Interfaces
{
    public  interface IService
    {
        string CheckBeforSave(JObject frmData, ref Notification _message, Shared.Ultilities.Enums.TypeService type, bool isUpdate = false);



        Response CreateContract(CreateContractViewModel input);



        Response GetRestaurant();
        Response GetWaitingRestaurant(Guid idUser);
        Response CreateRestaurant(CreateRestaurantViewModel input);
        Response DeleteRestaurant(Guid id, Guid idUser);






        Response GetWaitingHPlace(Guid idUser);
        Response GetPlace();
        Response CreatePlace(CreatePlaceViewModel input);
        Response DeletePlace(Guid id, Guid idUser);




        Response GetHotel();
        Response GetWaitingHotel(Guid idUser);
        Response CreateHotel(CreateHotelViewModel input);
        Response UpdateHotel(UpdateHotelViewModel input);

        Response DeleteHotel(Guid id, Guid idUser);

        Response ApproveHotel(Guid id);
        Response RefusedHotel(Guid id);

    }
}
