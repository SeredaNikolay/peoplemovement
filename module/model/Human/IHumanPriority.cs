using System;
using System.Collections.Generic;
using System.Text;

namespace VisitCounter
{
    /// <summary>HumanPriority interface.</summary>
    public interface IHumanPriority: IMovingObject
    {
        /// <value>
        /// Property <c>GetDestinatinTypeDic</c> 
        /// Dictionary of destination types.
        /// </value>
        public Dictionary<String, IDestinationType> DestinatinTypeDic
        {
            get;
        }
    }
}
