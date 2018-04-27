namespace MarsRoverKata.Messages.Events
{
    public class ShipPositionChanged : IBusMessage
    {
        public ShipPositionChanged(Position position)
        {
            this.Position = position;
        }

        public Position Position { get; }
    }
}
