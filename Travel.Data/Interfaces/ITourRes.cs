using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Shared.ViewModels;
using Travel.Shared.ViewModels.Travel.TourVM;

namespace Travel.Data.Interfaces
{
   public  interface ITourRes
    {
        Response Create(CreateTourViewModel input);

    }
}
