using System;
using System.Collections.Generic;

namespace _3.Cards
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.Unicode;

            string[] input = Console.ReadLine()
                .Split(", ", StringSplitOptions.RemoveEmptyEntries);

            List<Card> cards = new List<Card>();

            foreach(string inputInfo in input)
            {
                string[] cardInfo = inputInfo.Split(' ', StringSplitOptions.RemoveEmptyEntries);

                try
                {
                    string cardFace = cardInfo[0];
                    char cardSuit = cardInfo[1][0];

                    Card currentCard = new Card(cardFace, cardSuit);
                    cards.Add(currentCard);
                }
                catch (InvalidCardException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            Console.WriteLine(string.Join(" ", cards));
        }
    }
}
