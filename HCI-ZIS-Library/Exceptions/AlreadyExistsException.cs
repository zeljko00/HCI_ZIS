using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCI_ZIS_Library.Exceptions
{
    public class AlreadyExistsException: Exception
    {
        public AlreadyExistsException(string msg): base(msg) { }
        public AlreadyExistsException() : base() { }
    }
}
