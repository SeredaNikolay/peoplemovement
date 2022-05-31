using System;
using System.Collections.Generic;
using System.Text;

namespace VisitCounter
{
    public interface IHumanPriority: IMovingObject
    {
        public Dictionary<String, IDestinationType> GetDestinatinTypeDic
        {
            get;
        }
    }
}
