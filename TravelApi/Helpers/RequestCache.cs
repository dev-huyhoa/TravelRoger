using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelApi.Configs;

namespace TravelApi.Helpers
{
    // vercel.com
    public static class RequestCache
    {
        private static IHttpContextAccessor _httpAccessor = GetConfigItems.HttpContextAccessor;
   
        public static T Get<T>(string key)
        {

        }


        public static void Remove(string key)
        {
            throw new NotImplementedException();
        }

        public static bool Set<T>(T data, string key)
        {
            throw new NotImplementedException();
        }

        public static bool Update<T>(T data, string key)
        {
            throw new NotImplementedException();
        }
    }
}
