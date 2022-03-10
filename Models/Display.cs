using System;

namespace DisplayCalculator
{

    public readonly struct Display
    {
        public enum Side { Width, Height };

        public readonly double Diagonal;
        public readonly Corralation Correlation;
        public readonly double Width;
        public readonly double Height;

        public Display(double Diagonal, Corralation Correlation)
        {
            this.Diagonal = Diagonal;
            this.Correlation = Correlation;

            double kof = Math.Sqrt(Math.Pow(Correlation.Height, 2) + Math.Pow(Correlation.Width, 2));

            Height = (Correlation.Height * this.Diagonal / kof);
            Width = (Correlation.Width * this.Diagonal / kof);
        }

        public Display(Corralation Corralation, double LenOfSide, Side side)
        {
            Correlation = Corralation;
            if (side == Side.Width)
            {
                Width = LenOfSide;
                Height = (LenOfSide / Corralation.Width) * Corralation.Height;

                Diagonal = Math.Sqrt(Math.Pow(Width, 2) + Math.Pow(Height, 2));
            }
            else
            {
                Height = LenOfSide;
                Width = (LenOfSide / Corralation.Height) * Corralation.Width;

                Diagonal = Math.Sqrt(Math.Pow(Width, 2) + Math.Pow(Height, 2));
            }
        }

        public Display(double Diagonal, double LenOfSide, Side side)
        {
            if (Diagonal < LenOfSide) throw new ArgumentException("Довжина строни неможе бути меншою за діагональ");
            this.Diagonal = Diagonal;
            if (side == Side.Width)
            {
                Width = LenOfSide;
                Height = Math.Sqrt(Math.Pow(this.Diagonal, 2) - Math.Pow(Width, 2));
            }
            else
            {
                Height = LenOfSide;
                Width = Math.Sqrt(Math.Pow(this.Diagonal, 2) - Math.Pow(Height, 2));
            }
            /*Interim solution*/
            /*Possible errors*/
            try
            {
                Correlation = new Corralation((uint)(Width * 1000), (uint)(Height * 1000));
            }
            catch
            {
                Correlation = new Corralation();
            }
        }

        public Display(double Width, double Height)
        {
            this.Width = Width;
            this.Height = Height;
            Diagonal = Math.Sqrt(Math.Pow(this.Width, 2) + Math.Pow(this.Height, 2));
            /*Interim solution*/
            /*Possible errors*/
            try
            {
                Correlation = new Corralation((uint)(this.Width * 1000), (uint)(this.Height * 1000));
            }
            catch
            {
                Correlation = new Corralation();
            }
        }
    }
}
