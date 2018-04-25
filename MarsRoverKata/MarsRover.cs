using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters;

namespace MarsRoverKata
{
    public enum RotationDirection
    {
        Left,
        Right
    }

    public enum TranslationDirection
    {
        Forewards,
        Backguards
    }

    public interface IShipControl
    {
        void Calibrate();
    }

    public abstract class ShipControl : IShipControl
    {
        public ShipControl()
        {
            this.Calibrate();
        }

        public abstract void Calibrate();
    }

    public class RotationShipControl : ShipControl 
    {
        private char[] faces = "NESW".ToCharArray();
        private int faceIndex;

        public override void Calibrate()
        {
            this.faceIndex = 0;
        }
        
        public void Rotate(RotationDirection rotationDirection)
        {
            switch(rotationDirection)
            {
                case RotationDirection.Right:
                    this.RotateRigth();
                    break;

                case RotationDirection.Left:
                    this.RotateLeft();
                    break;
            }
        }

        public char GetCurrentDirection()
        {
            return this.faces[this.faceIndex];
        }

        private void RotateRigth()
        {
            if (this.faceIndex < faces.Length - 1)
            {
                this.faceIndex++;
            }
            else
            {
                this.faceIndex = 0;
            }            
        }

        private void RotateLeft()
        {
            if (this.faceIndex == 0)
            {
                this.faceIndex = this.faces.Length - 1;
            }
            else
            {
                this.faceIndex--;
            }
        }
    }

    public class TranslationShipControl : ShipControl
    {
        private int x;
        private int y;
        private int planetX;
        private int planetY;

        public override void Calibrate()
        {
            this.x = 0;
            this.y = 0;
        }

        public TranslationShipControl(int planetX, int planetY)
        {
            this.planetX = planetX;
            this.planetY = planetY;
        }

        public void Translate(char currentDirection, TranslationDirection translationDirection)
        {
            // TODO: Implement wrapping on boundaries (using planetX and planetY)
            switch (currentDirection)
            {
                case 'N':
                    if (translationDirection == TranslationDirection.Forewards)
                    {
                        this.y++;
                    }
                    else
                    {
                        this.y--;
                    }                    ;
                    break;
                case 'E':
                    if (translationDirection == TranslationDirection.Forewards)
                    {
                        this.x++;
                    }
                    else
                    {
                        this.x--;
                    }                    
                    break;
                case 'S':
                    if (translationDirection == TranslationDirection.Forewards)
                    {
                        this.y--;
                    }
                    else
                    {
                        this.y++;
                    }                    
                    break;
                case 'W':
                    if (translationDirection == TranslationDirection.Forewards)
                    {
                        this.x--;
                    }
                    else
                    {
                        this.x++;
                    }                    
                    break;
            }
        }

        public int GetCurrentXPosition()
        {
            return this.x;
        }

        public int GetCurrentYPosition()
        {
            return this.y;
        }
    }

    public class MarsRoverShip
    {
        private readonly RotationShipControl rotationShipControl;
        public readonly TranslationShipControl translationShipControl;

        public MarsRoverShip(RotationShipControl rotationShipControl, TranslationShipControl translationShipControl)
        {            
            this.rotationShipControl = rotationShipControl;
            this.translationShipControl = translationShipControl;
        }

        public void Navigate(char command)
        {
            switch (command)
            {
                case 'F':
                    this.translationShipControl.Translate(this.Position.Direction, TranslationDirection.Forewards);
                    break;
                case 'B':
                    this.translationShipControl.Translate(this.Position.Direction, TranslationDirection.Backguards);
                    break;
                case 'R':
                    this.rotationShipControl.Rotate(RotationDirection.Right);
                    break;
                case 'L':
                    this.rotationShipControl.Rotate(RotationDirection.Left);
                    break;
            }
        }

        public RoverPosition Position => new RoverPosition(
                this.translationShipControl.GetCurrentXPosition(),
                this.translationShipControl.GetCurrentYPosition(),
                this.rotationShipControl.GetCurrentDirection());        
    }

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