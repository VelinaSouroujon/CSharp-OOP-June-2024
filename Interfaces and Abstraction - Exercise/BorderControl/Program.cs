using System;

namespace BorderControl
{
    public class Program
    {
        static void Main(string[] args)
        {
            List<SocietyMember> societyMembers = new List<SocietyMember>();

            string input = "";
            while((input = Console.ReadLine()).ToLower() != "end")
            {
                SocietyMember societyMember = null;

                string[] tokens = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);

                if(tokens.Length == 2)
                {
                    string robotModel = tokens[0];
                    string robotId = tokens[1];

                    societyMember = new Robot(robotModel, robotId);
                }
                else if(tokens.Length == 3)
                {
                    string citizenName = tokens[0];
                    int citizenAge = int.Parse(tokens[1]);
                    string citizenId = tokens[2];

                    societyMember = new Citizen(citizenName, citizenAge, citizenId);
                }

                societyMembers.Add(societyMember);
            }
            string fakeIdPattern = Console.ReadLine();

            foreach(SocietyMember member in societyMembers)
            {
                if (member.IsIdFake(fakeIdPattern))
                {
                    Console.WriteLine(member.Id);
                }
            }
        }
    }
}
