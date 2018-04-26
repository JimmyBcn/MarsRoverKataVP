namespace MarsRoverKata
{
    public abstract class ShipControl : IShipControl
    {
        public ShipControl()
        {
            this.Calibrate();
        }

        public abstract void Calibrate();
    }
}