using System.Linq;
using System.Reactive.Subjects;
using MarsRoverKata.Messages;
using MarsRoverKata.Messages.Commands;

namespace MarsRoverKata
{
    public class Bus
    {
        public Bus()
        {
            this.MessageStream = new Subject<IBusMessage>();
        }

        public Subject<IBusMessage> MessageStream { get; }

        public void SendMessage(IBusMessage message)
        {
            MessageStream.OnNext(message);
        }

        public void SendCommands(string commands)
        {
            commands.Select(c => new ShipCommand(c)).ToList().ForEach(this.SendMessage);
        }
    }
}