using Microsoft.AspNetCore.Http;
using System;

namespace DatingApp.API.Helper
{
    public static class Extensionscs
    {
        public static void AddApplicationError(this HttpResponse response, string message){
            response.Headers.Add("Application-error",message);
            response.Headers.Add("Access-Control-Expose-Headers","Application-Error");
            response.Headers.Add("Access-Control-Allow-Origin","*");
        }
        public static int CalculateAge(this DateTime thedateTime)
        {
            var age = DateTime.Today.Year - thedateTime.Year;
            if (thedateTime.AddYears(age)> DateTime.Today)
            {
                age--;
            }
            return age;
        }
    }
}