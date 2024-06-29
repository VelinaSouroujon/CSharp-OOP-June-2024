using System;
using System.Collections.Generic;
using System.Linq;

namespace FootballTeamGenerator
{
    public class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string, Team> teams = new Dictionary<string, Team>();
            const string InvalidTeamNameMessage = "Team {0} does not exist.";

            string input = "";
            while((input = Console.ReadLine()).ToLower() != "end")
            {
                try
                {
                    string[] tokens = input.Split(';', StringSplitOptions.RemoveEmptyEntries);
                    string command = tokens[0].ToLower();
                    string teamName = tokens[1];

                    switch (command)
                    {
                        case "team":
                            teams.Add(teamName, new Team(teamName));
                            break;

                        case "add":
                            ValidateTeam(teams, teamName, string.Format(InvalidTeamNameMessage, teamName));

                            string playerName = tokens[2];

                            int[] stats = tokens
                                .Skip(3)
                                .Select(int.Parse)
                                .ToArray();

                            Player player = new Player(playerName, stats);
                            teams[teamName].AddPlayer(player);

                            break;

                        case "remove":
                            ValidateTeam(teams, teamName, string.Format(InvalidTeamNameMessage, teamName));

                            playerName = tokens[2];
                            teams[teamName].RemovePlayer(playerName);

                            break;

                        case "rating":
                            ValidateTeam(teams, teamName, string.Format(InvalidTeamNameMessage, teamName));

                            int rating = teams[teamName].Rating;

                            Console.WriteLine($"{teamName} - {rating}");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
        static void ValidateTeam(Dictionary<string, Team> teams, string teamName, string exceptionMessage)
        {
            if (!teams.ContainsKey(teamName))
            {
                throw new ArgumentException(exceptionMessage);
            }
        }
    }
}
