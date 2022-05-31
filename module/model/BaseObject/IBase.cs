using System;
using System.Collections.Generic;
using System.Text;

namespace VisitCounter
{
    interface IBase
    {
        public Dictionary<String, List<IInfrastructure>> GetInfrastDic();
        public Dictionary<int, IHuman> GetHumanDic();
        public IGrid GetGrid();
    }
}
