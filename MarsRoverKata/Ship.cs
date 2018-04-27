using System;
using System.Reactive.Linq;
using MarsRoverKata.Controls;
using MarsRoverKata.Messages.Events;

namespace MarsRoverKata
{
    public class Ship: IDisposable
    {
        private readonly Bus bus;

        private readonly TranslationControl translationShipControl;
        private readonly RotationControl rotationShipControl;

        private readonly IDisposable busSubscription;

        public Ship(Bus bus)
        {
            this.bus = bus;

            this.rotationShipControl = new RotationControl(bus);
            this.translationShipControl = new TranslationControl(bus);

            this.busSubscription = this.bus.MessageStream
                .OfType<ShipPositionChanged>()
                .Subscribe(position => 
                    this.Position = position.Position);
        }

        public Position Position { get; private set; }

        public void Dispose()
        {
            this.rotationShipControl.Dispose();
            this.translationShipControl.Dispose();
            this.busSubscription.Dispose();
        }
    }
}