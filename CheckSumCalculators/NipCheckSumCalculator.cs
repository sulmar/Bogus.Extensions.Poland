namespace Bogus.Extensions.Poland
{
    public class NipCheckSumCalculator : CheckSumCalculator
    {
        private static readonly byte[] weights = new byte[] { 6, 5, 7, 2, 3, 4, 5, 6, 7 };

        public NipCheckSumCalculator() : base(weights)
        {
        }        
    }


}
