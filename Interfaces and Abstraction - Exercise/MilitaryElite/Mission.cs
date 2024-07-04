using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MilitaryElite
{
    public class Mission : IMission
    {
        public Mission(string codeName, MissionState state)
        {
            CodeName = codeName;
            State = state;
        }

        public string CodeName { get; private set; }

        public MissionState State { get; set; }

        public void CompleteMission()
        {
            State = MissionState.Finished;
        }
        public override string ToString()
        {
            return $"Code Name: {CodeName} State: {State}";
        }
    }
}
