using System;
using System.Reactive.Linq;

namespace MarsRoverKata
{
    public class MarsRoverShip: IDisposable
    {
        private readonly TranslationShipControl translationShipControl;
        private readonly RotationShipControl rotationShipControl;

        private IDisposable translationSubscription;

        public MarsRoverShip(MarsRoverBus bus)
        {
            this.rotationShipControl = new RotationShipControl(bus);
            this.translationShipControl = new TranslationShipControl(bus, this.rotationShipControl);

            translationSubscription = this.translationShipControl.TranslationStream
                .Subscribe(translation => this.Position = translation);
        }

        public RoverPosition Position { get; private set; }

        public void Dispose()
        {
            this.rotationShipControl.Dispose();
            this.translationShipControl.Dispose();
            this.translationSubscription.Dispose();
        }
    }
}