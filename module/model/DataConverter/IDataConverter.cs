using System;
using System.Collections.Generic;
using NetTopologySuite.Geometries;

namespace VisitCounter
{
    /// <summary>DataConverter interface.</summary>
    public interface IDataConverter: IBase
    {
        /// <summary>
        /// Adds osm infrastructure to the dictionary and to the map.
        /// </summary>
        /// <param name="add">
        /// Is it necessary to add osm infrastructure.
        /// </param>
        public void AddOsmInfrastructure(bool add);

        /// <summary>
        /// Adds infrastructure to the dictionary and to the map.
        /// </summary>
        /// <param name="geometryType">
        /// Infrastructure geometry type 
        /// ("polygon", "closedway", "way", "point") string key.
        /// </param>
        /// <param name="coordinates">
        /// Array of coordinates of length 1 to create point.
        /// </param>
        /// <param name="tagList">
        /// List of infrastructure string tags.
        /// </param>
        public void AddInfrastructure(String geometryType,
                                      Coordinate[] coordinates,
                                      List<String> tagList);

        /// <summary>
        /// Adds human to the dictionary and to the map.
        /// </summary>
        /// <param name="coordinates">
        /// Array of coordinates of length 1 to create human.
        /// </param>
        public void AddHuman(Coordinate[] coordinateArr);

        /// <summary>
        /// Gets information about infrastructure objects
        /// that intersect with circle.
        /// </summary>
        /// <param name="infrastInfoList">
        /// An empty list for adding information about infrastructure.
        /// </param>
        /// <param name="crd">Coordinate of the circle center.</param>
        /// <param name="radius">Circle radius.</param>
        public void GetInfrastInfoInRadius(
            List<InfrastructureInfo> infrastInfoList,
            Coordinate crd,
            double radius);

        /// <summary>
        /// Gets information about people 
        /// that intersect with circle.
        /// </summary>
        /// <param name="infrastInfoList">
        /// An empty list for adding information about human.
        /// </param>
        /// <param name="crd">Coordinate of the circle center.</param>
        /// <param name="radius">Circle radius.</param>
        public void GetHumanInfoInRadius(List<HumanInfo> humanInfoList,
                                         Coordinate crd,
                                         double radius);

        /// <summary>
        /// Creates a csv with the number of cell visits.
        /// </summary>
        public void CreateCSV();

        /// <summary>
        /// Outputs information about the infrastructure to the console.
        /// </summary>
        /// <param name="infrastInfoList">
        /// List of InfrastructureInfo to output.
        /// </param>
        public void OutputInfrastInfoToConsole(
            List<InfrastructureInfo> infrastInfoList);

        /// <summary>
        /// Outputs information about the human to the console.
        /// </summary>
        /// <param name="humanInfoList">
        /// List of HumanInfo to output.
        /// </param>
        public void OutputHumanInfoToConsole(
            List<HumanInfo> humanInfoList);

        /// <summary>
        /// Gets tags for geometry types 
        /// ("polygon", "closedway", "way", "point")
        /// </summary>
        /// <param name="geomType">String of geometry type.</param>
        /// <returns>String tag list for geometry type.</returns>
        public List<String> GetInfrastTagListByGeomType(String geomType);
    }
}
