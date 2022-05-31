using System.Collections.Generic;
using NetTopologySuite.Geometries;

namespace VisitCounter
{
    /// <summary>MovingObject interface.</summary>
    public interface IMovingObject
    {
        /// <value>
        /// Property <c>DstCoordinates</c> 
        /// List of destination coordinates.
        /// </value>
        public List<Coordinate> DstCoordinates { get; set; }

        /// <value>
        /// Property <c>VisitedCrdList</c> 
        /// List of visited coordinates.
        /// </value>
        public List<Coordinate> VisitedCrdList { get; }

        /// <value>
        /// Property <c>PrevCellI</c> 
        /// X-axis index of previous visited cell.
        /// </value>
        public int PrevCellI { get; set; }

        /// <value>
        /// Property <c>PrevCellJ</c> 
        /// Y-axis index of previous cell.
        /// </value>
        public int PrevCellJ { get; set; }
    }
}
