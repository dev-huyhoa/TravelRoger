using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travel.Shared.ViewModels.Travel.ServiceVM
{
    public class ServiceViewModel
    {
        public class RestaurantViewModel : ParentProperty
        {
            private Guid idRestaurant;
            public Guid IdRestaurant { get => idRestaurant; set => idRestaurant = value; }
        }
        public class HotelViewModel : ParentProperty
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
            private Guid contractId;
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
            public Guid ContractId { get => contractId; set => contractId = value; }
        }
    }
}
