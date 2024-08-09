using NauticalCatchChallenge.Core.Contracts;
using NauticalCatchChallenge.Models;
using NauticalCatchChallenge.Models.Contracts;
using NauticalCatchChallenge.Repositories;
using NauticalCatchChallenge.Repositories.Contracts;
using NauticalCatchChallenge.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NauticalCatchChallenge.Core
{
    public class Controller : IController
    {
        private readonly IRepository<IDiver> divers;
        private readonly IRepository<IFish> fish;

        public Controller()
        {
            divers = new DiverRepository();
            fish = new FishRepository();
        }
        public string ChaseFish(string diverName, string fishName, bool isLucky)
        {
            IDiver currentDiver = divers.GetModel(diverName);
            if (currentDiver is null)
            {
                return string.Format(OutputMessages.DiverNotFound, nameof(DiverRepository), diverName);
            }

            IFish currentFish = fish.GetModel(fishName);
            if(currentFish is null)
            {
                return string.Format(OutputMessages.FishNotAllowed, fishName);
            }

            if(currentDiver.HasHealthIssues)
            {
                return string.Format(OutputMessages.DiverHealthCheck, diverName);
            }
            if(currentDiver.OxygenLevel < currentFish.TimeToCatch)
            {
                currentDiver.Miss(currentFish.TimeToCatch);

                return string.Format(OutputMessages.DiverMisses, diverName, fishName);
            }
            if(currentDiver.OxygenLevel == currentFish.TimeToCatch)
            {
                if(isLucky)
                {
                    currentDiver.Hit(currentFish);
                    return string.Format(OutputMessages.DiverHitsFish, diverName, currentFish.Points, fishName);
                }

                currentDiver.Miss(currentFish.TimeToCatch);
                return string.Format(OutputMessages.DiverMisses, diverName, fishName);
            }

            currentDiver.Hit(currentFish);

            return string.Format(OutputMessages.DiverHitsFish, diverName, currentFish.Points, fishName);
        }

        public string CompetitionStatistics()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("**Nautical-Catch-Challenge**");

            List<IDiver> orderedDivers = divers.Models
                .Where(x => !x.HasHealthIssues)
                .OrderByDescending(x => x.CompetitionPoints)
                .ThenByDescending(x => x.Catch.Count)
                .ThenBy(x => x.Name)
                .ToList();

            foreach(IDiver diver in orderedDivers)
            {
                sb.AppendLine(diver.ToString());
            }

            return sb.ToString().TrimEnd();
        }

        public string DiveIntoCompetition(string diverType, string diverName)
        {
            if (diverType != nameof(FreeDiver)
                && diverType != nameof(ScubaDiver))
            {
                return string.Format(OutputMessages.DiverTypeNotPresented, diverType);
            }

            if(divers.GetModel(diverName) is not null)
            {
                return string.Format(OutputMessages.DiverNameDuplication, diverName, nameof(DiverRepository));
            }

            IDiver diver = null;
            if(diverType == nameof(FreeDiver))
            {
                diver = new FreeDiver(diverName);
            }
            else if(diverType == nameof(ScubaDiver))
            {
                diver = new ScubaDiver(diverName);
            }

            divers.AddModel(diver);

            return string.Format(OutputMessages.DiverRegistered, diverName, nameof(DiverRepository));
        }

        public string DiverCatchReport(string diverName)
        {
            IDiver diver = divers.GetModel(diverName);
            StringBuilder sb = new StringBuilder();

            sb.AppendLine(diver.ToString());
            sb.AppendLine("Catch Report:");

            foreach(string name in diver.Catch)
            {
                IFish catchedFish = fish.GetModel(name);
                sb.AppendLine(catchedFish.ToString());
            }

            return sb.ToString().TrimEnd();
        }

        public string HealthRecovery()
        {
            int diversRecoveredCount = 0;
            foreach(IDiver diver in divers.Models)
            {
                if(diver.HasHealthIssues)
                {
                    diversRecoveredCount++;

                    diver.UpdateHealthStatus();
                    diver.RenewOxy();
                }
            }

            return string.Format(OutputMessages.DiversRecovered, diversRecoveredCount);
        }

        public string SwimIntoCompetition(string fishType, string fishName, double points)
        {
            if (fishType != nameof(ReefFish)
                && fishType != nameof(DeepSeaFish)
                && fishType != nameof(PredatoryFish))
            {
                return string.Format(OutputMessages.FishTypeNotPresented, fishType);
            }
            if(fish.GetModel(fishName) is not null)
            {
                return string.Format(OutputMessages.FishNameDuplication, fishName, nameof(FishRepository));
            }

            IFish fishToAdd = null;
            if(fishType == nameof(ReefFish))
            {
                fishToAdd = new ReefFish(fishName, points);
            }
            else if(fishType == nameof(DeepSeaFish))
            {
                fishToAdd = new DeepSeaFish(fishName, points);
            }
            else if(fishType == nameof(PredatoryFish))
            {
                fishToAdd = new PredatoryFish(fishName, points);
            }
            fish.AddModel(fishToAdd);

            return string.Format(OutputMessages.FishCreated, fishName);
        }
    }
}
