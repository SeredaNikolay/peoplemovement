using System;
using System.Collections.Generic;
using NetTopologySuite.Geometries;
using CityDataExpansionModule.OsmGeometries;

namespace VisitCounter
{
    class DataProvider : IDataProvider
    {
        OsmDataProvider _osmDataProvider;
        UserDataProvider _userDataProvider;
        public DataProvider(
            OSMLSGlobalLibrary.IInheritanceTreeCollection<Geometry>
            mapObjects)
        {
            this._osmDataProvider = new OsmDataProvider(mapObjects);
            this._userDataProvider = new UserDataProvider();
        }
        public double GetAdditionalRadiusByTags(
            Dictionary<String, bool> hasTagDic)
        {
            bool has;
            if (hasTagDic.TryGetValue("passageWay", out has))
                return 3.0;
            if (hasTagDic.TryGetValue("walkWay", out has))
                return 0.75;
            if (hasTagDic.TryGetValue("weakWay", out has))
                return 2.0;
            if (hasTagDic.TryGetValue("strongWay", out has))
                return 5.0;
            return 0.0;
        }
        public OSMLSGlobalLibrary.IInheritanceTreeCollection<Geometry>
            GetMapObjects()
        {
            return this._osmDataProvider.MapObjects;
        }
        public List<OsmClosedWay> GetClosedWayList()
        {
            return this._osmDataProvider.ClosedWayList;
        }
        public List<OsmWay> GetNotClosedWayList()
        {
            return this._osmDataProvider.NotClosedWayList;
        }
        public List<OsmNode> GetNodeList()
        {
            return this._osmDataProvider.NodeList;
        }

        public Dictionary<String, TagsOfOutTag>
            GetPolygonTagsToOutTagsDic()
        {
            return this._osmDataProvider.PolygonTagsToOutTagsDic;
        }
        public Dictionary<String, TagsOfOutTag>
            GetPolygonWalkableTagsDic()
        {
            return this._osmDataProvider.PolygonWalkableTagsDic;
        }
        public Dictionary<String, TagsOfOutTag>
            GetLineStringTagsToOutTagsDic()
        {
            return this._osmDataProvider.LineStringTagsToOutTagsDic;
        }
        public Dictionary<String, TagsOfOutTag>
            GetLineStringWalkableTagsDic()
        {
            return this._osmDataProvider.LineStringWalkableTagsDic;
        }
        public Dictionary<String, TagsOfOutTag> GetPointTagsToOutTagsDic()
        {
            return this._osmDataProvider.PointTagsToOutTagsDic;
        }
        public List<String> GetPolygonTagList()
        {
            return this._userDataProvider.PolygonTagList;
        }
        public List<String> GetWayTagList()
        {
            return this._userDataProvider.WayTagList;
        }
        public List<String> GetPointTagList()
        {
            return this._userDataProvider.PointTagList;
        }
    }
}
