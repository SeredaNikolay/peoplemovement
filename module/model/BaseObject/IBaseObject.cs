using NetTopologySuite.Geometries;

namespace VisitCounter
{
    public interface IBaseObject
    {
        public Geometry Geometry { get; set; }
    }
}
