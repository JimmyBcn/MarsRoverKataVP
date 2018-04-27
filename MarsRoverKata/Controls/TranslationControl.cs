using System;
using MarsRoverKata.Messages.Commands;
using MarsRoverKata.Messages.Events;

namespace MarsRoverKata.Controls
{
    public class TranslationControl : IDisposable
    {
        private int x;
        private int y;
        private char currentDirection;

        private readonly Bus bus;

        private readonly IDisposable busSubscription;

        public TranslationControl(Bus bus)
        {
            this.bus = bus;
            
            this.x = 0;
            this.y = 0;
            this.currentDirection = 'N';
          
            this.busSubscription = this.bus.MessageStream
                .Subscribe(
                    message =>
                    {
                        switch (message)
                        {
                            case ShipCommand command:
                                this.HandleCommandMessage(command);
                                break;
                            case ShipDirectionChanged rotation:
                                this.HandleRotationMessage(rotation);
                                break;
                        }
                    });
        }

        public void Dispose()
        {
            this.busSubscription.Dispose();
        }

        private void HandleCommandMessage(ShipCommand command)
        {
            switch (command.Command)
            {
                case 'F':
                    this.MoveForwards();
                    break;
                case 'B':
                    this.MoveBackguards();
                    break;
                default:
                    return;
            }

            this.bus.SendMessage(new ShipPositionChanged(new Position(this.x, this.y, this.currentDirection)));
        }

        private void HandleRotationMessage(ShipDirectionChanged rotation)
        {
            this.currentDirection = rotation.Direction;
            this.bus.SendMessage(new ShipPositionChanged(new Position(this.x, this.y, this.currentDirection)));
        }

        private void MoveForwards()
        {
            switch (this.currentDirection)
            {
                case 'N':
                    this.y++;
                    break;
                case 'E':
                    this.x++;
                    break;
                case 'S':
                    this.y--;
                    break;
                case 'W':
                    this.x--;
                    break;
            }
        }

        private void MoveBackguards()
        {
            switch (this.currentDirection)
            {
                case 'N':
                    this.y--;
                    break;
                case 'E':
                    this.x--;
                    break;
                case 'S':
                    this.y++;
                    break;
                case 'W':
                    this.x++;
                    break;
            }
        }
    }
}