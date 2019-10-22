using IDataInterface;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Restaurant;

namespace UnitTests
{
    [TestClass]
    public class FurnitureAPITests
    {
        [TestMethod]
        public void TestAddTable()
        {
            Mock<ITableManager> furnitureManagerMock = SetupMock(null);
            bool successfull = AddTableNumberOne(furnitureManagerMock);
            Assert.IsTrue(successfull);
            furnitureManagerMock.Verify(
                m => m.AddTable(It.Is<int>(i => i == 1)),
                    Times.Once());
        }        

        [TestMethod]
        public void TestAddExistingTable()
        {
            var furnitureManagerMock = SetupMock(new Table());
            bool successfull = AddTableNumberOne(furnitureManagerMock);
            Assert.IsFalse(successfull);
            furnitureManagerMock.Verify(
                m => m.AddTable(It.Is<int>(i => i == 1)),
                    Times.Never());
        }

        [TestMethod]
        public void MoveChairOk()
        {
            var tableManagerMock = new Mock<ITableManager>();
            var chairManagerMock = new Mock<IChairManager>();

            tableManagerMock.Setup(m =>
               m.GetTableByTableNumber(It.IsAny<int>()))
                .Returns(new Table { TableID = 2});

            chairManagerMock.Setup(m =>
              m.GetChairByChairNumber(It.IsAny<int>()))
               .Returns(new Chair
               {
                   ChairID = 2,
                   Table = new Table()
               }); 

            var furnitureAPI = new FurnitureAPI(tableManagerMock.Object, chairManagerMock.Object);
            var result = furnitureAPI.MoveChair(1, 1);
            Assert.AreEqual(MoveChairErrorCodes.Ok, result);
            chairManagerMock.Verify(m =>
                m.MoveChair(2, 2), Times.Once());
        }

        private static Mock<ITableManager> SetupMock(Table table)
        {
            var furnitureManagerMock = new Mock<ITableManager>();

            furnitureManagerMock.Setup(m =>
                    m.GetTableByTableNumber(It.IsAny<int>()))
                .Returns(table);

            furnitureManagerMock.Setup(m =>
                m.AddTable(It.IsAny<int>()));
            return furnitureManagerMock;
        }

        private static bool AddTableNumberOne(Mock<ITableManager> furnitureManagerMock)
        {
            var furnitureAPI = new FurnitureAPI(furnitureManagerMock.Object, null);
            var successfull = furnitureAPI.AddTable(1);
            return successfull;
        }
    }
}
