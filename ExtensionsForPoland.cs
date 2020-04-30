using System;
using System.Linq;
using System.Text;

namespace Bogus.Extensions.Poland
{
    public static class ExtensionsForPoland
    {
        /// <summary>
        /// Personal Identity Number
        /// </summary>
        /// <param name="p">Person</param>
        /// <returns></returns>
        public static string Pesel(this Person p)
        {
            var peselStringBuilder = new StringBuilder();

            peselStringBuilder
                .AppendPeselDate(p.DateOfBirth)
                .AppendRandomNumbers(3)
                .AppendGender(p.Gender)
                .AppendChecksum();
          
            return peselStringBuilder.ToString();
        }

        private static StringBuilder AppendChecksum(this StringBuilder builder)
        {
            return builder.Append(PeselCheckSumCalculator.Calculate(builder.ToString()));
        }

    private static StringBuilder AppendPeselDate(this StringBuilder builder, DateTime date)
        {
            builder.Append((date.Year % 100).ToString("00"));
            builder.Append(GetPeselMonthShiftedByYear(date));
            builder.Append(date.Day.ToString("00"));

            return builder;
        }

        private static StringBuilder AppendRandomNumbers(this StringBuilder builder, int count)
        {
            Faker f = new Faker();
            var randomNumbers = f.Random.Chars('0', '9', 3);
            builder.Append(randomNumbers);

            return builder;
        }

        private static StringBuilder AppendGender(this StringBuilder builder, DataSets.Name.Gender gender)
        {
            var females = new byte[] { 0, 2, 4, 6, 8 };
            var males = new byte[] { 1, 3, 5, 7, 9 };

            Faker f = new Faker();

            if (gender == DataSets.Name.Gender.Female)
            {
                builder.Append(f.PickRandom(females));
            }
            else
            {
                builder.Append(f.PickRandom(males));
            }

            return builder;
        }

        private static string GetPeselMonthShiftedByYear(DateTime date)
        {
            if (date.Year < 1900 || date.Year > 2299)
            {
                throw new NotSupportedException($"PESEL for year: {date.Year} is not supported");
            }

            int monthShift = (int)((date.Year - 1900) / 100) * 20;

            return (date.Month + monthShift).ToString("00");
        }

        internal class PeselCheckSumCalculator
        {
            private static readonly int[] _Weight = new[] { 1, 3, 7, 9, 1, 3, 7, 9, 1, 3 };

            public static int Calculate(string pesel)
            {
                int checkSum = pesel.Zip(_Weight, (digit, weight) => (digit - '0') * weight)
                    .Sum();

                int lastDigit = checkSum % 10;

                return lastDigit == 0 ? 0 : 10 - lastDigit;
            }
        }
    }

  
}
