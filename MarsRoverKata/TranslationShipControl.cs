namespace MarsRoverKata
{
    public class TranslationShipControl : ShipControl
    {
        private int x;
        private int y;
        private int planetX;
        private int planetY;

        public override void Calibrate()
        {
            this.x = 0;
            this.y = 0;
        }

        public TranslationShipControl(int planetX, int planetY)
        {
            this.planetX = planetX;
            this.planetY = planetY;
        }

        public void Translate(char currentDirection, TranslationDirection translationDirection)
        {
            // TODO: Implement wrapping on boundaries (using planetX and planetY)
            switch (currentDirection)
            {
                case 'N':
                    if (translationDirection == TranslationDirection.Forewards)
                    {
                        this.y++;
                    }
                    else
                    {
                        this.y--;
                    }                    ;
                    break;
                case 'E':
                    if (translationDirection == TranslationDirection.Forewards)
                    {
                        this.x++;
                    }
                    else
                    {
                        this.x--;
                    }                    
                    break;
                case 'S':
                    if (translationDirection == TranslationDirection.Forewards)
                    {
                        this.y--;
                    }
                    else
                    {
                        this.y++;
                    }                    
                    break;
                case 'W':
                    if (translationDirection == TranslationDirection.Forewards)
                    {
                        this.x--;
                    }
                    else
                    {
                        this.x++;
                    }                    
                    break;
            }
        }

        public int GetCurrentXPosition()
        {
            return this.x;
        }

        public int GetCurrentYPosition()
        {
            return this.y;
        }
    }
}