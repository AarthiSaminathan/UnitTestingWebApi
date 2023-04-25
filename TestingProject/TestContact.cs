using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Win32;
using MySqlX.XDevAPI.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTestingWebApiDemo;
using UnitTestingWebApiDemo.Controllers;
using UnitTestingWebApiDemo.Model;
using Xunit;

namespace TestingProject
{
    public abstract class TestContact
    {
        public TestContact(DbContextOptions<AppDbContext> contextOptions)
        {
            ContextOptions = contextOptions;
            Seed();
        }

        protected DbContextOptions<AppDbContext> ContextOptions { get; }

        private void Seed()
        {
            using (var context = new AppDbContext(ContextOptions))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                var one = new Contact()
                {
                    Name = "Aarthi.S",
                    Email = "aarthi@gmail.com",
                    Phone = "678974323",
                    Address = "Theni"
                };

                var two = new Contact()
                {
                    Name = "Monica.A",
                    Email = "moni@gmail.com",
                    Phone = "6367892124",
                    Address = "Mettupalayam"
                };

                var three = new Contact()
                {
                    Name = "Sandhiya.R",
                    Email = "sandy@gmail.com",
                    Phone = "915324678",
                    Address = "Udumalaipettai"
                };
                context.AddRange(one, two, three);
                context.SaveChanges();
            }
        }
        [Fact]
        public void Test_Read_GET_ReturnsViewResult_WithAListOfContacts()
        {
            using (var context = new AppDbContext(ContextOptions))
            {
                // Arrange
                var controller = new ContactController(context);

                // Act
                var result = controller.GetContact();

                // Assert

                var viewResult = Assert.IsType<OkObjectResult>(result);
                var model = Assert.IsType<List<Contact>>(viewResult.Value);
                Assert.Equal(3, model.Count());


            }
        }

        [Fact]
        public void Test_Create_GET_ReturnsViewResultNullModel()
        {
            using (var context = new AppDbContext(ContextOptions))
            {
                // Arrange
                var controller = new ContactController(context);
                var postId = 2;


                // Act
                var result = controller.GetContactById(postId) as OkObjectResult;

                // Assert
                Assert.IsType<Contact>(result.Value);
                Assert.Equal(postId, (result.Value as Contact).Id);
            }
        }

        

        [Fact]
        public void Add_ValidObjectPassed_ReturnedResponseHasCreatedItem()
        {
            // Arrange
            using (var context = new AppDbContext(ContextOptions))
            {
                var controller = new ContactController(context);

                var testItem = new AddContact()
                {
                    Name = "TamilSelvi",
                    Email = "joytamil@gmail.com",
                    Phone = "636754897",
                    Address = "Vilupuram"
                };
                // Act
                var createdResponse = controller.AddContact(testItem)  as OkObjectResult;
                var item = createdResponse.Value as Contact;
                // Assert
                Assert.IsType<Contact>(item);
                Assert.Equal("TamilSelvi",  item.Name);
                Assert.Equal("joytamil@gmail.com", item.Email);
                Assert.Equal("636754897", item.Phone);
                Assert.Equal("Vilupuram", item.Address);
            }
        }
        
        [Fact]
        public async void Task_Delete_Post_Return_OkResult()
        {
            using (var context = new AppDbContext(ContextOptions))
            {
                // Arrange
                var controller = new ContactController(context);
                var postId = 3;

                //Act
                var actual =  controller.GetContact();
                var actual2 = actual as OkObjectResult;
                var noContentResponse = controller.DeleteContacts(postId);

                //Assert
                var items = Assert.IsType<List<Contact>>(actual2.Value);
                Assert.Equal(3, items.Count);
            }
        }


    }
}




