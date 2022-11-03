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

        Response GetsRestaurant();
        Response GetsWaitingRestaurant(Guid idUser);
        Response CreateRestaurant(CreateRestaurantViewModel input);
        Response DeleteRestaurant(Guid id, Guid idUser);
        Response GetsWaitingHPlace(Guid idUser);
        Response GetsPlace();
        Response CreatePlace(CreatePlaceViewModel input);
        Response DeletePlace(Guid id, Guid idUser);
        Response UpdatePlace(UpdatePlaceViewModel input);
        Response ApprovePlace(Guid id);
        Response RefusedPlace(Guid id);




        Response GetsHotel();
        Response GetsWaitingHotel(Guid idUser);
        Response CreateHotel(CreateHotelViewModel input);
        Response UpdateHotel(UpdateHotelViewModel input);

        Response DeleteHotel(Guid id, Guid idUser);
        Response ApproveHotel(Guid id);
        Response RefusedHotel(Guid id);
        Response RestoreHotel(Guid id, Guid idUser);

        Response RefusedRestaurant(Guid id);
        Response ApproveRestaurant(Guid id);
        Response UpdateRestaurant(UpdateRestaurantViewModel input);


    }
}
