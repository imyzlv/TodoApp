using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Todoapp.Controllers;
using Todoapp.Database;

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
        Xunit.Assert.Equal(appName, config.GetAppName());
    }

    [Fact]
    public void HomeController_Privacy()
    {
        //Arrange
        var controller = new HomeController();

        //Act
        var result = controller.Privacy();

        //Assert
        Xunit.Assert.IsAssignableFrom<ViewResult>(result);
    }

//    [Fact]
//    public void ToDoListController_EditTask_Valid()
//    {
//        //Arrange
//        var mocktoDoTask = new ToDoList()
//        {
//            Id = 1,
//            Title = "Tīrīt",
//            TaskText = "Satīrīt galdu",
//            TaskList = "Mājas",
//            TaskLevel = ToDoList.TaskLevelEnum.Low,
//            DateTimeFinal = DateTime.Parse("2022-6-30"),
//            TaskDone = false
//        },

//        var mock = new MockPlayerService().MockGetByID(mockPlayer);

//        var controller = new ToDoListController(new MockLeagueService().Object,
//                                              mockPlayerService.Object);

//        //Act
//        var result = controller.Index(1); //ID doesn't matter

//        //Assert
//        Assert.IsAssignableFrom<ViewResult>(result);
//        mockPlayerService.VerifyGetByID(Times.Once());
//    }
//}
//    [Fact]
//    public void ToDoListController_EditTask_Post_NotUserIdentityIsAuthenticated()
//    {
//        //Arrange
//        var toDoTask = new ToDoList()
//        {
//            Title = "Tīrīt",
//            TaskText = "Satīrīt galdu",
//            TaskList = "Mājas",
//            TaskLevel = ToDoList.TaskLevelEnum.Low,
//            DateTimeFinal = DateTime.Parse("2022-6-30"),
//            TaskDone = false
//        },

//        //var mockTeamService = new MockTeamService().MockEdit(toDoTask);

//        var controller = new ToDoListController(toDoTask);
//        controller.ModelState.AddModelError("Test", "Test");

//        //Act
//        var result = controller.EditTask(new ());

//        //Assert
//        Assert.IsAssignableFrom<RedirectToActionResult>(result);
//        mockTeamService.VerifySearch(Times.Never());

//        var redirectToAction = (RedirectToActionResult)result; //See note below.
//        Assert.Equal("Search", redirectToAction.ActionName);
//    }
}
