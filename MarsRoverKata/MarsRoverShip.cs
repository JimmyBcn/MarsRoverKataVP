namespace MarsRoverKata
{
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
}