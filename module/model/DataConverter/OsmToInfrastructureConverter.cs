using System;
using System.Collections.Generic;
using System.Linq;
using CityDataExpansionModule.OsmGeometries;

namespace VisitCounter
{
    public class OsmToInfrastructureConverter: IOsmToInfrastructureConverter
    {
        public OsmToInfrastructureConverter()
        {

        }
        public void AddWalkableTag(
            Dictionary<String, TagsOfOutTag> TagsToOutTagsDic,
            IInfrastructure infrastObj,
            IOsmObject osmClosedWay)
        {
            foreach (KeyValuePair<String, TagsOfOutTag> pair
                in TagsToOutTagsDic)
            {
                if (pair.Value.ContainsTagsFromLists)
                {
                    if (this.IsTag(pair.Value, osmClosedWay, true))
                    {
                        infrastObj.HasTagDic.Add(pair.Key, true);
                    }
                    else
                    {
                        infrastObj.HasTagDic.Add("!" + pair.Key, true);
                    }
                }
            }
        }
        public void AddOutTags(
            Dictionary<String, TagsOfOutTag> TagsToOutTagsDic,
            IInfrastructure infrastObj,
            IOsmObject osmClosedWay)
        {
            foreach (KeyValuePair<String, TagsOfOutTag> pair
                in TagsToOutTagsDic)
            {
                if (pair.Value.ContainsTagsFromLists)
                {
                    if (this.IsTag(pair.Value, osmClosedWay, true))
                    {
                        infrastObj.HasTagDic.Add(
                            pair.Value.ContainsTagsPrefix + pair.Key,
                            true);
                    }
                }
                if (pair.Value.DoesNotContainTagsFromLists)
                {
                    if (this.IsTag(pair.Value, osmClosedWay, false))
                    {
                        infrastObj.HasTagDic.Add(
                            pair.Value.DoesNotContainTagsPrefix + pair.Key,
                            true);
                    }
                }
            }
        }
        bool IsTag(TagsOfOutTag tagsOfOutTag,
                   IOsmObject osmObject,
                   bool retValOnContains)
        {
            bool contains;
            List<String> keys = osmObject.Tags.Keys
                .Intersect(tagsOfOutTag.Tags.Keys).ToList<String>();
            if (keys != null && keys.Count > 0)
            {
                contains = this.OutTagContainsTags(
                    tagsOfOutTag,
                    osmObject,
                    keys,
                    retValOnContains);
                return contains;
            }
            return false;
        }
        bool OutTagContainsTags(TagsOfOutTag tagsOfOutTag,
                                IOsmObject osmObject,
                                List<String> keys,
                                bool retValOnContainsTag)
        {

            bool containsAnyTag, containsTagFromList;
            foreach (String key in keys)
            {
                containsAnyTag = tagsOfOutTag.Tags[key]
                    .Contains(Constant.tagOfAllTags);
                containsTagFromList = tagsOfOutTag.Tags[key]
                    .Contains(osmObject.Tags[key]);
                if (containsAnyTag || containsTagFromList)
                {
                    return retValOnContainsTag;
                }
            }
            return !retValOnContainsTag;
        }
    }
}