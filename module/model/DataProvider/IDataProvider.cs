using System;
using System.Collections.Generic;
using CityDataExpansionModule.OsmGeometries;
using NetTopologySuite.Geometries;

namespace VisitCounter
{
    public interface IDataProvider
    {
        public double GetAdditionalRadiusByTags(
            Dictionary<String, bool> hasTagObj);
        public OSMLSGlobalLibrary.IInheritanceTreeCollection<Geometry>
            GetMapObjects();
        public List<OsmClosedWay> GetClosedWayList();
        public List<OsmWay> GetNotClosedWayList();
        public List<OsmNode> GetNodeList();
        public Dictionary<String, TagsOfOutTag>
            GetPolygonTagsToOutTagsDic();
        public Dictionary<String, TagsOfOutTag>
            GetPolygonWalkableTagsDic();
        public Dictionary<String, TagsOfOutTag>
            GetLineStringTagsToOutTagsDic();
        public Dictionary<String, TagsOfOutTag>
            GetLineStringWalkableTagsDic();
        public Dictionary<String, TagsOfOutTag> GetPointTagsToOutTagsDic();
        public List<String> GetPolygonTagList();
        public List<String> GetWayTagList();
        public List<String> GetPointTagList();
    }
}
