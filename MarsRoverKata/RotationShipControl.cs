namespace MarsRoverKata
{
    public class RotationShipControl : ShipControl 
    {
        private char[] faces = "NESW".ToCharArray();
        private int faceIndex;

        public override void Calibrate()
        {
            this.faceIndex = 0;
        }
        
        public void Rotate(RotationDirection rotationDirection)
        {
            switch(rotationDirection)
            {
                case RotationDirection.Right:
                    this.RotateRigth();
                    break;

                case RotationDirection.Left:
                    this.RotateLeft();
                    break;
            }
        }

        public char GetCurrentDirection()
        {
            return this.faces[this.faceIndex];
        }

        private void RotateRigth()
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