using Newtonsoft.Json.Linq;
using Travel.Shared.ViewModels;
using Travel.Shared.ViewModels.Travel.ContractVM;

namespace Travel.Data.Interfaces
{
    public  interface IService
    {
        string CheckBeforSave(JObject frmData, ref Notification _message, Shared.Ultilities.Enums.TypeService type, bool isUpdate = false);
        Response GetHotel();
        Response GetWaitingHotel();
        Response CreateHotel(CreateHotelViewModel input);
        Response UpdateHotel(UpdateHotelViewModel input);
        Response GetRestaurant();
        Response GetWaitingRestaurant();

        Response CreateRestaurant(CreateRestaurantViewModel input);
        Response GetWaitingHPlace();
        Response GetPlace();
        Response CreatePlace(CreatePlaceViewModel input);

        Response CreateContract(CreateContractViewModel input);
    }
}
