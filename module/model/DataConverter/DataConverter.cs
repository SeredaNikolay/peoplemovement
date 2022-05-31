using System;
using System.Collections.Generic;
using System.Linq;
using CityDataExpansionModule.OsmGeometries;
using NetTopologySuite.Geometries;

namespace VisitCounter
{
    public class DataConverter: IDataConverter
    {
        bool _allowedToAddOsm;
        bool _allowedToAddInfrast;
        String _dicKey;
        OsmToInfrastructureConverter _osmToInfrastructureConverter;
        Dictionary<String, List<IInfrastructure>> _infrastructureDic;
        Dictionary<int, IHuman> _humanDic;
        IOutputter _outputter;

        IDataProvider _dataProvider;
        ICustomizer _customizer;
        Polygon _mainRectangle;
        IGrid _grid;
        public DataConverter(IDataProvider dataProvider,
                             Polygon mainRectangle)
        {
            this._allowedToAddOsm = true;
            this._allowedToAddInfrast = true;
            this._customizer = new Customizer();
            this._osmToInfrastructureConverter = 
                new OsmToInfrastructureConverter();
            this._infrastructureDic = 
                new Dictionary<String, List<IInfrastructure>>()
                {
                    {"polygon", new List<IInfrastructure>() },
                    {"way", new List<IInfrastructure>() },
                    {"point", new List<IInfrastructure>() },
                    {"pointbuffer", new List<IInfrastructure>() }
                };
            this._humanDic = new Dictionary<int, IHuman>();
            this._mainRectangle = mainRectangle;
            this._grid = new Grid(this._mainRectangle, 2.0, this._infrastructureDic);
            this._dataProvider = dataProvider;
            this._outputter = new Outputter(dataProvider.GetMapObjects());
            this._outputter.AddInfrastObjToMap(this._mainRectangle, null);
        }
        public List<String> GetInfrastTagListByGeomType(String geomType)
        {
            List<String> list;
            switch(geomType)
            {
                case "polygon":
                    list = this._dataProvider.GetPolygonTagList();
                    break;
                case "closedway":
                    list = this._dataProvider.GetWayTagList();
                    break;
                case "way":
                    list = this._dataProvider.GetWayTagList();
                    break;
                case "point":
                    list = this._dataProvider.GetPointTagList();
                    break;
                default:
                    list = new List<string>();
                    break;
            }
            return list;
        }
        void AddPolygonByClosedWayToInfrastDic(
            IInfrastructure infrastObj,
            OsmClosedWay osmClosedWay)
        {
            this._osmToInfrastructureConverter.AddWalkableTag(
                this._dataProvider.GetPolygonWalkableTagsDic(),
                infrastObj,
                osmClosedWay);
            infrastObj.SetBufferedGeometry(
                Constant.humanRadius,
                true);
            infrastObj.CustomType = this._customizer
                .GetCustomizerTypeByTags(infrastObj.HasTagDic);
            infrastObj.Geometry = this._customizer.Customize(
                (Polygon)infrastObj.Geometry,
                infrastObj.CustomType);
            this._infrastructureDic["polygon"].Add(infrastObj);
        }
        void AddClosedWayByItSelfToInfrastDic(
            IInfrastructure infrastObj,
            OsmClosedWay osmClosedWay)
        {
            
            double additionalRadius;
            this._osmToInfrastructureConverter
                .AddWalkableTag(
                    this._dataProvider.GetLineStringWalkableTagsDic(),
                    infrastObj,
                    osmClosedWay);
            additionalRadius = this._dataProvider
                .GetAdditionalRadiusByTags(infrastObj.HasTagDic);
            infrastObj.SetBufferedGeometry(
                Constant.humanRadius + additionalRadius,
                false);
            infrastObj.CustomType = this._customizer
                .GetCustomizerTypeByTags(
                    infrastObj.HasTagDic);
            infrastObj.Geometry = this._customizer
                .Customize(
                    (Polygon)infrastObj.Geometry,
                    infrastObj.CustomType);
            this._infrastructureDic["way"].Add(infrastObj);
        }

