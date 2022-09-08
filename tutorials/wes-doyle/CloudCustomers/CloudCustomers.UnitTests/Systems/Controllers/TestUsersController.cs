using System.Collections.Generic;

using CloudCustomers.Api.Controllers;
using CloudCustomers.Api.UseCases;
using CloudCustomers.Api.Entities;

using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace CloudCustomers.UnitTests;

public class TestUsersController
{
    [Fact]
    async void GetUsers_OnSucess_Returns200()
    {
        var mock = new Mock<IUserUseCases>();
        mock.Setup(m => m.GetAllUsers().Result).Returns(new List<User>() { new User() { Name="John Doe" } });
        // Given
        var controller = new UsersController(mock.Object);
        // When
        var result = (OkObjectResult) await controller.GetUsers();
        // Then
        result.StatusCode.Should().Be(200);
    }

    [Fact]
    async void GetUsers_OnSuccess_InvokeUsersUseCases()
    {
        var mock = new Mock<IUserUseCases>();
        mock.Setup(m => m.GetAllUsers().Result).Returns(new List<User>() { new User() { Name="John Doe" } });
        // Given
        var controller = new UsersController(mock.Object);
        // When
        var result = (OkObjectResult) await controller.GetUsers();
        // Then
        mock.Verify(userUseCases => userUseCases.GetAllUsers(), Times.Once());
    }

    [Fact]
    async void GetUsers_OnSuccess_GetListOfUsers()
    {
        var mock = new Mock<IUserUseCases>();
        mock.Setup(m => m.GetAllUsers().Result).Returns(new List<User>() { new User() { Name="John Doe" } });
        // Given
        var controller = new UsersController(mock.Object);
        // When
        var result = (OkObjectResult) await controller.GetUsers();
        // Then
        result.Value.Should().NotBe(null);
        result.Value.Should().BeOfType<List<User>>();
    }

    [Fact]
    async void GetUsers_OnNoUsersFound_Returs204()
    {
        var mock = new Mock<IUserUseCases>();
        mock.Setup(m => m.GetAllUsers().Result).Returns(new List<User>());
        // Given
        var controller = new UsersController(mock.Object);
        // When
        var result = (NoContentResult) await controller.GetUsers();
        // Then
        result.StatusCode.Should().Be(204);
    }
}
