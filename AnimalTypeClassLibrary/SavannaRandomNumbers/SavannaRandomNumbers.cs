using System;

namespace Savanna.SavannaRandomNumbers
{
    public class SavannaRandomNumbers : ISavannaRandomNumbers
    {
        /// <summary>
        /// Returns random int within given range (starts from 0)
        /// </summary>
        public int GetRandomNumber(int range)
        {
            Random random = new Random();
            return random.Next(range);
        }

        /// <summary>
        /// Return
        /// </summary>
        /// <param name="from">starting point, minimal value that can be returned</param>
        /// <param name="to">end point, maximal value that can be returned</param>
        /// <returns></returns>
        public int GetRandomNumber(int from, int to)
        {
            Random random = new Random();
            return random.Next(Math.Abs(from - to) + 1) + from;
        }
    }
}