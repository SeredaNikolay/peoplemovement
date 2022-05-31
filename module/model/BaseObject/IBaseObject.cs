using NetTopologySuite.Geometries;

namespace VisitCounter
{
    interface IBaseObject
    {
        public Geometry Geometry { get; set; }
    }
}
