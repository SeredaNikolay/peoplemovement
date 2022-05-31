using System;
using System.Collections.Generic;
using CityDataExpansionModule.OsmGeometries;

namespace VisitCounter
{
    public interface IOsmToInfrastructureConverter
    {
        public void AddWalkableTag(
            Dictionary<String, TagsOfOutTag> TagsToOutTagsDic,
            IInfrastructure infrastObj,
            IOsmObject osmClosedWay);
        public void AddOutTags(
            Dictionary<String, TagsOfOutTag> TagsToOutTagsDic,
            IInfrastructure infrastObj,
            IOsmObject osmClosedWay);
    }
}
