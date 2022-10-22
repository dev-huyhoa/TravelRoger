using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travel.Shared.ViewModels.Travel
{
    public class UpdatePromotionViewModel : CreatePromotionViewModel
    {

    }

    public class CreatePromotionViewModel
    {
        private int idPromotion;
        private int value;
        private long toDate;
        private long fromDate;

        public int IdPromotion { get => idPromotion; set => idPromotion = value; }
        public int Value { get => value; set => this.value = value; }
        public long ToDate { get => toDate; set => toDate = value; }
        public long FromDate { get => fromDate; set => fromDate = value; }
        //private virtual Schedule Schedules;


    }
}
