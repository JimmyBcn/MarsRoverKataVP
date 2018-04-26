using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters;

namespace MarsRoverKata
{
    public class MarsRover
    {
        public MarsRover()
        {
            var rotationShipControl = new RotationShipControl();
            var translationShipControl = new TranslationShipControl(100,100);
            this.Ship = new MarsRoverShip(rotationShipControl, translationShipControl);
        }

        public MarsRoverShip Ship { get; }

        public void SendCommands(string commands)
        {
            foreach (var command in commands)
            {
                this.Ship.Navigate(command);
            }
        }
    }
}