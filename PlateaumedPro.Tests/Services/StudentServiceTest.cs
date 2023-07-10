using Moq;
using PlateaumedPro.Common;
using PlateaumedPro.Contracts;
using PlateaumedPro.Services;
using PlateaumedPro.Tests.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlateaumedPro.Tests.Services
{
    public class StudentServiceTest : BaseServiceTest
    {
        [Fact]
        public void TestStudentServiceShouldReturnTrueStatusGivenRightInput()
        {
           
            var auditTrailServiceMock = new Mock<IAuditTrailService>();
            var loggerServiceMock = new Mock<ILoggerService>();
            var createOrderService = new StudentServices(MockContext, auditTrailServiceMock.Object);
            var students = PlateaumedProFaker.GetStudents();
            var studentData = students.FirstOrDefault();
            studentData.Id = 0;
            var expextedMessage = "Record created successfully";


            // When
            var order = createOrderService.CreateStudent(studentData).GetAwaiter().GetResult();
            // Then
            Assert.True(order.ResponseType.Status);
            Assert.Equal(order.ResponseType.Message, expextedMessage);
        }

        //[Fact]
        //public void TestCreateOrderServiceShouldReturnTrueStatusGivenRightInputWebsiteAndPaidSearch()
        //{
        //    // Given
        //    var channels = PlateaumedProFaker.GetStudents();
        //    foreach (var channel in channels)
        //    {
        //        MockContext.Channels.Add(channel);
        //    }

        //    var products = PlateaumedProFaker.GetProducts();
        //    foreach (var product in products)
        //    {
        //        MockContext.Products.Add(product);
        //    }

        //    var channelProducts = PlateaumedProFaker.GetChannelProducts();
        //    foreach (var channelProduct in channelProducts)
        //    {
        //        MockContext.ChannelProducts.Add(channelProduct);
        //    }

        //    MockContext.SaveChanges();


        //    var auditTrailServiceMock = new Mock<IAuditTrailService>();
        //    var loggerServiceMock = new Mock<ILoggerService>();
        //    var createOrderService = new CreateOrderService(MockContext, auditTrailServiceMock.Object, loggerServiceMock.Object);
        //    var orders = PlateaumedProFaker.GetOrders();
        //    var paidSearch = orders.Last();
        //    var expextedMessage = "Record created successfully";


        //    // When
        //    var order = createOrderService.CreateOrder(paidSearch).GetAwaiter().GetResult();
        //    // Then
        //    Assert.True(order.ResponseType.Status);
        //    Assert.Equal(order.ResponseType.Message, expextedMessage);
        //}

        //[Fact]
        //public void TestCreateOrderServiceShouldReturnTrueStatusGivenRightInputPaidSearch()
        //{
        //    // Given
        //    var channels = PlateaumedProFaker.GetStudents();
        //    foreach (var channel in channels)
        //    {
        //        MockContext.Channels.Add(channel);
        //    }

        //    var products = PlateaumedProFaker.GetProducts();
        //    foreach (var product in products)
        //    {
        //        MockContext.Products.Add(product);
        //    }

        //    var channelProducts = PlateaumedProFaker.GetChannelProducts();
        //    foreach (var channelProduct in channelProducts)
        //    {
        //        MockContext.ChannelProducts.Add(channelProduct);
        //    }

        //    MockContext.SaveChanges();


        //    var auditTrailServiceMock = new Mock<IAuditTrailService>();
        //    var loggerServiceMock = new Mock<ILoggerService>();
        //    var createOrderService = new CreateOrderService(MockContext, auditTrailServiceMock.Object, loggerServiceMock.Object);
        //    var orders = PlateaumedProFaker.GetOrders();
        //    var paidSearch = orders.Skip(1).First();
        //    var expextedMessage = "Record created successfully";


        //    // When
        //    var order = createOrderService.CreateOrder(paidSearch).GetAwaiter().GetResult();
        //    // Then
        //    Assert.True(order.ResponseType.Status);
        //    Assert.Equal(order.ResponseType.Message, expextedMessage);
        //}

        //[Fact]
        //public void TestCreateOrderServiceShouldReturnFalseWithWrongPartner()
        //{
        //    // Given
        //    var channels = PlateaumedProFaker.GetStudents();
        //    foreach (var channel in channels)
        //    {
        //        MockContext.Channels.Add(channel);
        //    }

        //    var products = PlateaumedProFaker.GetProducts();
        //    foreach (var product in products)
        //    {
        //        MockContext.Products.Add(product);
        //    }

        //    var channelProducts = PlateaumedProFaker.GetChannelProducts();
        //    foreach (var channelProduct in channelProducts)
        //    {
        //        MockContext.ChannelProducts.Add(channelProduct);
        //    }

        //    MockContext.SaveChanges();


        //    var auditTrailServiceMock = new Mock<IAuditTrailService>();
        //    var loggerServiceMock = new Mock<ILoggerService>();
        //    var createOrderService = new CreateOrderService(MockContext, auditTrailServiceMock.Object, loggerServiceMock.Object);
        //    var orders = PlateaumedProFaker.GetOrders();
        //    var website = orders.FirstOrDefault();
        //    website.Partner = "12345678";
        //    var expextedMessage = $"Partner with [{website.Partner}] does not exist";


        //    // When
        //    var order = createOrderService.CreateOrder(website).GetAwaiter().GetResult();
        //    // Then
        //    Assert.False(order.ResponseType.Status);
        //    Assert.Equal(order.ResponseType.Message, expextedMessage);
        //}

        //[Fact]
        //public void TestCreateOrderServiceShouldReturnFalseWithWrongInactivePartner()
        //{
        //    // Given
        //    var channels = PlateaumedProFaker.GetStudents();
        //    foreach (var channel in channels)
        //    {
        //        channel.IsActive = false;
        //        MockContext.Channels.Add(channel);
        //    }

        //    var products = PlateaumedProFaker.GetProducts();
        //    foreach (var product in products)
        //    {
        //        MockContext.Products.Add(product);
        //    }

        //    var channelProducts = PlateaumedProFaker.GetChannelProducts();
        //    foreach (var channelProduct in channelProducts)
        //    {
        //        MockContext.ChannelProducts.Add(channelProduct);
        //    }

        //    MockContext.SaveChanges();


        //    var auditTrailServiceMock = new Mock<IAuditTrailService>();
        //    var loggerServiceMock = new Mock<ILoggerService>();
        //    var createOrderService = new CreateOrderService(MockContext, auditTrailServiceMock.Object, loggerServiceMock.Object);
        //    var orders = PlateaumedProFaker.GetOrders();
        //    var website = orders.FirstOrDefault();
        //    var expextedMessage = $"Partner with [{website.Partner}] is not active";


        //    // When
        //    var order = createOrderService.CreateOrder(website).GetAwaiter().GetResult();
        //    // Then
        //    Assert.False(order.ResponseType.Status);
        //    Assert.Equal(order.ResponseType.Message, expextedMessage);
        //}

        //[Fact]
        //public void TestCreateOrderServiceShouldReturnFalseWithoutLineItems()
        //{
        //    // Given
        //    var channels = PlateaumedProFaker.GetStudents();
        //    foreach (var channel in channels)
        //    {
        //        MockContext.Channels.Add(channel);
        //    }

        //    var products = PlateaumedProFaker.GetProducts();
        //    foreach (var product in products)
        //    {
        //        MockContext.Products.Add(product);
        //    }

        //    var channelProducts = PlateaumedProFaker.GetChannelProducts();
        //    foreach (var channelProduct in channelProducts)
        //    {
        //        MockContext.ChannelProducts.Add(channelProduct);
        //    }

        //    MockContext.SaveChanges();


        //    var auditTrailServiceMock = new Mock<IAuditTrailService>();
        //    var loggerServiceMock = new Mock<ILoggerService>();
        //    var createOrderService = new CreateOrderService(MockContext, auditTrailServiceMock.Object, loggerServiceMock.Object);
        //    var orders = PlateaumedProFaker.GetOrders();
        //    var website = orders.FirstOrDefault();
        //    website.LineItems = new List<OrderLineItemDto>();
        //    var expextedMessage = "No Line item was added to your order";

        //    // When
        //    var order = createOrderService.CreateOrder(website).GetAwaiter().GetResult();

        //    // Then
        //    Assert.False(order.ResponseType.Status);
        //    Assert.Equal(order.ResponseType.Message, expextedMessage);


        //}

        //[Fact]
        //public void TestCreateOrderServiceShouldReturnFalseWhenOrderIdAlreadyExist()
        //{
        //    // Given
        //    var channels = PlateaumedProFaker.GetStudents();
        //    foreach (var channel in channels)
        //    {
        //        MockContext.Channels.Add(channel);
        //    }

        //    var products = PlateaumedProFaker.GetProducts();
        //    foreach (var product in products)
        //    {
        //        MockContext.Products.Add(product);
        //    }

        //    var channelProducts = PlateaumedProFaker.GetChannelProducts();
        //    foreach (var channelProduct in channelProducts)
        //    {
        //        MockContext.ChannelProducts.Add(channelProduct);
        //    }

        //    MockContext.SaveChanges();


        //    var auditTrailServiceMock = new Mock<IAuditTrailService>();
        //    var loggerServiceMock = new Mock<ILoggerService>();
        //    var createOrderService = new CreateOrderService(MockContext, auditTrailServiceMock.Object, loggerServiceMock.Object);
        //    var orders = PlateaumedProFaker.GetOrders();
        //    var website = orders.FirstOrDefault();
        //    var expextedMessage1 = "Record created successfully";
        //    var expextedMessage2 = $"Order with OrderId: [{website.OrderId}] already exist";

        //    // When
        //    var order1 = createOrderService.CreateOrder(website).GetAwaiter().GetResult();
        //    var order2 = createOrderService.CreateOrder(website).GetAwaiter().GetResult();
        //    // Then
        //    Assert.True(order1.ResponseType.Status);
        //    Assert.Equal(order1.ResponseType.Message, expextedMessage1);

        //    Assert.False(order2.ResponseType.Status);
        //    Assert.Equal(order2.ResponseType.Message, expextedMessage2);
        //}

        //[Fact]
        //public void TestCreateOrderServiceShouldReturnFalseWithWrongProduct()
        //{
        //    // Given
        //    var channels = PlateaumedProFaker.GetStudents();
        //    foreach (var channel in channels)
        //    {
        //        MockContext.Channels.Add(channel);
        //    }

        //    var products = PlateaumedProFaker.GetProducts();
        //    foreach (var product in products)
        //    {
        //        MockContext.Products.Add(product);
        //    }

        //    var channelProducts = PlateaumedProFaker.GetChannelProducts();
        //    foreach (var channelProduct in channelProducts)
        //    {
        //        MockContext.ChannelProducts.Add(channelProduct);
        //    }

        //    MockContext.SaveChanges();


        //    var auditTrailServiceMock = new Mock<IAuditTrailService>();
        //    var loggerServiceMock = new Mock<ILoggerService>();
        //    var createOrderService = new CreateOrderService(MockContext, auditTrailServiceMock.Object, loggerServiceMock.Object);
        //    var orders = PlateaumedProFaker.GetOrders();
        //    var website = orders.FirstOrDefault();
        //    website.LineItems.FirstOrDefault().ProductID = "FakeProduct";
        //    var expextedMessage = $"Product with ProductId [{website.LineItems.FirstOrDefault().ProductID}] does not exist";

        //    // When
        //    var order = createOrderService.CreateOrder(website).GetAwaiter().GetResult();

        //    // Then
        //    Assert.False(order.ResponseType.Status);
        //    Assert.Equal(order.ResponseType.Message, expextedMessage);


        //}

        //[Fact]
        //public void TestCreateOrderServiceShouldReturnFalseWithInactiveProduct()
        //{
        //    // Given
        //    var channels = PlateaumedProFaker.GetStudents();
        //    foreach (var channel in channels)
        //    {
        //        MockContext.Channels.Add(channel);
        //    }

        //    var products = PlateaumedProFaker.GetProducts();
        //    foreach (var product in products)
        //    {
        //        product.IsActive = false;
        //        MockContext.Products.Add(product);
        //    }

        //    var channelProducts = PlateaumedProFaker.GetChannelProducts();
        //    foreach (var channelProduct in channelProducts)
        //    {
        //        MockContext.ChannelProducts.Add(channelProduct);
        //    }

        //    MockContext.SaveChanges();


        //    var auditTrailServiceMock = new Mock<IAuditTrailService>();
        //    var loggerServiceMock = new Mock<ILoggerService>();
        //    var createOrderService = new CreateOrderService(MockContext, auditTrailServiceMock.Object, loggerServiceMock.Object);
        //    var orders = PlateaumedProFaker.GetOrders();
        //    var website = orders.FirstOrDefault();
        //    var expextedMessage = $"Product with ProductId [{website.LineItems.FirstOrDefault().ProductID}] is not active";

        //    // When
        //    var order = createOrderService.CreateOrder(website).GetAwaiter().GetResult();

        //    // Then
        //    Assert.False(order.ResponseType.Status);
        //    Assert.Equal(order.ResponseType.Message, expextedMessage);


        //}

        //[Fact]
        //public void TestCreateOrderServiceShouldReturnFalseWhenProductTypeDoesNotMatch()
        //{
        //    // Given
        //    var channels = PlateaumedProFaker.GetStudents();
        //    foreach (var channel in channels)
        //    {
        //        MockContext.Channels.Add(channel);
        //    }

        //    var products = PlateaumedProFaker.GetProducts();
        //    foreach (var product in products)
        //    {
        //        MockContext.Products.Add(product);
        //    }

        //    var channelProducts = PlateaumedProFaker.GetChannelProducts();
        //    foreach (var channelProduct in channelProducts)
        //    {
        //        MockContext.ChannelProducts.Add(channelProduct);
        //    }

        //    MockContext.SaveChanges();


        //    var auditTrailServiceMock = new Mock<IAuditTrailService>();
        //    var loggerServiceMock = new Mock<ILoggerService>();
        //    var createOrderService = new CreateOrderService(MockContext, auditTrailServiceMock.Object, loggerServiceMock.Object);
        //    var orders = PlateaumedProFaker.GetOrders();
        //    var website = orders.FirstOrDefault();
        //    website.LineItems.FirstOrDefault().ProductType = Enum.GetName(typeof(ProductType), ProductType.PaidSearchCampaigns);
        //    var expextedMessage = $"Product Type [{website.LineItems.FirstOrDefault().ProductType}] does not match ProductId [{website.LineItems.FirstOrDefault().ProductID}]";

        //    // When
        //    var order = createOrderService.CreateOrder(website).GetAwaiter().GetResult();

        //    // Then
        //    Assert.False(order.ResponseType.Status);
        //    Assert.Equal(order.ResponseType.Message, expextedMessage);
        //}

        //[Fact]
        //public void TestCreateOrderServiceShouldReturnFalseWhenPartnerProductIsInactive()
        //{
        //    // Given
        //    var channels = PlateaumedProFaker.GetStudents();
        //    foreach (var channel in channels)
        //    {
        //        MockContext.Channels.Add(channel);
        //    }

        //    var products = PlateaumedProFaker.GetProducts();
        //    foreach (var product in products)
        //    {
        //        MockContext.Products.Add(product);
        //    }

        //    var channelProducts = PlateaumedProFaker.GetChannelProducts();
        //    foreach (var channelProduct in channelProducts)
        //    {
        //        MockContext.ChannelProducts.Add(channelProduct);
        //    }

        //    MockContext.SaveChanges();


        //    var auditTrailServiceMock = new Mock<IAuditTrailService>();
        //    var loggerServiceMock = new Mock<ILoggerService>();
        //    var createOrderService = new CreateOrderService(MockContext, auditTrailServiceMock.Object, loggerServiceMock.Object);
        //    var orders = PlateaumedProFaker.GetOrders();
        //    var website = orders.FirstOrDefault();
        //    var websiteAndPaidSearch = orders.Last();
        //    websiteAndPaidSearch.Partner = website.Partner;
        //    var expextedMessage = $"Product [Paid Search Campaigns] does not exist for partner [Partner A]";

        //    // When
        //    var order = createOrderService.CreateOrder(websiteAndPaidSearch).GetAwaiter().GetResult();

        //    // Then
        //    Assert.False(order.ResponseType.Status);
        //    Assert.Equal(order.ResponseType.Message, expextedMessage);
        //}

        //[Fact]
        //public void TestCreateOrderServiceShouldReturnFalseWhenProductIsNotForPartner()
        //{
        //    // Given
        //    var channels = PlateaumedProFaker.GetStudents();
        //    foreach (var channel in channels)
        //    {
        //        MockContext.Channels.Add(channel);
        //    }

        //    var products = PlateaumedProFaker.GetProducts();
        //    foreach (var product in products)
        //    {
        //        MockContext.Products.Add(product);
        //    }

        //    var channelProducts = PlateaumedProFaker.GetChannelProducts();
        //    foreach (var channelProduct in channelProducts)
        //    {
        //        channelProduct.IsActive = false;
        //        MockContext.ChannelProducts.Add(channelProduct);
        //    }

        //    MockContext.SaveChanges();


        //    var auditTrailServiceMock = new Mock<IAuditTrailService>();
        //    var loggerServiceMock = new Mock<ILoggerService>();
        //    var createOrderService = new CreateOrderService(MockContext, auditTrailServiceMock.Object, loggerServiceMock.Object);
        //    var orders = PlateaumedProFaker.GetOrders();
        //    var website = orders.FirstOrDefault();
        //    var expextedMessage = $"Product [Website] is not active for partner [Partner A]";

        //    // When
        //    var order = createOrderService.CreateOrder(website).GetAwaiter().GetResult();

        //    // Then
        //    Assert.False(order.ResponseType.Status);
        //    Assert.Equal(order.ResponseType.Message, expextedMessage);
        //}
    }
}
