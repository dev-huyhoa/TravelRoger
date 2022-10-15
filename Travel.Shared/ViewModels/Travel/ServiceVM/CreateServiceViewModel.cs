using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Travel.Shared.Ultilities.Enums;

namespace Travel.Shared.ViewModels.Travel.ContractVM
{
    public class UpdateHotelViewModel : CreateHotelViewModel
    {

    }
    public class UpdateRestaurantViewModel : CreateRestaurantViewModel
    {

    }
    public class UpdatePlaceViewModel : CreatePlaceViewModel
    {

    }
    public class CreateContractViewModel
    {
        private string modifyBy;
        private long modifyDate;
        private string createBy;
        private long createDate;
        private TypeService typeService;
        private string contractName;
        private long signDate;
        private long expDate;
        private Guid idService;


        public string ModifyBy { get => modifyBy; set => modifyBy = value; }
        public long ModifyDate { get => modifyDate; set => modifyDate = value; }
        public string CreateBy { get => createBy; set => createBy = value; }
        public long CreateDate { get => createDate; set => createDate = value; }
        public string ContractName { get => contractName; set => contractName = value; }
        public long SignDate { get => signDate; set => signDate = value; }
        public long ExpDate { get => expDate; set => expDate = value; }
        public Guid IdService { get => idService; set => idService = value; }
        public TypeService TypeService { get => typeService; set => typeService = value; }
    }
    public class CreatePlaceViewModel : ParentProperty
    {
        private Guid idPlace;
        private float priceTicket;
    

        public Guid IdPlace { get => idPlace; set => idPlace = value; }
        public float PriceTicket { get => priceTicket; set => priceTicket = value; }
    }
    public class CreateRestaurantViewModel : ParentProperty
    {
        private Guid idRestaurant;
        public Guid IdRestaurant { get => idRestaurant; set => idRestaurant = value; }
    }
    public class CreateHotelViewModel : ParentProperty
    {
        private Guid idHotel;
        private int star;
        private int quantitySR;
        private int quantityDBR;
        private float singleRoomPrice;
        private float doubleRoomPrice;

        public Guid IdHotel { get => idHotel; set => idHotel = value; }
        public int Star { get => star; set => star = value; }
        public int QuantitySR { get => quantitySR; set => quantitySR = value; }
        public int QuantityDBR { get => quantityDBR; set => quantityDBR = value; }
        public float SingleRoomPrice { get => singleRoomPrice; set => singleRoomPrice = value; }
        public float DoubleRoomPrice { get => doubleRoomPrice; set => doubleRoomPrice = value; }
    }
    public class ParentProperty
    {
        private string modifyBy;
        private long modifyDate;
        private string phone;
        private string address;
        private string name;


        public string ModifyBy { get => modifyBy; set => modifyBy = value; }
        public long ModifyDate { get => modifyDate; set => modifyDate = value; }
        public string Phone { get => phone; set => phone = value; }
        public string Address { get => address; set => address = value; }
        public string Name { get => name; set => name = value; }
    }
}
