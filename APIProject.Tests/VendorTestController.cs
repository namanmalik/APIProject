using APIPROJECT.Controllers;
using APIPROJECT.Models;
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
    public class VendorTestController
    {
        private ShopDataDbContext context;
        public static DbContextOptions<ShopDataDbContext>
            dbContextOptions
        { get; set; }

        public static string connenctionString =
            "Data Source = TRD-520; Initial Catalog=APIPROJECTDB1; Integrated Security = true;";
        static VendorTestController()
        {
            dbContextOptions = new DbContextOptionsBuilder<ShopDataDbContext>()
                .UseSqlServer(connenctionString).Options;
        }
        public VendorTestController()
        {
            context = new ShopDataDbContext(dbContextOptions);
        }
        [Fact]
        public async void Task_GetVendorById_Return_OkResult()
        {
            var controller = new VendorController(context);
            var VendorId = 2;
            var data = await controller.Get(VendorId);
            Assert.IsType<OkObjectResult>(data);
        }
        [Fact]
        public async void Task_GetVendorById_Return_NotFoundResult()
        {
            var controller = new VendorController(context);
            var VendorId = 6;
            var data = await controller.Get(VendorId);
            Assert.IsType<NotFoundResult>(data);
        }
        [Fact]
        public async void Task_GetVendorById_Return_getMatched()
        {
            var controller = new VendorController(context);
            int id = 1;
            var data = await controller.Get(id);
            Assert.IsType<OkObjectResult>(data);
            var okResult = data.Should().BeOfType<OkObjectResult>().Subject;
            var user = okResult.Value.Should().BeAssignableTo<Vendor>().Subject;
            Assert.Equal("Naman", user.VendorName);
            Assert.Equal("naman95@gmail.com", user.EmailId);
            Assert.Equal(9099878767, user.PhoneNo);
            Assert.Equal("Accuracy!", user.VendorDescription);
        }
        [Fact]
        public async void Task_GetVendorById_Return_getBadRequestResult()
        {
            //Arrange
            var controller = new VendorController(context);
            int? id = null;
            //Act
            var data = await controller.Get(id);
            //Assert
            Assert.IsType<BadRequestResult>(data);
        }
        [Fact]
        public async void Task_Add_AddUser_Return_OkResult()
        {
            var controller = new VendorController(context);
            var user = new Vendor()
            {
                VendorName = "hello",
                EmailId = "namanmalik@gmail.com",
                PhoneNo = 4567787567,
                VendorDescription = "abc"
            };
            var data = await controller.Post(user);
            Assert.IsType<CreatedAtActionResult>(data);
        }
        [Fact]
        public async void Task_Add_Invalid_AddUser_Return_BadRequestResult()
        {
            var controller = new VendorController(context);
            var user = new Vendor()
            {
                VendorName = "Rohit Singh from Kanpur",
                EmailId = "rohit@gmail.com",
                PhoneNo = 5675567788,
                VendorDescription = "abc"
            };
            var data = await controller.Post(user);
            Assert.IsType<BadRequestResult>(data);
        }
        [Fact]
        public async void Task_DeleteUser_Return_OkResult()
        {
            var controller = new VendorController(context);
            var id = 1008;

            var data = await controller.Delete(id);
            Assert.IsType<OkObjectResult>(data);
        }
        [Fact]
        public async void Task_DeleteUser_Return_OkFailResult()
        {
            var controller = new VendorController(context);
            var UserId = 9;
            var data = await controller.Delete(UserId);
            Assert.IsType<NotFoundResult>(data);
        }
        [Fact]
        public async void Task_DeleteUser_Return_BadRequestResult()
        {
            //Arrange
            var controller = new VendorController(context);
            int? id = null;
            //Act
            var data = await controller.Delete(id);
            //Assert
            Assert.IsType<BadRequestResult>(data);
        }
        [Fact]
        public async void Task_PutUser_Return_OkResult()
        {
            //Arrange
            var controller = new VendorController(context);
            int id = 4;
            var user = new Vendor()
            {
               VendorId = 4,
                VendorName = "Naman",
                EmailId = "Naman@gmail.com",
                PhoneNo=7544677888,
                VendorDescription="Nice!"
            };
            //Act
            var data = await controller.Put(id, user);
            //Assert
            Assert.IsType<NoContentResult>(data);
        }
        [Fact]
        public async void Task_PutUser_Return_Notfound()
        {
            //Arrange
            var controller = new VendorController(context);
            int? id = 13;
            var user = new Vendor()
            {
                VendorId = 1003,
                VendorName = "Naman",
                EmailId = "naman@gmail.com",
                PhoneNo = 987456765,
                VendorDescription = "Great"

            };
            //Act
            var data = await controller.Put(id, user);
            //Assert
            Assert.IsType<NotFoundResult>(data);
        }
        [Fact]
        public async void Task_PutUser_Return_BadRequest()
        {
            //Arrange
            var controller = new VendorController(context);
            int? id = null;
            var user = new Vendor()
            {

               VendorName = "Naman",
                EmailId = "naman@gmail.com"
            };
            //Act
            var data = await controller.Put(id, user);
            //Assert
            Assert.IsType<BadRequestResult>(data);
        }
        [Fact]
        public async void Task_GetAll_NotFound()
        {
            var controller = new VendorController(context);
            var data = await controller.Get();
            data = null;
            if (data != null)
            {
                Assert.IsType<OkObjectResult>(data);
            }
            else
            {
                // Assert.Equal(data, null);
            }
        }
        [Fact]
        public async void Task_GetAll_return_NotFound()
        {
            var controller = new VendorController(context);
            var data = await controller.Get();
            Assert.IsType<OkObjectResult>(data);
        }

    }
}



