using Bogus.DataSets;
using System;
using System.Linq;

namespace Bogus.Extensions.Poland
{
    public static class CompanyExtensions
    {
        public static string Nip(this Company company)
        {
            string result = GenerateRandomizeNumbers(8);

            CheckSumCalculator checkSumCalculator = new NipCheckSumCalculator();
            var crc = checkSumCalculator.CalculateCRC(result);

            result = AppendCRC(result, crc);

            return result;
        }

        public static string Regon(this Company company, RegonType regonType = RegonType.Regon9)
        {
            var length = (byte)regonType;

            string result = GenerateRandomizeNumbers(length);

            CheckSumCalculator checkSumCalculator = RegonWeightsFactory.Create(regonType);
            var crc = checkSumCalculator.CalculateCRC(result);

            result = AppendCRC(result, crc);

            return result;
        }

        private static string AppendCRC(string numbers, int crc) => numbers += crc.ToString();

        private static string GenerateRandomizeNumbers(byte length)
        {
            var f = new Faker();
            var numbers = Enumerable.Range(0, length - 1)
                            .Select(n => f.Random.Number(0, 9));

            string result = string.Join(string.Empty, numbers);
            return result;
        }

        public enum RegonType
        {
            Regon9 = 9,
            Regon14 = 14
        }

        public class RegonWeightsFactory
        {
            public static CheckSumCalculator Create(RegonType regonType)
            {
                switch (regonType)
                {
                    case RegonType.Regon9: return new Regon9CheckSumCalculator();
                    case RegonType.Regon14: return new Regon14CheckSumCalculator();

                    default: throw new NotSupportedException();
                }
            }
        }
    }


}
