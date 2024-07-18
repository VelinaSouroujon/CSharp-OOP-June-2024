using Logging.Interfaces;
using Logging.Interfaces.Factories;
using Logging.Layouts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logging.Factories.Layouts
{
    public class XmlLayoutFactory : ILayoutFactory
    {
        public ILayout CreateLayout()
        {
            return new XmlLayout();
        }
    }
}
