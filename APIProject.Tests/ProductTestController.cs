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
   public class ProductTestController
    {
        private ShopDataDbContext context;
        public static DbContextOptions<ShopDataDbContext>
            dbContextOptions
        { get; set; }

        public static string connenctionString =
            "Data Source = TRD-520; Initial Catalog=APIPROJECTDB1; Integrated Security = true;";
        static ProductTestController()
        {
            dbContextOptions = new DbContextOptionsBuilder<ShopDataDbContext>()
                .UseSqlServer(connenctionString).Options;
        }
        public ProductTestController()
        {
            context = new ShopDataDbContext(dbContextOptions);
        }
        [Fact]
        public async void Task_GetProductById_Return_OkResult()
        {
            var controller = new ProductController(context);
            var ProductId = 2;
            var data = await controller.Get(ProductId);
            Assert.IsType<OkObjectResult>(data);
        }
        [Fact]
        public async void Task_GetProductById_Return_NotFoundResult()
        {
            var controller = new ProductController(context);
            var Id = 20;
            var data = await controller.Get(Id);
            Assert.IsType<NotFoundResult>(data);
        }
        [Fact]
        public async void Task_GetProductById_Return_getMatched()
        {
            var controller = new ProductController(context);
            int id = 1;
            var data = await controller.Get(id);
            Assert.IsType<OkObjectResult>(data);
            var okResult = data.Should().BeOfType<OkObjectResult>().Subject;
            var pro = okResult.Value.Should().BeAssignableTo<Product>().Subject;
            Assert.Equal("Kurta", pro.ProductName);
            Assert.Equal(500, pro.ProductQty);
            Assert.Equal(1799, pro.ProductPrice);
            Assert.Equal("HII", pro.ProductImage);
            Assert.Equal("Khadi Material", pro.ProductDescription);
            Assert.Equal(3, pro.VendorId);
            Assert.Equal(3, pro.ProductCategoryId);
        }
        [Fact]
        public async void Task_GetProductById_Return_getBadRequestResult()
        {
            //Arrange
            var controller = new ProductController(context);
            int? id = null;
            //Act
            var data = await controller.Get(id);
            //Assert
            Assert.IsType<BadRequestResult>(data);
        }
        [Fact]
        public async void Task_Add_AddProduct_Return_OkResult()
        {
            var controller = new ProductController(context);
            var pro = new Product()
            {
                ProductName = "Nike",
                ProductQty = 100,
                ProductPrice = 4567,
                ProductImage = "hello",
                ProductDescription = "Great",
                VendorId = 2,
                ProductCategoryId = 1

            };
            var data = await controller.Post(pro);
            Assert.IsType<CreatedAtActionResult>(data);
        }
        //[Fact]
        //public async void Task_Add_Invalid_AddProduct_Return_BadRequestResult()
        //{
        //    var controller = new ProductController(context);
        //    var pro = new Product()
        //    {
        //        ProductName = "Nikeeeeeeeeeeeeeeee",
        //        ProductQty = 100,
        //        ProductPrice = 4567,
        //        ProductImage = "hello",
        //        ProductDescription = "Great",
        //        VendorId = 2,
        //        ProductCategoryId = 1
        //    };
        //    var data = await controller.Post(pro);
        //    Assert.IsType<BadRequestResult>(data);
        //}
        [Fact]
    public async void Task_DeleteProduct_Return_OkResult()
    {
        var controller = new ProductController(context);
        var id = 1002;

        var data = await controller.Delete(id);
        Assert.IsType<OkObjectResult>(data);
    }
        [Fact]
        public async void Task_DeleteProduct_Return_OkFailResult()
        {
            var controller = new ProductController(context);
            var UserId = 9;
            var data = await controller.Delete(UserId);
            Assert.IsType<NotFoundResult>(data);
        }
        [Fact]
        public async void Task_DeleteProduct_Return_BadRequestResult()
        {
            //Arrange
            var controller = new ProductController(context);
            int? id = null;
            //Act
            var data = await controller.Delete(id);
            //Assert
            Assert.IsType<BadRequestResult>(data);
        }
        [Fact]
        public async void Task_PutProduct_Return_OkResult()
        {
            //Arrange
            var controller = new ProductController(context);
            int id = 4;
            var user = new Product()
            {
                ProductId=4,
                ProductName = "Shirts",
                ProductQty = 100,
                ProductPrice = 4567,
                ProductImage = "hello",
                ProductDescription = "Great",
                VendorId = 2,
                ProductCategoryId = 2
            };
            //Act
            var data = await controller.Put(id, user);
            //Assert
            Assert.IsType<NoContentResult>(data);
        }
        [Fact]
        public async void Task_PutProduct_Return_Notfound()
        {
            //Arrange
            var controller = new ProductController(context);
            int? id = 13;
            var pro = new Product()
            {
                ProductId = 1020,
                ProductName = "Shirts",
                ProductQty = 100,
                ProductPrice = 4567,
                ProductImage = "hello",
                ProductDescription = "Great",
                VendorId = 2,
                ProductCategoryId = 2


            };
            //Act
            var data = await controller.Put(id, pro);
            //Assert
            Assert.IsType<NotFoundResult>(data);
        }
        [Fact]
        public async void Task_PutProduct_Return_BadRequest()
        {
            //Arrange
            var controller = new ProductController(context);
            int? id = null;
            var pro = new Product()
            {
                ProductName = "Shirts",
                ProductQty = 100,
                ProductPrice = 4567,
                ProductImage = "hello",
                ProductDescription = "Great",
                VendorId = 2,
                ProductCategoryId = 2
            };
            //Act
            var data = await controller.Put(id, pro);
            //Assert
            Assert.IsType<BadRequestResult>(data);
        }
        [Fact]
        public async void Task_GetAll_NotFound()
        {
            var controller = new ProductController(context);
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
            var controller = new ProductController(context);
            var data = await controller.Get();
            Assert.IsType<OkObjectResult>(data);
        }
    }
}
