using System;
using System.Collections.Generic;

namespace DisplayCalculator
{

    public readonly struct Corralation
    {
        public readonly uint Width;
        public readonly uint Height;

        public Corralation(uint Width, uint Height)
        {
            if (Width == 0 || Height == 0) throw new ArgumentException();
            if (Height == Width)
            {
                this.Height = 1;
                this.Width = 1;
                return;
            }

            this.Height = Height;
            this.Width = Width;

            List<uint> BList = GetDivisors(Width);
            List<uint> SList = GetDivisors(Height);
            SortLists(ref SList, ref BList);
            SList.Sort();
            BList.Sort();

            uint cof = 1;
            for (int s = 0; s < SList.Count; s++)
            {
                for (int b = 0; b < BList.Count; b++)
                {
                    if (BList[b] == SList[s])
                    {
                        cof *= BList[b];
                        BList.RemoveAt(b);
                        break;
                    }
                }
            }
            this.Width /= cof;
            this.Height /= cof;

        }

        private List<uint> GetDivisors(uint num)
        {
            List<uint> List = new List<uint>();
            uint number = num;
            while (true)
            {
                bool IsNatural = false;
                for (uint i = 2; i <= 5; i++)
                {
                    if (i == 4) continue;
                    if (number % i == 0)
                    {
                        number /= i;
                        List.Add(i);
                        break;
                    }
                    IsNatural = true;
                }
                if (IsNatural)
                {
                    List.Add(number);
                    break;
                }
            }
            return List;
        }
        private void SortLists(ref List<uint> smallList, ref List<uint> bigList)
        {
            if (smallList.Count == bigList.Count || smallList.Count < bigList.Count) return;
            List<uint> tmp = smallList;
            smallList = bigList;
            bigList = tmp;
        }

        public override string ToString()
        {
            return $"{Width}:{Height}";
        }
    }
}
