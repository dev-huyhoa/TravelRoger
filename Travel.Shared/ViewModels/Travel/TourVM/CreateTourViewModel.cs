using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travel.Shared.ViewModels.Travel.TourVM
{
    public class CreateTourViewModel
    {
        private string idTour;
        private string nameTour;
        private string thumbsnail;
        private string fromPlace;
        private string toPlace;


        public string IdTour { get => idTour; set => idTour = value; }
        public string NameTour { get => nameTour; set => nameTour = value; }
        public string Thumbsnail { get => thumbsnail; set => thumbsnail = value; }
        public string FromPlace { get => fromPlace; set => fromPlace = value; }
        public string ToPlace { get => toPlace; set => toPlace = value; }
 
    }
}
