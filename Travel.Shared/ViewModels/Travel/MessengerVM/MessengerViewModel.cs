using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Context.Models.Notification;

namespace Travel.Shared.ViewModels.Travel.MessengerVM
{
    public class MessengerViewModel
    {
        public Guid IdCustomer { get; set; }
        public string NameCustomer { get; set; }
        public List<Messenger> Messengers { get; set; }
    }
}
