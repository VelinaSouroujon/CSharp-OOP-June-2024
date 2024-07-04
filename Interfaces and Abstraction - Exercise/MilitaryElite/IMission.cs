using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilitaryElite
{
    public interface IMission
    {
        string CodeName { get; }
        MissionState State { get; set; }

        void CompleteMission();
    }
}
