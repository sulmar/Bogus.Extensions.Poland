namespace Bogus.Extensions.Poland
{

    public class Regon14CheckSumCalculator : CheckSumCalculator
    {
        private static readonly byte[] weights = new byte[] { 2, 4, 8, 5, 0, 9, 7, 3, 6, 1, 2, 4, 8 };

        public Regon14CheckSumCalculator() : base(weights) { }        
    }


}
