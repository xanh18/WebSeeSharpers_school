using WebApplication2.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSeeSharpers.Controllers;
using WebSeeSharpers.Data;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FluentAssertions;
using WebSeeSharpers.Models;

namespace WebApplication2.Controllers.Tests
{
    public class LostItemsControllerTests
    {
        [Fact]
        public async Task Index_Test_Async()
        {
            //arragen
            var contextOptionsBuilder = new DbContextOptionsBuilder<WebSeeSharpersContext>();
            contextOptionsBuilder.UseInMemoryDatabase("MyDb-1");

            var context = new WebSeeSharpersContext(contextOptionsBuilder.Options);

            context.LostItems.Add(new LostItem { Id = 1, Description = "verloren schoen  zaal 2", Item = "context" });
            context.SaveChanges();

            var controller = new LostItemsController(context);

            //act
            var result = await controller.Index();

            //assert
            result.Should().BeAssignableTo<ViewResult>();

            var viewResult = (ViewResult)result;
            viewResult.Model.Should().BeAssignableTo<IEnumerable<LostItem>>();

            var items = (IEnumerable<LostItem>)viewResult.Model;

            items.Should().HaveCount(1);

        }

        [Fact]
        public async Task Details_Test_Async()
        {

            //arrage
            var contextOptionsBuilder = new DbContextOptionsBuilder<WebSeeSharpersContext>();
            contextOptionsBuilder.UseInMemoryDatabase("MyDb-2");

            var context = new WebSeeSharpersContext(contextOptionsBuilder.Options);

            context.LostItems.Add(new LostItem { Id = 12, Description = "Somethign else", Item = "coat"});
            context.LostItems.Add(new LostItem { Id = 15, Description = "Blue coat found in room 5.", Item = "coat"});
            context.SaveChanges();

            var controller = new LostItemsController(context);


            //act
            var result = await controller.Details(15);

            //assert

            result.Should().BeAssignableTo<ViewResult>();

            var viewResult = (ViewResult)result;
            viewResult.Model.Should().BeAssignableTo<LostItem>();

            var item = (LostItem)viewResult.Model;
            item.Id.Should().Be(15);
            item.Description.Should().Be("Blue coat found in room 5.");
            item.Item.Should().Be("coat");
        }

        [Fact]
        public async Task Details_ReturnNotFound_IfIdNotInDB()
        {

            //arrage
            var contextOptionsBuilder = new DbContextOptionsBuilder<WebSeeSharpersContext>();
            contextOptionsBuilder.UseInMemoryDatabase("MyDb-454");

            var context = new WebSeeSharpersContext(contextOptionsBuilder.Options);
            var controller = new LostItemsController(context);


            //act
            var notFound = await controller.Details(1546);

            //assert
            notFound.Should().BeAssignableTo<NotFoundResult>();
        }

        [Fact]
        public async Task Details_ReturnNotFound_IfInputIsNull()
        {
            //arrage
            var controller = new LostItemsController(null);

            //act
            var notFound = await controller.Details(null);

            //assert
            notFound.Should().BeAssignableTo<NotFoundResult>();
        }

        [Fact]
        public async Task Create_Test_Async()
        {

            var contextOptionsBuilder = new DbContextOptionsBuilder<WebSeeSharpersContext>();
            contextOptionsBuilder.UseInMemoryDatabase("MyDb-3");

            var context = new WebSeeSharpersContext(contextOptionsBuilder.Options);

            var controller = new LostItemsController(context);

            var itemToInsert = new LostItem
            {
                Item = "Kapstok",
            };
            var result = await controller.Create(itemToInsert);

            var savedItem = context.LostItems.FirstOrDefault(x => x.Item == "Kapstok");
            savedItem.Should().NotBeNull();
            context.LostItems.Count().Should().Be(1);

        }

        [Fact]
        public async Task Delete_TestAsync()
        {
            //arrage
            var contextOptionsBuilder = new DbContextOptionsBuilder<WebSeeSharpersContext>();
            contextOptionsBuilder.UseInMemoryDatabase("MyDb-44");

            var context = new WebSeeSharpersContext(contextOptionsBuilder.Options);

            var controller = new LostItemsController(context);


            context.LostItems.Add(new LostItem { Id = 12, Description = "Somethign else", Item = "coat", TimeFound = DateTime.Now });


            //act
            var result = await controller.Delete(12);

            //assert
            result.Should().BeAssignableTo<NotFoundResult>();

        }
    }
}