using System;
using System.Collections.Generic;
using NetTopologySuite.Geometries;

namespace VisitCounter
{
    /// <summary>Outputter interface.</summary>
    public interface IOutputter
    {
        /// <summary>Adds infrastructure dictionary to map.</summary>
        /// <param name="infrastDic">
        /// Dictionary with string key
        /// ("polygon", "way", "point", "pointbuffer")
        /// and geometry list value.
        /// </param>
        public void AddInfrastToMap(
            Dictionary<String, List<Geometry>> infrastDic);

        /// <summary>Adds people to map.</summary>
        /// <param name="humanList">List of human points.</param>
        public void AddPeopleToMap(List<Point> humanList);

        /// <summary>Adds infrastructure object to map.</summary>
        /// <param name="polygon">
        /// Infrastructure geometry 
        /// (point buffer for point infrastructure).
        /// </param>
        /// <param name="point">
        /// Point geometry for point infrastructure.
        /// </param>
        public void AddInfrastObjToMap(Polygon polygon, Point point);

        /// <summary>Adds human to map.</summary>
        /// <param name="point">Human geometry.</param>
        public void AddHumanObjToMap(Point point);

        /// <summary>Creates CSV file.</summary>
        /// <param name="grid">Cell visits matrix.</param>
        public void CreateCSV(int[,] grid);

        /// <summary>Prints InfrastructureInfo to console.</summary>
        /// <param name="infrastInfoList">
        /// List of InfrastructureInfo.
        /// </param>
        public void OutputInfrastInfoToConsole(
            List<InfrastructureInfo> infrastInfoList);

        /// <summary>Prints HumanInfo to console.</summary>
        /// <param name="humanInfoList">
        /// List of HumanInfo.
        /// </param>
        public void OutputHumanInfoToConsole(
            List<HumanInfo> humanInfoList);
    }
}
