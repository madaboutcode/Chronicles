using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chronicles.Framework
{
    public class ConstraintViolationException:Exception
    {
        public ConstraintViolationException(string message):base(message) { }
    }
}
