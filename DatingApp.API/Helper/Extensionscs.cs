using Microsoft.AspNetCore.Http;

namespace DatingApp.API.Helper
{
    public static class Extensionscs
    {
        public static void AddApplicationError(this HttpResponse response, string message){
            response.Headers.Add("Application-error",message);
            response.Headers.Add("Access-Control-Expose-Headers","Application-Error");
            response.Headers.Add("Access-Control-Allow-Origin","*");
        }
    }
}