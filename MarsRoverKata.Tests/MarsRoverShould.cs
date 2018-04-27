using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MarsRoverKata.Tests
{
    [TestClass]
    public class MarsRoverShould
    {
        [TestMethod]
        public void ChangeOrientationAccordinglyWhenRotatingRight()
        {
            // Arrange
            var bus = new Bus();
            var ship = new Ship(bus);

            // Act
            bus.SendCommands("FLFFLBLFFFRF");

            // Assert
            Assert.AreEqual(new Position(1, 1, 'S'), ship.Position);

            // Clean
            ship.Dispose();
        }
    }
}
