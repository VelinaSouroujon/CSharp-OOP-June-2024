using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3.Cards
{
    public static class CardFaceHelper
    {
        private static HashSet<string> faces = new HashSet<string>()
        {
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "J",
            "Q",
            "K",
            "A"
        };

        public static void ValidateFace(string face)
        {
            if(!faces.Contains(face))
            {
                throw new InvalidCardException();
            }
        }
    }
}
