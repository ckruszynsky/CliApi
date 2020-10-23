using System;

using Xunit;
using System.Collections.Generic;
using Moq;
using AutoMapper;
using CliApi.Core.Data;
using CliApi.Core.Domain.Models;
using CliApi.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using CliApi.Core.Application.Commands;

namespace CliApi.Web.Tests
{
    public class CommandControllerTests : IDisposable
    {

        Mock<ICommandRepository> mockRepo;
        CommandsProfile realProfile;
        MapperConfiguration configuration;
        IMapper mapper;

        public CommandControllerTests()
        {
            mockRepo = new Mock<ICommandRepository>();
            realProfile = new CommandsProfile();
            configuration = new MapperConfiguration(cfg => cfg.AddProfile(realProfile));
            mapper = new Mapper(configuration);
        }

        public void Dispose()
        {
            mockRepo = null;
            mapper = null;
            configuration = null;
            realProfile = null;
        }

        //**************************************************
        //*
        //GET   /api/commands Unit Tests
        //*
        //**************************************************
        [Fact]
        public void GetAll_ReturnsZeroResources_WhenDBIsEmpty()
        {
            //Arrange 
            mockRepo.Setup(repo => repo.GetAll()).Returns(GetByIds(0));

            var controller = new CommandsController(mockRepo.Object, mapper);

            //Act
            var result = controller.GetAll();

            //Assert
            Assert.IsType<OkObjectResult>(result.Result);

        }

        [Fact]
        public void GetAll_ReturnsOneResource_WhenDBHasOneResource()
        {
            //Arrange 
            mockRepo.Setup(repo =>
              repo.GetAll()).Returns(GetByIds(1));

            var controller = new CommandsController(mockRepo.Object, mapper);

            //Act
            var result = controller.GetAll();

            //Assert
            //  var okResult = result.Result as OkObjectResult;

            //var commands = okResult.Value as CommandEnvelope;

            //Assert.Single(commands);
        }

        [Fact]
        public void GetAll_Returns200OK_WhenDBHasOneResource()
        {
            //Arrange 
            mockRepo.Setup(repo =>
              repo.GetAll()).Returns(GetByIds(1));

            var controller = new CommandsController(mockRepo.Object, mapper);

            //Act
            var result = controller.GetAll();

            //Assert
            Assert.IsType<OkObjectResult>(result.Result);

        }

        [Fact]
        public void GetAll_ReturnsCorrectType_WhenDBHasOneResource()
        {
            //Arrange 
            mockRepo.Setup(repo =>
              repo.GetAll()).Returns(GetByIds(1));

            var controller = new CommandsController(mockRepo.Object, mapper);

            //Act
            var result = controller.GetAll();

            //Assert
            Assert.IsType<ActionResult<IEnumerable<CommandDto>>>(result);
        }

