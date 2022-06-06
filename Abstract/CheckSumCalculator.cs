using System;
using System.Linq;

namespace Bogus.Extensions.Poland
{

    public abstract class CheckSumCalculator
    {
        private byte[] weights;

        protected CheckSumCalculator(byte[] weights) => this.weights = weights;

        public int CalculateCRC(string input)
        {
            Validate(input);

            var numbers = ToByteArray(input);

            var sumControl = CalculateSumControl(numbers, weights);

            var crc = CalculateCheckControl(sumControl);

            return crc;

        }

        private void Validate(string input)
        {
            if (!IsNumbersOnly(input))
            {
                throw new FormatException();
            }

            if (input.Length != weights.Length)
            {
                throw new InvalidOperationException();
            }
        }

        private static byte[] ToByteArray(string input) => input
                                                .ToCharArray()
                                                .Select(c => byte.Parse(c.ToString()))
                                                .ToArray();

        private static bool IsNumbersOnly(string input) => input.ToCharArray()
                                .All(c => char.IsNumber(c));


        public int CalculateSumControl(string numbers) => CalculateSumControl(ToByteArray(numbers), weights);

        private int CalculateSumControl(byte[] numbers, byte[] weights) => numbers
                                                                            .Zip(weights, (digit, weight) => digit * weight)
                                                                            .Sum();

        protected virtual int CalculateCheckControl(int sumControl) => sumControl % 11;
    }


}
