namespace MarsRoverKata.Messages.Events
{
    public class ShipDirectionChanged : IBusMessage
    {
        public ShipDirectionChanged(char direction)
        {
            this.Direction = direction;
        }

        public char Direction { get; }
    }
}