        //**************************************************
        //*
        //GET   /api/commands/{id} Unit Tests
        //*
        //**************************************************
        [Fact]
        public void GetById_Returns404NotFound_WhenNonExistentIDProvided()
        {
            //Arrange 
            mockRepo.Setup(repo => repo.GetById(0)).Returns(() => null);
            var controller = new CommandsController(mockRepo.Object, mapper);

            //Act
            var result = controller.GetById(1);

            //Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public void GetById_Returns200OK__WhenValidIDProvided()
        {
            //Arrange 
            mockRepo.Setup(repo =>
              repo.GetById(1)).Returns(new Command { Id = 1, HowTo = "mock", Platform = "Mock", CommandLine = "Mock" });

            var controller = new CommandsController(mockRepo.Object, mapper);

            //Act
            var result = controller.GetById(1);

            //Assert
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public void GetByID_ReturnsCorrectResouceType_WhenValidIDProvided()
        {
            //Arrange 
            mockRepo.Setup(repo =>
              repo.GetById(1)).Returns(new Command { Id = 1, HowTo = "mock", Platform = "Mock", CommandLine = "Mock" });

            var controller = new CommandsController(mockRepo.Object, mapper);

            //Act
            var result = controller.GetById(1);

            //Assert
            Assert.IsType<ActionResult<CommandDto>>(result);
        }

        //**************************************************
        //*
        //POST   /api/commands/ Unit Tests
        //*
        //**************************************************        
        [Fact]
        public void Create_ReturnsCorrectResourceType_WhenValidObjectSubmitted()
        {
            //Arrange 
            mockRepo.Setup(repo =>
              repo.GetById(1)).Returns(new Command { Id = 1, HowTo = "mock", Platform = "Mock", CommandLine = "Mock" });

            var controller = new CommandsController(mockRepo.Object, mapper);

            //Act
            var result = controller.Create(new Create.CreateCommandRequest() { });

            //Assert
            Assert.IsType<ActionResult<CommandDto>>(result);
        }
        [Fact]
        public void Create_Returns201Created_WhenValidObjectSubmitted()
        {
            //Arrange 
            mockRepo.Setup(repo =>
              repo.GetById(1)).Returns(new Command { Id = 1, HowTo = "mock", Platform = "Mock", CommandLine = "Mock" });

            var controller = new CommandsController(mockRepo.Object, mapper);

            //Act
            var result = controller.Create(new Create.CreateCommandRequest() { });

            //Assert
            Assert.IsType<CreatedAtRouteResult>(result.Result);
        }

        //**************************************************
        //*
        //PUT   /api/commands/{id} Unit Tests
        //*
        //**************************************************
        [Fact]
        public void UpdateCommand_Returns204NoContent_WhenValidObjectSubmitted()
        {
            //Arrange 
            mockRepo.Setup(repo =>
              repo.GetById(1)).Returns(new Command { Id = 1, HowTo = "mock", Platform = "Mock", CommandLine = "Mock" });

            var controller = new CommandsController(mockRepo.Object, mapper);

            //Act
            var result = controller.Update(1, new Update.UpdateCommandRequest { });

            //Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void UpdateCommand_Returns404NotFound_WhenNonExistentResourceIDSubmitted()
        {
            //Arrange 
            mockRepo.Setup(repo =>
              repo.GetById(0)).Returns(() => null);

            var controller = new CommandsController(mockRepo.Object, mapper);

            //Act
            var result = controller.Update(0, new Update.UpdateCommandRequest { });

            //Assert
            Assert.IsType<NotFoundResult>(result);
        }

        //**************************************************
        //*
        //PATCH   /api/commands/{id} Unit Tests
        //*
        //**************************************************
        [Fact]
        public void PartialCommandUpdate_Returns404NotFound_WhenNonExistentResourceIDSubmitted()
        {
            //Arrange 
            mockRepo.Setup(repo =>
              repo.GetById(0)).Returns(() => null);

            var controller = new CommandsController(mockRepo.Object, mapper);

            //Act
            var result = controller.PartialCommandUpdate(0, new Microsoft.AspNetCore.JsonPatch.JsonPatchDocument<Update.UpdateCommandRequest> { });

            //Assert
            Assert.IsType<NotFoundResult>(result);
        }

        //**************************************************
        //*
        //DELETE   /api/commands/{id} Unit Tests
        //*
        //**************************************************

        [Fact]
        public void Delete_Returns200OK_WhenValidResourceIDSubmitted()
        {
            //Arrange 
            mockRepo.Setup(repo =>
              repo.GetById(1)).Returns(new Command { Id = 1, HowTo = "mock", Platform = "Mock", CommandLine = "Mock" });

            var controller = new CommandsController(mockRepo.Object, mapper);

            //Act
            var result = controller.Delete(1);

            //Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void Delete_Returns_404NotFound_WhenNonExistentResourceIDSubmitted()
        {
            //Arrange 
            mockRepo.Setup(repo =>
              repo.GetById(0)).Returns(() => null);

            var controller = new CommandsController(mockRepo.Object, mapper);

            //Act
            var result = controller.Delete(0);

            //Assert
            Assert.IsType<NotFoundResult>(result);

        }




        //**************************************************
        //*
        //Private Support Methods
        //*
        //**************************************************
        private List<Command> GetByIds(int num)
        {
            var commands = new List<Command>();
            if (num > 0)
            {
                commands.Add(new Command
                {
                    Id = 0,
                    HowTo = "How to genrate a migration",
                    CommandLine = "dotnet ef migrations add <Name of Migration>",
                    Platform = ".Net Core EF"
                });
            }
            return commands;
        }
    }
}
