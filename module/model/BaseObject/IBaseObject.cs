using NetTopologySuite.Geometries;

namespace VisitCounter
{
    /// <summary>BaseObject interface.</summary>
    public interface IBaseObject
    {
        /// <value>
        /// Property <c>Geometry</c> represents the geometry.
        /// </value>
        public Geometry Geometry { get; set; }
    }
}
