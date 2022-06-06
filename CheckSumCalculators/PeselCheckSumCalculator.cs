namespace Bogus.Extensions.Poland
{
    public class PeselCheckSumCalculator : CheckSumCalculator
    {
        private static readonly byte[] weights = new byte[] { 1, 3, 7, 9, 1, 3, 7, 9, 1, 3 };

        public PeselCheckSumCalculator(byte[] weights) : base(weights) { }

        protected override int CalculateCheckControl(int sumControl) => 10 - sumControl % 10;
    }


}
