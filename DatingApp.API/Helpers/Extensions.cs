using System;
using Microsoft.AspNetCore.Http;

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

        public static int WyliczWiek(this DateTime theDateTime)
        {
            var wiek = DateTime.Today.Year - theDateTime.Year;
            if (theDateTime.AddYears(wiek) > DateTime.Today)
                wiek--;
            
            return wiek;
        }
    }
}