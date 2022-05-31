using System;
using System.Collections.Generic;
using CityDataExpansionModule.OsmGeometries;

namespace VisitCounter
{
    /// <summary>OsmToInfrastructureConverter interface.</summary>
    public interface IOsmToInfrastructureConverter
    {
        /// <summary>Adds walkable tag to infrastructure.</summary>
        /// <param name="TagsToOutTagsDic">
        /// Dictionary with string key tag and TagsOfOutTag value.
        /// </param>
        /// <param name="infrastObj">IInfrastructure to add tag.</param>
        /// <param name="osmObj">
        /// IOsmObject with tags to check for obstacles.
        /// </param>
        public void AddWalkableTag(
            Dictionary<String, TagsOfOutTag> TagsToOutTagsDic,
            IInfrastructure infrastObj,
            IOsmObject osmObj);

        /// <summary>Adds out tags to infrastructure.</summary>
        /// <param name="TagsToOutTagsDic">
        /// Dictionary with string tag and TagsOfOutTag value.
        /// </param>
        /// <param name="infrastObj">IInfrastructure to add tag.</param>
        /// <param name="osmObj">
        /// IOsmObject with tags to check for out tags.
        /// </param>
        public void AddOutTags(
            Dictionary<String, TagsOfOutTag> TagsToOutTagsDic,
            IInfrastructure infrastObj,
            IOsmObject osmObj);
    }
}
