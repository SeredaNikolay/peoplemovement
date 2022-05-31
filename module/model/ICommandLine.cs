using System;
using System.Collections.Generic;
using System.Text;

namespace VisitCounter
{
    /// <summary>CommandLine interface.</summary>
    public interface ICommandLine: IBase
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
        /// Geometry type of added infrastructure.
        /// </param>
        public void AddInfrastructure(String geometryType);

        /// <summary>
        /// Adds human to the dictionary and to the map.
        /// </summary>
        public void AddHuman();

        /// Gets information about infrastructure objects
        /// that intersect with circle.
        /// <param name="strRadius">String value of circle radius.</param>
        /// <param name="strX">
        /// String value of X-axis circle center coordinate
        /// </param>
        /// <param name="strY">
        /// String value of Y-axis circle center coordinate
        /// </param>
        public void ShowInfrastInfo(String strRadius,
                                    String strX,
                                    String strY);

        /// Gets information about people
        /// that intersect with circle.
        /// <param name="strRadius">String value of circle radius.</param>
        /// <param name="strX">
        /// String value of X-axis circle center coordinate
        /// </param>
        /// <param name="strY">
        /// String value of Y-axis circle center coordinate
        /// </param>
        public void ShowHumanInfo(String strRadius,
                                  String strX,
                                  String strY);

        /// <summary>
        /// Creates a csv with the number of cell visits.
        /// </summary>
        public void CreateCSV();
    }
}
