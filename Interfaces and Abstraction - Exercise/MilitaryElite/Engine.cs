using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilitaryElite
{
    public class Engine
    {
        private List<ISoldier> soldiers;
        private Dictionary<string, IPrivate> privateSoldiersById;

        public Engine()
        {
            soldiers = new List<ISoldier>();
            privateSoldiersById = new Dictionary<string, IPrivate>();
        }
        public void Run()
        {
            string input = "";
            while((input = Console.ReadLine()).ToLower() != "end")
            {
                ISoldier soldier = GetSoldier(input);
                if(soldier == null)
                {
                    continue;
                }

                soldiers.Add(soldier);
            }

            PrintSoldiers();
        }
        private ISoldier GetSoldier(string input)
        {
            string[] info = input
                .Split(' ', StringSplitOptions.RemoveEmptyEntries);

            string type = info[0].ToLower();
            string id = info[1];
            string firstName = info[2];
            string lastName = info[3];

            if(type == "spy")
            {
                int codeNumber = int.Parse(info[4]);
                return new Spy(id, firstName, lastName, codeNumber);
            }

            decimal salary = decimal.Parse(info[4]);

            if(type == "private")
            {
                Private privateSoldier = new Private(id, firstName, lastName, salary);
                AddPrivate(privateSoldier);

                return privateSoldier;
            }

            if(type == "lieutenantgeneral")
            {
                LieutenantGeneral general = new LieutenantGeneral(id, firstName, lastName, salary);

                string[] privatesId = info
                    .Skip(5)
                    .ToArray();
                foreach(string soldierId in privatesId)
                {
                    general.AddSoldier(privateSoldiersById[soldierId]);
                }
                
                return general;
            }

            if (Enum.TryParse<Corps>(info[5], out Corps corps) == false)
            {
                return null;
            }

            if(type == "engineer")
            {
                Engineer engineer = new Engineer(id, firstName, lastName, salary, corps);

                for(int i = 6; i < info.Length - 1; i += 2)
                {
                    string repairPart = info[i];
                    int repairHours = int.Parse(info[i + 1]);

                    IRepair repair = new Repair(repairPart, repairHours);
                    engineer.AddRepair(repair);
                }

                return engineer;
            }
            if(type == "commando")
            {
                Commando commando = new Commando(id, firstName, lastName, salary, corps);

                for (int i = 6; i < info.Length - 1; i += 2)
                {
                    if (Enum.TryParse<MissionState>(info[i + 1], out MissionState missionState) == false)
                    {
                        continue;
                    }

                    string missionCodeName = info[i];

                    commando.AddMission(new Mission(missionCodeName, missionState));
                }

                return commando;
            }

            return null;
        }
        private void AddPrivate(IPrivate privateSoldier)
        {
            if(!privateSoldiersById.ContainsKey(privateSoldier.Id))
            {
                privateSoldiersById.Add(privateSoldier.Id, privateSoldier);
            }
        }
        private void PrintSoldiers()
        {
            foreach(ISoldier soldier in soldiers)
            {
                Console.WriteLine(soldier);
            }
        }
    }
}
