using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCI_ZIS_Library.Exceptions
{
    internal class NotFoundException: Exception
    {
        public NotFoundException(string msg) : base(msg) { }
        public NotFoundException() : base() { }
    }
}
