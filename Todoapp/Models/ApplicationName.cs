using System;
using Newtonsoft.Json.Linq;

namespace Todoapp.Models
{
    public static class ApplicationName
    {
        public static string AppName { get; set; }

        public static void GetApplicationName()
        {
            //Read the stored config file and get application name from it
            string configFile = System.IO.File.ReadAllText(@"appsettings.json");
            var details = JObject.Parse(configFile);
            AppName = Convert.ToString(details["ApplicationName"]);
        }
    }
}

