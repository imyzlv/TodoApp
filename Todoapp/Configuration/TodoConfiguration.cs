using System;
namespace Todoapp.Configuration
{
    public class TodoConfiguration
    {
        private readonly IConfiguration _config;

        public TodoConfiguration(IConfiguration config)
        {
            _config = config;
        }

        public string GetAppName()
        {
            return _config["ApplicationName"];
        }
    }
}

