using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mTickLibs.IcCardAdapter
{
    class IcCardException:Exception
    {
        public IcCardException(string message):base(message)
        {
        }
    }
}
