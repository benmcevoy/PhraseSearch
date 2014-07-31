using System;
using System.Collections.Generic;
using System.Linq;

namespace TestDataset
{
    public static class RandomHelper
    {
        private static readonly Random Random = new Random();

        public static int NextRandom(int max)
        {
            return Random.Next(max);
        }

        public static int NextRandom(int min, int max)
        {
            return Random.Next(min, max);
        }

        public static double NextRandom(double max)
        {
            return Random.NextDouble()*max;
        }

        public static double NextRandom(double min, double max)
        {
            return Random.NextDouble()*(max - min) + min;
        }

        /// <summary>
        ///     Get a random number between 0 and the count of weights.
        ///     The probability of the random number is based on the weight/sum of the weights.
        ///     e.g. for 10, 20, 5
        ///     - probability of 0 is 10/(10+20+5) or 29%
        ///     - probability of 1 is 20/35 or 57%
        ///     - probability of 2 is 5/35 or 14%
        /// </summary>
        /// <param name="weights"></param>
        /// <returns></returns>
        public static int NextRandom(IEnumerable<int> weights)
        {
            var weightArray = weights as int[] ?? weights.ToArray();
            var rnd = NextRandom(weightArray.Sum());
            var sum = 0;
            var index = 0;

            foreach (var weight in weightArray)
            {
                sum = sum + weight;

                if (rnd < sum)  break; 
               
                index++;
            }

            return index;
        }
    }
}