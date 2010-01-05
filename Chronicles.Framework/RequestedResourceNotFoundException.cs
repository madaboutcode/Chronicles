using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chronicles.Framework
{
    public class RequestedResourceNotFoundException:Exception
    {
        public RequestedResourceNotFoundException() 
            : base("Sorry, I don't have that page. Please verify the url") {}

        public RequestedResourceNotFoundException(string message)
            : base(message) {}
    }
}
