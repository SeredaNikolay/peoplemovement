using System;
using System.Collections.Generic;
using CityDataExpansionModule.OsmGeometries;
using NetTopologySuite.Geometries;

namespace VisitCounter
{
    /// <summary>DataProvider interface.</summary>
    public interface IDataProvider
    {
        /// <summary>Gets additional radius by tags for ways.</summary>
        /// <param name="hasTagObj">
        /// Dictionary with string key tag and bool value 
        /// of existing this key.
        /// </param>
        /// <returns>Additional double radius for ways.</returns>
        public double GetAdditionalRadiusByTags(
            Dictionary<String, bool> hasTagObj);

        /// <summary>Gets map objects.</summary>
        /// <returns>
        /// IInheritanceTreeCollection<Geometry>map objects.
        /// </returns>
        public OSMLSGlobalLibrary.IInheritanceTreeCollection<Geometry>
            GetMapObjects();

        /// <summary>Gets closed way list.</summary>
        /// <returns>List of OsmClosedWay closed ways.</returns>
        public List<OsmClosedWay> GetClosedWayList();

        /// <summary>Gets not closed way list.</summary>
        /// <returns>List of OsmWay not closed ways.</returns>
        public List<OsmWay> GetNotClosedWayList();

        /// <summary>Gets node list.</summary>
        /// <returns>List of OsmNode nodes.</returns>
        public List<OsmNode> GetNodeList();

        /// <summary>Gets TagsToOutTags dictionary for polygon.</summary>
        /// <returns>Dictionary of TagsToOutTags.</returns>
        public Dictionary<String, TagsOfOutTag>
            GetPolygonTagsToOutTagsDic();

        /// <summary>
        /// Gets polygon walkable TagsToOutTags dictionary.
        /// </summary>
        /// <returns>Dictionary of TagsToOutTags.</returns>
        public Dictionary<String, TagsOfOutTag>
            GetPolygonWalkableTagsDic();

        /// <summary>Gets TagsToOutTags dictionary for linestring.</summary>
        /// <returns>Dictionary of TagsToOutTags.</returns>
        public Dictionary<String, TagsOfOutTag>
            GetLineStringTagsToOutTagsDic();

        /// <summary>
        /// Gets linestring walkable TagsToOutTags dictionary.
        /// </summary>
        /// <returns>Dictionary of TagsToOutTags.</returns>
        public Dictionary<String, TagsOfOutTag>
            GetLineStringWalkableTagsDic();

        /// <summary>Gets TagsToOutTags dictionary for point.</summary>
        /// <returns>Dictionary of TagsToOutTags.</returns>
        public Dictionary<String, TagsOfOutTag> GetPointTagsToOutTagsDic();

        /// <summary>Gets polygon tag list.</summary>
        /// <returns>String tag list for polygon.</returns>
        public List<String> GetPolygonTagList();

        /// <summary>Gets way tag list.</summary>
        /// <returns>String tag list for way.</returns>
        public List<String> GetWayTagList();

        /// <summary>Gets point tag list.</summary>
        /// <returns>String tag list for point.</returns>
        public List<String> GetPointTagList();
    }
}
