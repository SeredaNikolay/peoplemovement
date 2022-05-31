using System;
using System.Collections.Generic;
using NetTopologySuite.Geometries;

namespace VisitCounter
{
    interface IOutputter
    {
        public void AddInfrastToMap(
            Dictionary<String, List<Geometry>> infrastDic);
        public void AddPeopleToMap(List<Point> humanList);
        public void AddInfrastObjToMap(Polygon polygon, Point point);
        public void AddHumanObjToMap(Point point);
        public void CreateCSV(int[,] grid);
        public void OutputInfrastInfoToConsole(
            List<InfrastructureInfo> infrastInfoList);
        public void OutputHumanInfoToConsole(
            List<HumanInfo> humanInfoList);
    }
}
