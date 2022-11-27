using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Shared.ViewModels;

namespace Travel.Data.Interfaces.INotify
{
    public interface INotification
    {
        Task<Response> Get(Guid idEmployee);
        Task<Response> UpdateIsSeen(Guid idNotification);
    }
}
