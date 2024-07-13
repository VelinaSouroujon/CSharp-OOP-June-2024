using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3.Cards
{
    public class Card
    {
        private string face;
        private char suit;
        public Card(string face, char suit)
        {
            Face = face;
            Suit = suit;
        }

        public string Face
        {
            get => face;
            private set
            {
                CardFaceHelper.ValidateFace(value);
                face = value;
            }
        }
        public char Suit
        {
            get => suit;
            private set
            {
                suit = CardSuitHelper.GetSuit(value);
            }
        }
        public override string ToString()
        {
            return $"[{Face}{Suit}]";
        }
    }
}
