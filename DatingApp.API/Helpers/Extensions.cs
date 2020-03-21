using System;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace DatingApp.API.Helpers
{
    public static class Extensions
    {
        public static void AddApplicationError(this HttpResponse response, string message)
        {
            response.Headers.Add("Application-Error", message);
            response.Headers.Add("Access-Control-Expose-Headers", "Application-Error");
            response.Headers.Add("Access-Control-Allow-Origin", "*");
        }

        public static void DodajPaginacje(this HttpResponse response,
            int domyslnaStrona, int itemsNaStrone, int caloscItems, int caloscStron)
        {
            var naglowkiStron = new NaglowkiStron(domyslnaStrona, itemsNaStrone, caloscItems, caloscStron);
            var camelCaseFormatter = new JsonSerializerSettings();
            camelCaseFormatter.ContractResolver = new CamelCasePropertyNamesContractResolver();
            response.Headers.Add("Naglowki", 
                JsonConvert.SerializeObject(naglowkiStron, camelCaseFormatter));
            response.Headers.Add("Access-Control-Expose-Headers", "Naglowki");
        }

        public static int WyliczWiek(this DateTime theDateTime)
        {
            var wiek = DateTime.Today.Year - theDateTime.Year;
            if (theDateTime.AddYears(wiek) > DateTime.Today)
                wiek--;
            
            return wiek;
        }
    }
}