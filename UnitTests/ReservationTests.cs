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
    public class ReservationTests
    {
        [TestMethod]
        public void TestOkBooking()
        {
            ReservationAPI reservationApi = SetupTestData();
            var result = reservationApi.FindNextOpenTableForPartySize(2, new DateTime(2000, 1, 1));
            Assert.IsTrue(DateTime.Compare(new DateTime(2000, 1, 2), result) == 0);
        }

        [TestMethod]
        public void TestBookingPartyTooBig()
        {
            ReservationAPI reservationApi = SetupTestData();
            var result = reservationApi.FindNextOpenTableForPartySize(3, new DateTime(2000, 1, 1));
            Assert.IsTrue(DateTime.Compare(DateTime.MaxValue, result) == 0);
        }

        private static ReservationAPI SetupTestData()
        {
            var reservationManager = new Mock<IReservationManager>();
            var tableManager = new Mock<ITableManager>();
            var reservationApi = new ReservationAPI(reservationManager.Object, tableManager.Object);
            tableManager.Setup(m =>
               m.GetAllTables())
                .Returns(new List<Table>
                {
                    new Table
                    {
                        TableID = 1,
                        Chairs = new List<Chair>
                        {
                            new Chair(),
                            new Chair(),
                            new Chair()
                        }
                    },
                    new Table
                    {
                        TableID = 2,
                        Chairs = new List<Chair>
                        {
                            new Chair(),
                            new Chair()
                        }
                    },
                });
            reservationManager.Setup(m =>
                m.GetTimeSlotsFrom(It.IsAny<DateTime>()))
                .Returns(new List<TimeSlot>
                {
                    new TimeSlot
                    {
                        Start = new DateTime(2000, 1, 1),
                        Reservations = new List<Reservation>
                        {
                            new Reservation
                            {
                                TableID = 1
                            },
                            new Reservation
                            {
                                TableID = 2
                            }
                        }
                    },
                    new TimeSlot
                    {
                        Start = new DateTime(2000, 1, 2),
                        Reservations = new List<Reservation>
                        {
                            new Reservation
                            {
                                TableID = 1
                            }
                        }
                    }

                });
            return reservationApi;
        }
    }
}
