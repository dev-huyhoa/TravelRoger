using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Context.Models;
using Travel.Shared.ViewModels;

namespace Travel.Data.Interfaces
{
    public interface IAuthentication
    {
        Employee EmpLogin(string email);
        Employee EmpLogin(string email, string password);
        bool EmpAddToken(string token, Guid idEmp);
        bool EmpActive(string email);
        bool EmpIsNew(string email);
        bool EmpDeleteToken(string Id);

        Customer CusLogin(string email);
        Customer CusLogin(string email, string password);
        Response CusDeleteToken(Guid idCus);
        bool CreateAccountGoogle(Customer cus);
        bool CusAddToken(string token, Guid idCus);
        bool CusAddTokenGoogle(string token, Guid idCus);
        string Encryption(string password);
    }
}
