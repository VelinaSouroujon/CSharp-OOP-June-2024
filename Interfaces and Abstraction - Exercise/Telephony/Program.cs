using System;

namespace Telephony
{
    public class Program
    {
        static void Main(string[] args)
        {
            string[] phoneNumbers = Console.ReadLine()
                .Split(' ', StringSplitOptions.RemoveEmptyEntries);

            foreach (string number in phoneNumbers)
            {
                if(!IsPhoneNumberValid(number))
                {
                    Console.WriteLine("Invalid number!");
                }
                else
                {
                    ICallable phone;
                    if(number.Length == 10)
                    {
                        phone = new Smartphone();
                    }
                    else
                    {
                        phone = new StationaryPhone();
                    }

                    Console.WriteLine(phone.Call(number));
                }
            }

            string[] websites = Console.ReadLine()
                .Split(' ', StringSplitOptions.RemoveEmptyEntries);

            foreach(string url in websites)
            {
                if(!IsURLValid(url))
                {
                    Console.WriteLine("Invalid URL!");
                }
                else
                {
                    Smartphone smartphone = new Smartphone();
                    Console.WriteLine(smartphone.Browse(url));
                }
            }
        }
        static bool IsPhoneNumberValid(string phoneNumber)
        {
            return (phoneNumber.Length == 10 || phoneNumber.Length == 7)
                && phoneNumber.All(x => Char.IsDigit(x));
        }
        static bool IsURLValid(string url)
        {
            return !url.Any(x => Char.IsDigit(x));
        }
    }
}
