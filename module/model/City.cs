using System;
using System.Collections.Generic;
using NetTopologySuite.Geometries;
using CityDataExpansionModule.OsmGeometries;
using System.Linq;
using System.IO;

namespace VisitCounter
{
    class City
    {
        List<String> _polygonTagList = new List<String>()
        {
            "walkable", "!walkable", "water", "building", "barrier",
        };
        List<String> _wayTagList = new List<String>()
        {
            "walkable", "!walkable", "water", "walkWay", "weakWay",
            "strongWay", "barrier", "passge"
        };
        List<String> _pointTagList = new List<String>()
        {
            "leisure", "work", "home", "food",
        };

        //-------------------------------------------------------------------------------------------
        static Dictionary<String, List<String>> leisureTags = new Dictionary<String, List<string>>
        {
            {"amenity", new List<String>()
                {
                    "arts_centre", "brothel", "casino", "cinema",
                    "community_centre", "conference_centre", "events_venue",
                    "fountain", "gambling", "love_hotel",
                    "planetarium", "public_bookcase", "social_centre",
                    "stripclub", "swingerclub", "theatre"
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
        static Dictionary<String, List<String>> foodTags = new Dictionary<String, List<String>>
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
        static Dictionary<String, List<String>> residentialBuildingTags = new Dictionary<String, List<String>>()
        {
            { "building", new List<String>()
                {
                    "apartments", "barracks", "bungalow", "cabin", "detached",
                    "dormitory", "farm", "ger", "house", "residential",
                    "semidetached_house", "static_caravan", "terrace"
                }
            }
        };
        static Dictionary<String, List<String>> waterTags = new Dictionary<String, List<String>>()
        {
            { "water", new List<String>()
                {
                    Constant.tagOfAllTags
                }
            }
        };
        static Dictionary<String, List<String>> walkableTags = new Dictionary<String, List<string>>
        {
            {"leisure", new List<String>()
                {
                    "swimming_area", "playground", "park", "nature_reserve", "miniature_golf",
                    "golf_course", "garden", "fitness_station", "fishing", "dog_park",
                    "disc_golf_course", "beach_resort", "pitch"
                }
            }
        };
        static TagsOfOutTag leisureDic = new TagsOfOutTag(leisureTags, true, false, "", "!");
        static TagsOfOutTag foodDic = new TagsOfOutTag(foodTags, true, false, "", "!");
        static TagsOfOutTag residentialBuildingDic = new TagsOfOutTag(residentialBuildingTags, true, true, "", "!");
        static TagsOfOutTag waterDic = new TagsOfOutTag(waterTags, true, false, "", "!");
        static TagsOfOutTag polygonWalkableDic = new TagsOfOutTag(walkableTags, true, false, "", "!");

        //-------------------------------------------------------------------------------------------
        static Dictionary<String, List<String>> walkWayTags = new Dictionary<String, List<String>>()
        {
            { "highway", new List<String>()
                {
                    "pedestrian", "footway", "steps", "path", "corridor",
                    "track"
                }
            }
        };
        static Dictionary<String, List<String>> weakWayTags = new Dictionary<String, List<String>>()
        {
            { "highway", new List<String>()
                {
                    "cycleway", "tertiary_link", "service", "bridleway"
                }
            }
        };
        static Dictionary<String, List<String>> passageTags = new Dictionary<String, List<String>>()
        {
            { "crossing", new List<String>()
                {
                    "no"
                }
            }
        };
        static Dictionary<String, List<String>> notStrongWayTags = new Dictionary<String, List<String>>()
        {
            { "highway",
                walkWayTags["highway"].Concat(weakWayTags["highway"]).ToList<String>()
            }
        };
        static Dictionary<String, List<String>> notForPeopleBarrierTags = new Dictionary<String, List<String>>()
        {
            { "barrier", new List<String>()
                {
                    "motorcycle_barrier", "spikes", "sump_buster", "bollar", "chain",
                    "kerb", "log"
                }
            }
        };
        static TagsOfOutTag walkWayDic = new TagsOfOutTag(walkWayTags, true, false, "", "!");
        static TagsOfOutTag weakWayDic = new TagsOfOutTag(weakWayTags, true, false, "", "!");
        static TagsOfOutTag passageDic = new TagsOfOutTag(passageTags, false, true, "", "");
        static TagsOfOutTag notStrongWayDic = new TagsOfOutTag(notStrongWayTags, false, true, "", "");
        static TagsOfOutTag notForPeopleBarrierDic = new TagsOfOutTag(notForPeopleBarrierTags, false, true, "", "");
        static TagsOfOutTag wayWalkableDic = new TagsOfOutTag(notStrongWayTags, true, false, "", "!");

        //-------------------------------------------------------------------------------------------
        static Dictionary<String, TagsOfOutTag> PolygonTagsToOutTagsDic = new Dictionary<String, TagsOfOutTag>
        {
            {"residentionalBuilding", City.residentialBuildingDic },
            {"leisure", City.leisureDic },
            {"food", City.foodDic },
            {"water", City.waterDic },
        };
        static Dictionary<String, TagsOfOutTag> LineStringTagsToOutTagsDic = new Dictionary<String, TagsOfOutTag>
        {
            {"walkWay", City.walkWayDic },
            {"weakWay", City.weakWayDic },
            {"passageWay", City.passageDic},
            {"strongWay", City.notStrongWayDic },
            {"barrier", City.notForPeopleBarrierDic }
        };
        static Dictionary<String, TagsOfOutTag> PointTagsToOutTagsDic = new Dictionary<String, TagsOfOutTag>
        {
            {"leisure", City.leisureDic },
            {"food", City.foodDic },
        };
        static Dictionary<String, TagsOfOutTag> PolygonWalkableTagsDic = new Dictionary<String, TagsOfOutTag>
        {
            {"walkable", City.polygonWalkableDic }
        };
        static Dictionary<String, TagsOfOutTag> LineStringWalkableTagsDic = new Dictionary<String, TagsOfOutTag>
        {
             {"walkable", City.wayWalkableDic }
        };
        static List<String> notEntranceTagList = new List<String>()
        {
            "exit", "emergency"
        };
        static List<String> notResidentialEntranceTagList = new List<String>()
        {
            "service", "shop"
        };
        Dictionary<String, List<OsmClosedWay>> selectedOsmClosedWayDic = new Dictionary<String, List<OsmClosedWay>>();
        //-------------------------------------------------------------------------------------------
        OSMLSGlobalLibrary.IInheritanceTreeCollection<Geometry> _mapObjects;
        Polygon _mainRectangle;
        //public static OSMLSGlobalLibrary.IInheritanceTreeCollection<Geometry> MapObjectsTmp;
        Customizer _customizer = Customizer.Instance;
        InfrastructureObject _infrastObj = null;
        String _dicKey = null;
        Human _humanObj = null;
        double _additionalRadius = 0.0;
        List<String> _infrastructureTagList = null;
        public City(OSMLSGlobalLibrary.IInheritanceTreeCollection<Geometry> mapObjects,
            Polygon mainRectangle)
        {
            this._mapObjects = mapObjects;
            //City.MapObjectsTmp = this._mapObjects;
            this._mainRectangle = mainRectangle;

            Dictionary<String, List<InfrastructureObject>> infrastructureDic = new Dictionary<String, List<InfrastructureObject>>()
            {
                {"polygon", new List<InfrastructureObject>() },
                {"way", new List<InfrastructureObject>() },
                {"point", new List<InfrastructureObject>() },
                {"pointbuffer", new List<InfrastructureObject>() }
            };
            
            String command = "";
            String[] commandPart = null;
            this._mapObjects.Add(this._mainRectangle);
            Console.WriteLine("Add osm objects: ");
            command = Console.ReadLine();
            if (command == "yes")
            {
                this.AddOsmObjectsCommand();
            }
            do
            {
                command = Console.ReadLine();
                commandPart = command.Split(' ');
                if (command != "start")
                {
                    if (commandPart.Length == 2)
                    {
                        this.AddCommand(commandPart);
                    }
                    else if (commandPart.Length == 5)
                    {
                        this.ShowInfoCommand(commandPart);
                    }
                }
            } while (command != "start");
            Grid.GridInstance = new Grid(this._mainRectangle, 2.0);
        }
        void AddOsmObjectsCommand()
        {
            List<OsmClosedWay> closedWayList = this._mapObjects.GetAll<OsmClosedWay>();
            List<OsmWay> notClosedWayList = this._mapObjects.GetAll<OsmWay>();
            List<OsmNode> nodeList = this._mapObjects.GetAll<OsmNode>();
            Customizer customizer = Customizer.Instance;
            City.AddClosedWaysToDic(closedWayList, customizer);
            City.AddWaysToDic(notClosedWayList, customizer);
            City.ExtendInfrastrObjTags(nodeList);
            this.AddObjectsToMap();
        }
        void AddCommand(String[] commandPart)
        {
            if (commandPart[0] == "add")
            {
                if (commandPart[1] == "human")
                {
                    this.AddHumanToDic();
                    if (Human.HumanDic.Keys.Count == 1)
                    {
                        this._humanObj.SetDestinationCoordinates(new List<Coordinate>() { new Coordinate(4194240, 7516324) });
                    }
                    else if (Human.HumanDic.Keys.Count == 2)
                    {
                        this._humanObj.SetDestinationCoordinates(new List<Coordinate>() { new Coordinate(4194302, 7516220), new Coordinate(4194296, 7516230) });
                    }
                    this.AddHumanToMap();
                }
                else
                {
                    this.AddInfrastructureToDic(commandPart[1]);
                    this.AddInfrastToMap();
                }
            }
        }
        void ShowInfoCommand(String[] commandPart)
        {
            if (commandPart[0] == "infobycircle")
            {
                if (commandPart[1] == "infrast")
                {
                    this.ShowInfrastInfoInRadius(commandPart[2], commandPart[3], commandPart[4]);
                }
                else if (commandPart[1] == "human")
                {
                    this.ShowHumanInfoInRadius(commandPart[2], commandPart[3], commandPart[4]);
                }
                else
                {
                    Console.WriteLine("Wrong infobycircle argument");
                }
            }
        }
        static void AddClosedWaysToDic(List<OsmClosedWay> closedWayList, Customizer customizer)
        {
            InfrastructureObject infrastObj;
            double additionalRadius;
            Dictionary<String, List<InfrastructureObject>> infrastructureDic =
                InfrastructureObject.InfrastructureDic;
            foreach (OsmClosedWay osmClosedWay in closedWayList)
            {
                if (osmClosedWay != null && !osmClosedWay.IsEmpty)
                {
                    infrastObj = new InfrastructureObject(osmClosedWay.ToPolygon, null);
                    City.AddOutTags(City.PolygonTagsToOutTagsDic, infrastObj, osmClosedWay);
                    if (infrastObj.HasTagDic.Count > 0)
                    {
                        City.AddWalkableTag(City.PolygonWalkableTagsDic, infrastObj, osmClosedWay);
                        infrastObj.SetBufferedGeometry(Constant.humanRadius, true);
                        infrastObj.CustomType = Customizer.GetCustomizerType(infrastObj.HasTagDic);
                        infrastObj.Geometry = customizer.Customize((Polygon)infrastObj.Geometry, infrastObj.CustomType);
                        infrastructureDic["polygon"].Add(infrastObj);
                    }
                    else
                    {
                        infrastObj = new InfrastructureObject(osmClosedWay.ToPolygon.ExteriorRing, null);
                        City.AddOutTags(City.LineStringTagsToOutTagsDic, infrastObj, osmClosedWay);
                        if (infrastObj.HasTagDic.Count > 0)
                        {
                            City.AddWalkableTag(City.LineStringWalkableTagsDic, infrastObj, osmClosedWay);
                            additionalRadius = InfrastructureObject.GetAdditionalRadiusForInfrastructureWay(infrastObj);
                            infrastObj.SetBufferedGeometry(Constant.humanRadius + additionalRadius, false);
                            infrastObj.CustomType = Customizer.GetCustomizerType(infrastObj.HasTagDic);
                            infrastObj.Geometry = customizer.Customize((Polygon)infrastObj.Geometry, infrastObj.CustomType);
                            infrastructureDic["way"].Add(infrastObj);
                        }
                    }
                }
            }
        }
        static void AddWaysToDic(List<OsmWay> notClosedWayList, Customizer customizer)
        {
            Dictionary<String, List<InfrastructureObject>> infrastructureDic =
                InfrastructureObject.InfrastructureDic;
            InfrastructureObject infrastObj;
            double additionalRadius;
            foreach (OsmWay osmWay in notClosedWayList)
            {
                if (osmWay != null && !osmWay.IsEmpty)
                {
                    infrastObj = new InfrastructureObject(osmWay, null);
                    City.AddOutTags(City.LineStringTagsToOutTagsDic, infrastObj, osmWay);
                    if (infrastObj.HasTagDic.Count > 0)
                    {
                        City.AddWalkableTag(City.LineStringWalkableTagsDic, infrastObj, osmWay);
                        additionalRadius = InfrastructureObject.GetAdditionalRadiusForInfrastructureWay(infrastObj);
                        infrastObj.SetBufferedGeometry(Constant.humanRadius + additionalRadius, false);
                        infrastObj.CustomType = Customizer.GetCustomizerType(infrastObj.HasTagDic);
                        infrastObj.Geometry = customizer.Customize(infrastObj.Geometry, infrastObj.CustomType);
                        infrastructureDic["way"].Add(infrastObj);
                    }
                }
            }
        }
        static void ExtendInfrastrObjTags(List<OsmNode> nodeList)
        {
            Dictionary<String, List<InfrastructureObject>> infrastructureDic =
                InfrastructureObject.InfrastructureDic;
            bool containsTag;
            InfrastructureObject infrastPoint;
            foreach (OsmNode osmNode in nodeList)
            {
                if (osmNode != null && !osmNode.IsEmpty)
                {
                    infrastPoint = new InfrastructureObject(osmNode, null);
                    City.AddOutTags(City.PointTagsToOutTagsDic, infrastPoint, osmNode);
                    if (infrastPoint.HasTagDic.Count > 0)
                    {
                        foreach (InfrastructureObject infrastructureObject in infrastructureDic["polygon"])
                        {
                            if (infrastructureObject.HasTagDic.TryGetValue("residentionalBuilding", out containsTag)
                                || infrastructureObject.HasTagDic.TryGetValue("!residentionalBuilding", out containsTag))
                            {
                                if (infrastructureObject.Geometry.Intersects(infrastPoint.Geometry))
                                {
                                    foreach (String key in infrastPoint.HasTagDic.Keys)
                                    {
                                        if (!infrastructureObject.HasTagDic.Keys.Contains(key))
                                        {
                                            infrastructureObject.HasTagDic.Add(key, true);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        static void AddWalkableTag(Dictionary<String, TagsOfOutTag> TagsToOutTagsDic, InfrastructureObject infrastObj, IOsmObject osmClosedWay)//OsmClosedWay
        {
            foreach (KeyValuePair<String, TagsOfOutTag> pair in TagsToOutTagsDic)
            {
                if (pair.Value.ContainsTagsFromLists)
                {
                    if (City.IsTag(pair.Value, osmClosedWay, true))
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
        static void AddOutTags(Dictionary<String, TagsOfOutTag> TagsToOutTagsDic, InfrastructureObject infrastObj, IOsmObject osmClosedWay)
        {
            foreach (KeyValuePair<String, TagsOfOutTag> pair in TagsToOutTagsDic)
            {
                if (pair.Value.ContainsTagsFromLists)
                {
                    if (City.IsTag(pair.Value, osmClosedWay, true))
                    {
                        infrastObj.HasTagDic.Add(pair.Value.ContainsTagsPrefix + pair.Key, true);
                    }
                }
                if (pair.Value.DoesNotContainTagsFromLists)
                {
                    if (City.IsTag(pair.Value, osmClosedWay, false))
                    {
                        infrastObj.HasTagDic.Add(pair.Value.DoesNotContainTagsPrefix + pair.Key, true);
                    }
                }
            }
        }
        static bool IsTag(TagsOfOutTag tagsOfOutTag, IOsmObject osmObject, bool retValOnContains)
        {
            bool contains;
            List<String> keys = osmObject.Tags.Keys.Intersect(tagsOfOutTag.Tags.Keys).ToList<String>();
            if (keys != null && keys.Count > 0)
            {
                contains = City.OutTagContainsTags(tagsOfOutTag, osmObject, keys, retValOnContains);
                return contains;
            }
            return false;
        }
        static bool OutTagContainsTags(TagsOfOutTag tagsOfOutTag, IOsmObject osmObject, List<String> keys, bool retValOnContainsTag)
        {
            bool containsTag;
            foreach (String key in keys)
            {
                containsTag = tagsOfOutTag.Tags[key].Contains(Constant.tagOfAllTags)
                    || tagsOfOutTag.Tags[key].Contains(osmObject.Tags[key]);
                if (containsTag)
                {
                    return retValOnContainsTag;
                }
            }
            return !retValOnContainsTag;
        }
        void AddObjectsToMap()
        {
            foreach (KeyValuePair<String, List<InfrastructureObject>> pair in InfrastructureObject.InfrastructureDic)
            {
                foreach (InfrastructureObject infrastObj in pair.Value)
                {
                    this._mapObjects.Add(infrastObj.Geometry);
                }
            }
            foreach (KeyValuePair<int, Human> pair in Human.HumanDic)
            {
                this._mapObjects.Add(pair.Value.Geometry);
            }
        }
        void AddInfrastToMap()
        {
            if (this._dicKey == "pointbuffer")
            {
                this._mapObjects.Add(
                    InfrastructureObject
                    .InfrastructureDic["point"][^1].Geometry);
            }
            this._mapObjects.Add(this._infrastObj.Geometry);
        }
        void AddHumanToMap()
        {
            this._mapObjects.Add(this._humanObj.Geometry);
        }
        Coordinate[] StringToCoordinateArr(String strCoordinates)
        {
            List<Coordinate> coordinates = new List<Coordinate>();
            String[] crdStrPair = null;
            String[] strCrd = null;
            double x, y;
            bool err = false;
            if (strCoordinates != null && strCoordinates.Length != 0)
            {
                crdStrPair = strCoordinates.Split(new string[] { ", " }, StringSplitOptions.None);
                foreach (String strPair in crdStrPair)
                {
                    strCrd = strPair.Split(' ');
                    if (strCrd.Length == 2)
                    {
                        try
                        {
                            x = Convert.ToDouble(strCrd[0]);
                            y = Convert.ToDouble(strCrd[1]);
                            coordinates.Add(new Coordinate(x, y));
                        }
                        catch (Exception e)
                        {
                            err = true;
                            Console.WriteLine("Wrong coordinate");
                        }
                    }
                    else
                    {
                        err = true;
                        Console.WriteLine("Wrong coordinates in coordinate substring");
                    }
                }
            }
            else
            {
                err = true;
                Console.WriteLine("Wrong coordinate string");
            }
            if (err)
                return null;
            return coordinates.ToArray();
        }
        List<String> StringToTagList(String tagsStr)
        {
            bool noErr = true;
            List<String> tagList = new List<String>();
            if (tagsStr != null && tagsStr.Length != 0)
            {
                tagList = tagsStr.Split(' ').ToList<String>();
                foreach (String tag in tagList)
                {
                    if (!this._infrastructureTagList.Contains(tag))
                    {
                        noErr = false;
                        Console.WriteLine("Wrong tag list");
                        break;
                    }
                }
            }
            else
            {
                noErr = false;
                Console.WriteLine("Wrong tag string");
            }
            if (noErr)
                return tagList;
            return null;
        }
        void AddTags()
        {
            bool repeat = true;
            String tagString;
            List<String> tagList = null;
            do
            {
                Console.WriteLine("Input tags:");
                tagString = Console.ReadLine();
                if (tagString != "cancle")
                {
                    tagList = StringToTagList(tagString);
                    if (tagList != null)
                    {
                        foreach (String tag in tagList)
                        {
                            this._infrastObj.HasTagDic.TryAdd(tag, true);
                        }
                        repeat = false;
                    }
                    else
                    {
                        Console.WriteLine("Null StringToTagList result");
                    }
                }
                else
                {
                    repeat = false;
                }
            } while (repeat);
        }
        bool AddPolygonToDic(Coordinate[] coordinateArr)
        {
            bool noErr = true;
            this._infrastObj = new InfrastructureObject(
                new Polygon(
                    new LinearRing(coordinateArr)
                ),
                null
            );
            this._infrastructureTagList = this._polygonTagList;
            this.AddTags();
            this._infrastObj.SetBufferedGeometry(Constant.humanRadius, true);
            this._infrastObj.CustomType = Customizer.GetCustomizerType(this._infrastObj.HasTagDic);
            this._infrastObj.Geometry = this._customizer.Customize((Polygon)this._infrastObj.Geometry, this._infrastObj.CustomType);
            if (this._infrastObj.Geometry != null)
            {
                InfrastructureObject.InfrastructureDic["polygon"].Add(this._infrastObj);
                this._dicKey = "polygon";
            }
            else
            {
                noErr = false;
                Console.WriteLine("Wrong tags for customization");
            }
            return noErr;
        }
        bool AddClosedWayToDic(Coordinate[] coordinateArr)
        {
            bool noErr = true;
            this._infrastObj = new InfrastructureObject(
                new LinearRing(coordinateArr),
                null
            );
            this._infrastructureTagList = this._wayTagList;
            this.AddTags();
            this._additionalRadius = InfrastructureObject.GetAdditionalRadiusForInfrastructureWay(this._infrastObj);
            this._infrastObj.SetBufferedGeometry(Constant.humanRadius + this._additionalRadius, false);
            this._infrastObj.CustomType = Customizer.GetCustomizerType(this._infrastObj.HasTagDic);
            this._infrastObj.Geometry = this._customizer.Customize(this._infrastObj.Geometry, this._infrastObj.CustomType);
            if (this._infrastObj.Geometry != null)
            {
                InfrastructureObject.InfrastructureDic["way"].Add(this._infrastObj);
                this._dicKey = "way";
            }
            else
            {
                noErr = false;
                Console.WriteLine("Wrong tags for customization");
            }
            return noErr;
        }
        bool AddWayToDic(Coordinate[] coordinateArr)
        {
            bool noErr = true;
            this._infrastObj = new InfrastructureObject(
                new LineString(coordinateArr),
                null
            );
            this._infrastructureTagList = this._wayTagList;
            this.AddTags();
            this._additionalRadius = InfrastructureObject.GetAdditionalRadiusForInfrastructureWay(this._infrastObj);
            this._infrastObj.SetBufferedGeometry(Constant.humanRadius + this._additionalRadius, false);
            this._infrastObj.CustomType = Customizer.GetCustomizerType(this._infrastObj.HasTagDic);
            this._infrastObj.Geometry = this._customizer.Customize(this._infrastObj.Geometry, this._infrastObj.CustomType);
            if (this._infrastObj.Geometry != null)
            {
                InfrastructureObject.InfrastructureDic["way"].Add(this._infrastObj);
                this._dicKey = "way";
            }
            else
            {
                noErr = true;
                Console.WriteLine("Wrong tags for customization");
            }
            return noErr;
        }
        bool AddPointToDic(Coordinate[] coordinateArr)
        {
            bool noErr = false;
            if (coordinateArr.Length != 1)
            {
                Console.WriteLine("Wrong coordinate count for point");
            }
            else
            {
                this._infrastObj = new InfrastructureObject(
                    new Point(coordinateArr[0]),
                    null
                );
                this._infrastructureTagList = this._pointTagList;
                this.AddTags();
                this._infrastObj.CustomType = Customizer.GetCustomizerType(this._infrastObj.HasTagDic);
                this._infrastObj.Geometry = this._customizer.Customize(this._infrastObj.Geometry, this._infrastObj.CustomType);
                if (this._infrastObj.Geometry != null)
                {
                    InfrastructureObject.InfrastructureDic["point"].Add(this._infrastObj);
                    this._infrastObj = new InfrastructureObject(
                        new Point(coordinateArr[0]).Buffer(Constant.humanRadius),
                        null
                    );
                    this._infrastObj.HasTagDic.Add("destination", true);
                    this._infrastObj.CustomType = Customizer.GetCustomizerType(this._infrastObj.HasTagDic);
                    this._infrastObj.Geometry = this._customizer.Customize(this._infrastObj.Geometry, this._infrastObj.CustomType);
                    InfrastructureObject.InfrastructureDic["pointbuffer"].Add(this._infrastObj);
                    this._dicKey = "pointbuffer";
                    noErr = true;
                }
                else
                {
                    Console.WriteLine("Wrong tags for customization");
                }
            }
            return noErr;
        }
        void AddInfrastructureToDic(String geometry)
        {
            bool repeat = true;
            String coordinates;
            Coordinate[] coordinateArr = null;
            do
            {
                Console.WriteLine("Input coordinates:");
                coordinates = Console.ReadLine();
                if (coordinates != "cancle")
                {
                    coordinateArr = StringToCoordinateArr(coordinates);
                    if (coordinateArr != null)
                    {
                        try
                        {
                            switch (geometry)
                            {
                                case "polygon":
                                    repeat = !this.AddPolygonToDic(coordinateArr);
                                    break;
                                case "closedway":
                                    repeat = !this.AddClosedWayToDic(coordinateArr);
                                    break;
                                case "way":
                                    repeat = !this.AddWayToDic(coordinateArr);
                                    break;
                                case "point":
                                    repeat = !this.AddPointToDic(coordinateArr);
                                    break;
                            }
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Wrong coordinates for geometry");
                        }
                    }
                }
                else
                {
                    repeat = false;
                }
            } while (repeat);
        }
        void AddHumanToDic()
        {
            bool repeat = true;
            String coordinates;
            Coordinate[] coordinateArr = null;
            this._humanObj = null;
            do
            {
                Console.WriteLine("Input coordinates:");
                coordinates = Console.ReadLine();
                if (coordinates != "cancle")
                {
                    coordinateArr = StringToCoordinateArr(coordinates);
                    if (coordinateArr != null)
                    {
                        try
                        {
                            if (coordinateArr.Length != 1)
                            {
                                Console.WriteLine("Wrong coordinate count for human");
                            }
                            else
                            {
                                this._humanObj = new Human(
                                    new Point(coordinateArr[0])
                                );
                                this._humanObj.Geometry = this._customizer.Customize(this._humanObj.Geometry, CustomizerType.HumanPoint);
                                if (this._humanObj.Geometry != null)
                                {
                                    Human.HumanDic.Add(this._humanObj.ID, this._humanObj);
                                    repeat = false;
                                }
                                else
                                {
                                    Console.WriteLine("Wrong human geometry for customization");
                                }
                            }
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Wrong values for human");
                        }
                    }
                }
                else
                {
                    repeat = false;
                }
            } while (repeat);
        }

        bool StringToDouble(String doub, out double val, double? lowLimit = null)
        {
            try
            {
                val = Convert.ToDouble(doub);
                if (lowLimit == null || val >= lowLimit)
                    return true;
                Console.WriteLine("Too small double");
            }
            catch (Exception e)
            {
                Console.WriteLine("Wrong double value");
            }
            val = 0.0;
            return false;
        }
        class InfrastructureInfo
        {
            public String Type { get; set; }
            public int ID { get; set; }
            public List<String> TagList { get; set; }
            public InfrastructureInfo(String type, int id, List<String> tagList)
            {
                this.Type = type;
                this.ID = id;
                this.TagList = tagList;
            }
        }
        void GetInfrastInfoInRadius(List<InfrastructureInfo> infrastInfoList,
            String strX, String strY, String strRadius)
        {
            double x, y, radius;
            bool noErr = true;
            noErr &= this.StringToDouble(strX, out x, null);
            noErr &= this.StringToDouble(strY, out y, null);
            noErr &= this.StringToDouble(strRadius, out radius, 0.1);
            if (noErr)
            {
                Polygon buffer = (Polygon)(new Point(x, y).Buffer(radius));
                foreach (KeyValuePair<String, List<InfrastructureObject>> pair
                    in InfrastructureObject.InfrastructureDic)
                {
                    foreach (InfrastructureObject infrastObj in pair.Value)
                    {
                        if (buffer.Intersects(infrastObj.Geometry))
                        {
                            infrastInfoList.Add(new InfrastructureInfo(
                                pair.Key, infrastObj.ID,
                                infrastObj.HasTagDic.Keys.ToList())
                            );
                        }
                    }
                }
            }
        }
        void GetHumanInfoInRadius(Dictionary<int, List<DestinationType>>
            humanInfoDic, String strX, String strY, String strRadius)
        {
            double x, y, radius;
            bool noErr = true;
            noErr &= this.StringToDouble(strX, out x, null);
            noErr &= this.StringToDouble(strY, out y, null);
            noErr &= this.StringToDouble(strRadius, out radius, 0.1);
            if (noErr)
            {
                Console.WriteLine(new Point(x, y));
                Polygon buffer = (Polygon)(new Point(x, y).Buffer(radius));
                foreach (Human human in Human.HumanDic.Values)
                {
                    if (buffer.Intersects(human.Geometry))
                    {
                        List<DestinationType> dstTypeList = new List<DestinationType>();
                        foreach (DestinationType humanInfo in human.GetDestinatinTypeDic.Values)
                        {
                            dstTypeList.Add(humanInfo);
                        }
                        humanInfoDic.Add(human.ID, dstTypeList);
                    }
                }
            }
        }
        void ShowInfrastInfoInRadius(String strRadius, String strX, String strY)
        {
            List<InfrastructureInfo> infrastInfoList = new List<InfrastructureInfo>();
            this.GetInfrastInfoInRadius(infrastInfoList, strX, strY, strRadius);
            foreach (InfrastructureInfo infrastInf in infrastInfoList)
            {
                Console.WriteLine("===============================================");
                Console.WriteLine("Type: " + infrastInf.Type);
                Console.WriteLine("ID: " + infrastInf.ID);
                Console.WriteLine("Tags: " + String.Join(", ", infrastInf.TagList));
            }
        }
        void ShowHumanInfoInRadius(String strRadius, String strX, String strY)
        {
            Dictionary<int, List<DestinationType>> humanInfoDic
                = new Dictionary<int, List<DestinationType>>();
            this.GetHumanInfoInRadius(humanInfoDic, strX, strY, strRadius);
            foreach (KeyValuePair<int, List<DestinationType>>
                 pair in humanInfoDic)
            {
                Console.WriteLine("===============================================");
                Console.WriteLine("ID: " + pair.Key);
                foreach (DestinationType humanInf in pair.Value)
                {
                    Console.WriteLine("--------------------");
                    Console.WriteLine("Name: " + humanInf.Name);
                    Console.WriteLine("Value: " + humanInf.Value);
                    Console.WriteLine("TypePriority: " + humanInf.TypePriority);
                }
            }
        }
    }
}
