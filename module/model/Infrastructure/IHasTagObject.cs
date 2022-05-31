using System;
using System.Collections.Generic;
using System.Text;

namespace VisitCounter
{
    public interface IHasTagObject
    {
        public Dictionary<String, bool> HasTagDic { get; set; }
    }
}
