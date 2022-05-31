using System;
using System.Collections.Generic;
using NetTopologySuite.Geometries;
using CityDataExpansionModule.OsmGeometries;
using System.Linq;
using System.IO;

namespace VisitCounter
{
    class UserDataProvider
    {
        public List<String> PolygonTagList { get; private set; }
        public List<String> WayTagList { get; private set; }
        public List<String> PointTagList { get; private set; }
        void InitGeometryTypeTagLists()
        {
            this.PolygonTagList = new List<String>()
            {
                "walkable", "!walkable", "water", "building", "barrier",
            };
            this.WayTagList = new List<String>()
            {
                "walkable", "!walkable", "water", "walkWay", "weakWay",
                "strongWay", "barrier", "passge"
            };
            this.PointTagList = new List<String>()
            {
                "leisure", "work", "home", "food"
            };
        }
        public UserDataProvider()
        {
            this.InitGeometryTypeTagLists();
        }
    }
}
