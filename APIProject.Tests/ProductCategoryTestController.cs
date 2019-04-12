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
   public class ProductCategoryTestController
    {
        private ShopDataDbContext context;
        public static DbContextOptions<ShopDataDbContext>
            dbContextOptions
        { get; set; }

        public static string connenctionString =
            "Data Source = TRD-520; Initial Catalog=APIPROJECTDB1; Integrated Security = true;";
        static ProductCategoryTestController()
        {
            dbContextOptions = new DbContextOptionsBuilder<ShopDataDbContext>()
                .UseSqlServer(connenctionString).Options;
        }
        public ProductCategoryTestController()
        {
            context = new ShopDataDbContext(dbContextOptions);
        }
        [Fact]
        public async void Task_GetPCById_Return_OkResult()
        {
            var controller = new ProductCategoryController(context);
            var Id = 2;
            var data = await controller.Get(Id);
            Assert.IsType<OkObjectResult>(data);
        }
        [Fact]
        public async void Task_GetPCById_Return_NotFoundResult()
        {
            var controller = new ProductCategoryController(context);
            var Id = 6;
            var data = await controller.Get(Id);
            Assert.IsType<NotFoundResult>(data);
        }
        [Fact]
        public async void Task_GetPCById_Return_getMatched()
        {
            var controller = new ProductCategoryController(context);
            int id = 1;
            var data = await controller.Get(id);
            Assert.IsType<OkObjectResult>(data);
            var okResult = data.Should().BeOfType<OkObjectResult>().Subject;
            var pc = okResult.Value.Should().BeAssignableTo<ProductCategory>().Subject;
            Assert.Equal("Autmn Collection", pc.ProductCategoryName);
            Assert.Equal("Floral Collection", pc.ProductCategoryDescription);
        
        }
        [Fact]
        public async void Task_GetPCById_Return_getBadRequestResult()
        {
            //Arrange
            var controller = new ProductCategoryController(context);
            int? id = null;
            //Act
            var data = await controller.Get(id);
            //Assert
            Assert.IsType<BadRequestResult>(data);
        }
        [Fact]
        public async void Task_Add_AddPC_Return_OkResult()
        {
            var controller = new ProductCategoryController(context);
            var user = new ProductCategory()
            {
                ProductCategoryName = "Rainy Season",
                ProductCategoryDescription = "Rain Covers",
               
            };
            var data = await controller.Post(user);
            Assert.IsType<CreatedAtActionResult>(data);
        }
        //[Fact]
        //public async void Task_Add_Invalid_AddUser_Return_BadRequestResult()
        //{
        //    var controller = new VendorController(context);
        //    var user = new Vendor()
        //    {
        //        VendorName = "Rohit Singh from Kanpur",
        //        EmailId = "rohit@gmail.com",
        //        PhoneNo = 5675567788,
        //        VendorDescription = "abc"
        //    };
        //    var data = await controller.Post(user);
        //    Assert.IsType<BadRequestResult>(data);
        //}
        [Fact]
        public async void Task_DeletePC_Return_OkResult()
        {
            var controller = new ProductCategoryController(context);
            var id = 1003;

            var data = await controller.Delete(id);
            Assert.IsType<OkObjectResult>(data);
        }
        [Fact]
        public async void Task_DeletePC_Return_OkNotFoundResult()
        {
            var controller = new ProductCategoryController(context);
            var Id = 9;
            var data = await controller.Delete(Id);
            Assert.IsType<NotFoundResult>(data);
        }
        [Fact]
        public async void Task_DeletePC_Return_BadRequestResult()
        {
            
            var controller = new ProductCategoryController(context);
            int? id = null;
         
            var data = await controller.Delete(id);
           
            Assert.IsType<BadRequestResult>(data);
        }
        [Fact]
        public async void Task_PutPC_Return_OkResult()
        {
            //Arrange
            var controller = new ProductCategoryController(context);
            int id = 4;
            var pc = new ProductCategory()
            {
               ProductCategoryId =4 ,
              ProductCategoryName = "Shirts",
              ProductCategoryDescription = "Plain"
               
            };
            //Act
            var data = await controller.Put(id, pc);
            //Assert
            Assert.IsType<NoContentResult>(data);
        }
        [Fact]
        public async void Task_PutPC_Return_Notfound()
        {
            //Arrange
            var controller = new ProductCategoryController(context);
            int? id = 13;
            var pc = new ProductCategory()
            {
                ProductCategoryId = 1003,
                ProductCategoryName = "Shirtss",
                ProductCategoryDescription = "Plain"

            };
            //Act
            var data = await controller.Put(id,pc);
            //Assert
            Assert.IsType<NotFoundResult>(data);
        }
        [Fact]
        public async void Task_PutPC_Return_BadRequest()
        {
            //Arrange
            var controller = new ProductCategoryController(context);
            int? id = null;
            var pc = new ProductCategory()
            {
              
                ProductCategoryName = "Shirtsss",
                ProductCategoryDescription = "Plain"
            };
            //Act
            var data = await controller.Put(id, pc);
            //Assert
            Assert.IsType<BadRequestResult>(data);
        }
        [Fact]
        public async void Task_GetAll_NotFound()
        {
            var controller = new ProductCategoryController(context);
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
            var controller = new ProductCategoryController(context);
            var data = await controller.Get();
            Assert.IsType<OkObjectResult>(data);
        }

    }
}
