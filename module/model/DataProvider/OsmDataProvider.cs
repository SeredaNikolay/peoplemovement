using System;
using System.Collections.Generic;
using NetTopologySuite.Geometries;
using CityDataExpansionModule.OsmGeometries;
using System.Linq;

namespace VisitCounter
{
    class OsmDataProvider
    {
        public OSMLSGlobalLibrary.IInheritanceTreeCollection<Geometry>
            MapObjects
        { get; private set; }
        public List<OsmClosedWay> ClosedWayList { get; private set; }
        public List<OsmWay> NotClosedWayList { get; private set; }
        public List<OsmNode> NodeList { get; private set; }
        public Dictionary<String, TagsOfOutTag> PolygonTagsToOutTagsDic
        {
            get; private set;
        }
        public Dictionary<String, TagsOfOutTag> PolygonWalkableTagsDic
        {
            get; private set;
        }
        public Dictionary<String, TagsOfOutTag> LineStringTagsToOutTagsDic
        {
            get; private set;
        }
        public Dictionary<String, TagsOfOutTag> LineStringWalkableTagsDic
        {
            get; private set;
        }
        public Dictionary<String, TagsOfOutTag> PointTagsToOutTagsDic
        {
            get; private set;
        }
        //-----------------------------------------------------------------
        Dictionary<String, List<String>> _leisureTags;
        Dictionary<String, List<String>> _foodTags;
        Dictionary<String, List<String>> _residentialBuildingTags;
        Dictionary<String, List<String>> _waterTags;
        Dictionary<String, List<String>> _walkableTags;

        TagsOfOutTag _leisureDic;
        TagsOfOutTag _foodDic;
        TagsOfOutTag _residentialBuildingDic;
        TagsOfOutTag _waterDic;
        TagsOfOutTag _polygonWalkableDic;

        //-----------------------------------------------------------------
        Dictionary<String, List<String>> _walkWayTags;
        Dictionary<String, List<String>> _weakWayTags;
        Dictionary<String, List<String>> _passageTags;
        Dictionary<String, List<String>> _notStrongWayTags;
        Dictionary<String, List<String>> _notForPeopleBarrierTags;

        TagsOfOutTag _walkWayDic;
        TagsOfOutTag _weakWayDic;
        TagsOfOutTag _passageDic;
        TagsOfOutTag _notStrongWayDic;
        TagsOfOutTag _notForPeopleBarrierDic;
        TagsOfOutTag _wayWalkableDic;

