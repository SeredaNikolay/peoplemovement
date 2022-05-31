using System;
using System.Collections.Generic;
using System.Text;

namespace VisitCounter
{
    interface IHasTagObject
    {
        public Dictionary<String, bool> HasTagDic { get; set; }
    }
}
