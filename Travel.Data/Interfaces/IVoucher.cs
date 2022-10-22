﻿using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Shared.ViewModels;
using Travel.Shared.ViewModels.Travel.VoucherVM;

namespace Travel.Data.Interfaces
{
   public interface IVoucher
    {
        string CheckBeforSave(JObject frmData, ref Notification _message, bool isUpdate);
        Response GetsVoucher(bool isDelete);
       // Response SearchRole(JObject frmData);
        Response CreateVoucher(CreateVoucherViewModel input);
        Response UpdateVoucher(UpdateVoucherViewModel input);
        Response DeleteVoucher(int id);
        Response RestoreVoucher(int id);
    }
}
