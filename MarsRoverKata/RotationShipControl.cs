using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace MarsRoverKata
{
    public class RotationShipControl : IDisposable
    {
        private char[] faces = "NESW".ToCharArray();
        private int faceIndex;

        private readonly MarsRoverBus bus;

        private IDisposable busSubscription;

        public RotationShipControl(MarsRoverBus bus)
        {
            this.bus = bus;
            this.RotationStream = new Subject<char>();

            this.faceIndex = 0;

            this.busSubscription = this.bus.CommandsStream    
                .StartWith(this.faces[this.faceIndex])
                .Subscribe(
                    command =>
                    {
                        switch (command)
                        {
                            case 'R':
                                this.Rotate(RotationDirection.Right);
                                break;
                            case 'L':
                                this.Rotate(RotationDirection.Left);
                                break;
                        }

                        this.RotationStream.OnNext(this.faces[this.faceIndex]);
                    });
        }   
        
        public void Dispose()
        {
            this.busSubscription.Dispose();
        }

        public Subject<char> RotationStream { get; set; }

        public void Rotate(RotationDirection rotationDirection)
        {
            switch (rotationDirection)
            {
                case RotationDirection.Right:
                    this.RotateRight();
                    break;

                case RotationDirection.Left:
                    this.RotateLeft();
                    break;
            }
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