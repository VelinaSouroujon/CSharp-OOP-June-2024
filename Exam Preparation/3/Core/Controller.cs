using HighwayToPeak.Core.Contracts;
using HighwayToPeak.Models;
using HighwayToPeak.Models.Contracts;
using HighwayToPeak.Repositories;
using HighwayToPeak.Repositories.Contracts;
using HighwayToPeak.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HighwayToPeak.Core
{
    public class Controller : IController
    {
        private static HashSet<string> difficultyLevels = new HashSet<string>()
        {
            "Extreme",
            "Hard",
            "Moderate"
        };
        private const int MinStamina = 0;
        private const int MaxStamina = 10;

        private readonly IRepository<IPeak> peaks;
        private readonly IRepository<IClimber> climbers;
        private readonly IBaseCamp baseCamp;

        public Controller()
        {
            peaks = new PeakRepository();
            climbers = new ClimberRepository();
            baseCamp = new BaseCamp();
        }
        public string AddPeak(string name, int elevation, string difficultyLevel)
        {
            if(peaks.Get(name) is not null)
            {
                return string.Format(OutputMessages.PeakAlreadyAdded, name);
            }
            if(!difficultyLevels.Contains(difficultyLevel))
            {
                return string.Format(OutputMessages.PeakDiffucultyLevelInvalid, difficultyLevel);
            }

            IPeak peak = new Peak(name, elevation, difficultyLevel);
            peaks.Add(peak);

            return string.Format(OutputMessages.PeakIsAllowed, name, nameof(PeakRepository));
        }

        public string AttackPeak(string climberName, string peakName)
        {
            IClimber climber = climbers.Get(climberName);
            if(climber is null)
            {
                return string.Format(OutputMessages.ClimberNotArrivedYet, climberName);
            }

            IPeak peak = peaks.Get(peakName);
            if(peak is null)
            {
                return string.Format(OutputMessages.PeakIsNotAllowed, peakName);
            }
            if(!baseCamp.Residents.Contains(climberName))
            {
                return string.Format(OutputMessages.ClimberNotFoundForInstructions, climberName, peakName);
            }
            if(peak.DifficultyLevel == "Extreme" && climber.GetType().Name == nameof(NaturalClimber))
            {
                return string.Format(OutputMessages.NotCorrespondingDifficultyLevel, climberName, peakName);
            }

            baseCamp.LeaveCamp(climberName);
            climber.Climb(peak);

            if(climber.Stamina == MinStamina)
            {
                return string.Format(OutputMessages.NotSuccessfullAttack, climberName);
            }

            baseCamp.ArriveAtCamp(climberName);

            return string.Format(OutputMessages.SuccessfulAttack, climberName, peakName);
        }

        public string BaseCampReport()
        {
            if (baseCamp.Residents.Count == 0)
            {
                return "BaseCamp is currently empty.";
            }

            StringBuilder sb = new StringBuilder();

            sb.AppendLine("BaseCamp residents:");

            foreach(string name in baseCamp.Residents)
            {
                IClimber climber = climbers.Get(name);
                sb.AppendLine($"Name: {climber.Name}, Stamina: {climber.Stamina}, Count of Conquered Peaks: {climber.ConqueredPeaks.Count}");
            }

            return sb.ToString().TrimEnd();
        }

        public string CampRecovery(string climberName, int daysToRecover)
        {
            if(!baseCamp.Residents.Contains(climberName))
            {
                return string.Format(OutputMessages.ClimberIsNotAtBaseCamp, climberName);
            }

            IClimber climber = climbers.Get(climberName);

            if(climber.Stamina == MaxStamina)
            {
                return string.Format(OutputMessages.NoNeedOfRecovery, climberName);
            }

            climber.Rest(daysToRecover);

            return string.Format(OutputMessages.ClimberRecovered, climberName, daysToRecover);
        }

        public string NewClimberAtCamp(string name, bool isOxygenUsed)
        {
            if(climbers.Get(name) is not null)
            {
                return string.Format(OutputMessages.ClimberCannotBeDuplicated, name, nameof(ClimberRepository));
            }

            IClimber climber = isOxygenUsed ? new OxygenClimber(name) : new NaturalClimber(name);
            climbers.Add(climber);
            baseCamp.ArriveAtCamp(name);

            return string.Format(OutputMessages.ClimberArrivedAtBaseCamp, name);
        }

        public string OverallStatistics()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("***Highway-To-Peak***");

            List<IClimber> orderedClimbers = climbers.All
                .OrderByDescending(x => x.ConqueredPeaks.Count)
                .ThenBy(x => x.Name)
                .ToList();

            foreach(IClimber climber in orderedClimbers)
            {
                sb.AppendLine(climber.ToString());

                List<IPeak> orderedPeaks = climber.ConqueredPeaks
                    .Select(x => peaks.Get(x))
                    .OrderByDescending(x => x.Elevation)
                    .ToList();

                foreach (IPeak peak in orderedPeaks)
                {
                    sb.AppendLine(peak.ToString());
                }
            }

            return sb.ToString().TrimEnd();
        }
    }
}
