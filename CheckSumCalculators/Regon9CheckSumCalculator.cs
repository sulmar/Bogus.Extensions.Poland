namespace Bogus.Extensions.Poland
{
    public class Regon9CheckSumCalculator : CheckSumCalculator        
    {
        private static readonly byte[] weights = new byte[] { 8, 9, 2, 3, 4, 5, 6, 7 };
        public Regon9CheckSumCalculator() : base(weights) { }

        
    }


}
