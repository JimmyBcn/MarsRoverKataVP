namespace MarsRoverKata.Messages.Commands
{
    public class ShipCommand : IBusMessage
    {
        public ShipCommand(char command)
        {
            this.Command = command;
        }

        public char Command { get; }
    }
}
