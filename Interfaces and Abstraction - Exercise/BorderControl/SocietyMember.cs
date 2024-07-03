using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BorderControl
{
    public abstract class SocietyMember : IIdentifiable
    {
        protected SocietyMember(string id)
        {
            Id = id;
        }

        public string Id { get; private set; }

        public bool IsIdFake(string fakeIdPattern)
        {
            return Id.EndsWith(fakeIdPattern);
        }
    }
}
