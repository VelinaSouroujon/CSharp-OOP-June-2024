using FootballManager.Core.Contracts;
using FootballManager.Models;
using FootballManager.Models.Contracts;
using FootballManager.Repositories;
using FootballManager.Repositories.Contracts;
using FootballManager.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballManager.Core
{
    public class Controller : IController
    {
        private const int MaxCapacity = 10;
        private readonly IRepository<ITeam> championship;

        public Controller()
        {
            championship = new TeamRepository();
        }
        public string ChampionshipRankings()
        {
            StringBuilder sb = new StringBuilder();

            List<ITeam> orderedTeams = championship.Models
                .OrderByDescending(x => x.ChampionshipPoints)
                .ThenByDescending(x => x.PresentCondition)
                .ToList();

            sb.AppendLine("***Ranking Table***");

            for (int i = 0; i < orderedTeams.Count; i++)
            {
                ITeam team = orderedTeams[i];
                sb.AppendLine($"{i + 1}. {team}/{team.TeamManager}");
            }

            return sb.ToString().TrimEnd();
        }

        public string JoinChampionship(string teamName)
        {
            if(championship.Capacity == MaxCapacity)
            {
                return OutputMessages.ChampionshipFull;
            }
            if(championship.Exists(teamName))
            {
                return string.Format(OutputMessages.TeamWithSameNameExisting, teamName);
            }

            ITeam team = new Team(teamName);
            championship.Add(team);

            return string.Format(OutputMessages.TeamSuccessfullyJoined, teamName);
        }

        public string MatchBetween(string teamOneName, string teamTwoName)
        {
            ITeam teamOne = championship.Get(teamOneName);
            ITeam teamTwo = championship.Get(teamTwoName);

            if(teamOne is null || teamTwo is null)
            {
                return OutputMessages.OneOfTheTeamDoesNotExist;
            }
            if(teamOne.PresentCondition == teamTwo.PresentCondition)
            {
                teamOne.GainPoints(1);
                teamTwo.GainPoints(1);

                return string.Format(OutputMessages.MatchIsDraw, teamOneName, teamTwoName);
            }

            ITeam winningTeam = teamOne.PresentCondition > teamTwo.PresentCondition ? teamOne : teamTwo;
            ITeam losingTeam = winningTeam == teamOne ? teamTwo : teamOne;

            winningTeam.GainPoints(3);
            if(winningTeam.TeamManager is not null)
            {
                winningTeam.TeamManager.RankingUpdate(5);
            }
            if(losingTeam.TeamManager is not null)
            {
                losingTeam.TeamManager.RankingUpdate(-5);
            }

            return string.Format(OutputMessages.TeamWinsMatch, winningTeam.Name, losingTeam.Name);
        }

        public string PromoteTeam(string droppingTeamName, string promotingTeamName, string managerTypeName, string managerName)
        {
            ITeam droppingTeam = championship.Get(droppingTeamName);
            if(droppingTeam is null)
            {
                return string.Format(OutputMessages.DroppingTeamDoesNotExist, droppingTeamName);
            }
            if(championship.Exists(promotingTeamName))
            {
                return string.Format(OutputMessages.TeamWithSameNameExisting, promotingTeamName);
            }

            ITeam promotingTeam = new Team(promotingTeamName);

            IManager manager = GetManager(managerTypeName, managerName);
            if((manager is not null) && (!HasManagerSignedWithAnotherTeam(managerName)))
            {
                promotingTeam.SignWith(manager);
            }

            foreach(ITeam t in championship.Models)
            {
                t.ResetPoints();
            }

            championship.Remove(droppingTeamName);
            championship.Add(promotingTeam);

            return string.Format(OutputMessages.TeamHasBeenPromoted, promotingTeamName);
        }

        public string SignManager(string teamName, string managerTypeName, string managerName)
        {
            ITeam team = championship.Get(teamName);
            if(team is null)
            {
                return string.Format(OutputMessages.TeamDoesNotTakePart, teamName);
            }

            IManager manager = GetManager(managerTypeName, managerName);
            if(manager is null)
            {
                return string.Format(OutputMessages.ManagerTypeNotPresented, managerTypeName);
            }

            if(team.TeamManager is not null)
            {
                return string.Format(OutputMessages.TeamSignedWithAnotherManager, teamName, team.TeamManager.Name);
            }
            if(HasManagerSignedWithAnotherTeam(managerName))
            {
                return string.Format(OutputMessages.ManagerAssignedToAnotherTeam, managerName);
            }

            team.SignWith(manager);

            return string.Format(OutputMessages.TeamSuccessfullySignedWithManager, managerName, teamName);
        }
        private IManager GetManager(string managerTypeName, string managerName)
        {
            switch (managerTypeName)
            {
                case nameof(AmateurManager):
                    return new AmateurManager(managerName);

                case nameof(SeniorManager):
                    return new SeniorManager(managerName);

                case nameof(ProfessionalManager):
                    return new ProfessionalManager(managerName);

                default:
                    return null;
            }
        }
        private bool HasManagerSignedWithAnotherTeam(string managerName)
        {
            return championship.Models
               .Where(x => x.TeamManager is not null)
               .Any(x => x.TeamManager.Name == managerName);
        }
    }
}
