using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace MarsRoverKata
{
    public class MarsRoverBus
    {
        public MarsRoverBus()
        {
            this.CommandsStream = new Subject<char>();
        }

        public Subject<char> CommandsStream { get; set; }

        public void SendCommands(string commands)
        {
            commands.ToObservable<char>().Subscribe(CommandsStream);
        }
    }
}