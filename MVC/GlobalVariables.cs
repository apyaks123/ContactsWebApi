using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http;

namespace MVC
{
    public static  class GlobalVariables
    {

        public static HttpClient WebApiClient = new HttpClient();


        static GlobalVariables()
        {
            WebApiClient.BaseAddress = new Uri("https://webapimvc.azurewebsites.net/api/");
            WebApiClient.DefaultRequestHeaders.Clear();
            WebApiClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}