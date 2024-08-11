using Composite.Contracts;
using System;

namespace Composite
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IGift perfume = new Gift("Perfume", 15);
            IGift shampoo = new Gift("Shampoo", 10);
            IGift toy = new Gift("Toy", 5);
            IGift money = new Gift("Money", 20);

            CompositeGift cosmeticBox = new CompositeGift("Cosmetic box");
            cosmeticBox.AddGift(perfume);
            cosmeticBox.AddGift(shampoo);

            CompositeGift giftsFromRelatives = new CompositeGift("Gifts from relatives");
            giftsFromRelatives.AddGift(cosmeticBox);
            giftsFromRelatives.AddGift(toy);
            giftsFromRelatives.AddGift(money);

            CompositeGift giftsFromFriends = new CompositeGift("Gifts from friends");

            CompositeGift allGifts = new CompositeGift("All gifts");
            allGifts.AddGift(giftsFromRelatives);
            allGifts.AddGift(giftsFromFriends);

            Console.WriteLine(allGifts);
        }
    }
}
