using System;
using System.Collections.Generic;

namespace DisplayCalculator
{

    public readonly struct Correlation
    {
        public readonly uint Width;
        public readonly uint Height;

        public Correlation(uint Width, uint Height)
        {
            if (Width == 0 || Height == 0) throw new ArgumentException();
            if (Height == Width)
            {
                this.Height = 1;
                this.Width = 1;
            }
            else
            {
                this.Height = Height / gcd(Height, Width);
                this.Width = Width / gcd(Height, Width);
            }
        }
        
        private int gcd(int a, int b) => b == 0 ? a : gcd(b, a % b);

        public override string ToString()
        {
            return $"{Width}:{Height}";
        }
    }
}
