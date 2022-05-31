using System;
using System.Collections.Generic;
using NetTopologySuite.Geometries;

namespace VisitCounter
{
    interface IDataConverter: IBase
    {
        public void AddOsmInfrastructure(bool add);
        public void AddInfrastructure(String geometryType,
                                      Coordinate[] coordinates,
                                      List<String> tagList);
        public void AddHuman(Coordinate[] coordinateArr);
        public void GetInfrastInfoInRadius(
            List<InfrastructureInfo> infrastInfoList,
            Coordinate crd,
            double radius);
        public void GetHumanInfoInRadius(List<HumanInfo> humanInfoList,
                                         Coordinate crd,
                                         double radius);
        public void CreateCSV();
        public void OutputInfrastInfoToConsole(
            List<InfrastructureInfo> infrastInfoList);
        public void OutputHumanInfoToConsole(
            List<HumanInfo> humanInfoList);
        public List<String> GetInfrastTagListByGeomType(String geomType);
    }
}
