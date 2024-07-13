using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3.Cards
{
    public static class CardSuitHelper
    {
        private static Dictionary<char, char> suits = new Dictionary<char, char>()
        {
            ['S'] = '\u2660',
            ['H'] = '\u2665',
            ['D'] = '\u2666',
            ['C'] = '\u2663'
        };
        public static char GetSuit(char value)
        {
            if (!suits.TryGetValue(value, out char suit))
            {
                throw new InvalidCardException();
            }

            return suit;
        }
    }
}
