using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace MarsRoverKata
{
    public class TranslationShipControl : IDisposable
    {
        private int x;
        private int y;
        private char currentDirection;

        private readonly MarsRoverBus bus;
        private readonly RotationShipControl rotationShipControl;

        private IDisposable busSubscription;
        private IDisposable rotationControlSubscription;

        public TranslationShipControl(MarsRoverBus bus, RotationShipControl rotationShipControl)
        {
            this.bus = bus;
            this.rotationShipControl = rotationShipControl;
            
            this.x = 0;
            this.y = 0;

            this.TranslationStream = new Subject<RoverPosition>();
            
            this.busSubscription = this.bus.CommandsStream
                .Subscribe(
                    command =>
                    {
                        switch (command)
                        {
                            case 'F':
                                this.Move(TranslationDirection.Forewards);
                                break;
                            case 'B':
                                this.Move(TranslationDirection.Backguards);
                                break;
                        }
                    });

            this.rotationControlSubscription = this.rotationShipControl.RotationStream
                .Subscribe(direction => this.currentDirection = direction);
        }

        public Subject<RoverPosition> TranslationStream { get; set; }

        public void Dispose()
        {
            this.busSubscription.Dispose();
            this.rotationControlSubscription.Dispose();
        }

        private void Move(TranslationDirection translationDirection)
        {
            switch (this.currentDirection)
            {
                case 'N':
                    this.y = translationDirection == TranslationDirection.Forewards ? this.y + 1 :this.y - 1;
                    break;
                case 'E':
                    this.x = translationDirection == TranslationDirection.Forewards ? this.x + 1 : this.x - 1;
                    break;
                case 'S':
                    this.y = translationDirection == TranslationDirection.Forewards ? this.y - 1 : this.y + 1;
                    break;
                case 'W':
                    this.x = translationDirection == TranslationDirection.Forewards ? this.x - 1 : this.x + 1;
                    break;
            }

            this.TranslationStream.OnNext(new RoverPosition(this.x, this.y, this.currentDirection));
        }
    }
}