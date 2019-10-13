using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Building;
using Building.Common;

namespace BuildingTests
{
    [TestClass]
    public class BuldingTests
    {
        [TestMethod]
        public void ValidationTest()
        {
            //Arrange
            Building.ElevatedFloor efloor = new Building.ElevatedFloor(500, 0, 100);
            //Act
            int expected = 100;
            
            //Arrange
            int actual = efloor.OfficesMaxCapacity;
            Assert.AreEqual(expected, actual);
        }
    }
}