        void InitPolygonTagDics()
        {
            this._leisureTags = new Dictionary<String, List<string>>
            {
                {"amenity", new List<String>()
                    {
                        "arts_centre", "brothel", "casino", "cinema",
                        "community_centre", "conference_centre",
                        "events_venue","fountain", "gambling",
                        "love_hotel","planetarium", "public_bookcase",
                        "social_centre", "stripclub", "swingerclub",
                        "theatre"
                    }
                },
                {"tourism", new List<String>()
                    {
                        "aquarium", "artwork", "attraction", "gallery",
                        "museum", "picnic_site", "theme_park", "viewpoint",
                        "zoo"
                    }
                },
                {"shop", new List<String>()
                    {
                        "department_store", "mall"
                    }
                },
                {"leisure", new List<String>()
                   {
                        Constant.tagOfAllTags
                    }
                }
            };
            this._foodTags = new Dictionary<String, List<String>>
            {
                {"amenity", new List<String>()
                    {
                        "bar", "biergarten", "cafe", "fast_food",
                        "food_court", "ice_cream", "pub", "restaurant"
                    }
                },
                {"shop", new List<String>()
                    {
                        "bakery", "beverages", "butcher", "cheese",
                        "chocolate", "confectionery", "confectionery",
                        "deli", "dairy", "farm", "frozen_food",
                        "frozen_food", "health_food", "ice_cream",
                        "pasta", "pastry", "seafood", "department_store",
                        "general", "supermarket"
                    }
                }
            };
            this._residentialBuildingTags =
                new Dictionary<String, List<String>>()
            {
                { "building", new List<String>()
                    {
                        "apartments", "barracks", "bungalow", "cabin",
                        "detached", "dormitory", "farm", "ger", "house",
                        "residential", "semidetached_house",
                        "static_caravan", "terrace"
                    }
                }
            };
            this._waterTags = new Dictionary<String, List<String>>()
            {
                { "water", new List<String>()
                    {
                        Constant.tagOfAllTags
                    }
                }
            };
            this._walkableTags = new Dictionary<String, List<string>>
            {
                {"leisure", new List<String>()
                   {
                        "swimming_area", "playground", "park",
                        "nature_reserve", "miniature_golf","golf_course",
                        "garden", "fitness_station", "fishing", "dog_park",
                        "disc_golf_course", "beach_resort", "pitch"
                   }
                }
            };
        }
        void InitWayTagDics()
        {
            this._walkWayTags = new Dictionary<String, List<String>>()
            {
                { "highway", new List<String>()
                    {
                        "pedestrian", "footway", "steps", "path",
                        "corridor","track"
                    }
                }
            };
            this._weakWayTags = new Dictionary<String, List<String>>()
            {
                { "highway", new List<String>()
                    {
                        "cycleway", "tertiary_link", "service",
                        "bridleway"
                    }
                }
            };
            this._passageTags = new Dictionary<String, List<String>>()
            {
                { "crossing", new List<String>()
                    {
                        "no"
                    }
                }
            };
            this._notStrongWayTags = new Dictionary<String, List<String>>()
            {
                {   "highway",
                    this._walkWayTags["highway"]
                        .Concat(this._weakWayTags["highway"])
                        .ToList<String>()
                }
            };
            this._notForPeopleBarrierTags =
                new Dictionary<String, List<String>>()
            {
                { "barrier", new List<String>()
                    {
                        "motorcycle_barrier", "spikes", "sump_buster",
                        "bollar", "chain", "kerb", "log"
                    }
                }
            };
        }
        void InitPolygonTagsOfOutTagDic()
        {
            this._leisureDic = new TagsOfOutTag(
                this._leisureTags,
                true, false,
                "", "!");
            this._foodDic = new TagsOfOutTag(
                this._foodTags,
                true, false,
                "", "!");
            this._residentialBuildingDic = new TagsOfOutTag(
                this._residentialBuildingTags,
                true, true,
                "", "!");
            this._waterDic = new TagsOfOutTag(
                this._waterTags,
                true, false,
                "", "!");
            this._polygonWalkableDic = new TagsOfOutTag(
                this._walkableTags,
                true, false,
                "", "!");
        }
        void InitWayTagsOfOutTagDic()
        {
            this._walkWayDic = new TagsOfOutTag(
                    this._walkWayTags,
                    true, false,
                    "", "!");
            this._weakWayDic = new TagsOfOutTag(
                    this._weakWayTags,
                    true, false,
                    "", "!");
            this._passageDic = new TagsOfOutTag(
                    this._passageTags,
                    false, true,
                    "", "");
            this._notStrongWayDic = new TagsOfOutTag(
                    this._notStrongWayTags,
                    false, true,
                    "", "");
            this._notForPeopleBarrierDic =
                new TagsOfOutTag(
                    this._notForPeopleBarrierTags,
                    false, true,
                    "", "");
            this._wayWalkableDic =
                new TagsOfOutTag(
                    this._notStrongWayTags,
                    true, false,
                    "", "!");
        }
        void InitPublicProperty()
        {
            this.PointTagsToOutTagsDic =
                new Dictionary<String, TagsOfOutTag>
            {
                {"leisure", this._leisureDic },
                {"food", this._foodDic },
            };
            this.PolygonWalkableTagsDic =
                new Dictionary<String, TagsOfOutTag>
            {
                {"walkable", this._polygonWalkableDic }
            };
            this.LineStringWalkableTagsDic =
                new Dictionary<String, TagsOfOutTag>
            {
                {"walkable", this._wayWalkableDic }
            };
            this.PolygonTagsToOutTagsDic =
                new Dictionary<String, TagsOfOutTag>
            {
                    {
                        "residentionalBuilding",
                        this._residentialBuildingDic
                    },
                    {"leisure", this._leisureDic },
                    {"food", this._foodDic },
                    {"water", this._waterDic },
            };
            this.LineStringTagsToOutTagsDic =
                new Dictionary<String, TagsOfOutTag>
            {
                {"walkWay", this._walkWayDic },
                {"weakWay", this._weakWayDic },
                {"passageWay", this._passageDic},
                {"strongWay", this._notStrongWayDic },
                {"barrier", this._notForPeopleBarrierDic }
            };
        }

        public OsmDataProvider(
            OSMLSGlobalLibrary.IInheritanceTreeCollection<Geometry>
                mapObjects)
        {
            this.MapObjects = mapObjects;
            this.ClosedWayList = this.MapObjects.GetAll<OsmClosedWay>();
            this.NotClosedWayList = this.MapObjects.GetAll<OsmWay>();
            this.NodeList = this.MapObjects.GetAll<OsmNode>();
            this.InitPolygonTagDics();
            this.InitWayTagDics();
            this.InitPolygonTagsOfOutTagDic();
            this.InitWayTagsOfOutTagDic();
            this.InitPublicProperty();
        }
    }
}