        void AddClosedWaysToInfrastructureDic()
        {
            IInfrastructure infrastObj;
            List<OsmClosedWay> osmClosedWayList =
                this._dataProvider.GetClosedWayList();
            foreach (OsmClosedWay osmClosedWay in osmClosedWayList)
            {
                if (osmClosedWay != null && !osmClosedWay.IsEmpty)
                {
                    infrastObj = new Infrastructure(
                        osmClosedWay.ToPolygon,
                        null);
                    this._osmToInfrastructureConverter.AddOutTags(
                        this._dataProvider.GetPolygonTagsToOutTagsDic(),
                        infrastObj,
                        osmClosedWay);
                    if (infrastObj.HasTagDic.Count > 0)
                    {
                        this.AddPolygonByClosedWayToInfrastDic(
                            infrastObj,
                            osmClosedWay);
                    }
                    else
                    {
                        infrastObj = new Infrastructure(
                            osmClosedWay.ToPolygon.ExteriorRing,
                            null);
                        this._osmToInfrastructureConverter.AddOutTags(
                            this._dataProvider
                                .GetLineStringTagsToOutTagsDic(),
                            infrastObj,
                            osmClosedWay);
                        if (infrastObj.HasTagDic.Count > 0)
                        {
                            this.AddClosedWayByItSelfToInfrastDic(
                                infrastObj,
                                osmClosedWay);
                        }
                    }
                }
            }
        }
        void AddWaysToInfrastructureDic()
        {
            double additionalRadius;
            IInfrastructure infrastObj;
            List<OsmWay> notClosedWayList = 
                this._dataProvider.GetNotClosedWayList();
            foreach (OsmWay osmWay in notClosedWayList)
            {
                if (osmWay != null && !osmWay.IsEmpty)
                {
                    infrastObj = new Infrastructure(osmWay, null);
                    this._osmToInfrastructureConverter.AddOutTags(
                        this._dataProvider.GetLineStringTagsToOutTagsDic(),
                        infrastObj,
                        osmWay);
                    if (infrastObj.HasTagDic.Count > 0)
                    {
                        this._osmToInfrastructureConverter.AddWalkableTag(
                            this._dataProvider
                                .GetLineStringWalkableTagsDic(),
                            infrastObj,
                            osmWay);
                        additionalRadius = this._dataProvider
                            .GetAdditionalRadiusByTags(
                                infrastObj.HasTagDic);
                        infrastObj.SetBufferedGeometry(
                            Constant.humanRadius + additionalRadius,
                            false);
                        infrastObj.CustomType = this._customizer
                            .GetCustomizerTypeByTags(infrastObj.HasTagDic);
                        infrastObj.Geometry = this._customizer.Customize(
                            infrastObj.Geometry,
                            infrastObj.CustomType);
                        this._infrastructureDic["way"].Add(infrastObj);
                    }
                }
            }
        }
        void ExtendPolygonInfrastructureTags()
        {
            bool containsTag;
            IInfrastructure infrastPoint;
            List<OsmNode> nodeList = this._dataProvider.GetNodeList();
            bool isResBuild, isNotResBuild, isInBuilding;
            foreach (OsmNode osmNode in nodeList)
            {
                if (osmNode != null && !osmNode.IsEmpty)
                {
                    infrastPoint = new Infrastructure(osmNode, null);
                    this._osmToInfrastructureConverter.AddOutTags(
                        this._dataProvider.GetPointTagsToOutTagsDic(),
                        infrastPoint,
                        osmNode);
                    if (infrastPoint.HasTagDic.Count > 0)
                    {
                        foreach (IInfrastructure infrastObj
                            in this._infrastructureDic["polygon"])
                        {
                            isResBuild = infrastObj.HasTagDic
                                .TryGetValue("residentionalBuilding",
                                             out containsTag);
                            isNotResBuild = infrastObj.HasTagDic
                                .TryGetValue("!residentionalBuilding",
                                             out containsTag);
                            if (isResBuild || isNotResBuild)
                            {
                                isInBuilding = infrastObj.Geometry
                                    .Intersects(infrastPoint.Geometry);
                                if (isInBuilding)
                                {
                                    foreach (String key 
                                        in infrastPoint.HasTagDic.Keys)
                                    {
                                        infrastObj.HasTagDic
                                            .TryAdd(key, true);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        void AddTagsToInfrastObj(IInfrastructure infrastObj,
                                 List<String> tagList)
        {
            if (infrastObj != null)
            {
                if(tagList != null)
                {
                    foreach (String tag in tagList)
                    {
                        infrastObj.HasTagDic.TryAdd(tag, true);
                    }
                }
                else
                {
                    infrastObj.HasTagDic.TryAdd("none", true);
                }
            }
            else
            {
                Console.WriteLine("UserDataProvider:"
                    + " infrastructure object is null");
            }
        }
        bool AddPolygonToInfrastDic(Coordinate[] coordinateArr,
                                    List<String> tagList)
        {
            bool noErr = true;
            if (coordinateArr == null || coordinateArr.Length == 1)
            {
                Console.WriteLine("Wrong coordinate for polygon");
                noErr = false;
            }
            else
            {
                IInfrastructure infrastObj = new Infrastructure(
                    new Polygon(
                        new LinearRing(coordinateArr)
                    ),
                    null
                );
                this.AddTagsToInfrastObj(infrastObj, tagList);
                infrastObj.SetBufferedGeometry(Constant.humanRadius, true);
                infrastObj.CustomType = this._customizer
                    .GetCustomizerTypeByTags(infrastObj.HasTagDic);
                infrastObj.Geometry = this._customizer.Customize(
                    (Polygon)infrastObj.Geometry,
                    infrastObj.CustomType);
                this._infrastructureDic["polygon"].Add(infrastObj);
                this._dicKey = "polygon";
                this._allowedToAddOsm = false;
            }
            return noErr;
        }
        bool AddClosedWayToInfrastDic(Coordinate[] coordinateArr,
                                      List<String> tagList)
        {
            bool noErr = true;
            if (coordinateArr == null && coordinateArr.Length == 1)
            {
                Console.WriteLine("Wrong coordinate for closed way");
                noErr = false;
            }
            else
            {
                double additionalRadius;
                IInfrastructure infrastObj = new Infrastructure(
                    new LinearRing(coordinateArr),
                    null
                );
                this.AddTagsToInfrastObj(infrastObj, tagList);
                additionalRadius = this._dataProvider
                    .GetAdditionalRadiusByTags(infrastObj.HasTagDic);
                infrastObj.SetBufferedGeometry(
                    Constant.humanRadius + additionalRadius,
                    false);
                infrastObj.CustomType = this._customizer
                    .GetCustomizerTypeByTags(infrastObj.HasTagDic);
                infrastObj.Geometry = this._customizer.Customize(
                    infrastObj.Geometry,
                    infrastObj.CustomType);
                this._infrastructureDic["way"].Add(infrastObj);
                this._dicKey = "way";
                this._allowedToAddOsm = false;
            }
            return noErr;
        }
        bool AddWayToInfrastDic(Coordinate[] coordinateArr,
                                List<String> tagList)
        {
            bool noErr = true;
            if (coordinateArr == null && coordinateArr.Length == 1)
            {
                Console.WriteLine("Wrong coordinate for way");
                noErr = false;
            }
            else
            {
                double additionalRadius;
                IInfrastructure infrastObj = new Infrastructure(
                    new LineString(coordinateArr),
                    null
                );
                this.AddTagsToInfrastObj(infrastObj, tagList);
                additionalRadius = this._dataProvider
                    .GetAdditionalRadiusByTags(infrastObj.HasTagDic);
                infrastObj.SetBufferedGeometry(
                    Constant.humanRadius + additionalRadius,
                    false);
                infrastObj.CustomType = this._customizer
                    .GetCustomizerTypeByTags(infrastObj.HasTagDic);
                infrastObj.Geometry = this._customizer.Customize(
                    infrastObj.Geometry,
                    infrastObj.CustomType);
                this._infrastructureDic["way"].Add(infrastObj);
                this._dicKey = "way";
                this._allowedToAddOsm = false;
            }
            return noErr;
        }
        bool AddPointToInfrasDic(Coordinate[] coordinateArr,
                                 List<String> tagList)
        {
            bool noErr = true;
            if (coordinateArr == null)
            {
                Console.WriteLine("Wrong coordinate for point");
                noErr = false;
            }
            else if(coordinateArr.Length != 1)
            {
                Console.WriteLine("Wrong coordinate count for point");
                noErr = false;
            }
            else
            {
                IInfrastructure infrastObj = new Infrastructure(
                    new Point(coordinateArr[0]),
                    null
                );
                this.AddTagsToInfrastObj(infrastObj, tagList);
                infrastObj.CustomType = this._customizer
                    .GetCustomizerTypeByTags(infrastObj.HasTagDic);
                infrastObj.Geometry = this._customizer.Customize(
                    infrastObj.Geometry,
                    infrastObj.CustomType);
                this._infrastructureDic["point"].Add(infrastObj);
                infrastObj = new Infrastructure(
                    new Point(coordinateArr[0]).Buffer(
                        Constant.humanRadius),
                    null
                );
                infrastObj.HasTagDic.Add("destination", true);
                infrastObj.CustomType = this._customizer
                    .GetCustomizerTypeByTags(infrastObj.HasTagDic);
                infrastObj.Geometry = this._customizer.Customize(
                    infrastObj.Geometry,
                    infrastObj.CustomType);
                this._infrastructureDic["pointbuffer"].Add(infrastObj);
                this._dicKey = "pointbuffer";
                this._allowedToAddOsm = false;
            }
            return noErr;
        }
        bool FillInfrastructureDic()
        {
            bool noErr = true;
            if (this._allowedToAddOsm)
            {
                try
                {
                    this.AddClosedWaysToInfrastructureDic();
                    this.AddWaysToInfrastructureDic();
                    this.ExtendPolygonInfrastructureTags();
                    this._allowedToAddOsm = false;
                }
                catch(Exception e)
                {
                    Console.WriteLine("Wrong osm input");
                    noErr = false;
                }
            }
            else
            {
                Console.WriteLine("You cannot add osm");
                noErr = false;
            }
            return noErr;
        }
        bool AddInfrastructureToInfrastDic(String geometryType,
                                           Coordinate[] coordinates,
                                           List<String> tagList)
        {
            bool noErr = true;
            try
            {
                switch (geometryType)
                {
                    case "polygon":
                        noErr &= this.AddPolygonToInfrastDic(coordinates,
                                                             tagList);
                        break;
                    case "closedway":
                        noErr &= this.AddClosedWayToInfrastDic(coordinates,
                                                               tagList);
                        break;
                    case "way":
                        noErr &= this.AddWayToInfrastDic(coordinates,
                                                         tagList);
                        break;
                    case "point":
                        noErr &= this.AddPointToInfrasDic(coordinates,
                                                          tagList);
                        break;
                    default:
                        Console.WriteLine("Wrong geometry type");
                        noErr = false;
                        break;
                }
            }
            catch(Exception e)
            {
                Console.WriteLine("Wrong argument for infrastructure");
                noErr = false;
            }
            return noErr;
        }
        void HumanIsInObstacle(Point point, ref bool noErr)
        {
            int j, i;
            bool isInObstacle, f;
            bool notOutOfCells = this._grid.GetIndexesByCoordinate(
                point.Coordinate,
                out j, out i);
            if(notOutOfCells)
            {
                ICell cell = this._grid.GetCellByIndexes(j, i);
                foreach (KeyValuePair<String, List<IInfrastructure>> p
                    in this._infrastructureDic)
                {
                    foreach (IInfrastructure infrastObj in p.Value)
                    {
                        if (infrastObj.Geometry.Intersects(
                            cell.CellPolygon))
                        {
                            isInObstacle = infrastObj.HasTagDic
                                .TryGetValue("!walkable", out f);
                            if (isInObstacle)
                            {
                                noErr = false;
                                break;
                            }
                        }
                    }
                    if (!noErr)
                    {
                        break;
                    }
                }
            }
        }
        bool AddHumanToHumanDic(Coordinate[] coordinateArr, out int humID)
        {
            bool noErr = true;
            if (coordinateArr == null)
            {
                Console.WriteLine("Wrong coordinate for human");
                noErr = false;
                humID = -1;
            }
            else if (coordinateArr.Length != 1)
            {
                Console.WriteLine("Wrong coordinate count for human");
                noErr = false;
                humID = -1;
            }
            else
            {
                try
                {
                    Point point = new Point(coordinateArr[0]);
                    this.HumanIsInObstacle(point, ref noErr);
                    if (noErr)
                    {
                        IHuman humanObj = new Human(point);
                        humanObj.Geometry = this._customizer.Customize(
                            humanObj.Geometry,
                            CustomizerType.HumanPoint);
                        this._humanDic.Add(humanObj.ID, humanObj);
                        humID = humanObj.ID;
                        this._allowedToAddOsm = false;
                        this._allowedToAddInfrast = false;
                    }
                    else
                    {
                        Console.WriteLine("Human in obstacle/out of cells");
                        humID = -1;
                    }
                }
                catch(Exception e)
                {
                    Console.WriteLine("Wrong argument for human");
                    noErr = false;
                    humID = -1;
                }
            }
            return noErr;
        }
        public void AddOsmInfrastructure(bool add)
        {
            if(add)
            {
                bool noErr = this.FillInfrastructureDic();
                if (noErr)
                {
                    Dictionary<String, List<Geometry>> infrastGeomDic =
                        new Dictionary<String, List<Geometry>>();
                    foreach (KeyValuePair<String, List<IInfrastructure>> p
                        in this._infrastructureDic)
                    {
                        List<Geometry> geomList = new List<Geometry>();
                        foreach (IInfrastructure infrastObj in p.Value)
                        {
                            geomList.Add(infrastObj.Geometry);
                        }
                        infrastGeomDic.Add(p.Key, geomList);
                    }
                    this._outputter.AddInfrastToMap(infrastGeomDic);
                }
            }
            else
            {
                this._allowedToAddOsm = false;
            }          
        }
        public void AddInfrastructure(String geometryType,
                                      Coordinate[] coordinates,
                                      List<String> tagList)
        {
            if (this._allowedToAddInfrast)
            {
                bool noErr = this.AddInfrastructureToInfrastDic(
                    geometryType,
                    coordinates,
                    tagList);
                if (noErr)
                {
                    Point point = null;
                    Polygon polygon = (Polygon)this
                        ._infrastructureDic[this._dicKey][^1].Geometry;
                    if (this._dicKey == "pointbuffer")
                    {
                        point = (Point)this._infrastructureDic["point"][^1]
                            .Geometry;
                    }
                    this._outputter.AddInfrastObjToMap(polygon, point);
                }
            }
            else
            {
                Console.WriteLine("You cannot add infrastructure");
            }
        }
        public void AddHuman(Coordinate[] coordinateArr)
        {
            int id;
            bool noErr = this.AddHumanToHumanDic(coordinateArr, out id);
            if (noErr)
            {
                Point point = (Point)this._humanDic[id].Geometry;
                this._outputter.AddHumanObjToMap(point);
            }
        }
        public void GetInfrastInfoInRadius(
            List<InfrastructureInfo> infrastInfoList,
            Coordinate crd,
            double radius)
        {
            if (radius >= 0.1)
            {
                Polygon buffer = (Polygon)(
                    new Point(crd.X, crd.Y).Buffer(radius));
                foreach (KeyValuePair<String, List<IInfrastructure>> p
                    in this._infrastructureDic)
                {
                    foreach (IInfrastructure infrastObj in p.Value)
                    {
                        if (buffer.Intersects(infrastObj.Geometry))
                        {
                            infrastInfoList.Add(
                                new InfrastructureInfo(
                                    p.Key, infrastObj.ID,
                                    infrastObj.HasTagDic.Keys.ToList())
                            );
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine("Minimum allowable radius is 0.1 "
                    +"(infrast search)");
            }
        }
        public void GetHumanInfoInRadius(List<HumanInfo> humanInfoList,
                                         Coordinate crd,
                                         double radius)
        {
            if (radius >= 0.1)
            {
                Polygon buffer = (Polygon)(
                    new Point(crd.X, crd.Y).Buffer(radius));
                foreach (IHuman human in this._humanDic.Values)
                {
                    if (buffer.Intersects(human.Geometry))
                    {
                        List<IDestinationType> dstTypeList =
                            new List<IDestinationType>();
                        foreach (IDestinationType humInfo in
                            human.GetDestinatinTypeDic.Values)
                        {
                            dstTypeList.Add(humInfo);
                        }
                        humanInfoList.Add(
                            new HumanInfo(human.ID, dstTypeList));
                    }
                }
            }
            else
            {
                Console.WriteLine("Minimum allowable radius is 0.1 "
                    + "(human search)");
            }
        }
        public void CreateCSV()
        {
            try
            {
                int[,] matrix = this._grid.GetVisitMatrix();
                this._outputter.CreateCSV(matrix);
                Console.WriteLine("CSV created");
            }
            catch(Exception e)
            {
                Console.WriteLine("You can't create a csv");
            }
        }

        public void OutputInfrastInfoToConsole(
            List<InfrastructureInfo> infrastInfoList)
        {
            this._outputter.OutputInfrastInfoToConsole(infrastInfoList);
        }
        public void OutputHumanInfoToConsole(List<HumanInfo> humanInfoList)
        {
            this._outputter.OutputHumanInfoToConsole(humanInfoList);
        }
        public Dictionary<String, List<IInfrastructure>> GetInfrastDic()
        {
            return this._infrastructureDic;
        }
        public Dictionary<int, IHuman> GetHumanDic()
        {
            return this._humanDic;
        }
        public IGrid GetGrid()
        {
            return this._grid;
        }
    }
}
