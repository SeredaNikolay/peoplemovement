

namespace VisitCounter
{
    /// <summary>Infrastructure interface.</summary>
    public interface IInfrastructure : IHasTagObject, IID, IBaseObject
    {
        /// <value>
        /// Property <c>CustomType</c> 
        /// IInfrastructure CustomizerType.
        /// </value>
        public CustomizerType CustomType { get; set; }

        /// <summary>Sets buffer for IInfrastructure geometry.</summary>
        /// <param name="additionalRadius">Buffer radius</param>
        /// <param name="needsPolygon">
        /// Polygon flag (true - buffer for polygon geometry;
        /// false - buffer for linear ring geometry).
        /// </param>
        public void SetBufferedGeometry(double additionalRadius,
                                        bool needsPolygon);
    }
}
