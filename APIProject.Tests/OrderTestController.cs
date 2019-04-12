using APIPROJECT.Controllers;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace APIProject.Tests
{
   public class OrderTestController
    {
        private ShopDataDbContext context;
        public static DbContextOptions<ShopDataDbContext>
            dbContextOptions
        { get; set; }

        public static string connenctionString =
            "Data Source = TRD-520; Initial Catalog=APIPROJECTDB1; Integrated Security = true;";
        static OrderTestController()
        {
            dbContextOptions = new DbContextOptionsBuilder<ShopDataDbContext>()
                .UseSqlServer(connenctionString).Options;
        }
        public OrderTestController()
        {
            context = new ShopDataDbContext(dbContextOptions);
        }
        [Fact]
        public async void Task_GetOrderById_Return_OkResult()
        {
            var controller = new OrdersController(context);
            var Id = 2;
            var data = await controller.GetOrder(Id);
            Assert.IsType<OkObjectResult>(data);
        }
        [Fact]
        public async void Task_GetOrderById_Return_NotFoundResult()
        {
            var controller = new ProductController(context);
            var Id = 20;
            var data = await controller.Get(Id);
            Assert.IsType<NotFoundResult>(data);
        }
        //[Fact]
        //public async void Task_GetOrderById_Return_getMatched()
        //{
        //    var controller = new ProductController(context);
        //    int id = 1;
        //    var data = await controller.Get(id);
        //    Assert.IsType<OkObjectResult>(data);
        //    var okResult = data.Should().BeOfType<OkObjectResult>().Subject;
        //    var pro = okResult.Value.Should().BeAssignableTo<Product>().Subject;
        //    Assert.Equal("Kurta", pro.ProductName);
        //    Assert.Equal(500, pro.ProductQty);
        //    Assert.Equal(1799, pro.ProductPrice);
        //    Assert.Equal("HII", pro.ProductImage);
        //    Assert.Equal("Khadi Material", pro.ProductDescription);
        //    Assert.Equal(3, pro.VendorId);
        //    Assert.Equal(3, pro.ProductCategoryId);
        //}
        //[Fact]
        //public async void Task_GetProductById_Return_getBadRequestResult()
        //{
        //    //Arrange
        //    var controller = new ProductController(context);
        //    int? id = null;
        //    //Act
        //    var data = await controller.Get(id);
        //    //Assert
        //    Assert.IsType<BadRequestResult>(data);
        //}
    }
}
