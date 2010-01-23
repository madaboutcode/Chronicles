using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chronicles.Framework.Caching
{
    [Serializable]
    public class CachedPage
    {
        public byte[] Content { get; set; }
        public string ContentType { get; set; }
        public DateTime Time { get; set; }
        public bool IsCompressed { get; set; }
    }
}
