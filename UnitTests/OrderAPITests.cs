using IDataInterface;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Restaurant;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTests
{
    [TestClass]
    public class OrderAPITests
    {
        [TestMethod]
        public void AddToOrderNoSuchDish()
        {
            var dishManagerMock = new Mock<IDishManager>();
            var orderApi = new OrderAPI(dishManagerMock.Object, null, null);
            var result = orderApi.AddToOrder(0, 0);
            Assert.AreEqual(OrderErrorCodes.NoSuchDish, result);
        }

        [TestMethod]
        public void AddToOrderNoSuchTable()
        {
            var dishManagerMock = new Mock<IDishManager>();
            dishManagerMock.Setup(m =>
                   m.GetDishByDishNumber(It.IsAny<int>()))
                .Returns(
                         new Dish()
                    );

            var tableManager = new Mock<ITableManager>();

            var orderApi = new OrderAPI(dishManagerMock.Object, null, tableManager.Object);
            var result = orderApi.AddToOrder(0, 0);
            Assert.AreEqual(OrderErrorCodes.NoSuchTable, result);
        }

        [TestMethod]
        public void AddToOrderNoActiveOrder()
        {
            const int tableId = 10;

            var dishManagerMock = new Mock<IDishManager>();
            dishManagerMock.Setup(m =>
                   m.GetDishByDishNumber(It.IsAny<int>()))
                .Returns(
                         new Dish
                         {
                             DishID = 20
                         }
                    );

            var tableManagerMock = new Mock<ITableManager>();
            tableManagerMock.Setup(m =>
                    m.GetTableByTableNumber(It.IsAny<int>()))
                .Returns(
                    new Table
                    {
                        TableID = tableId
                    }
                );

            var orderManagerMock = new Mock<IOrderManager>();
            orderManagerMock.Setup(m =>
                    m.CreateActiveOrder(It.Is<int>(i => i == tableId)))
                .Returns(
                    new Order
                    {
                        OrderID = 30
                    }
                );
            orderManagerMock.Setup(m =>
                    m.AddToOrder(It.Is<int>(i => i == 30), It.Is<int>(i => i == 20)));
                
            var orderApi = new OrderAPI(dishManagerMock.Object, 
                orderManagerMock.Object, tableManagerMock.Object);
            var result = orderApi.AddToOrder(0, 0);

            orderManagerMock.Verify(m => m.CreateActiveOrder(It.Is<int>(i => i == tableId))
                , Times.Once);

            orderManagerMock.Verify(m => 
                m.AddToOrder(It.Is<int>(i => i == 30), It.Is<int>(i => i == 20))
                , Times.Once);

            Assert.AreEqual(OrderErrorCodes.Ok, result);
        }
    }
}
