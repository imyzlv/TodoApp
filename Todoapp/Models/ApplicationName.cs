﻿using System;
using Newtonsoft.Json.Linq;

namespace Todoapp.Models
{
    public static class ApplicationName
    {
        public static string AppName { get; set; }

        public static void GetApplicationName()
        {
            //Read the stored config file and get application name from it
            string configFilePath = @"appsettings.json";
            if (File.Exists(configFilePath))
            {
                string configFileContents = System.IO.File.ReadAllText(configFilePath);
                var details = JObject.Parse(configFileContents);
                AppName = Convert.ToString(details["ApplicationName"]);
            }
        }
    }
}

