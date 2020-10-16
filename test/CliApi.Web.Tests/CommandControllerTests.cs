using System;
using CliApi.Web.Models;
using Xunit;
using System.Collections.Generic;
using Moq;
using AutoMapper;
using CliApi.Web.Models;
using CliApi.Web.Data;
using Microsoft.EntityFrameworkCore;
using CliApi.Web.Controllers;

namespace CliApi.Web.Tests
{
    public class CommandControllerTests
    {
        DbContextOptionsBuilder<CommandContext> optionsBuilder;
        CommandContext dbContext;
        CommandsController controller;

        public CommandControllerTests()
        {

        }
        [Fact]
        public void GetCommands_ReturnsZeroItems_WhenDBIsEmpty()
        {

        }
    }
}
