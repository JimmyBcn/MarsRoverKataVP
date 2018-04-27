using System;
using System.Reactive.Linq;
using MarsRoverKata.Messages.Commands;
using MarsRoverKata.Messages.Events;

namespace MarsRoverKata.Controls
{
    public class RotationControl : IDisposable
    {
        private readonly char[] faces = "NESW".ToCharArray();
        private int faceIndex;

        private readonly Bus bus;

        private readonly IDisposable busSubscription;

        public RotationControl(Bus bus)
        {
            this.bus = bus;

            this.faceIndex = 0;

            this.busSubscription = this.bus.MessageStream
                .OfType<ShipCommand>()
                .Subscribe(this.HandleCommandMessage);
        }   
        
        public void Dispose()
        {
            this.busSubscription.Dispose();
        }

        private void HandleCommandMessage(ShipCommand command)
        {
            switch (command.Command)
            {
                case 'R':
                    this.RotateRight();
                    break;
                case 'L':
                    this.RotateLeft();
                    break;
                default:
                    return;
            }

            this.bus.SendMessage(new ShipDirectionChanged(this.faces[this.faceIndex]));
        }

        private void RotateRight()
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
}