using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travel.Shared.ViewModels.Travel.VoucherVM
{
    public class UpdateVoucherViewModel : CreateVoucherViewModel
    {
    }
    public class CreateVoucherViewModel
    {
        private Guid idVoucher;
        private string code;
        private string description;
        private int value;
        private long startDate;
        private long endDate;     
        private int point;   
        private Guid customerId;

      
        public string Code { get => code; set => code = value; }
        public string Description { get => description; set => description = value; }
        public int Value { get => value; set => this.value = value; }
        public long StartDate { get => startDate; set => startDate = value; }
        public long EndDate { get => endDate; set => endDate = value; }
     
        public int Point { get => point; set => point = value; }
       
        public Guid CustomerId { get => customerId; set => customerId = value; }
        public Guid IdVoucher { get => idVoucher; set => idVoucher = value; }
    }
}
