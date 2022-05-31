using System.Collections.Generic;
using NetTopologySuite.Geometries;

namespace VisitCounter
{
    public interface IMovingObject
    {
        public List<Coordinate> DstCoordinates { get; set; }
        public List<Coordinate> VisitedCrdList { get; }
        public int PrevCellI { get; set; }
        public int PrevCellJ { get; set; }
    }
}
