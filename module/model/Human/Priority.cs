using System;
using System.Collections.Generic;
using NetTopologySuite.Geometries;


namespace VisitCounter
{
    public abstract class HumanPriority : MovingObject, IHumanPriority
    {
        Dictionary<String, IDestinationType> _dstTypeDic = 
            new Dictionary<String, IDestinationType>()
        {
            { "work", new DestinationType("work", 112.0, 1) },
            { "food", new DestinationType("food", 96.0, 2)},
            { "home", new DestinationType("home", 73.0, 3)},
            { "leisure", new DestinationType("leisure", 35.0, 4)}
        };

        public Dictionary<String, IDestinationType> GetDestinatinTypeDic 
        { 
            get { return this._dstTypeDic; } 
        }
        public HumanPriority(Point point) : base(point)
        {

        }
    }
        
}
