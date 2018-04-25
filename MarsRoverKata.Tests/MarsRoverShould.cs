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
            var rover = new MarsRover();
            rover.SendCommands("FLFFLBLFFFRF");
            Assert.AreEqual(new RoverPosition(1, 1, 'S'), rover.Ship.Position);
        }
    }
}
