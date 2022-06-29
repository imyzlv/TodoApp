using Microsoft.Extensions.Configuration;

namespace Todoapp.Tests;

public class TodoappTests
{
    [Fact]
    public void GetAppName_DuringAppStartup_GetsNameFromConfig()
    {
        // Arrange
        string appName = "Todo application";
        // Act
        // Assert

        //var conf = new ConfigurationBuilder().SetBasePath(AppContext.BaseDirectory).AddJsonFile("appsettings.json", false, true).Build();

        var config = new Todoapp.Configuration.TodoConfiguration(new ConfigurationBuilder().SetBasePath(AppContext.BaseDirectory).AddJsonFile("appsettings.json", false, true).Build());
        Assert.Equal(appName, config.GetAppName());
    }
}
