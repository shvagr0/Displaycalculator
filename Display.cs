using System;

namespace MonitorCalculator
{

    public readonly struct Display
    {
        public enum Side { Width, Height };

        public readonly double diagonal;

        public readonly Corralation correlation;

        public readonly double height;

        public readonly double width;

        public Display(double Diagonal, Corralation Correlation)
        {
            diagonal = Diagonal * 2.54;
            correlation = Correlation;

            double kof = Math.Sqrt(Math.Pow(Correlation.Height, 2) + Math.Pow(Correlation.Width, 2));

            height = (Correlation.Height * diagonal / kof);
            width = (Correlation.Width * diagonal / kof);
        }

        public Display(Corralation Corralation, double LenOfSide, Side side)
        {
            correlation = Corralation;
            if (side == Side.Width)
            {
                width = LenOfSide;
                height = (LenOfSide / Corralation.Width) * Corralation.Height;

                diagonal = Math.Sqrt(Math.Pow(width, 2) + Math.Pow(height, 2)) / 2.54;
            }
            else
            {
                height = LenOfSide;
                width = (LenOfSide / Corralation.Height) * Corralation.Width;

                diagonal = Math.Sqrt(Math.Pow(width, 2) + Math.Pow(height, 2)) / 2.54;
            }
        }
    }
}
